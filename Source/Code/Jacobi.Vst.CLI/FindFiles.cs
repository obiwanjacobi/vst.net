using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Jacobi.Vst.CLI
{
    internal sealed class FindFiles
    {
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
            @"microsoft.extensions.fileproviders.physical\3.1.4\lib\netcoreapp3.1\Microsoft.Extensions.FileProviders.Physical.dll",
            @"newtonsoft.json\12.0.2\lib\netstandard2.0\Newtonsoft.Json.dll",
        };

        private readonly string _nugetPath;

        public FindFiles(string nugetPath)
        {
            _nugetPath = nugetPath ?? throw new ArgumentNullException(nameof(nugetPath));
        }

        public ProcessorArchitecture ProcessorArchitecture { get; set; }

        public IEnumerable<string> GetFilePaths(TargetName targetName)
        {
            var paths = new List<string>();

            foreach (var kvp in targetName)
            {
                if (kvp.Value.Runtime != null)
                {
                    foreach (var depInfo in kvp.Value.Runtime)
                    {
                        if (!depInfo.Key.StartsWith("Jacobi.Vst.") &&   // nuget package troubles
                            (depInfo.Value.AssemblyVersion != null ||
                            depInfo.Value.FileVersion != null))
                        {
                            paths.Add(Path.Combine(_nugetPath, kvp.Key, depInfo.Key));
                        }
                    }
                }
                else
                {
                    var platform = ToString(ProcessorArchitecture);

                    // TODO: detect if the assemblies are at 'lib' or 'processor architecture' level 
                    // TODO: tfm from params (netcoreapp3.1)
                    var path = Path.Combine(_nugetPath, kvp.Key, "lib", "netcoreapp3.1", platform);
                    paths.AddRange(Directory.EnumerateFiles(path, "*.dll"));

                    var rt = kvp.Value.RuntimeTargets?.Keys.FirstOrDefault(rt => rt.Contains(platform));
                    if (rt != null)
                    {
                        paths.Add(Path.Combine(_nugetPath, kvp.Key, rt));
                    }
                }
            }

            paths.AddRange(InteropDependencies
                .Select(dep => Path.Combine(_nugetPath, dep)));

            return paths.Select(p => p.Replace("/", "\\")).Distinct();
        }

        private static string ToString(ProcessorArchitecture processorArchitecture)
        {
            return processorArchitecture switch
            {
                ProcessorArchitecture.Amd64 => "x64",
                ProcessorArchitecture.X86 => "x86",
                ProcessorArchitecture.MSIL => "AnyCPU",
                _ => string.Empty
            };
        }
    }
}
