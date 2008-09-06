namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    /// <summary>
    /// A dummy audio processor only used for the timing of midi processing.
    /// </summary>
    class AudioProcessor : IVstPluginAudioProcessor
    {
        private Plugin _plugin;
        private MidiProcessor _midiProcessor;
        private IVstMidiProcessor _hostProcessor;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(Plugin plugin)
        {
            _plugin = plugin;
            
            _midiProcessor = plugin.GetInstance<MidiProcessor>();
        }

        #region IVstPluginAudioProcessor Members

        public int BlockSize { get; set; }

        public int InputCount
        {
            get { return 0; }
        }

        public int OutputCount
        {
            get { return 0; }
        }

        public void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
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
        }

        public double SampleRate { get; set; }

        public int TailSize
        {
            get { return 0; }
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }
        #endregion
    }
}
