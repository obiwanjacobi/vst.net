using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ComLightPlugin
{
    public static class PublicExport
    {
        // singleton
        private static PluginFactory _factory = new PluginFactory();

        [SupportedOSPlatform("windows")]
        //[UnmanagedCallersOnly(EntryPoint = "GetPluginFactory")]
        public static IntPtr GetPluginFactory_Win()
            => Marshal.GetComInterfaceForObject(_factory, typeof(IPluginFactory));

        [SupportedOSPlatform("linux")]
        [UnmanagedCallersOnly(EntryPoint = "GetPluginFactory")]
        public static IntPtr GetPluginFactory_Lnx()
            => IntPtr.Zero;

        [SupportedOSPlatform("windows")]
        [UnmanagedCallersOnly]
        public static Int32 InitDll()
        {
            // 0 = failure, 1 = success
            return 1;
        }

        [SupportedOSPlatform("windows")]
        [UnmanagedCallersOnly]
        public static void ExitDll()
        {
            // no-op
        }

        [SupportedOSPlatform("linux")]
        [UnmanagedCallersOnly]
        public static Int32 ModuleEntry(IntPtr module)
        {
            // 0 = failure, 1 = success
            return 1;
        }

        [SupportedOSPlatform("linux")]
        [UnmanagedCallersOnly]
        public static Int32 ModuleExit()
        {
            // 0 = failure, 1 = success
            return 1;
        }
    }
}
