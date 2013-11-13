using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteOffEvent
    {
        [MarshalAs(UnmanagedType.I2)]
        public Int16 Channel;			///< channel index in event bus

        [MarshalAs(UnmanagedType.I2)]
        public Int16 Pitch;			///< range [0, 127] = [C-2, G8] with A3=440Hz

        [MarshalAs(UnmanagedType.R4)]
        public Single Velocity;			///< range [0.0, 1.0]

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NoteId;			///< associated noteOn identifier (if not available then -1)

        [MarshalAs(UnmanagedType.R4)]
        public Single Tuning;			///< 1.f = +1 cent, -1.f = -1 cent 
    }
}
