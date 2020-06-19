using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct PolyPressureEvent
    {
        public static readonly int Size = Marshal.SizeOf<PolyPressureEvent>();

        [MarshalAs(UnmanagedType.I2)]
        public Int16 Channel;			// channel index in event bus

        [MarshalAs(UnmanagedType.I2)]
        public Int16 Pitch;			    // range [0, 127] = [C-2, G8] with A3=440Hz

        [MarshalAs(UnmanagedType.R4)]
        public Single Pressure;			// range [0.0, 1.0]

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NoteId;			// event should be applied to the noteId (if not -1)
    }
}
