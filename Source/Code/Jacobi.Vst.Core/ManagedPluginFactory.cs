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
    /// The interop dll that is needed for each managed plugin is renamed to the same name of the plugin assembly with a .Interop postfix.
    /// The plugin class is decorated with an attribute.
    /// </remarks>
    public class ManagedPluginFactory
    {
        private string _assemblyPath;
        private Assembly _assembly;

        public ManagedPluginFactory(string interopAssemblyPath)
        {
            string dir = Path.GetDirectoryName(interopAssemblyPath);
            string file = Path.GetFileNameWithoutExtension(interopAssemblyPath);

            int index = file.IndexOf(".Interop");
            if (index == -1)
            {
                throw new ArgumentException(
                    "The VST Interop assembly name should end with '.Interop' (not counting the .dll extension).", 
                    "interopAssemblyPath");
            }

            file = file.Substring(index);
            file += ".dll";

            _assemblyPath = Path.Combine(dir, file);
        }

        public IVstPluginCommandStub CreatePluginCommandStub()
        {
            Type pluginType = LocateTypeByInterface(typeof(IVstPluginCommandStub));

            if (pluginType == null)
            {
                // TODO: assign the standard plugin stub type (from Jacobi.Vst.Core)
                //pluginType = typeof(Jacobi::Vst::Core::StdPluginCommandStub);
            }

            return (IVstPluginCommandStub)Activator.CreateInstance(pluginType);
        }

        private void LoadAssembly()
        {
            if (_assembly == null)
            {
                _assembly = Assembly.LoadFile(_assemblyPath);
            }
        }

        private Type LocateTypeByAttribute(Type typeOfAttribute)
        {
            LoadAssembly();

            if (_assembly != null)
            {
                foreach (Type type in _assembly.GetTypes())
                {
                    object[] attrs = type.GetCustomAttributes(typeOfAttribute, false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        return type;
                    }
                }
            }

            return null;
        }

        private Type LocateTypeByInterface(Type typeOfInterface)
        {
            LoadAssembly();

            if (_assembly != null)
            {
                foreach (Type type in _assembly.GetTypes())
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
