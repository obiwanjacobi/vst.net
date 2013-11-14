using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IBStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBStream
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Read(
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr buffer,
            [MarshalAs(UnmanagedType.I4), In] Int32 numBytes,
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 numBytesRead);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Write(
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr buffer,
            [MarshalAs(UnmanagedType.I4), In] Int32 numBytes,
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 numBytesWritten);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Seek(
            [MarshalAs(UnmanagedType.I8), In] Int64 pos,
            [MarshalAs(UnmanagedType.I4), In] StreamSeekMode mode,
            [MarshalAs(UnmanagedType.I8), In, Out] ref Int64 result);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Tell(
            [MarshalAs(UnmanagedType.I8), In] ref Int64 pos);
    }

    public enum StreamSeekMode
    {
        SeekSet = 0, ///< set absolute seek position
        SeekCur,     ///< set seek position relative to current position
        SeekEnd      ///< set seek position relative to stream end
    };
}
