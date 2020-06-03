using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IEditController2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEditController2
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetKnobMode(
            [MarshalAs(UnmanagedType.I4), In] KnobModes mode);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OpenHelp(
            [MarshalAs(UnmanagedType.U1), In] Boolean onlyCheck);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OpenAboutBox(
            [MarshalAs(UnmanagedType.U1), In] Boolean onlyCheck);
    }
}
