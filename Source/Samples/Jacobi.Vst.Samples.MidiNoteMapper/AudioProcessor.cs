namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework;
    using Jacobi.Vst.Plugin.Framework.Plugin;

    /// <summary>
    /// A dummy audio processor only used for the timing of midi processing.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessor
    {
        private MidiProcessor? _midiProcessor;
        private IVstMidiProcessor? _hostProcessor;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(IVstPluginEvents pluginEvents)
            : base(0, 0, 0, noSoundInStop: true)
        {
            pluginEvents.Opened += Plugin_Opened;
        }

        private void Plugin_Opened(object? sender, System.EventArgs e)
        {
            var plugin = (VstPlugin?)sender;
            _midiProcessor = plugin?.GetInstance<MidiProcessor>();
            _hostProcessor = plugin?.Host?.GetInstance<IVstMidiProcessor>();
        }

        /// <inheritdoc />
        /// <remarks>This method is used to push midi events to the host.</remarks>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (_hostProcessor != null &&
                _midiProcessor != null &&
                _midiProcessor.Events.Count > 0)
            {
                // we use the AudioProcessor as a trigger to process Midi
                _hostProcessor.Process(_midiProcessor.Events);
                _midiProcessor.Events.Clear();
            }

            // perform audio-through
            base.Process(inChannels, outChannels);
        }
    }
}
