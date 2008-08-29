namespace Jacobi.Vst.Core.Plugin
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Loads and creates a managed Vst plugin from an assembly
    /// </summary>
    /// <remarks>
    /// The problem of what CLR assembly to load can be solved with a convention.
    /// The interop dll that is needed for each managed plugin is renamed to the name of the plugin. 
    /// The managed plugin assembly must have the same name but with a .net postfix (asside from the.dll file extension).
    /// The managed assembly should expose a public class that implements the IVstPluginCommandStub interface.
    /// </remarks>
    public class ManagedPluginFactory
    {
        private Assembly _assembly;

        public ManagedPluginFactory(string interopAssemblyPath)
        {
            string dir = Path.GetDirectoryName(interopAssemblyPath);
            string filePath = Path.Combine(dir, Path.GetFileNameWithoutExtension(interopAssemblyPath));

            // default .net.dll extension
            string dotNetFile = filePath + ".net.dll";

            // fallback to .net.vstdll
            if (!File.Exists(dotNetFile))
            {
                dotNetFile = filePath + ".net.vstdll";
            }

            if (!File.Exists(dotNetFile))
            {
                throw new FileNotFoundException(
                    "Could not find the managed VST plugin assembly with either the .net.dll or .net.vstdll extension.", 
                    filePath);
            }

            _assembly = Assembly.LoadFile(dotNetFile);
        }

        public IVstPluginCommandStub CreatePluginCommandStub()
        {
            Type pluginType = LocateTypeByInterface(typeof(IVstPluginCommandStub));

            if (pluginType == null)
            {
                throw new InvalidOperationException(
                    "\"" + _assembly.FullName + "\" does not expose a public class that implements the IVstPluginCommandStub interface.");
            }

            return (IVstPluginCommandStub)Activator.CreateInstance(pluginType);
        }

        private Type LocateTypeByInterface(Type typeOfInterface)
        {
            foreach (Type type in _assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    foreach (Type intfType in type.GetInterfaces())
                    {
                        if (intfType.FullName.Equals(typeOfInterface.FullName))
                        {
                            return type;
                        }
                    }
                }
            }

            return null;
        }
    }
}
