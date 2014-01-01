using Jacobi.Vst3.Interop;
using Jacobi.Vst3.Plugin;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;
using System.Threading;

namespace Jacobi.Vst3.TestPlugin
{
    public static class Exports
    {
        // This will automatically load dependent assemblies that were added as embedded resource to the project (root).
        private static readonly AssemblyDependencyResourceLoader _dependencyLoader = new AssemblyDependencyResourceLoader();
        private static PluginClassFactory _factory; // singleton

        [DllExport(ExportName = "InitDll", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static bool InitDll()
        {
            _factory = new PluginClassFactory("Jacobi Software", "obiwanjacobi@hotmail.com", "http://vstnet.codeplex.com");
            var reg = _factory.Register(typeof(PluginComponent), ClassRegistration.ObjectClasses.AudioModuleClass);
            reg.Categories = new CategoryCollection(CategoryCollection.Fx);

            _factory.Register(typeof(MyEditController), ClassRegistration.ObjectClasses.ComponentControllerClass);

            return true;
        }

        [DllExport(ExportName = "ExitDll", CallingConvention = Platform.DefaultCallingConvention)]
        public static void ExitDll()
        {
            _factory.Dispose();
            _factory = null;
        }

        [DllExport(ExportName = "GetPluginFactory", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static IPluginFactory GetPluginFactory()
        {
            return _factory;
        }
    }
}
