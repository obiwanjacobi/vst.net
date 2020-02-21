namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Indicates under what category the plugin falls.
    /// </summary>
    public enum VstPluginCategory
    {
        /// <summary>Unknown, category not implemented.</summary>
        Unknown = 0,
        /// <summary>Simple Effect.</summary>
        Effect,
        /// <summary>VST Instrument (Synths, samplers, ...).</summary>
        Synth,
        /// <summary>Scope, Tuner, ...</summary>
        Analysis,
        /// <summary>Dynamics, ...</summary>
        Mastering,
        /// <summary>Panners, ...</summary>
        Spacializer,
        /// <summary>Delays and Reverbs.</summary>
        RoomFx,
        /// <summary>Dedicated surround processor.</summary>
        SurroundFx,
        /// <summary>Denoiser, ...</summary>
        Restoration,
        /// <summary>Offline Process.</summary>
        OfflineProcess,
        /// <summary>Plug-in is container of other plug-ins.</summary>
        Shell,
        /// <summary>ToneGenerator, ...</summary>
        Generator,
    }
}
