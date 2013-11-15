using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.ISizeableStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISizeableStream
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetStreamSize(
            [MarshalAs(UnmanagedType.I8), In, Out] ref Int64 size);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetStreamSize(
            [MarshalAs(UnmanagedType.I8), In] Int64 size);
    }
}
