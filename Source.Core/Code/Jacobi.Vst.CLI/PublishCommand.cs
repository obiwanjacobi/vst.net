using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Jacobi.Vst.CLI
{
    internal class PublishCommand : ICommand
    {
        private const string CommandInstructions = "vstnet publish <target> -o:<path>";

        // Interop mixed C++/CLR project does not publish dependencies.
        // Manually maintain them here.
        private static readonly string[] InteropDependencies = new[]
        {
            @"Microsoft.Extensions.Configuration\3.1.2\lib\netcoreapp3.1\Microsoft.Extensions.Configuration.dll",
            @"Microsoft.Extensions.Configuration.Abstractions\3.1.2\lib\netcoreapp3.1\Microsoft.Extensions.Configuration.Abstractions.dll",
            @"Microsoft.Extensions.Configuration.FileExtensions\3.1.1\lib\netcoreapp3.1\Microsoft.Extensions.Configuration.FileExtensions.dll",
            @"Microsoft.Extensions.Configuration.Json\2.1.0\lib\netstandard2.0\Microsoft.Extensions.Configuration.Json.dll",
            @"Microsoft.Extensions.FileProviders.Physical\3.1.1\lib\netcoreapp3.1\Microsoft.Extensions.FileProviders.Physical.dll",
            @"Microsoft.Extensions.FileProviders.Abstractions\3.1.1\lib\netcoreapp3.1\Microsoft.Extensions.FileProviders.Abstractions.dll",
        };

        public bool Execute()
        {
            if (String.IsNullOrEmpty(NuGetPath))
            {
                NuGetPath = FileExtensions.GetNuGetLocation();
            }
            if (String.IsNullOrEmpty(DeployPath))
            {
                DeployPath = @".\deploy";
                FileExtensions.EnsureDirectoryExists(DeployPath);
            }

            var depsFile = GetDepsFile();
            if (depsFile == null)
            {
                ConsoleOutput.Warning($"Unable to find the .deps.json file based on {FilePath}. Cannot copy dependencies.");
            }
            else
            {
                CopyDependencies(depsFile);
            }

            var plugin = GetPluginFile();
            var host = GetHostFile();

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

            if (depsFile != null)
            {
                ConsoleOutput.Warning($"Unable to find the code file (.dll/.exe) based on {FilePath}. Cannot perform code publication.");
                return true;
            }

            return false;
        }

        public string NuGetPath { get; set; }
        public string DeployPath { get; set; }
        public string FilePath { get; set; }

        private void CopyDependencies(string depsFile)
        {
            ConsoleOutput.Progress($"Copy Dependencies to {DeployPath}");
            using var stream = File.OpenRead(depsFile);
            var json = Parse(stream);
            var paths = json.Targets.First().Value.GetFilePaths();

            foreach (var path in paths.Concat(InteropDependencies).Distinct())
            {
                var filePath = Path.Combine(NuGetPath, path);
                if (File.Exists(filePath))
                {
                    ConsoleOutput.Progress(filePath);
                    File.Copy(filePath, Path.Combine(DeployPath, Path.GetFileName(filePath)), overwrite: true);
                }
                else
                {
                    ConsoleOutput.Warning($"Could not find: {filePath}");
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
            if (!File.Exists(runtimeConfig))
            {
                var assembly = typeof(PublishCommand).Assembly;
                using var stream = assembly.GetManifestResourceStream("Jacobi.Vst.CLI.runtimeconfig.json");
                var reader = new StreamReader(stream);
                File.WriteAllText(runtimeConfig, reader.ReadToEnd());

                ConsoleOutput.Progress($"Creating {runtimeConfig}");
            }
        }

        private void PublishHost(string hostPath)
        {
            // TODO: make sure ijwhost.dll is present
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
