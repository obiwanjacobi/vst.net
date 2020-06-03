using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IParamValueQueue)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IParamValueQueue
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        UInt32 GetParameterId();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetPointCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetPoint(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 sampleOffset,
            [MarshalAs(UnmanagedType.R8), In, Out] ref Double value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 AddPoint(
            [MarshalAs(UnmanagedType.I4), In] Int32 sampleOffset,
            [MarshalAs(UnmanagedType.R8), In] Double value,
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 index);
    }
}
