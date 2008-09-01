namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
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

        public bool MidiThru { get; set; }

        #region IVstMidiProcessor Members

        public int ChannelCount
        {
            get { return _plugin.ChannelCount; }
        }

        public void Process(VstEventCollection events)
        {
            foreach (VstEvent evnt in events)
            {
                if (evnt.EventType != VstEventTypes.MidiEvent) continue;

                VstMidiEvent midiEvent = (VstMidiEvent)evnt;
                VstMidiEvent mappedEvent = null;

                if ( ((midiEvent.MidiData[0] & 0xF0) == 0x80 || (midiEvent.MidiData[0] & 0xF0) == 0x90) &&
                    _plugin.NoteMap.Contains(midiEvent.MidiData[1]))
                {
                    byte[] midiData = new byte[4];
                    midiData[0] = midiEvent.MidiData[0];
                    midiData[1] = _plugin.NoteMap[midiEvent.MidiData[1]].OutputNoteNumber;
                    midiData[2] = midiEvent.MidiData[2];

                    mappedEvent = new VstMidiEvent(midiEvent.DeltaFrames, 
                        midiEvent.NoteLength, 
                        midiEvent.NoteOffset, 
                        midiData, 
                        midiEvent.Detune, 
                        midiEvent.NoteOffVelocity);

                    Events.Add(mappedEvent);
                }
            }
        }

        #endregion
    }
}
