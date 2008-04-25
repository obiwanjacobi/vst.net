namespace Jacobi.Vst.Framework
{
    using System;

    public interface IVstAudioProcessor : IDisposable
    {
        int InputCount { get; }
        int OutputCount { get; }
        double SampleRate { get;set; }
        int BlockSize { get;set; }
        void Process(VstAudioChannel[] inChannels, VstAudioChannel[] outChannels);
    }

    public interface IVstAudioPrecissionProcessor : IVstAudioProcessor
    {
        void Process(VstAudioPrecisionChannel[] inChannels, VstAudioPrecisionChannel[] outChannels);
    }
}
