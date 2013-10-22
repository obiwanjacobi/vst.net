using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;
using RGiesecke.DllExport;

namespace Jacobi.Vst3.TestPlugin
{
    public static class Exports
    {
        [DllExport(ExportName = "InitDll", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static bool InitDll()
        {
            return true;
        }

        [DllExport(ExportName = "ExitDll", CallingConvention = Platform.DefaultCallingConvention)]
        public static void ExitDll()
        {
        }

        [DllExport(ExportName = "GetPluginFactory", CallingConvention = Platform.DefaultCallingConvention)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static IPluginFactory GetPluginFactory()
        {
            return new PluginFactory();
        }
    }
}
