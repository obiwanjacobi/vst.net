using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;

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
            string paths = ConfigurationManager.AppSettings["vstnetProbePaths"];

            if (!String.IsNullOrEmpty(paths))
            {
                GlobalProbePaths.AddRange(paths.Split(';'));
            }

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            FileFinder fileFinder = CreateFileFinder();
            fileFinder.Extensions.Add(".dll");

            AssemblyName assName = new AssemblyName(args.Name);
            string filePath = fileFinder.Find(assName.Name);

            return Assembly.LoadFile(filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileFinder CreateFileFinder()
        {
            FileFinder fileFinder = new FileFinder();
            fileFinder.Paths.AddRange(PrivateProbePaths);
            fileFinder.Paths.AddRange(GlobalProbePaths);

            return fileFinder;
        }

        private static AssemblyLoader _current = new AssemblyLoader();
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
        /// <param name="fileName"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public Assembly LoadAssembly(string fileName, IEnumerable<string> extensions)
        {
            FileFinder fileFinder = CreateFileFinder();
            fileFinder.Extensions.AddRange(extensions);

            string filePath = fileFinder.Find(fileName);

            return Assembly.LoadFile(filePath);
        }
    }
}
