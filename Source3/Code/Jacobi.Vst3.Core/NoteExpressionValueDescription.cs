using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteExpressionValueDescription
    {
        [MarshalAs(UnmanagedType.R8)]
        public Double DefaultValue;		// default normalized value [0,1]

        [MarshalAs(UnmanagedType.R8)]
        public Double Minimum;			// minimum normalized value [0,1]

        [MarshalAs(UnmanagedType.R8)]
        public Double Maximum;			// maximum normalized value [0,1]

        [MarshalAs(UnmanagedType.I4)]
        public Int32 StepCount;						// number of discrete steps (0: continuous, 1: toggle, discrete value otherwise - see \ref vst3parameterIntro)

    }
}
