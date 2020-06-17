using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteExpressionValueEvent
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 TypeId;		// see \ref NoteExpressionTypeID

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NoteId;						// associated note identifier to apply the change	

        [MarshalAs(UnmanagedType.R8)]
        public Double Value;			// normalized value [0.0, 1.0].
    }

    public enum NoteExpressionTypeIDs
    {
        VolumeTypeID = 0,		// Volume, plain range [0 = -oo , 0.25 = 0dB, 0.5 = +6dB, 1 = +12dB]: plain = 20 * log (4 * norm)
        PanTypeID,				// Panning (L-R), plain range [0 = left, 0.5 = center, 1 = right]
        TuningTypeID,           // Tuning, plain range [0 = -120.0 (ten octaves down), 0.5 none, 1 = +120.0 (ten octaves up)]
                                // plain = 240 * (norm - 0.5) and norm = plain / 240 + 0.5
                                // oneOctave is 12.0 / 240.0; oneHalfTune = 1.0 / 240.0;
        VibratoTypeID,			// Vibrato
        ExpressionTypeID,		// Expression
        BrightnessTypeID,		// Brightness
        TextTypeID,			    // TODO:
        PhonemeTypeID,			// TODO:

        CustomStart = 100000	// custom note change type ids must start from here
    };
}
