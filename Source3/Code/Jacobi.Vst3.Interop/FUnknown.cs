using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.FUnknown)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface FUnknown
    {
        [return: MarshalAs(UnmanagedType.Error)]
        TResult QueryInterface(Guid iid, [MarshalAs(UnmanagedType.Interface)] out FUnknown obj);

        [return:MarshalAs(UnmanagedType.U4)]
        UInt32 AddRef();

        [return:MarshalAs(UnmanagedType.U4)]
        UInt32 Release();
    }
}
