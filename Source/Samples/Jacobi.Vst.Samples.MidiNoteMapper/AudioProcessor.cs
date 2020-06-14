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
        private readonly Plugin _plugin;
        private readonly MidiProcessor _midiProcessor;
        private IVstMidiProcessor _hostProcessor;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(Plugin plugin)
            : base(0, 0, 0, noSoundInStop: true)
        {
            _plugin = plugin;

            _midiProcessor = plugin.GetInstance<MidiProcessor>();
        }

        /// <inheritdoc />
        /// <remarks>This method is used to push midi events to the host.</remarks>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (_hostProcessor == null)
            {
                _hostProcessor = _plugin.Host.GetInstance<IVstMidiProcessor>();
            }

            if (_midiProcessor != null && _hostProcessor != null &&
                _midiProcessor.Events.Count > 0)
            {
                _hostProcessor.Process(_midiProcessor.Events);
                _midiProcessor.Events.Clear();
            }

            // perform audio-through
            base.Process(inChannels, outChannels);
        }
    }
}
