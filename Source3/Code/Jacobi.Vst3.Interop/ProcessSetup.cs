using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessSetup
    {
        Int32 ProcessMode;			///< \ref ProcessModes
        Int32 SymbolicSampleSize;	///< \ref SymbolicSampleSizes
        Int32 MaxSamplesPerBlock;	///< maximum number of samples per audio block
        Double SampleRate;		///< sample rate
    }
}
