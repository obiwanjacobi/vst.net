using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Explicit, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct FVariant
    {
        [FieldOffset(0)]
        public VariantType Type;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.I8)]
        public Int64 IntValue;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.R8)]
        public Double floatValue;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.LPStr)]
        public String string8;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.LPWStr)]
        public String string16;

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.IUnknown)]
        public Object Obj;

        public enum VariantType
        {
            Empty = 0,
            Integer = 1 << 0,
            Float = 1 << 1,
            String8 = 1 << 2,
            Object = 1 << 3,
            Owner = 1 << 4,
            String16 = 1 << 5
        }
    }
}
