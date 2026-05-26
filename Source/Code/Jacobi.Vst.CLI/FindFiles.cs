using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jacobi.Vst.CLI
{
    internal sealed class FindFiles
    {
        // Interop mixed C++/CLR project does not publish dependencies.
        // Manually maintain them here.
        private static readonly string[] InteropDependencies = new[]
        {
            @"Microsoft.Extensions.Configuration\10.0.0\lib\net10.0\Microsoft.Extensions.Configuration.dll",
            @"Microsoft.Extensions.Configuration.Abstractions\10.0.8\lib\net10.0\Microsoft.Extensions.Configuration.Abstractions.dll",
            @"Microsoft.Extensions.Configuration.FileExtensions\10.0.8\lib\net10.0\Microsoft.Extensions.Configuration.FileExtensions.dll",
            @"Microsoft.Extensions.Configuration.Json\10.0.8\lib\net10.0\Microsoft.Extensions.Configuration.Json.dll",
            @"Microsoft.Extensions.FileProviders.Abstractions\10.0.8\lib\net10.0\Microsoft.Extensions.FileProviders.Abstractions.dll",
            @"Microsoft.Extensions.FileProviders.Physical\10.0.8\lib\net10.0\Microsoft.Extensions.FileProviders.Physical.dll",
            @"Microsoft.Extensions.FileSystemGlobbing\10.0.8\lib\net10.0\Microsoft.Extensions.FileSystemGlobbing.dll",
            @"Microsoft.Extensions.Primitives\10.0.8\lib\net10.0\Microsoft.Extensions.Primitives.dll",
        };

        // scan dependency files for these framework monikers. [hack]
        private static readonly string[] TargetFrameworkMonikers =
        {
            "net10.0-windows",
            "netstandard2.1",
            "netstandard2.0",
        };

        private readonly string _nugetPath;
        private readonly string _binPath;

        public string Platform { get; set; }

        public FindFiles(string nugetPath, string binPath)
        {
            _nugetPath = nugetPath ?? throw new ArgumentNullException(nameof(nugetPath));
            _binPath = binPath ?? throw new ArgumentNullException(nameof(binPath));
        }

        public IEnumerable<string> GetFilePaths(TargetName targetName)
        {
            var paths = new List<string>();

            foreach (var kvp in targetName)
            {
                if (kvp.Value.Runtime != null)
                {
                    foreach (var depInfo in kvp.Value.Runtime)
                    {
                        if (!depInfo.Key.StartsWith("Jacobi.Vst."))   // nuget package troubles
                        {
                            if (depInfo.Value.AssemblyVersion != null ||
                                depInfo.Value.FileVersion != null)
                            {
                                paths.Add(Path.Combine(_nugetPath, kvp.Key, depInfo.Key));
                            }

                            if (!depInfo.Key.StartsWith("lib"))
                            {
                                paths.Add(Path.Combine(_binPath, depInfo.Key));
                            }
                        }
                    }
                }
                else
                {
                    var dependencies = FindDependencyFiles(kvp.Key, Platform);
                    paths.AddRange(dependencies);

                    var rt = kvp.Value.RuntimeTargets?.Keys.FirstOrDefault(rt => rt.Contains(Platform));
                    if (rt != null)
                    {
                        var path = Path.Combine(_nugetPath, kvp.Key, rt);
                        if (File.Exists(path))
                            paths.Add(path);
                    }
                }
            }

            paths.AddRange(InteropDependencies
                .Select(dep => Path.Combine(_nugetPath, dep)));

            return paths.Select(p => p.Replace("/", "\\")).Distinct();
        }

        private IEnumerable<string> FindDependencyFiles(string dependency, string platform)
        {
            const string fileExt = "*.dll";
            var pathTemplate = Path.Combine(_nugetPath, dependency, "lib", "{0}");

            foreach (var tfm in TargetFrameworkMonikers)
            {
                var path = String.Format(pathTemplate, tfm);
                if (Directory.Exists(path))
                {
                    var files = Directory.EnumerateFiles(path, fileExt);
                    if (files.Any())
                        return files;
                }

                if (!String.IsNullOrEmpty(platform))
                {
                    path = Path.Combine(path, platform);
                    if (Directory.Exists(path))
                    {
                        var files = Directory.EnumerateFiles(path, fileExt);
                        if (files.Any())
                            return files;
                    }
                }
            }

            ConsoleOutput.Warning(
                $"No library files (*.dll) could be found for: '{dependency}' ({platform})!");
            return Enumerable.Empty<string>();
        }
    }
}
