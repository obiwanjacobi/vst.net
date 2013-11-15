using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IAttributes)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAttributes
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Set(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.Struct), In] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Queue(
            [MarshalAs(UnmanagedType.LPStr), In] String listID,
            [MarshalAs(UnmanagedType.Struct), In] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetBinaryData(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr data,
            [MarshalAs(UnmanagedType.U4), In] UInt32 bytes,
            [MarshalAs(UnmanagedType.U1), In] Boolean copyBytes);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Get(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Unqueue(
            [MarshalAs(UnmanagedType.LPStr), In] String listID,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetQueueItemCount(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ResetQueue(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ResetAllQueues();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetBinaryData(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.SysInt), In, Out] ref IntPtr data,
            [MarshalAs(UnmanagedType.U4), In] UInt32 bytes);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        UInt32 GetBinaryDataSize(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);
    }


    [ComImport]
    [Guid(Interfaces.IAttributes2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAttributes2
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Set(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.Struct), In] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Queue(
            [MarshalAs(UnmanagedType.LPStr), In] String listID,
            [MarshalAs(UnmanagedType.Struct), In] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetBinaryData(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr data,
            [MarshalAs(UnmanagedType.U4), In] UInt32 bytes,
            [MarshalAs(UnmanagedType.U1), In] Boolean copyBytes);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Get(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Unqueue(
            [MarshalAs(UnmanagedType.LPStr), In] String listID,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref FVariant data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetQueueItemCount(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ResetQueue(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ResetAllQueues();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetBinaryData(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID,
            [MarshalAs(UnmanagedType.SysInt), In, Out] ref IntPtr data,
            [MarshalAs(UnmanagedType.U4), In] UInt32 bytes);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        UInt32 GetBinaryDataSize(
            [MarshalAs(UnmanagedType.LPStr), In] String attrID);

        //---------------------------------------------------------------------

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 CountAttributes();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPStr)]
        String GetAttributeID(
            [MarshalAs(UnmanagedType.I4), In] Int32 index);
    }
}
