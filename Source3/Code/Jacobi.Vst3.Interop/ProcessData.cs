using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessData
    {
        Int32 ProcessMode;			///< processing mode - value of \ref ProcessModes
        Int32 SymbolicSampleSize;   ///< sample size - value of \ref SymbolicSampleSizes
        Int32 NumSamples;			///< number of samples to process
        Int32 NumInputs;			///< number of audio input buses
        Int32 NumOutputs;			///< number of audio output buses
        
        // AudioBusBuffers pointers
        IntPtr Inputs;	///< buffers of input buses
        IntPtr Outputs;	///< buffers of output buses

        IParameterChanges InputParameterChanges;	///< incoming parameter changes for this block 
        IParameterChanges OutputParameterChanges;	///< outgoing parameter changes for this block (optional)
        IEventList InputEvents;				///< incoming events for this block (optional)
        IEventList OutputEvents;				///< outgoing events for this block (optional)
        
        // ProcessContext pointer
        IntPtr ProcessContext;			///< processing context (optional, but most welcome)
    }
}
