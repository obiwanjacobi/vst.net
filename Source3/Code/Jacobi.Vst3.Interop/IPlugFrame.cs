using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IPlugFrame)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPlugFrame
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ResizeView(
            [MarshalAs(UnmanagedType.Interface), In] IPlugView view, 
            [MarshalAs(UnmanagedType.Struct), In] ref ViewRect newSize);
    }
}
