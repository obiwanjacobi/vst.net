using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Jacobi.Vst3.Interop
{
    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=Platform.StructurePack)]
    public struct PClassInfo
    {
        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
        public string Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public string Name;
    }

    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct PClassInfo2
    {
        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
        public string Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public string Name;

        // --------------------------------------------------------------------

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ClassFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeSubCategories)]
        public string SubCategories;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
        public string Vendor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public string Version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public string SdkVersion;
    }

    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = Platform.StructurePack)]
    public struct PClassInfoW
    {
        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        public AnsiCategory Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public string Name;

        // --------------------------------------------------------------------

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ClassFlags;

        public AnsiSubCategories SubCategories;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
        public string Vendor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public string Version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public string SdkVersion;

        //---------------------------------------------------------------------
        // need extra structs to solve mixed Ansi/Unicode strings

        [NativeCppClass]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
        public struct AnsiCategory
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
            public string Value;
        }

        [NativeCppClass]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
        public struct AnsiSubCategories
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeSubCategories)]
            public string Value;
        }
    }
}


