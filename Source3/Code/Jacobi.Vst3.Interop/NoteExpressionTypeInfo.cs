using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteExpressionTypeInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 typeId;			///< unique identifier of this note Expression type

        [MarshalAs(UnmanagedType.LPWStr)]
        public String title;						///< note Expression type title (e.g. "Volume")

        [MarshalAs(UnmanagedType.LPWStr)]
        public String shortTitle;					///< note Expression type short title (e.g. "Vol")

        [MarshalAs(UnmanagedType.LPWStr)]
        public String units;						///< note Expression type unit (e.g. "dB")

        [MarshalAs(UnmanagedType.I4)]
        public Int32 unitId;							///< id of unit this NoteExpression belongs to (see \ref vst3UnitsIntro), in order to sort the note expression, it is possible to use unitId like for parameters. -1 means no unit used.

        [MarshalAs(UnmanagedType.Struct)]
        public NoteExpressionValueDescription valueDesc;	///< value description see \ref NoteExpressionValueDescription

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 associatedParameterId;			///< optional associated parameter ID (for mapping from note expression to global (using the parameter automation for example) and back). Only used when kAssociatedParameterIDValid is set in flags.

        [MarshalAs(UnmanagedType.I4)]
        public NoteExpressionTypeFlags flags;							///< NoteExpressionTypeFlags (see below)

        public enum NoteExpressionTypeFlags
        {
            kIsBipolar = 1 << 0,			///< event is bipolar (centered), otherwise unipolar
            kIsOneShot = 1 << 1,			///< event occurs only one time for its associated note (at begin of the noteOn)
            kIsAbsolute = 1 << 2,			///< This note expression will apply an absolute change to the sound (not relative (offset))
            kAssociatedParameterIDValid = 1 << 3,///< indicates that the associatedParameterID is valid and could be used
        }
    }
}
