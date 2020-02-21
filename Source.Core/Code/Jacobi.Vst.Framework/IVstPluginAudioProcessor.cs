namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// A plugin implements this interface when it processes audio.
    /// </summary>
    /// <remarks>Effect plugins will use both input and output channels. 
    /// Instrument plugins will typically only use the output audio channels. 
    /// Sampler plugins typically use both input (for recording) and output (for playback) audio channels.</remarks>
    public interface IVstPluginAudioProcessor
    {
        /// <summary>
        /// Gets the number of input audio channels the plugin supports.
        /// </summary>
        int InputCount { get; }
        /// <summary>
        /// Gets the number of output audio channels the plugin supports.
        /// </summary>
        int OutputCount { get; }
        /// <summary>
        /// Gets the number of samples that the plugin will output when audio input has stopped.
        /// </summary>
        /// <remarks>A delay, for instance, can still produce audio output when audio input has stopped.</remarks>
        int TailSize { get; }
        /// <summary>
        /// Gets or sets the sample rate that is used by the host.
        /// </summary>
        /// <remarks>The host will call into the plugin to set the sample rate (that call ends up here).
        /// When the sample rate changes, the plugin should prepare its inner structures (pre-allocated 
        /// memory buffer etc.) to accomodate the new sample rate.</remarks>
        float SampleRate { get;set; }
        /// <summary>
        /// Gets or sets the number of samples (per channel) that will be passed to the <see cref="Process"/> method.
        /// </summary>
        /// <remarks>The host will call into the plugin to set the block size (that call ends up here).
        /// When the block size changes, the plugin should prepare its inner structures (pre-allocated 
        /// memory buffer etc.) to accomodate the new block size.</remarks>
        int BlockSize { get;set; }
        /// <summary>
        /// Called by the host repeatedly to allow the plugin to process the incoming audio and/or output altered audio.
        /// </summary>
        /// <param name="inChannels">An array with audio channels that contain the audio input.</param>
        /// <param name="outChannels">An array with audio channels to which the plugin can write its audio output.</param>
        /// <remarks>The size of the array of <paramref name="inChannels"/> and <paramref name="outChannels"/> is not 
        /// guarenteed to be equal to the <see cref="InputCount"/> and <see cref="OutputCount"/> property. 
        /// VST.NET just passes on what it receieves from the host.
        /// This method is called when the host calls processReplacing. 
        /// Refer to the VST SDK documentation for more information on processReplacing.</remarks>
        void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels);
        /// <summary>
        /// Informs the plugin of the pan algorithm to use.
        /// </summary>
        /// <param name="type">The pan algorithm type.</param>
        /// <param name="gain">A gain factor.</param>
        /// <returns>Returns true when the plugin support setting the pan law.</returns>
        bool SetPanLaw(VstPanLaw type, float gain);
    }

    /// <summary>
    /// This interface can be implemented by plugins that want to support double precision audio samples.
    /// </summary>
    /// <remarks>Note that this interface derives from <see cref="IVstPluginAudioProcessor"/> and that a
    /// plugin that supports double precision should also support 'normal' audio processing. 
    /// Not all hosts support double precision audio samples.</remarks>
    public interface IVstPluginAudioPrecisionProcessor : IVstPluginAudioProcessor
    {
        /// <summary>
        /// Called by the host repeatedly to allow the plugin to process the incoming audio and/or output altered audio.
        /// </summary>
        /// <param name="inChannels">An array with audio channels that contain the audio input.</param>
        /// <param name="outChannels">An array with audio channels to which the plugin can write its audio output.</param>
        /// <remarks>All remarks mentioned by <see cref="IVstPluginAudioProcessor.Process"/> also apply here.</remarks>
        void Process(VstAudioPrecisionBuffer[] inChannels, VstAudioPrecisionBuffer[] outChannels);
    }
}
