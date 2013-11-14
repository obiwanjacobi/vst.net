using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = Platform.StructurePack)]
    public struct ProcessContext
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 State;					///< a combination of the values from \ref StatesAndFlags

        [MarshalAs(UnmanagedType.R8)]
        public Double SampleRate;				///< current sample rate (always valid)

        [MarshalAs(UnmanagedType.I8)]
        public Int64 ProjectTimeSamples;	///< project time in samples (always valid)

        [MarshalAs(UnmanagedType.I8)]
        public Int64 SystemTime;				///< system time in nanoseconds (optional)

        [MarshalAs(UnmanagedType.I8)]
        public Int64 ContinousTimeSamples;	///< project time, without loop (optional)

        [MarshalAs(UnmanagedType.R8)]
        public Double ProjectTimeMusic;	///< musical position in quarter notes (1.0 equals 1 quarter note)

        [MarshalAs(UnmanagedType.R8)]
        public Double BarPositionMusic;	///< last bar start position, in quarter notes

        [MarshalAs(UnmanagedType.R8)]
        public Double CycleStartMusic;	///< cycle start in quarter notes

        [MarshalAs(UnmanagedType.R8)]
        public Double CycleEndMusic;	///< cycle end in quarter notes

        [MarshalAs(UnmanagedType.R8)]
        public Double Tempo;					///< tempo in BPM (Beats Per Minute)

        [MarshalAs(UnmanagedType.I4)]
        public Int32 TimeSigNumerator;			///< time signature numerator (e.g. 3 for 3/4)

        [MarshalAs(UnmanagedType.I4)]
        public Int32 TimeSigDenominator;		///< time signature denominator (e.g. 4 for 3/4)

        [MarshalAs(UnmanagedType.Struct)]
        public Chord Chord;					///< musical info

        [MarshalAs(UnmanagedType.I4)]
        public Int32 SmpteOffsetSubframes;		///< SMPTE (sync) offset in subframes (1/80 of frame)

        [MarshalAs(UnmanagedType.Struct)]
        public FrameRate FrameRate;			///< frame rate

        [MarshalAs(UnmanagedType.I4)]
        public Int32 SamplesToNextClock;		///< MIDI Clock Resolution (24 Per Quarter Note), can be negative (nearest)

        public enum StatesAndFlags
        {
            Playing = 1 << 1,		///< currently playing
            CycleActive = 1 << 2,		///< cycle is active
            Recording = 1 << 3,		///< currently recording

            SystemTimeValid = 1 << 8,		///< systemTime contains valid information
            ContTimeValid = 1 << 17,	///< continousTimeSamples contains valid information

            ProjectTimeMusicValid = 1 << 9,///< projectTimeMusic contains valid information
            BarPositionValid = 1 << 11,	///< barPositionMusic contains valid information
            CycleValid = 1 << 12,	///< cycleStartMusic and barPositionMusic contain valid information

            TempoValid = 1 << 10,	///< tempo contains valid information
            TimeSigValid = 1 << 13,	///< timeSigNumerator and timeSigDenominator contain valid information
            ChordValid = 1 << 18,	///< chord contains valid information

            SmpteValid = 1 << 14,	///< smpteOffset and frameRate contain valid information
            ClockValid = 1 << 15		///< samplesToNextClock valid		
        }
    }
}
