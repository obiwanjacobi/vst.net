namespace Jacobi.Vst.Core
{
    public enum VstPluginCategory
    {
        Unknown = 0,	// Unknown, category not implemented
        Effect,			// Simple Effect
        Synth,			// VST Instrument (Synths, samplers,...)
        Analysis,		// Scope, Tuner, ...
        Mastering,		// Dynamics, ...
        Spacializer,	// Panners, ...
        RoomFx,			// Delays and Reverbs
        SurroundFx,		// Dedicated surround processor
        Restoration,	// Denoiser, ...
        OfflineProcess,	// Offline Process
        Shell,			// Plug-in is container of other plug-ins  @see effShellGetNextPlugin
        Generator,		// ToneGenerator, ...
    }
}
