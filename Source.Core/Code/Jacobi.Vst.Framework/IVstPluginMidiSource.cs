namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// This interface is implemented when a plugin wants to output Midi events to the host.
    /// </summary>
    /// <remarks>When a plugin calls the <see cref="IVstMidiProcessor.Process"/> of the 
    /// host and it does not implement this interface, an exception is thrown.</remarks>
    public interface IVstPluginMidiSource
    {
        /// <summary>
        /// Gets the number of channels the plugin supports for Midi Out.
        /// </summary>
        /// <remarks>Called by the host.</remarks>
        int ChannelCount { get; }
    }
}
