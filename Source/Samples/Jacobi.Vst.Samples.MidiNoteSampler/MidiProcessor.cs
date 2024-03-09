namespace Jacobi.Vst.Samples.MidiNoteSampler;

using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using System;

/// <summary>
/// Manages incoming midi events and sents them to the <see cref="SampleManager"/>.
/// </summary>
internal sealed class MidiProcessor : IVstMidiProcessor
{
    private readonly SampleManager _sampleManager;

    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    /// <param name="plugin"></param>
    public MidiProcessor(SampleManager sampleManager)
    {
        _sampleManager = sampleManager ?? throw new ArgumentNullException(nameof(sampleManager));
    }

    #region IVstMidiProcessor Members

    /// <summary>
    /// Always returns 16.
    /// </summary>
    public int ChannelCount
    {
        get { return 16; }
    }

    /// <summary>
    /// Handles and filters the incoming midi events.
    /// </summary>
    /// <param name="events">The midi events for the current cycle.</param>
    public void Process(VstEventCollection events)
    {
        foreach (VstEvent evnt in events)
        {
            if (evnt.EventType == VstEventTypes.MidiEvent)
            {
                var midiEvent = (VstMidiEvent)evnt;

                // pass note on and note off to the sample manager
                if ((midiEvent.Data[0] & 0xF0) == 0x80)
                {
                    _sampleManager.ProcessNoteOffEvent(midiEvent.Data[1]);
                }

                if ((midiEvent.Data[0] & 0xF0) == 0x90)
                {
                    // note on with velocity = 0 is a note off
                    if (midiEvent.Data[2] == 0)
                    {
                        _sampleManager.ProcessNoteOffEvent(midiEvent.Data[1]);
                    }
                    else
                    {
                        _sampleManager.ProcessNoteOnEvent(midiEvent.Data[1]);
                    }
                }
            }
        }
    }

    #endregion
}
