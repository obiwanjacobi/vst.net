using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteExpressionTypeInfo
    {
        public static readonly int Size = Marshal.SizeOf<NoteExpressionTypeInfo>();

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 TypeId;			            // unique identifier of this note Expression type

        [MarshalAs(UnmanagedType.LPWStr)]
        public String Title;						// note Expression type title (e.g. "Volume")

        [MarshalAs(UnmanagedType.LPWStr)]
        public String ShortTitle;					// note Expression type short title (e.g. "Vol")

        [MarshalAs(UnmanagedType.LPWStr)]
        public String Units;						// note Expression type unit (e.g. "dB")

        [MarshalAs(UnmanagedType.I4)]
        public Int32 UnitId;						// id of unit this NoteExpression belongs to (see \ref vst3UnitsIntro), in order to sort the note expression, it is possible to use unitId like for parameters. -1 means no unit used.

        [MarshalAs(UnmanagedType.Struct)]
        public NoteExpressionValueDescription ValueDesc;	// value description see \ref NoteExpressionValueDescription

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 AssociatedParameterId;		// optional associated parameter ID (for mapping from note expression to global (using the parameter automation for example) and back). Only used when kAssociatedParameterIDValid is set in flags.

        [MarshalAs(UnmanagedType.I4)]
        public NoteExpressionTypeFlags Flags;		// NoteExpressionTypeFlags (see below)

        [Flags]
        public enum NoteExpressionTypeFlags
        {
            IsBipolar = 1 << 0,			            // event is bipolar (centered), otherwise unipolar
            IsOneShot = 1 << 1,			            // event occurs only one time for its associated note (at begin of the noteOn)
            IsAbsolute = 1 << 2,			        // This note expression will apply an absolute change to the sound (not relative (offset))
            AssociatedParameterIDValid = 1 << 3,    // indicates that the associatedParameterID is valid and could be used
        }
    }
}
