﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Jacobi.Vst.CLI
{
    internal class PublishCommand : ICommand
    {
        public bool Execute()
        {
            if (String.IsNullOrEmpty(NuGetPath))
            {
                NuGetPath = FileExtensions.GetNuGetLocation();
            }
            if (String.IsNullOrEmpty(DeployPath))
            {
                DeployPath = @".\deploy";
            }
            FileExtensions.EnsureDirectoryExists(DeployPath);

            var depsFile = GetDepsFile();
            if (depsFile == null)
            {
                ConsoleOutput.Error($"Unable to find the .deps.json file based on {FilePath}. Cannot copy dependencies.");
                return false;
            }

            var plugin = GetPluginFile();
            var host = GetHostFile();

            var assemblyName = AssemblyName.GetAssemblyName(plugin ?? host);

            CopyDependencies(depsFile, assemblyName.ProcessorArchitecture);

            if (plugin != null)
            {
                PublishPlugin(plugin);
                return true;
            }

            if (host != null)
            {
                PublishHost(host);
                return true;
            }

            ConsoleOutput.Error($"Unable to find the code file (.dll/.exe) based on {FilePath}. Cannot perform code publication.");
            return false;
        }

        public string NuGetPath { get; set; }
        public string DeployPath { get; set; }
        public string FilePath { get; set; }

        private void CopyDependencies(string depsFile, ProcessorArchitecture processorArchitecture)
        {
            ConsoleOutput.Progress($"Copying dependencies to: {DeployPath}");
            using var stream = File.OpenRead(depsFile);
            var json = Parse(stream);

            var finder = new FindFiles(NuGetPath)
            {
                ProcessorArchitecture = processorArchitecture
            };
            var paths = finder.GetFilePaths(json.Targets.First().Value);

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    ConsoleOutput.Progress(path);
                    File.Copy(path, Path.Combine(DeployPath, Path.GetFileName(path)), overwrite: true);
                }
                else
                {
                    ConsoleOutput.Warning($"Could not find: {path}");
                }
            }
        }

        private void PublishPlugin(string pluginPath)
        {
            var name = Path.GetFileNameWithoutExtension(pluginPath);
            //var path = Path.GetDirectoryName(pluginPath);

            var managed = Path.Combine(DeployPath, $"{name}.net.vstdll");
            // copy over and rename the managed plugin dll.
            File.Copy(pluginPath, managed, overwrite: true);
            ConsoleOutput.Progress($"Renaming managed plugin: {pluginPath} => {managed}");

            var interop = Path.Combine(DeployPath, "Jacobi.Vst.Interop.dll");
            var entry = Path.Combine(DeployPath, $"{name}.dll");
            // rename Jacobi.Vst.Interop to plugin name
            File.Copy(interop, entry, overwrite: true);
            File.Delete(interop);
            ConsoleOutput.Progress($"Creating unmanged plugin: {interop} => {entry}");

            // copy in all other plugin related file (Debug?)
            //foreach (var sourceFile in Directory.EnumerateFiles(path, $"{name}.*"))
            //{
            //    var targetName = Path.GetFileName(sourceFile);
            //    var targetFile = Path.Combine(DeployPath, targetName);
            //    File.Copy(sourceFile, targetFile, overwrite: true);
            //    ConsoleOutput.Progress($"Copy target file: {sourceFile}");
            //}

            var runtimeConfig = Path.Combine(DeployPath, $"{name}.runtimeconfig.json");
            using var stream = typeof(PublishCommand).Assembly
                .GetManifestResourceStream("Jacobi.Vst.CLI.runtimeconfig.json");
            var reader = new StreamReader(stream);
            File.WriteAllText(runtimeConfig, reader.ReadToEnd());
            ConsoleOutput.Progress($"Creating {runtimeConfig}");
        }

        private void PublishHost(string hostPath)
        {
            // TODO: make sure ijwhost.dll is present
            throw new System.NotImplementedException();
        }

        private string GetDepsFile()
        {
            return GetFileByExtension(FilePath, ".deps.json");
        }

        private string GetPluginFile()
        {
            return GetFileByExtension(FilePath, ".dll");
        }

        private string GetHostFile()
        {
            return GetFileByExtension(FilePath, ".exe");
        }

        private static string GetFileByExtension(string baseFile, string extension)
        {
            if (baseFile.EndsWith(extension)) return baseFile;

            var name = Path.GetFileNameWithoutExtension(baseFile);
            var path = Path.GetDirectoryName(baseFile);
            var file = Path.Combine(path, $"{name}{extension}");

            if (File.Exists(file)) { return file; }

            return null;
        }

        private static DepsJson Parse(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,

            };
            var reader = new StreamReader(stream);
            return JsonSerializer.Deserialize<DepsJson>(reader.ReadToEnd(), options);
        }
    }
}
