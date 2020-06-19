using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct RepresentationInfo
    {
        public static readonly int Size = Marshal.SizeOf<RepresentationInfo>();

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Vendor;	// Vendor name of the associated representation (remote) (eg. "Yamaha").

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Name;		// Representation (remote) Name (eg. "O2").

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Version;	// Version of this "Remote" (eg. "1.0").

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Host;
    }
}
