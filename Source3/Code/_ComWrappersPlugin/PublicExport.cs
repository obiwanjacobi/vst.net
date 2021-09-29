using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ComWrappersPlugin
{
    public static class PublicExport
    {
        // singleton
        private static PluginFactory _factory = new PluginFactory();
        [SupportedOSPlatform("windows")]
        private static PluginComWrappers _comInterop = new PluginComWrappers();

        [SupportedOSPlatform("windows")]
        [UnmanagedCallersOnly()]
        public static IntPtr GetPluginFactory()
            => _comInterop.GetOrCreateComInterfaceForObject(_factory, CreateComInterfaceFlags.None);

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
