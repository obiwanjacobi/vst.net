using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ComLightPlugin
{
    [NativeCppClass]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
    public struct PClassInfo
    {
        public static readonly int Size = Marshal.SizeOf<PClassInfo>();

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
}


