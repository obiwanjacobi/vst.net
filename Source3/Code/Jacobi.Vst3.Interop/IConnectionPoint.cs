using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IConnectionPoint)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IConnectionPoint
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Connect (
            [MarshalAs(UnmanagedType.Interface), In] IConnectionPoint other);

	    [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Disconnect(
            [MarshalAs(UnmanagedType.Interface), In] IConnectionPoint other);
	    
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Notify(
            [MarshalAs(UnmanagedType.Interface), In] IMessage message);
    }
}
