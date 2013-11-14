using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IAttributeList)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAttributeList
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetInt(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.I8), In] Int64 value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetInt(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.I8), In, Out] ref Int64 value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetFloat(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.R8), In] Double value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetFloat(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.R8), In, Out] ref Double value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetString(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.LPWStr), In] String str);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetString(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 2), In] StringBuilder str,
            [MarshalAs(UnmanagedType.U4), In] UInt32 size);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetBinary(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr data,
            [MarshalAs(UnmanagedType.U4), In] UInt32 size);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetBinary(
            [MarshalAs(UnmanagedType.LPStr), In] String id,
            [MarshalAs(UnmanagedType.SysInt), In, Out] IntPtr data,
            [MarshalAs(UnmanagedType.U4), In, Out] ref UInt32 size);
    }
}
