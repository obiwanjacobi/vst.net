namespace Jacobi.Vst3.Interop
{
    public enum RestartFlags
    {
        None = 0,
        ReloadComponent = 1 << 0,	        ///< The Component should be reloaded
        IoChanged = 1 << 1,	                ///< Input and/or Output Bus configuration has changed
        ParamValuesChanged = 1 << 2,	    ///< Multiple parameter values have changed (as result of a program change for example) 
        LatencyChanged = 1 << 3,	        ///< Latency has changed (IAudioProcessor.getLatencySamples)
        ParamTitlesChanged = 1 << 4,	    ///< Parameter titles or default values or flags have changed
        MidiCCAssignmentChanged = 1 << 5,	///< MIDI Controller Assignments have changed     [SDK 3.0.1]
        NoteExpressionChanged = 1 << 6,	    ///< Note Expression has changed (info, count...) [SDK 3.5.0]
        IoTitlesChanged = 1 << 7	        ///< Input and/or Output bus titles have changed  [SDK 3.5.0]
    }
}
