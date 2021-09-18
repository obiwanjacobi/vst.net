using Jacobi.Vst3.Core;
using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    public static class PublicExport
    {
        [UnmanagedCallersOnly]
        public static int InitDll()
        {
            // 0 = failure, 1 = success
            return 1;
        }

        [UnmanagedCallersOnly]
        public static void ExitDll()
        {
            // no-op
        }

        // singleton
        private static PluginFactory _factory = new PluginFactory();

        [UnmanagedCallersOnly]
        public static IntPtr GetPluginFactory()
            => Marshal.GetComInterfaceForObject(_factory, typeof(IPluginFactory));
    }
}
