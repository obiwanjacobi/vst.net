using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IProgramListData)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IProgramListData
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ProgramDataSupported(
            [MarshalAs(UnmanagedType.I4), In] Int32 listId);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetProgramData(
            [MarshalAs(UnmanagedType.I4), In] Int32 listId,
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex,
            [MarshalAs(UnmanagedType.Interface), In] IBStream data);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetProgramData(
            [MarshalAs(UnmanagedType.I4), In] Int32 listId,
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex,
            [MarshalAs(UnmanagedType.Interface), In] IBStream data);
    }
}
