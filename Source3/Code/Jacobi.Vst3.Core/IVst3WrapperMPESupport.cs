using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IVst3WrapperMPESupport)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVst3WrapperMPESupport
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 EnableMPEInputProcessing(
            [MarshalAs(UnmanagedType.I4), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetMPEInputDeviceSettings(
            [MarshalAs(UnmanagedType.I4), In] Int32 masterChannel,
            [MarshalAs(UnmanagedType.I4), In] Int32 memberBeginChannel,
            [MarshalAs(UnmanagedType.I4), In] Int32 memberEndChannel);
    }
}
