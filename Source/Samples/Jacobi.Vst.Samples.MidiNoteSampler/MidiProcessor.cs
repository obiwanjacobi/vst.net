namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    internal class MidiProcessor : IVstMidiProcessor
    {
        private Plugin _plugin;

        public MidiProcessor(Plugin plugin)
        {
            _plugin = plugin;
        }

        #region IVstMidiProcessor Members

        public int ChannelCount
        {
            get { return 16; }
        }

        public void Process(VstEventCollection events)
        {
            foreach (VstEvent evnt in events)
            {
                if (evnt.EventType == VstEventTypes.MidiEvent)
                {
                    VstMidiEvent midiEvent = (VstMidiEvent)evnt;

                    System.Diagnostics.Debug.WriteLine("Receiving Midi Event:" + midiEvent.MidiData[0], "VST.NET");

                    // pass note on and note off to the sample manager

                    if ((midiEvent.MidiData[0] & 0xF0) == 0x80)
                    {
                        _plugin.SampleManager.ProcessNoteOffEvent(midiEvent.MidiData[1]);
                    }

                    if ((midiEvent.MidiData[0] & 0xF0) == 0x90)
                    {
                        // note on with velocity = 0 is a note off
                        if (midiEvent.MidiData[2] == 0)
                        {
                            _plugin.SampleManager.ProcessNoteOffEvent(midiEvent.MidiData[1]);
                        }
                        else
                        {
                            _plugin.SampleManager.ProcessNoteOnEvent(midiEvent.MidiData[1]);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
