using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Jacobi.Vst3.Interop
{
    // Note CharSet is not the same as the platform global value.
    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct PFactoryInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
        public string Vendor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeUrl)]
        public string Url;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeEmail)]
        public string Email;

        [MarshalAs(UnmanagedType.I4)]
        public FactoryFlags Flags;

        public void SafeSetVendor(string value)
        {
            Guard.ThrowIfTooLong("Vendor", value, 0, Constants.MaxSizeVendor);
            Vendor = value;
        }

        public void SafeSetEmail(string value)
        {
            Guard.ThrowIfTooLong("Email", value, 0, Constants.MaxSizeEmail);
            Email = value;
        }

        public void SafeSetUrl(string value)
        {
            Guard.ThrowIfTooLong("Url", value, 0, Constants.MaxSizeUrl);
            Url = value;
        }

        [Flags]
        public enum FactoryFlags
        {
            /// <summary>
            /// Nothing
            /// </summary>
            NoFlags = 0,

            /// <summary>
            /// The number of exported classes can change each time the Module is loaded. 
            /// If this flag is set, the host does not cache class information. 
            /// This leads to a longer startup time because the host always has to load the Module to get the current class information.
            /// </summary>
            ClassesDiscardable = 1 << 0,

            /// <summary>
            /// Class IDs of components are interpreted as Syncrosoft-License (LICENCE_UID). 
            /// Loaded in a Steinberg host, the module will not be loaded when the license is not valid.
            /// </summary>
            LicenseCheck = 1 << 1,

            /// <summary>
            /// Component won't be unloaded until process exit.
            /// </summary>
            ComponentNonDiscardable = 1 << 3,

            /// <summary>
            /// Components have entirely unicode encoded strings. (True for VST 3 Plug-ins so far)
            /// </summary>
            Unicode = 1 << 4
        }
    }
}
