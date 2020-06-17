using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct DataEvent
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 Size;			// size of the bytes

        [MarshalAs(UnmanagedType.U4)]
        public DataTypes Type;			// type of this data block (see \ref DataTypes)

        [MarshalAs(UnmanagedType.SysInt)]
        public IntPtr Bytes;		// pointer to the data block

        public enum DataTypes
        {
            MidiSysEx = 0		// for MIDI system exclusive message
        };
    }
}
