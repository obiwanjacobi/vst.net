namespace Jacobi.Vst.Core
{
    public enum PluginCapabilities
    {
        None = 0,
        HasEditor = 0x0001,
        IsAudioProcessor = 0x0010,
        SupportsProgramChunks = 0x0020,
        IsSynth = 0x0100,
        NoSoundInStop = 0x0200,
        IsPrecisionAudioProcessor = 0x1000,
    }
}
