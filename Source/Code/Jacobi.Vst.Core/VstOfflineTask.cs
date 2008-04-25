namespace Jacobi.Vst.Core
{
    using System;

    public class VstOfflineTask
    {
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

    [Flags]
    public enum VstOfflineTaskFlags
    {
        InvalidParameter = 1 << 0,	// set by Host
        NewFile = 1 << 1,	        // set by Host

        PlugError = 1 << 10,	    // set by plug-in
        InterleavedAudio = 1 << 11,	// set by plug-in
        TempOutputFile = 1 << 12,	// set by plug-in
        FloatOutputFile = 1 << 13,	// set by plug-in
        RandomWrite = 1 << 14,	    // set by plug-in
        Stretch = 1 << 15,	        // set by plug-in
        NoThread = 1 << 16	        // set by plug-in
    }

    public enum VstOfflineOption
    {
        Audio,		// reading/writing audio samples
        Peaks,		// reading graphic representation
        Parameter,	// reading/writing parameters
        Marker,		// reading/writing marker
        Cursor,		// reading/moving edit cursor
        Selection,	// reading/changing selection
        QueryFiles	// to request the Host to call asynchronously #offlineNotify
    }
}
