namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Framework;

    class MidiProcessor : IVstMidiProcessor
    {
        private Plugin _plugin;

        public MidiProcessor(Plugin plugin)
        {
            _plugin = plugin;
            Events = new VstEventCollection();
        }

        public VstEventCollection Events { get; private set; }

        #region IVstMidiProcessor Members

        public int ChannelCount
        {
            get { return _plugin.ChannelCount; }
        }

        public void Process(VstEventCollection events)
        {
            Events.AddRange(events);
        }

        #endregion
    }
}
