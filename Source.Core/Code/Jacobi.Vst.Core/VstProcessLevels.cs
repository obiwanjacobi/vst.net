namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Reports the logical thread ID to the plugin.
    /// </summary>
    public enum VstProcessLevels
    {
        /// <summary>Not supported by Host.</summary>
        Unknown = 0,
        /// <summary>1: currently in user thread (GUI).</summary>
        User,
        /// <summary>2: currently in audio thread (where process is called).</summary>
        Realtime,
        /// <summary>3: currently in 'sequencer' thread (MIDI, timer etc).</summary>
        Prefetch,
        /// <summary>4: currently offline processing and thus in user thread.</summary>
        Offline
    }
}
