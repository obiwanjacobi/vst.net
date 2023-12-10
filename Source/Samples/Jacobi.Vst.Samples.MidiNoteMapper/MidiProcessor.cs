﻿namespace Jacobi.Vst.Samples.MidiNoteMapper;

using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Implements the incoming Midi event handling for the plugin.
/// </summary>
internal sealed class MidiProcessor : IVstMidiProcessor, IVstPluginMidiSource
{
    private readonly MapNoteItemList _noteMap;

    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    /// <param name="plugin">Must not be null.</param>
    public MidiProcessor(MapNoteItemList noteMap)
    {
        _noteMap = noteMap ?? throw new ArgumentNullException(nameof(noteMap));
    }

    /// <summary>
    /// Gets the midi events that should be processed in the current cycle.
    /// </summary>
    public VstEventCollection Events { get; } = new VstEventCollection();

    /// <summary>
    /// Gets or sets a value indicating wether non-mapped midi events should be passed to the output.
    /// </summary>
    public bool MidiThru { get; set; }

    /// <summary>
    /// The raw note on note numbers.
    /// </summary>
    public Queue<byte> NoteOnEvents { get; } = new Queue<byte>();

    #region IVstMidiProcessor Members

    public int ChannelCount
    {
        get { return 16; }
    }

    public void Process(VstEventCollection events)
    {
        foreach (VstEvent evnt in events)
        {
            if (evnt.EventType != VstEventTypes.MidiEvent)
                continue;

            var midiEvent = (VstMidiEvent)evnt;

            if (((midiEvent.Data[0] & 0xF0) == 0x80 || (midiEvent.Data[0] & 0xF0) == 0x90))
            {
                if (_noteMap.Contains(midiEvent.Data[1]))
                {
                    byte[] midiData = new byte[4];
                    midiData[0] = midiEvent.Data[0];
                    midiData[1] = _noteMap[midiEvent.Data[1]].OutputNoteNumber;
                    midiData[2] = midiEvent.Data[2];

                    var mappedEvent = new VstMidiEvent(midiEvent.DeltaFrames,
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
                    // add original note event
                    Events.Add(evnt);
                }
            }
            else if (MidiThru)
            {
                // add original (non-note) event
                Events.Add(evnt);
            }
        }
    }

    #endregion
}
