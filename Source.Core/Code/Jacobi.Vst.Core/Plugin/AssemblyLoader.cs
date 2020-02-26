using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The AssemblyLoader class manages loading assemblies from non-standard probe paths.
    /// </summary>
    public class AssemblyLoader
    {
        private AssemblyLoader()
        {
            GlobalProbePaths = new List<string>();
            PrivateProbePaths = new List<string>();

            // read global (exe)config for global probe paths
            string paths = null; // FIXME: ConfigurationManager.AppSettings["vstnetProbePaths"];

            if (!String.IsNullOrEmpty(paths))
            {
                GlobalProbePaths.AddRange(paths.Split(';'));
            }

            AssemblyLoadContext.Default.Resolving += DefaultContext_ResolvingAssembly;
        }

        private Assembly DefaultContext_ResolvingAssembly(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            FileFinder fileFinder = CreateFileFinder(".dll");

            string filePath = fileFinder.Find(assemblyName.Name);

            if (!String.IsNullOrEmpty(filePath))
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
            }

            return null;
        }

        /// <summary>
        /// Instantiates a new instance of the <see cref="FileFinder"/> class with its <see cref="P:FileFinder.Paths"/> 
        /// collection filled with the <see cref="GlobalProbePaths"/> and <see cref="PrivateProbePaths"/> paths.
        /// </summary>
        /// <returns>Never returns null.</returns>
        public FileFinder CreateFileFinder(params string[] extensions)
        {
            FileFinder fileFinder = new FileFinder();
            fileFinder.Paths.AddRange(PrivateProbePaths);
            fileFinder.Paths.AddRange(GlobalProbePaths);

            if (extensions != null)
            {
                fileFinder.Extensions.AddRange(extensions);
            }
            return fileFinder;
        }

        private static readonly AssemblyLoader _current = new AssemblyLoader();
        /// <summary>
        /// Gets the current (and only) instance of the AssemblyLoader class.
        /// </summary>
        public static AssemblyLoader Current { get { return _current; } }

        /// <summary>
        /// Gets a collection with global probe paths.
        /// </summary>
        public List<string> GlobalProbePaths { get; private set; }

        /// <summary>
        /// Gets a collection with private probe paths.
        /// </summary>
        public List<string> PrivateProbePaths { get; private set; }

        /// <summary>
        /// Attempts to load an assembly using the <paramref name="fileName"/> and the list of <paramref name="extensions"/> 
        /// as well as the <see cref="PrivateProbePaths"/> and the <see cref="GlobalProbePaths"/>.
        /// </summary>
        /// <param name="fileName">Name of the assembly file without extension. Must not be null or empty.</param>
        /// <param name="extensions">A list of extensions to check for. Must not be null.</param>
        /// <returns>Returns null if no suitable assembly file was found.</returns>
        public Assembly LoadAssembly(string fileName, IEnumerable<string> extensions)
        {
            Throw.IfArgumentIsNull(extensions, nameof(extensions));

            FileFinder fileFinder = CreateFileFinder(extensions.ToArray());

            string filePath = fileFinder.Find(fileName);

            if (!String.IsNullOrEmpty(filePath))
            {
                System.Diagnostics.Debug.WriteLine(String.Format("AssemblyLoader loading Assembly: {0}.", filePath));
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
            }

            return null;
        }
    }
}
