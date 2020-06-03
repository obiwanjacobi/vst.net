using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct ViewRect
    {
        [MarshalAs(UnmanagedType.I4)]
        public Int32 Left;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Top;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Right;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 Bottom;
    }
}
