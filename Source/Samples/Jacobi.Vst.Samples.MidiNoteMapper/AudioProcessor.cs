namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Framework;

    class AudioProcessor : IVstPluginAudioProcessor
    {
        private Plugin _plugin;
        private MidiProcessor _midiProcessor;
        private IVstMidiProcessor _hostProcessor;

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

        public void Process(VstAudioChannel[] inChannels, VstAudioChannel[] outChannels)
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

        #endregion
    }
}
