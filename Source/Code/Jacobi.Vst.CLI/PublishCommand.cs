using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Jacobi.Vst.CLI
{
    internal class PublishCommand : ICommand
    {
        private const string RuntimeConfigJson = ".runtimeconfig.json";

        public bool Execute()
        {
            if (!File.Exists(FilePath))
            {
                ConsoleOutput.Error($"File {FilePath} does not exist.");
                return false;
            }

            if (String.IsNullOrEmpty(NuGetPath))
            {
                NuGetPath = FileExtensions.GetNuGetLocation();
            }
            if (String.IsNullOrEmpty(DeployPath))
            {
                DeployPath = @".\deploy";
            }
            FileExtensions.EnsureDirectoryExists(DeployPath);
            DeployBinPath = Path.Combine(DeployPath, "bin");
            FileExtensions.EnsureDirectoryExists(DeployBinPath);

            var depsFile = GetDepsFile();
            if (depsFile == null)
            {
                ConsoleOutput.Error($"Unable to find the .deps.json file based on {FilePath}. Cannot copy dependencies.");
                return false;
            }

            var plugin = GetPluginFile();
            var host = GetHostFile();

            if (host != null)
            {
                CopyDependencies(depsFile, DeployPath);
                PublishHost(host);
                return true;
            }

            if (plugin != null)
            {
                CopyDependencies(depsFile, DeployBinPath);
                PublishPlugin(plugin);
                return true;
            }

            ConsoleOutput.Error($"Unable to find the code file (.dll/.exe) based on {FilePath}. Cannot perform code publication.");
            return false;
        }

        public string NuGetPath { get; set; }
        public string DeployPath { get; set; }
        public string DeployBinPath { get; set; }
        public string FilePath { get; set; }
        public string Platform { get; set; }

        private void CopyDependencies(string depsFile, string deployPath)
        {
            ConsoleOutput.Progress($"Copying dependencies to: {deployPath}");
            using var stream = File.OpenRead(depsFile);
            var json = Parse(stream);

            var finder = new FindFiles(NuGetPath, Path.GetDirectoryName(FilePath))
            {
                Platform = Platform
            };
            var paths = finder.GetFilePaths(json.Targets.First().Value);

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    ConsoleOutput.Progress(path);
                    File.Copy(path, Path.Combine(deployPath, Path.GetFileName(path)), overwrite: true);
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
            var path = Path.GetDirectoryName(pluginPath);

            EnsureVstAssemblies();

            var managed = Path.Combine(DeployPath, $"{name}.net.vst2");
            // copy over and rename the managed plugin dll.
            File.Copy(pluginPath, managed, overwrite: true);
            ConsoleOutput.Progress($"Renaming managed plugin: {pluginPath} => {managed}");

            var interop = Path.Combine(DeployPath, "Jacobi.Vst.Plugin.Interop.dll");
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

            var runtimeConfig = Path.Combine(DeployPath, $"{name}{RuntimeConfigJson}");
            using var stream = typeof(PublishCommand).Assembly
                .GetManifestResourceStream($"Jacobi.Vst.CLI{RuntimeConfigJson}");
            var reader = new StreamReader(stream);
            File.WriteAllText(runtimeConfig, reader.ReadToEnd());
            ConsoleOutput.Progress($"Creating {runtimeConfig}");

            CopySettings(path, name);
        }

        // All Jacobi.Vst.* assemblies need to be next to plugin
        private void EnsureVstAssemblies()
        {
            var files = Directory.GetFiles(DeployBinPath, "Jacobi.Vst.*");
            foreach (var file in files)
            {
                File.Move(file, Path.Combine(DeployPath, Path.GetFileName(file)), overwrite: true);
            }

            const string ijwhost = "ijwhost.dll";
            File.Move(Path.Combine(DeployBinPath, ijwhost), Path.Combine(DeployPath, ijwhost), overwrite: true);
        }

        private void PublishHost(string hostPath)
        {
            var name = Path.GetFileNameWithoutExtension(hostPath);
            var path = Path.GetDirectoryName(hostPath);
            var sourceName = Path.Combine(path, name);

            var targetPath = Path.Combine(DeployPath, $"{name}.exe");
            File.Copy(hostPath, targetPath, overwrite: true);

            targetPath = Path.Combine(DeployPath, $"{name}.dll");
            File.Copy($"{sourceName}.dll", targetPath, overwrite: true);

            targetPath = Path.Combine(DeployPath, $"{name}{RuntimeConfigJson}");
            File.Copy($"{sourceName}{RuntimeConfigJson}", targetPath, overwrite: true);

            CopySettings(path, name);
        }

        private void CopySettings(string path, string name)
        {
            foreach (var settingsFile in Directory.EnumerateFiles(path, $"*settings.json"))
            {
                var targetName = Path.GetFileName(settingsFile);
                var targetFile =
                    String.Compare(targetName, "appsettings.json", StringComparison.InvariantCultureIgnoreCase) == 0
                    ? Path.Combine(DeployPath, $"{name}.{targetName}")
                    : Path.Combine(DeployPath, targetName);
                File.Copy(settingsFile, targetFile, overwrite: true);
                ConsoleOutput.Progress($"Copy settings file: {settingsFile} => {targetFile}");
            }
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
            if (baseFile.EndsWith(extension))
                return baseFile;

            var name = Path.GetFileNameWithoutExtension(baseFile);
            var path = Path.GetDirectoryName(baseFile);
            var file = Path.Combine(path, $"{name}{extension}");

            if (File.Exists(file))
            { return file; }

            return null;
        }

        private static DepsJson Parse(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,

            };
            var reader = new StreamReader(stream);
            return JsonSerializer.Deserialize<DepsJson>(reader.ReadToEnd(), options);
        }
    }
}
