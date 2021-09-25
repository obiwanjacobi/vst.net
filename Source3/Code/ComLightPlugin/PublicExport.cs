using ComLight;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ComLightPlugin
{
    public static class PublicExport
    {
        // singleton
        private static PluginFactory _factory = new PluginFactory();

        [UnmanagedCallersOnly()]
        // DNNE does not like this (return type must be blittable)
        //[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Marshaler<IPluginFactory>))]
        // not valid on return, only params
        //[return: ComLight.Marshaller(typeof(Marshaler<IPluginFactory>))]
        public static IntPtr GetPluginFactory()
            => ManagedWrapper.wrap<IPluginFactory>(_factory, addRef: true);

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
