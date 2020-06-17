using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct Chord
    {
        [MarshalAs(UnmanagedType.U1)]
        public Byte keyNote;		// key note in chord

        [MarshalAs(UnmanagedType.U1)]
        public Byte rootNote;		// lowest note in chord

        /** Bitmask of a chord.
            1st bit set: minor second; 2nd bit set: major second, and so on. \n
            There is \b no bit for the keynote (root of the chord) because it is inherently always present. \n
            Examples:
            - XXXX 0000 0100 1000 (= 0x0048) -> major chord\n
            - XXXX 0000 0100 0100 (= 0x0044) -> minor chord\n
            - XXXX 0010 0100 0100 (= 0x0244) -> minor chord with minor seventh  */

        [MarshalAs(UnmanagedType.I2)]
        public Int16 chordMask;

        public enum Masks
        {
            ChordMask = 0x0FFF,	    // mask for chordMask 
            ReservedMask = 0xF000	// reserved for future use
        }
    }
}
