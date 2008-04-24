namespace Jacobi.Vst.Framework
{
    using System;

    public interface IVstAudioProcessor : IDisposable
    {
        long SampleRate { get;set; }
        long BlockSize { get;set; }
        void Process(VstAudioChannelCollection inChannels, VstAudioChannelCollection outChannels, long sampleFrames);
    }

    public interface IVstAudioPrecissionProcessor : IVstAudioProcessor { } // marker interface
}
