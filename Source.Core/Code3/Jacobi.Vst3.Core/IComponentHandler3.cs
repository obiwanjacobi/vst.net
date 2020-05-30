using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IComponentHandler3)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IComponentHandler3
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Interface)]
        IContextMenu CreateContextMenu(
            [MarshalAs(UnmanagedType.Interface), In] IPlugView plugView,
            [MarshalAs(UnmanagedType.U4), In, Out] ref UInt32 paramID);
    }
}
