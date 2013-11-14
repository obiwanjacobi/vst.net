using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IUnitHandler)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IUnitHandler
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 NotifyUnitSelection(
            [MarshalAs(UnmanagedType.I4), In] Int32 unitId);
        
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 NotifyProgramListChange(
            [MarshalAs(UnmanagedType.I4), In] Int32 listId,
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex);
    }
}
