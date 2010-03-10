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
    /// The managed plugin assembly must have the same name but with a .net.dll or a .net.vstdll file extension.
    /// The managed assembly must expose a public class that implements the IVstPluginCommandStub interface.
    /// </remarks>
    public class ManagedPluginFactory
    {
        private Assembly _assembly;

        /// <summary>.net.dll</summary>
        public const string DefaultManagedExtension = ".net.dll";
        /// <summary>.net.vstdll</summary>
        public const string AlternateManagedExtension = ".net.vstdll";

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="interopAssemblyPath">The full file path to the interop assembly. Must not be null or empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="interopAssemblyPath"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="interopAssemblyPath"/> is empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when no suitable managed Plugin assembly could be found.</exception>
        /// <remarks>Note that the managed plugin assembly must be named exactly the same as the <paramref name="interopAssemblyPath"/>
        /// but with a <b>.net.dll</b> or a <b>.net.vstdll</b> extension.</remarks>
        public ManagedPluginFactory(string interopAssemblyPath)
        {
            Throw.IfArgumentIsNullOrEmpty(interopAssemblyPath, "interopAssemblyPath");

            string dir = Path.GetDirectoryName(interopAssemblyPath);
            string fileName = Path.GetFileNameWithoutExtension(interopAssemblyPath);

            if (!AssemblyLoader.Current.PrivateProbePaths.Contains(dir))
            {
                AssemblyLoader.Current.PrivateProbePaths.Add(dir);
            }

            _assembly = AssemblyLoader.Current.LoadAssembly(fileName, new string[] { DefaultManagedExtension, AlternateManagedExtension });

            if(_assembly == null)
            {
                throw new FileNotFoundException(Properties.Resources.ManagedPluginFactory_FileNotFound, Path.Combine(dir, fileName));
            }
        }

        /// <summary>
        /// Creates the public Plugin command stub.
        /// </summary>
        /// <returns>Returns an instance of the PluginCommandStub.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no public class could be found 
        /// that implemented the <see cref="IVstPluginCommandStub"/> interface.</exception>
        public IVstPluginCommandStub CreatePluginCommandStub()
        {
            Type pluginType = LocateTypeByInterface(typeof(IVstPluginCommandStub));

            if (pluginType == null)
            {
                throw new InvalidOperationException(
                    String.Format(Properties.Resources.ManagedPluginFactory_NoPublicStub, _assembly.FullName));
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
