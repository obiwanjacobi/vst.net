namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// This interface is implemented by a plugin that supports Midi and wants the enable Midi Programs.
    /// </summary>
    public interface IVstPluginMidiPrograms
    {
        /// <summary>
        /// Gets the collection of Midi Program info instances for each channel.
        /// </summary>
        VstMidiChannelInfoCollection ChannelInfos { get; }
    }
}
