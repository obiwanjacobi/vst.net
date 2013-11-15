using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IPersistent)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersistent
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassID(
            [MarshalAs(UnmanagedType.LPStr), In, Out] ref String uid);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SaveAttributes(
            [MarshalAs(UnmanagedType.Interface), In] IAttributes attrs);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 LoadAttributes(
            [MarshalAs(UnmanagedType.Interface), In] IAttributes attrs);

    }
}
