using Jacobi.Vst3.Core;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    public static class PublicExport
    {
        [UnmanagedCallersOnly]
        public static bool InitDll()
        {
            return true;
        }

        [UnmanagedCallersOnly]
        public static void ExitDll()
        {
            // no-op
        }

        [UnmanagedCallersOnly]
        public static IPluginFactory GetPluginFactory()
        {
            return new PluginFactory();
        }
    }
}
