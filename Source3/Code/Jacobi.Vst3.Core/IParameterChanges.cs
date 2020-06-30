using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IParameterChanges)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IParameterChanges
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetParameterCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.SysInt)]
        IntPtr GetParameterData(
            [MarshalAs(UnmanagedType.I4), In] Int32 index);
        //[return: MarshalAs(UnmanagedType.Interface)]
        //IParamValueQueue GetParameterData(
        //    [MarshalAs(UnmanagedType.I4), In] Int32 index);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.SysInt)]
        IntPtr AddParameterData(
            [MarshalAs(UnmanagedType.U4), In] UInt32 id,
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 index);
        //[return: MarshalAs(UnmanagedType.Interface)]
        //IParamValueQueue AddParameterData(
        //    [MarshalAs(UnmanagedType.U4), In] UInt32 id,
        //    [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 index);
    }
}
