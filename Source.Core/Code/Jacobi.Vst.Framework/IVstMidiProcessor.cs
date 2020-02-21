namespace Jacobi.Vst.Framework
{
    // Note: this interface is NOT called IVstPluginMidiProcessor because the Host implements it too.
    
    /// <summary>
    /// This interface is used to pass on incoming Midi data.
    /// </summary>
    /// <remarks>
    /// The host implements this interface to allow the plugin to send the host Midi data.
    /// A Plugin can implement this interface to receieve Midi data from the host.
    /// </remarks>
    public interface IVstMidiProcessor
    {
        /// <summary>
        /// Gets the number of Midi channels supported.
        /// </summary>
        int ChannelCount { get; }
        /// <summary>
        /// Called to process the specified Midi <paramref name="events"/>.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        void Process(VstEventCollection events);
    }
}
