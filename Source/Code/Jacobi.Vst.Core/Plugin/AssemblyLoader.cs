using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The AssemblyLoader class manages loading assemblies from non-standard probe paths.
    /// </summary>
    public sealed class AssemblyLoader
    {
        private readonly AssemblyLoadContext _loadContext;

        private AssemblyLoader()
        {
            _loadContext = AssemblyLoadContext.GetLoadContext(this.GetType().Assembly)
                ?? throw new InvalidOperationException();
            _loadContext.Resolving += LoadContext_ResolvingAssembly;
            BasePath = String.Empty;
        }

        private Assembly? LoadContext_ResolvingAssembly(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            var name = assemblyName.Name;
            if (!String.IsNullOrEmpty(name))
            {
                return LoadAssembly(name, ".dll");
            }
            return null;
        }

        private static readonly AssemblyLoader _current = new AssemblyLoader();
        /// <summary>
        /// Gets the current (and only) instance of the AssemblyLoader class.
        /// </summary>
        public static AssemblyLoader Current { get { return _current; } }

        public string BasePath { get; set; }

        public List<string> ProbePaths { get; } = new List<string>();

        /// <summary>
        /// Attempts to load an assembly using the <paramref name="fileName"/> and the <paramref name="extension"/>.
        /// </summary>
        /// <param name="fileName">Name of the assembly file without extension. Must not be null or empty.</param>
        /// <param name="extension">The extensions to check for. Must not be null or empty.</param>
        /// <returns>Returns null if no suitable assembly file was found.</returns>
        public Assembly? LoadAssembly(string fileName, string extension)
        {
            Throw.IfArgumentIsNullOrEmpty(extension, nameof(extension));
            var fileNameExt = $"{fileName}{extension}";

            foreach (var path in GetProbePaths())
            {
                var filePath = Path.Combine(path, fileNameExt);

                if (File.Exists(filePath))
                {
                    return _loadContext.LoadFromAssemblyPath(filePath);
                }
            }
            return null;
        }

        private IEnumerable<string> GetProbePaths()
        {
            var paths = new List<string>
            {
                BasePath
            };

            foreach (var path in ProbePaths)
            {
                if (!Path.IsPathRooted(path))
                    paths.Add(Path.Combine(BasePath, path));
                else
                    paths.Add(path);
            }

            return paths;
        }
    }
}
