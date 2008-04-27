namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public interface IVstHostSequencer : IExtensibleObject
    {
        VstTimeInfo GetTime(VstTimeInfoFlags filterFlags);
        double SampleRate { get;}
        int BlockSize { get;}
        int InputLatency { get;}
        int OutputLatency { get;}
        VstProcessLevels ProcessLevel { get; }
    }
}
