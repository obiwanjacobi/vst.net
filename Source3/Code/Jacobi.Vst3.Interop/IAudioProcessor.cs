using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IAudioProcessor)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioProcessor
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetBusArrangements([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), In] UInt64[] inputs, [MarshalAs(UnmanagedType.I4), In] Int32 numIns,
                                 [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3), In] UInt64[] outputs, [MarshalAs(UnmanagedType.I4), In] Int32 numOuts);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetBusArrangement([MarshalAs(UnmanagedType.I4), In] BusDirections dir, [MarshalAs(UnmanagedType.I4), In] Int32 index, 
                                [MarshalAs(UnmanagedType.U8), In, Out] ref UInt64 arr);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CanProcessSampleSize([MarshalAs(UnmanagedType.I4), In] Int32 symbolicSampleSize);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U4)]
        UInt32 GetLatencySamples();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetupProcessing([MarshalAs(UnmanagedType.Struct), In] ref ProcessSetup setup);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetProcessing([MarshalAs(UnmanagedType.U1), In]byte state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Process(ref ProcessData data);

    }
}
