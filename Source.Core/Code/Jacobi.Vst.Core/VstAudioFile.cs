//
// This source file is not compiled!
// It is part of offline processing which is not implemented
//
namespace Jacobi.Vst.Core
{
    using System;
    
    /// <summary>
    /// Information about an audio file.
    /// </summary>
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
        /// <summary>set by Host (in call #offlineNotify).</summary>
        ReadOnly = 1 << 0,
        /// <summary>set by Host (in call #offlineNotify).</summary>
        NoRateConversion = 1 << 1,
        /// <summary>set by Host (in call #offlineNotify).</summary>
        NoChannelChange = 1 << 2,

        /// <summary>set by plug-in (in call #offlineStart).</summary>
        CanProcessSelection = 1 << 10,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        NoCrossfade = 1 << 11,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        WantRead = 1 << 12,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        WantWrite = 1 << 13,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        WantWriteMarker = 1 << 14,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        WantMoveCursor = 1 << 15,
        /// <summary>set by plug-in (in call #offlineStart).</summary>
        WantSelect = 1 << 16
    }
}
