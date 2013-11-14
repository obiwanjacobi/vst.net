using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Explicit, Pack = Platform.StructurePack)]
    public class AudioBusBuffers
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.I4)]
        public Int32 NumChannels;		///< number of audio channels in bus

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.U8)]
        public UInt64 SilenceFlags;	///< Bitset of silence state per channel

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.SysInt)]
        // Single** pointer to an array Single[NumChannels][NumSamples]
        public IntPtr ChannelBuffers32;	///< sample buffers to process with 32-bit precision
        //public Single** ChannelBuffers32;	///< sample buffers to process with 32-bit precision

        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.SysInt)]
        // Double** pointer to an array Double[NumChannels][NumSamples]
        public IntPtr ChannelBuffers64;	///< sample buffers to process with 64-bit precision
        //public Double** ChannelBuffers64;	///< sample buffers to process with 64-bit precision
    }


}
