namespace Jacobi.Vst.Core
{
    using System;

    public class VstTimeInfo
    {
        public VstTimeInfoFlags Flags;
        public double SamplePosition;
        public double SampleRate;
        public double NanoSeconds;
        public double PpqPosition;
        public double Tempo;
        public double BarStartPosition;
        public double CycleStartPosition;
        public double CysleEndPosition;
        public int TimeSignatureNumerator;
        public int TimeSignatureDenominator;
        public int SmpteOffset;
        public VstSmpteFrameRate SmpteFrameRate;
        public int SamplesToNearestClock;
    }

    [Flags]
    public enum VstTimeInfoFlags
    {
        TransportChanged = 1,		    // indicates that play, cycle or record state has changed
        TransportPlaying = 1 << 1,	    // set if Host sequencer is currently playing
        TransportCycleActive = 1 << 2,	// set if Host sequencer is in cycle mode
        TransportRecording = 1 << 3,	// set if Host sequencer is in record mode
        AutomationWriting = 1 << 6,	    // set if automation write mode active (record parameter changes)
        AutomationReading = 1 << 7,	    // set if automation read mode active (play parameter changes)
        
        NanoSecondsValid = 1 << 8,	        // VstTimeInfo::nanoSeconds valid
        PpqPositionValid = 1 << 9,	        // VstTimeInfo::ppqPos valid
        TempoValid = 1 << 10,	        // VstTimeInfo::tempo valid
        BarStartPositionValid = 1 << 11,	        // VstTimeInfo::barStartPos valid
        CyclePositionValid = 1 << 12,	    // VstTimeInfo::cycleStartPos and VstTimeInfo::cycleEndPos valid
        TimeSignatureValid = 1 << 13,	        // VstTimeInfo::timeSigNumerator and VstTimeInfo::timeSigDenominator valid
        SmpteValid = 1 << 14,	        // VstTimeInfo::smpteOffset and VstTimeInfo::smpteFrameRate valid
        ClockValid = 1 << 15	        // VstTimeInfo::samplesToNextClock valid
    }

    public enum VstSmpteFrameRate
    {
        Smpte24fps = 0,		// 24 fps
        Smpte25fps = 1,		// 25 fps
        Smpte2997fps = 2,	// 29.97 fps
        Smpte30fps = 3,		// 30 fps
        Smpte2997dfps = 4,	// 29.97 drop
        Smpte30dfps = 5,	// 30 drop

        SmpteFilm16mm = 6, 	// Film 16mm
        SmpteFilm35mm = 7, 	// Film 35mm
        Smpte239fps = 10,	// HDTV: 23.976 fps
        Smpte249fps = 11,	// HDTV: 24.976 fps
        Smpte599fps = 12,	// HDTV: 59.94 fps
        Smpte60fps = 13		// HDTV: 60 fps
    }
}
