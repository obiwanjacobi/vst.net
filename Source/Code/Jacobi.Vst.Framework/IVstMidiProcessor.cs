namespace Jacobi.Vst.Framework
{
    // Note: this interface is NOT called IVstPluginMidiProcessor because the Host implements it too.
    public interface IVstMidiProcessor
    {
        int ChannelCount { get; }
        void Process(VstEventCollection events);
    }
}
