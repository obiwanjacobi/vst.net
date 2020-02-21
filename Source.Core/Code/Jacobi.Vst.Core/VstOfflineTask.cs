//
// This source file is not compiled!
// It is part of offline processing which is not implemented
//
namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Information on an offline task.
    /// </summary>
    public class VstOfflineTask
    {
        /// <summary>
        /// Flags for the offline task.
        /// </summary>
        public VstOfflineTaskFlags Flags;

        public string ProcessName;
        public double ReadPosition;
        public double WritePosition;
        public byte[] InputBuffer;
        public byte[] OutputBuffer;
        public double PositionToProcessFrom;
        public double NumberOfFramesToProcess;
        public double MaximumNumberOfFramesToWrite;

        public byte[] ExtraBuffer;
        public int Value;
        public int Index;

        public double NumberOfFramesInSourceFile;
        public double SampleRateOfSource;
        public double SampleRateOfDestination;
        public int NumberOfChannelsInSource;
        public int NumberOfChannelsInDestination;
        public int FormatOfSource;
        public int FormatOfDestination;
        public string OutputText;

        public double Progress;
        public string ProgressText;
    }

    /// <summary>
    /// Flags for an offline task.
    /// </summary>
    [Flags]
    public enum VstOfflineTaskFlags
    {
        /// <summary>set by Host.</summary>
        InvalidParameter = 1 << 0,
        /// <summary>set by Host.</summary>
        NewFile = 1 << 1,

        /// <summary>set by plug-in.</summary>
        PlugError = 1 << 10,
        /// <summary>set by plug-in.</summary>
        InterleavedAudio = 1 << 11,
        /// <summary>set by plug-in.</summary>
        TempOutputFile = 1 << 12,
        /// <summary>set by plug-in.</summary>
        FloatOutputFile = 1 << 13,
        /// <summary>set by plug-in.</summary>
        RandomWrite = 1 << 14,
        /// <summary>set by plug-in.</summary>
        Stretch = 1 << 15,
        /// <summary>set by plug-in.</summary>
        NoThread = 1 << 16
    }

    /// <summary>
    /// Options for an offline task.
    /// </summary>
    public enum VstOfflineOption
    {
        /// <summary>Reading/writing audio samples.</summary>
        Audio,
        /// <summary>Reading graphic representation.</summary>
        Peaks,
        /// <summary>Reading/writing parameters.</summary>
        Parameter,
        /// <summary>Reading/writing marker.</summary>
        Marker,
        /// <summary>Reading/moving edit cursor.</summary>
        Cursor,
        /// <summary>Reading/changing selection.</summary>
        Selection,
        /// <summary>To request the Host to call asynchronously #offlineNotify.</summary>
        QueryFiles
    }
}
