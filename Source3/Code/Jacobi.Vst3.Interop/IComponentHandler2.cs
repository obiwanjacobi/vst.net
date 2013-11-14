using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IComponentHandler2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IComponentHandler2
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetDirty(
            [MarshalAs(UnmanagedType.U1), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 RequestOpenEditor(
            [MarshalAs(UnmanagedType.LPStr), In] String name);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 StartGroupEdit();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 FinishGroupEdit();
    }
}
