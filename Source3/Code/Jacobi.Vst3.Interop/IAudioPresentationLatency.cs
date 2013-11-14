using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IAudioPresentationLatency)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioPresentationLatency
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetAudioPresentationLatencySamples(
            [MarshalAs(UnmanagedType.I4), In] BusDirections dir,
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.U4), In] UInt32 latencyInSamples);
    }
}
