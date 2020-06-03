using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessSetup
    {
        [MarshalAs(UnmanagedType.I4)]
        public ProcessModes ProcessMode;			///< \ref ProcessModes

        [MarshalAs(UnmanagedType.I4)]
        public SymbolicSampleSizes SymbolicSampleSize;	///< \ref SymbolicSampleSizes

        [MarshalAs(UnmanagedType.I4)]
        public Int32 MaxSamplesPerBlock;	///< maximum number of samples per audio block

        [MarshalAs(UnmanagedType.R8)]
        public Double SampleRate;		///< sample rate
    }
}
