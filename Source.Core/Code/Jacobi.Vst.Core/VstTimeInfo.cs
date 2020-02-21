namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Used to communicate Time information to the plugin.
    /// </summary>
    public class VstTimeInfo
    {
        /// <summary>
        /// Time format Flags.
        /// </summary>
        public VstTimeInfoFlags Flags { get; set; }

        /// <summary>
        /// The current Position in audio samples (always valid).
        /// </summary>
        /// <remarks>Current Position. It must always be valid, and should not cost a lot to ask for. 
        /// The sample position is ahead of the time displayed to the user. In sequencer stop mode, 
        /// its value does not change. A 32 bit integer is too small for sample positions, 
        /// and it's a double to make it easier to convert between ppq and samples.</remarks>
        public double SamplePosition { get; set; }

        /// <summary>
        /// The current Sample Rate in Herz (always valid).
        /// </summary>
        public double SampleRate { get; set; }

        /// <summary>
        /// System Time in nanoseconds (10^-9 second).
        /// </summary>
        public double NanoSeconds { get; set; }

        /// <summary>
        /// Musical Position, in Quarter Note (1.0 equals 1 Quarter Note).
        /// </summary>
        /// <remarks>At tempo 120, 1 quarter makes 1/2 second, so 2.0 ppq translates to 48000 samples at 48kHz sample rate.
        /// .25 ppq is one sixteenth note then. if you need something like 480ppq, you simply multiply ppq by that scaler.</remarks>
        public double PpqPosition { get; set; }

        /// <summary>
        /// current Tempo in BPM (Beats Per Minute).
        /// </summary>
        public double Tempo { get; set; }

        /// <summary>
        /// last Bar Start Position, in Quarter Note.
        /// </summary>
        /// <remarks>Say we're at bars/beats readout 3.3.3. That's 2 bars + 2 q + 2 sixteenth, 
        /// makes 2 * 4 + 2 + .25 = 10.25 ppq. at tempo 120, that's 10.25 * .5 = 5.125 seconds, 
        /// times 48000 = 246000 samples.</remarks>
        public double BarStartPosition { get; set; }

        /// <summary>
        /// Cycle Start (left locator), in Quarter Note.
        /// </summary>
        public double CycleStartPosition { get; set; }

        /// <summary>
        /// Cycle End (right locator), in Quarter Note.
        /// </summary>
        public double CycleEndPosition { get; set; }

        /// <summary>
        /// Time Signature Numerator (e.g. 3 for 3/4)
        /// </summary>
        public int TimeSignatureNumerator { get; set; }

        /// <summary>
        /// Time Signature Denominator (e.g. 4 for 3/4)
        /// </summary>
        public int TimeSignatureDenominator { get; set; }

        /// <summary>
        /// SMPTE offset (in SMPTE subframes (bits; 1/80 of a frame)).
        /// </summary>
        /// <remarks>The current SMPTE position can be calculated using #samplePos, #sampleRate, and #smpteFrameRate.</remarks>
        public int SmpteOffset { get; set; }

        /// <summary>
        /// Smpte frame rate.
        /// </summary>
        public VstSmpteFrameRate SmpteFrameRate { get; set; }

        /// <summary>
        /// MIDI Clock Resolution (24 Per Quarter Note), can be negative (nearest clock).
        /// </summary>
        /// <remarks>MIDI Clock Resolution (24 per Quarter Note), can be negative the distance to the next midi clock 
        /// (24 ppq, pulses per quarter) in samples. unless samplePos falls precicely on a midi clock, 
        /// this will either be negative such that the previous MIDI clock is addressed, 
        /// or positive when referencing the following (future) MIDI clock.</remarks>
        public int SamplesToNearestClock { get; set; }
    }

    /// <summary>
    /// Time information flags.
    /// </summary>
    [Flags]
    public enum VstTimeInfoFlags
    {
        /// <summary>Indicates that play, cycle or record state has changed.</summary>
        TransportChanged = 1,
        /// <summary>Set if Host sequencer is currently playing.</summary>
        TransportPlaying = 1 << 1,
        /// <summary>Set if Host sequencer is in cycle mode.</summary>
        TransportCycleActive = 1 << 2,
        /// <summary>Set if Host sequencer is in record mode.</summary>
        TransportRecording = 1 << 3,
        /// <summary>Set if automation write mode active (record parameter changes).</summary>
        AutomationWriting = 1 << 6,
        /// <summary>Set if automation read mode active (play parameter changes).</summary>
        AutomationReading = 1 << 7,

        /// <summary>VstTimeInfo::nanoSeconds valid</summary>
        NanoSecondsValid = 1 << 8,
        /// <summary>VstTimeInfo::ppqPos valid</summary>
        PpqPositionValid = 1 << 9,
        /// <summary>VstTimeInfo::tempo valid</summary>
        TempoValid = 1 << 10,
        /// <summary>VstTimeInfo::barStartPos valid</summary>
        BarStartPositionValid = 1 << 11,
        /// <summary>VstTimeInfo::cycleStartPos and VstTimeInfo::cycleEndPos valid</summary>
        CyclePositionValid = 1 << 12,
        /// <summary>VstTimeInfo::timeSigNumerator and VstTimeInfo::timeSigDenominator valid</summary>
        TimeSignatureValid = 1 << 13,
        /// <summary>VstTimeInfo::smpteOffset and VstTimeInfo::smpteFrameRate valid</summary>
        SmpteValid = 1 << 14,
        /// <summary>VstTimeInfo::samplesToNextClock valid</summary>
        ClockValid = 1 << 15
    }

    /// <summary>
    /// Smpte frame rates.
    /// </summary>
    public enum VstSmpteFrameRate
    {
        /// <summary>24 fps</summary>
        Smpte24fps = 0,
        /// <summary>25 fps</summary>
        Smpte25fps = 1,
        /// <summary>29.97 fps</summary>
        Smpte2997fps = 2,
        /// <summary>30 fps</summary>
        Smpte30fps = 3,
        /// <summary>29.97 drop</summary>
        Smpte2997dfps = 4,
        /// <summary>30 drop</summary>
        Smpte30dfps = 5,

        /// <summary>Film 16mm</summary>
        SmpteFilm16mm = 6,
        /// <summary>Film 35mm</summary>
        SmpteFilm35mm = 7,
        /// <summary>HDTV: 23.976 fps</summary>
        Smpte239fps = 10,
        /// <summary>HDTV: 24.976 fps</summary>
        Smpte249fps = 11,
        /// <summary>HDTV: 59.94 fps</summary>
        Smpte599fps = 12,
        /// <summary>HDTV: 60 fps</summary>
        Smpte60fps = 13
    }
}
