namespace Jacobi.Vst.Framework
{
    using System;

    [Flags]
    public enum VstPluginCapabilities
    {
        None, 
        [Obsolete("Implementing the IVstPluginAudioProcessor and the IVstMidiProcessor interfaces automatically turns on this flag.")]
        IsSynth,
        NoSoundInStop,
    }
}
