namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public interface IVstPluginMidiPrograms
    {
        VstMidiChannelInfoCollection ChannelInfos { get; }
    }
}
