using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IMidiLearn)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMidiLearn
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 OnLiveMIDIControllerInput(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.I2), In] Int16 midiControllerNumber);
    }
}
