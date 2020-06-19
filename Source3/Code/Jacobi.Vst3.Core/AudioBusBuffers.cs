using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Explicit)]
    public struct AudioBusBuffers
    {
        public static readonly int Size = Marshal.SizeOf<AudioBusBuffers>();

        [FieldOffset(FieldOffset_NumChannels)]
        [MarshalAs(UnmanagedType.I4)]
        public Int32 NumChannels;		// number of audio channels in bus

        [FieldOffset(FieldOffset_SilenceFlags)]
        [MarshalAs(UnmanagedType.U8)]
        public UInt64 SilenceFlags;	    // Bitset of silence state per channel

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.SysInt)]
        // Single** pointer to an array Single[NumChannels][NumSamples]
        public IntPtr ChannelBuffers32;	// sample buffers to process with 32-bit precision
        //public unsafe Single** ChannelBuffers32;

        [FieldOffset(FieldOffset_Union)]
        [MarshalAs(UnmanagedType.SysInt)]
        // Double** pointer to an array Double[NumChannels][NumSamples]
        public IntPtr ChannelBuffers64; // sample buffers to process with 64-bit precision
        //public unsafe Double** ChannelBuffers64;

#if X86
        internal const int FieldOffset_NumChannels = 0;
        internal const int FieldOffset_SilenceFlags = 4;
        internal const int FieldOffset_Union = 12;
#endif
#if X64
        internal const int FieldOffset_NumChannels = 0;
        internal const int FieldOffset_SilenceFlags = 8;
        internal const int FieldOffset_Union = 16;
#endif

    }
}
