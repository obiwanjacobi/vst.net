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
        /// Loads the managed plugin assembly with the same name as the specified <paramref name="interopAssemblyPath"/>.
        /// </summary>
        /// <param name="interopAssemblyPath">The full file path to the interop assembly. Must not be null or empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="interopAssemblyPath"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="interopAssemblyPath"/> is empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when no suitable managed Plugin assembly could be found.</exception>
        /// <remarks>Note that the managed plugin assembly must be named exactly the same as the <paramref name="interopAssemblyPath"/>
        /// but with a <b>.net.dll</b> or a <b>.net.vstdll</b> extension.</remarks>
        public void LoadAssemblyByDefaultName(string interopAssemblyPath)
        {
            Throw.IfArgumentIsNullOrEmpty(interopAssemblyPath, "interopAssemblyPath");

            string dir = Path.GetDirectoryName(interopAssemblyPath);
            string fileName = Path.GetFileNameWithoutExtension(interopAssemblyPath);

            if (!AssemblyLoader.Current.PrivateProbePaths.Contains(dir))
            {
                AssemblyLoader.Current.PrivateProbePaths.Add(dir);
            }

            LoadAssembly(fileName);
        }

        /// <summary>
        /// Attempts to load the assembly by the <paramref name="assemblyName"/>.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly, without a path indication or file extension. Must not be null or empty.</param>
        /// <exception cref="FileNotFoundException">Thrown when no suitable managed Plugin assembly could be found.</exception>
        public void LoadAssembly(string assemblyName)
        {
            Throw.IfArgumentIsNullOrEmpty(assemblyName, "assemblyName");

            // include an empty string as extension to allow to look for the raw assemblyName
            _assembly = AssemblyLoader.Current.LoadAssembly(assemblyName, new string[] { DefaultManagedExtension, AlternateManagedExtension, String.Empty });

            if (_assembly == null)
            {
                throw new FileNotFoundException(Properties.Resources.ManagedPluginFactory_FileNotFound, assemblyName);
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
                        // Generic types can have no FullName.
                        if (!string.IsNullOrEmpty(intfType.FullName) &&
                            intfType.FullName.Equals(typeOfInterface.FullName))
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
