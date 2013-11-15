using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.INoteExpressionController)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INoteExpressionController
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetNoteExpressionCount(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel);


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetNoteExpressionInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.I4), In] Int32 noteExpressionIndex,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref NoteExpressionTypeInfo info);


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetNoteExpressionStringByValue(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.U4), In] UInt32 noteExpressionTypeId,
            [MarshalAs(UnmanagedType.R8), In] Double valueNormalized,
            [MarshalAs(UnmanagedType.LPWStr), In, Out] ref String str);


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetNoteExpressionValueByString(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.U4), In] UInt32 noteExpressionTypeID,
            [MarshalAs(UnmanagedType.LPWStr), In] String str,
            [MarshalAs(UnmanagedType.R8), In, Out] ref Double valueNormalized);

    }
}
