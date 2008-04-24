namespace Jacobi.Vst.Core
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
            string file = Path.GetFileNameWithoutExtension(interopAssemblyPath);

            file += ".net.dll";

            string assemblyPath = Path.Combine(dir, file);

            _assembly = Assembly.LoadFile(assemblyPath);
        }

        public IVstPluginCommandStub CreatePluginCommandStub()
        {
            Type pluginType = LocateTypeByInterface(typeof(IVstPluginCommandStub));

            if (pluginType == null)
            {
                throw new InvalidOperationException(
                    "\"" + _assembly.FullName + "\" does not expose a public class that implements the IVstPluginCommandStub interface.");

                // TODO: assign the standard plugin stub type (from Jacobi.Vst.Framework) without introducing a hard dependency...?
                //pluginType = typeof(Jacobi::Vst::Framework::StdPluginCommandStub);
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
                        if (intfType.Equals(typeOfInterface))
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
