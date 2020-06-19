using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Explicit, CharSet = Platform.CharacterSet)]
    public struct FVariant
    {
        public static readonly int Size = Marshal.SizeOf<FVariant>();

        [FieldOffset(FieldOffset_Type)]
        public VariantType Type;

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.I8)]
        public Int64 IntValue;

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.R8)]
        public Double floatValue;

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.LPStr)]
        public String string8;

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.LPWStr)]
        public String string16;

        [FieldOffset(FieldOffset_Union)]
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

#if X86
        internal const int FieldOffset_Type = 0;
        internal const int FieldOffset_Union = 4;
#endif
#if X64
        internal const int FieldOffset_Type = 0;
        internal const int FieldOffset_Union = 8;
#endif
    }
}
