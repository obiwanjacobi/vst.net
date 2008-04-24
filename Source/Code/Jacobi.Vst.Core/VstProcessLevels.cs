namespace Jacobi.Vst.Core
{
    public enum VstProcessLevels
    {
        Unknown = 0,	// not supported by Host
        User,			// 1: currently in user thread (GUI)
        Realtime,		// 2: currently in audio thread (where process is called)
        Prefetch,		// 3: currently in 'sequencer' thread (MIDI, timer etc)
        Offline			// 4: currently offline processing and thus in user thread
    }
}
