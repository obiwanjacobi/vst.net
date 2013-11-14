using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IEditControllerHostEditing)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEditControllerHostEditing
    {
        Int32 BeginEditFromHost(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramID);

        Int32 EndEditFromHost(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramID);
    }
}
