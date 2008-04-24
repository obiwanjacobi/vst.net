namespace Jacobi.Vst.Core
{
    using System;

    public class VstPluginInfo
    {
        public VstPluginInfoFlags Flags;
        public int NumberOfPrograms;
        public int NumberOfParameters;
        public int NumberOfAudioInputs;
        public int NumberOfAudioOutputs;
        public int InitialDelay;
        public int PluginID;
        public int PluginVersion;
    }

    [Flags]
    public enum VstPluginInfoFlags
    {
        None = 0,
        HasEditor     = 1 << 0,			// set if the plug-in provides a custom editor
	    CanReplacing  = 1 << 4,			// supports replacing process mode (which should the default mode in VST 2.4)
	    ProgramChunks = 1 << 5,			// program data is handled in formatless chunks
	    IsSynth       = 1 << 8,			// plug-in is a synth (VSTi), Host may assign mixer channels for its outputs
	    NoSoundInStop = 1 << 9,			// plug-in does not produce sound when input is all silence
    	CanDoubleReplacing = 1 << 12,	// plug-in supports double precision processing
    }
}
