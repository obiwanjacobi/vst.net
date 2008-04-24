namespace Jacobi.Vst.Core
{
    using System;
    
    public class VstAudioFile
    {
        public VstAudioFileFlags Flags;
        public string Name;
        public int FileID;
        public double SampleRate;
        public int NumberOfChannels;
        public double NumberOfFrames;
        public int Format;
        public double EditCusrsorPosition;
        public double SelectionStart;
        public double SelectionSize;
        public int SelectedChannelMask;
        public int NumberOfMarkers;
        public int TimeRulerUnit;
        public double TimeRulerOffset;
        public double Tempo;
        public int TimeSignatureNumerator;
        public int TimeSignatureDenominator;
        public int TicksPerBlackNote;
        public int SmpteFrameRate;
    }

    [Flags]
    public enum VstAudioFileFlags
    {
        ReadOnly = 1 << 0,	            // set by Host (in call #offlineNotify)
        NoRateConversion = 1 << 1,	    // set by Host (in call #offlineNotify)
        NoChannelChange = 1 << 2,	    // set by Host (in call #offlineNotify)

        CanProcessSelection = 1 << 10,	// set by plug-in (in call #offlineStart)
        NoCrossfade = 1 << 11,	        // set by plug-in (in call #offlineStart)
        WantRead = 1 << 12,	            // set by plug-in (in call #offlineStart)
        WantWrite = 1 << 13,	        // set by plug-in (in call #offlineStart)
        WantWriteMarker = 1 << 14,	    // set by plug-in (in call #offlineStart)
        WantMoveCursor = 1 << 15,	    // set by plug-in (in call #offlineStart)
        WantSelect = 1 << 16	        // set by plug-in (in call #offlineStart)
    }
}
