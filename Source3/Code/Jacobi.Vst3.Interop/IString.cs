using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IString
    {
        [PreserveSig]
        void SetText8(
            [MarshalAs(UnmanagedType.LPStr), In] String text);

        [PreserveSig]
        void SetText16(
            [MarshalAs(UnmanagedType.LPWStr), In] String text);


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPStr)]
        String GetText8();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        String GetText16();


        [PreserveSig]
        void Take(
            [MarshalAs(UnmanagedType.SysInt), In] IntPtr s,
            [MarshalAs(UnmanagedType.U1), In] Boolean isWide);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U1)]
        Boolean IsWideString();
    }
}
