using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IContextMenuTarget)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IContextMenuTarget
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ExecuteMenuItem(
            [MarshalAs(UnmanagedType.I4), In]Int32 tag);
    }
}
