using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IMidiMapping)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMidiMapping
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetMidiControllerAssignment(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.I2), In] Int16 midiControllerNumber,
            [MarshalAs(UnmanagedType.U4), In, Out] ref UInt32 paramId);
    }
}
