using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessData
    {
        public static readonly int Size = Marshal.SizeOf<ProcessData>();

        [MarshalAs(UnmanagedType.I4)]
        public ProcessModes ProcessMode;			// processing mode - value of \ref ProcessModes

        [MarshalAs(UnmanagedType.I4)]
        public SymbolicSampleSizes SymbolicSampleSize;   // sample size - value of \ref SymbolicSampleSizes

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NumSamples;			// number of samples to process

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NumInputs;			    // number of audio input buses

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NumOutputs;			// number of audio output buses

        // AudioBusBuffers Inputs[NumBuses]
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr Inputs;	            // buffers of input buses
        //public unsafe AudioBusBuffers* Inputs;

        // AudioBusBuffers Outputs[NumBuses]
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr Outputs;	            // buffers of output buses
        //public unsafe AudioBusBuffers* Outputs;

        //[MarshalAs(UnmanagedType.Interface)]
        //public IParameterChanges InputParameterChanges;	// incoming parameter changes for this block 
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr InputParameterChangesPtr;

        //[MarshalAs(UnmanagedType.Interface)]
        //public IParameterChanges OutputParameterChanges;	// outgoing parameter changes for this block (optional)
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr OutputParameterChanges;

        //[MarshalAs(UnmanagedType.Interface)]
        //public IEventList InputEvents;				// incoming events for this block (optional)
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr InputEvents;

        //[MarshalAs(UnmanagedType.Interface)]
        //public IEventList OutputEvents;				// outgoing events for this block (optional)
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr OutputEvents;

        // ProcessContext pointer
        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr ProcessContext;			// processing context (optional, but most welcome)
    }
}
