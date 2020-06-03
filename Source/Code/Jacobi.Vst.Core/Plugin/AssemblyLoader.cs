using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The AssemblyLoader class manages loading assemblies from non-standard probe paths.
    /// </summary>
    public class AssemblyLoader
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

        /// <summary>
        /// The root path to look for loading assemblies.
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Attempts to load an assembly using the <paramref name="fileName"/> and the <paramref name="extension"/>.
        /// </summary>
        /// <param name="fileName">Name of the assembly file without extension. Must not be null or empty.</param>
        /// <param name="extension">The extensions to check for. Must not be null or empty.</param>
        /// <returns>Returns null if no suitable assembly file was found.</returns>
        public Assembly? LoadAssembly(string fileName, string extension)
        {
            Throw.IfArgumentIsNullOrEmpty(extension, nameof(extension));

            string filePath = Path.Combine(BasePath, $"{fileName}{extension}");

            if (!String.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                return _loadContext.LoadFromAssemblyPath(filePath);
            }

            return null;
        }
    }
}
