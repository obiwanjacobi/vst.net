using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;
using RGiesecke.DllExport;
using Jacobi.Vst3.Interop.Plugin;

namespace Jacobi.Vst3.TestPlugin
{
    public static class Exports
    {
        private static PluginClassFactory _factory = 
            new PluginClassFactory("Jacobi Software", "obiwanjacobi@hotmail.com", "http://vstnet.codeplex.com");

        [DllExport(ExportName = "InitDll", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static bool InitDll()
        {
            _factory.Register(typeof(PluginComponent), PluginClassFactory.AudioModuleClassCategory);
            _factory.Register(typeof(EditController), PluginClassFactory.ComponentControllerClassCategory);

            return true;
        }

        [DllExport(ExportName = "ExitDll", CallingConvention = Platform.DefaultCallingConvention)]
        public static void ExitDll()
        {
            //_factory.Dispose();
        }

        [DllExport(ExportName = "GetPluginFactory", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static IPluginFactory GetPluginFactory()
        {
            //return new PluginFactory();
            return _factory;
        }
    }
}
