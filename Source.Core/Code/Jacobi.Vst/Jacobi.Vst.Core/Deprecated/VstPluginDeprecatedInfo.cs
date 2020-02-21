namespace Jacobi.Vst.Core.Deprecated
{
    using System;

    using Jacobi.Vst.Core.Plugin;

    /// <summary>
    /// Plugin deprecated information passed to the Host.
    /// </summary>
    public class VstPluginDeprecatedInfo : VstPluginInfo
    {
        /// <summary>
        /// Plugin flags.
        /// </summary>
        public VstPluginDeprecatedFlags DeprecatedFlags { get; set; }

        /// <summary>
        /// Number of realtime qualities (0: realtime).
        /// </summary>
        public int RealQualities { get; set; }

        /// <summary>
        /// Number of offline qualities (0: realtime only).
        /// </summary>
        public int OfflineQualities { get; set; }

        /// <summary>
        /// Input samplerate to output samplerate ratio, not used yet.
        /// </summary>
        public float IoRatio { get; set; }
    }

    /// <summary>
    /// Deprecated capability flags for the plugin.
    /// </summary>
    [Flags]
    public enum VstPluginDeprecatedFlags
    {
        /// <summary>Null value.</summary>
        None = 0,
        /// <summary>Return > 1. in getVu() if clipped.</summary>
        HasClip = 1 << 1,
        /// <summary>Return vu value in getVu(); > 1. means clipped.</summary>
        HasVu = 1 << 2,
        /// <summary>If numInputs == 2, makes sense to be used for mono in.</summary>
        CanMono = 1 << 3,
        /// <summary>For external dsp; plug returns immedeately from process()
        /// host polls plug position (current block) via effGetCurrentPosition.</summary>
        ExtIsAsync = 1 << 10,
        /// <summary>external dsp, may have their own output buffe (32 bit float)
        /// host then requests this via effGetDestinationBuffer.</summary>
        ExtHasBuffer = 1 << 11,
    }
}
