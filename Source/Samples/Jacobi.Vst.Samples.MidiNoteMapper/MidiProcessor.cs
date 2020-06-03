namespace Jacobi.Vst.Samples.MidiNoteMapper
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework;

    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Implements the incoming Midi event handling for the plugin.
    /// </summary>
    internal sealed class MidiProcessor : IVstMidiProcessor
    {
        private readonly Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public MidiProcessor(Plugin plugin)
        {
            _plugin = plugin;
            Events = new VstEventCollection();
            NoteOnEvents = new Queue<byte>();
        }

        /// <summary>
        /// Gets the midi events that should be processed in the current cycle.
        /// </summary>
        public VstEventCollection Events { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating wether non-mapped midi events should be passed to the output.
        /// </summary>
        public bool MidiThru { get; set; }

        /// <summary>
        /// The raw note on note numbers.
        /// </summary>
        public Queue<byte> NoteOnEvents { get; private set; }

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

                if (((midiEvent.Data[0] & 0xF0) == 0x80 || (midiEvent.Data[0] & 0xF0) == 0x90))
                {
                    if (_plugin.NoteMap.Contains(midiEvent.Data[1]))
                    {
                        byte[] midiData = new byte[4];
                        midiData[0] = midiEvent.Data[0];
                        midiData[1] = _plugin.NoteMap[midiEvent.Data[1]].OutputNoteNumber;
                        midiData[2] = midiEvent.Data[2];

                        mappedEvent = new VstMidiEvent(midiEvent.DeltaFrames,
                            midiEvent.NoteLength,
                            midiEvent.NoteOffset,
                            midiData,
                            midiEvent.Detune,
                            midiEvent.NoteOffVelocity);

                        Events.Add(mappedEvent);

                        // add raw note-on note numbers to the queue
                        if ((midiEvent.Data[0] & 0xF0) == 0x90)
                        {
                            lock (((ICollection)NoteOnEvents).SyncRoot)
                            {
                                NoteOnEvents.Enqueue(midiEvent.Data[1]);
                            }
                        }
                    }
                    else if (MidiThru)
                    {
                        // add original event
                        Events.Add(evnt);
                    }
                }
            }
        }

        #endregion
    }
}
