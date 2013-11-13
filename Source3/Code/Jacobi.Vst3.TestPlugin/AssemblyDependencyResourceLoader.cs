using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;

namespace Jacobi.Vst3.TestPlugin
{
    // Example of how to 'merge' assembly dependencies into the plugin assembly.
    // 1) 'Add Existing Item' to the project and browse to dependent assembly (.dll)
    // 2) Press the down-arrow on the Add-button of the File-Open Dialog and select 'Add As Link'
    // 3) In the File-Properties (F4) change 'Build Action' to 'Embedded Resource'
    internal sealed class AssemblyDependencyResourceLoader
    {
        private string _basePath;

        // assumed is that the assembly is named same as root namespace.
        public AssemblyDependencyResourceLoader()
        {
            _basePath = Assembly.GetExecutingAssembly().GetName().Name.Replace(".dll", "");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        // specify resource base path explicitly
        public AssemblyDependencyResourceLoader(string resourceBasePath)
        {
            _basePath = resourceBasePath;

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var resourceName = _basePath + "." + new AssemblyName(args.Name).Name + ".dll";

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                Byte[] assemblyData = new Byte[stream.Length];

                stream.Read(assemblyData, 0, assemblyData.Length);

                return Assembly.Load(assemblyData);
            }
        }
    }
}
