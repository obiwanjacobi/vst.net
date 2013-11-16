using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;
using RGiesecke.DllExport;
using Jacobi.Vst3.Plugin;

namespace Jacobi.Vst3.TestPlugin
{
    public static class Exports
    {
        // This will automatically load dependent assemblies that were added as embedded resource to the project (root).
        private static readonly AssemblyDependencyResourceLoader _dependencyLoader = new AssemblyDependencyResourceLoader();
        private static PluginClassFactory _factory; // singleton

        [DllExport(ExportName = "InitDll", CallingConvention = Platform.CallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static bool InitDll()
        {
            _factory = new PluginClassFactory("Jacobi Software", "obiwanjacobi@hotmail.com", "http://vstnet.codeplex.com");
            _factory.Register(typeof(PluginComponent), ClassRegistration.ObjectClasses.AudioModuleClass);
            _factory.Register(typeof(EditController), ClassRegistration.ObjectClasses.ComponentControllerClass);

            return true;
        }

        [DllExport(ExportName = "ExitDll", CallingConvention = Platform.CallingConvention)]
        public static void ExitDll()
        {
            _factory.Dispose();
            _factory = null;
        }

        [DllExport(ExportName = "GetPluginFactory", CallingConvention = Platform.CallingConvention)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static IPluginFactory GetPluginFactory()
        {
            return _factory;
        }
    }
}
