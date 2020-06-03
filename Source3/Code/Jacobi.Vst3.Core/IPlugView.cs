using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IPlugView)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPlugView
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 IsPlatformTypeSupported(
            [MarshalAs(UnmanagedType.LPStr), In] String type);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Attached(
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr parent,
            [MarshalAs(UnmanagedType.LPStr), In] String type);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Removed();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnWheel(
            [MarshalAs(UnmanagedType.R4), In] Single distance);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnKeyDown(
            [MarshalAs(UnmanagedType.U2), In] Char key,
            [MarshalAs(UnmanagedType.I2), In] Int16 keyCode,
            [MarshalAs(UnmanagedType.I2), In] Int16 modifiers);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnKeyUp(
            [MarshalAs(UnmanagedType.U2), In] Char key,
            [MarshalAs(UnmanagedType.I2), In] Int16 keyCode,
            [MarshalAs(UnmanagedType.I2), In] Int16 modifiers);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetSize(
            [MarshalAs(UnmanagedType.Struct), In, Out] ref ViewRect size);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnSize(
            [MarshalAs(UnmanagedType.Struct), In] ref ViewRect newSize);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnFocus(
            [MarshalAs(UnmanagedType.U1), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetFrame(
            [MarshalAs(UnmanagedType.Interface), In] IPlugFrame frame);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CanResize();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CheckSizeConstraint(
            [MarshalAs(UnmanagedType.Struct), In] ref ViewRect rect);
    }
}
