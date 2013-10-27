using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Explicit, Pack = Platform.StructurePack)]
    public struct AudioBusBuffers
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.I4)]
        Int32 NumChannels;		///< number of audio channels in bus
        
        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U8)]
        UInt64 SilenceFlags;	///< Bitset of silence state per channel

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.R4)]
        // Single** pointer to an array Single[NumChannels][NumSamples]
        IntPtr ChannelBuffers32;	///< sample buffers to process with 32-bit precision
        
        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.R8)]
        // Double** pointer to an array Double[NumChannels][NumSamples]
		IntPtr ChannelBuffers64;	///< sample buffers to process with 64-bit precision
	    
    }
}
