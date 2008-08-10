namespace Jacobi.Vst.Framework
{
    using System;

    /// <summary>
    /// Flags that discribe plugin capabilities that could not be defined as interfaces.
    /// </summary>
    [Flags]
    public enum VstPluginCapabilities
    {
        /// <summary>The plugin has no extra capabilities to report to the host.</summary>
        None,
        /// <summary>The plugin is a VST Instrument (Synth).</summary>
        [Obsolete("Implementing the IVstPluginAudioProcessor and the IVstMidiProcessor interfaces automatically turns on this flag.")]
        IsSynth,
        /// <summary>The plugin will not produce any sound when audio input is silence.</summary>
        NoSoundInStop,
    }
}
