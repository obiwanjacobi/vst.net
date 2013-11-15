using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IErrorContext)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorContext
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        void DisableErrorUI(
            [MarshalAs(UnmanagedType.U1), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ErrorMessageShown();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 getErrorMessage(
            [MarshalAs(UnmanagedType.Interface), In] IString message);
    }
}
