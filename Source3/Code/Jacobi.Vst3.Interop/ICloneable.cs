using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.ICloneable)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICloneable
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.IUnknown)]
        Object Clone();
    }
}
