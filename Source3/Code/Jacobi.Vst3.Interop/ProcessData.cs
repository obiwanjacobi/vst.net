using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessData
    {
        [MarshalAs(UnmanagedType.I4)]
        Int32 ProcessMode;			///< processing mode - value of \ref ProcessModes

        [MarshalAs(UnmanagedType.I4)]
        Int32 SymbolicSampleSize;   ///< sample size - value of \ref SymbolicSampleSizes

        [MarshalAs(UnmanagedType.I4)]
        Int32 NumSamples;			///< number of samples to process

        [MarshalAs(UnmanagedType.I4)]
        Int32 NumInputs;			///< number of audio input buses

        [MarshalAs(UnmanagedType.I4)]
        Int32 NumOutputs;			///< number of audio output buses
        
        // AudioBusBuffers pointers
        [MarshalAs(UnmanagedType.SysInt)]
        IntPtr Inputs;	///< buffers of input buses
        
        [MarshalAs(UnmanagedType.SysInt)]
        IntPtr Outputs;	///< buffers of output buses

        [MarshalAs(UnmanagedType.Interface)]
        IParameterChanges InputParameterChanges;	///< incoming parameter changes for this block 
        
        [MarshalAs(UnmanagedType.Interface)]
        IParameterChanges OutputParameterChanges;	///< outgoing parameter changes for this block (optional)

        [MarshalAs(UnmanagedType.Interface)]
        IEventList InputEvents;				///< incoming events for this block (optional)

        [MarshalAs(UnmanagedType.Interface)]
        IEventList OutputEvents;				///< outgoing events for this block (optional)
        
        // ProcessContext pointer
        [MarshalAs(UnmanagedType.SysInt)]
        IntPtr ProcessContext;			///< processing context (optional, but most welcome)
    }
}
