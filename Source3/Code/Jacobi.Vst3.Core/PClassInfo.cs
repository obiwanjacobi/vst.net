using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct PClassInfo
    {
        public const int ClassCardinalityManyInstances = 0x7FFFFFFF;

        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
        public String Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Name;
    }

    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct PClassInfo2
    {
        public const int ClassCardinalityManyInstances = 0x7FFFFFFF;

        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
        public String Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Name;

        // --------------------------------------------------------------------

        [MarshalAs(UnmanagedType.U4)]
        public ComponentClassFlags ClassFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeSubCategories)]
        public String SubCategories;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
        public String Vendor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public String Version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public String SdkVersion;
    }

    public enum ComponentClassFlags
    {
        None = 0,
        Distributable = 1 << 0,	// Component can be run on remote computer
        SimpleModeSupported = 1 << 1	// Component supports simple IO mode (or works in simple mode anyway) see \ref vst3IoMode
    }

    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = Platform.StructurePack)]
    public struct PClassInfoW
    {
        public const int ClassCardinalityManyInstances = 0x7FFFFFFF;

        [MarshalAs(UnmanagedType.Struct)]
        public Guid ClassId;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Cardinality;

        public AnsiCategory Category;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeName)]
        public String Name;

        // --------------------------------------------------------------------

        [MarshalAs(UnmanagedType.U4)]
        public ComponentClassFlags ClassFlags;

        public AnsiSubCategories SubCategories;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
        public String Vendor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public String Version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVersion)]
        public String SdkVersion;

        //---------------------------------------------------------------------
        // need extra structs to solve mixed Ansi/Unicode strings

        [NativeCppClass]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
        public struct AnsiCategory
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeCategory)]
            public String Value;
        }

        [NativeCppClass]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
        public struct AnsiSubCategories
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeSubCategories)]
            public String Value;
        }
    }
}


