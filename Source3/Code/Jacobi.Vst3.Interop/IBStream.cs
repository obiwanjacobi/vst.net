using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IBStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IBStream
    {
        Int32 Read(byte[] buffer, Int32 numBytes, ref Int32 numBytesRead);

        Int32 Write (byte[] buffer, Int32 numBytes, ref Int32 numBytesWritten);

        Int32 Seek (Int64 pos, IStreamSeekMode mode, ref Int64 result);

        Int32 Tell (ref Int64 pos);
    }

    public enum IStreamSeekMode
    {
        IBSeekSet = 0, ///< set absolute seek position
        IBSeekCur,     ///< set seek position relative to current position
        IBSeekEnd      ///< set seek position relative to stream end
    };
}
