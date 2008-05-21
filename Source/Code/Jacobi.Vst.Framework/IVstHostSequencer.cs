namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public interface IVstHostSequencer : IExtensible
    {
        double SampleRate { get;}
        int BlockSize { get;}
        int InputLatency { get;}
        int OutputLatency { get;}
        VstProcessLevels ProcessLevel { get; }

        VstTimeInfo GetTime(VstTimeInfoFlags filterFlags);
        bool UpdatePluginIO();
    }
}
