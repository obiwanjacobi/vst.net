namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Capability flags for the plugin.
    /// </summary>
    [Flags]
    public enum VstPluginFlags
    {
        /// <summary>Null value.</summary>
        None = 0,
        /// <summary>Set if the plug-in provides a custom editor.</summary>
        HasEditor = 1 << 0,
        /// <summary>Supports replacing process mode (which should the default mode in VST 2.4).</summary>
        CanReplacing = 1 << 4,
        /// <summary>Program data is handled in formatless chunks.</summary>
        ProgramChunks = 1 << 5,
        /// <summary>Plug-in is a synth (VSTi), Host may assign mixer channels for its outputs.</summary>
        IsSynth = 1 << 8,
        /// <summary>Plug-in does not produce sound when input is all silence.</summary>
        NoSoundInStop = 1 << 9,
        /// <summary>Plug-in supports double precision processing.</summary>
        CanDoubleReplacing = 1 << 12,
    }
}
