namespace Jacobi.Vst.Framework
{
    public interface IVstPluginAudioProcessor
    {
        int InputCount { get; }
        int OutputCount { get; }
        int TailSize { get; }
        double SampleRate { get;set; }
        int BlockSize { get;set; }
        void Process(VstAudioChannel[] inChannels, VstAudioChannel[] outChannels);
    }

    public interface IVstPluginAudioPrecissionProcessor : IVstPluginAudioProcessor
    {
        void Process(VstAudioPrecisionChannel[] inChannels, VstAudioPrecisionChannel[] outChannels);
    }
}
