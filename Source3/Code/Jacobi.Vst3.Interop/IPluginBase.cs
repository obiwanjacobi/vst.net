using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IPluginBase)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPluginBase
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Initialize(
            [MarshalAs(UnmanagedType.IUnknown), In] Object context);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Terminate();
    }
}
