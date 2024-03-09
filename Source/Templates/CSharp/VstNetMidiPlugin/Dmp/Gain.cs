using Jacobi.Vst.Core;
using System;

namespace VstNetMidiPlugin.Dmp;

/// <summary>
/// Change the velocity of MIDI notes.
/// </summary>
internal sealed class Gain
{
    private readonly GainParameters _parameters;

    public Gain(GainParameters parameters)
    {
        _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
    }

    public VstMidiEvent ProcessEvent(VstMidiEvent inEvent)
    {
        if (!MidiHelper.IsNoteOff(inEvent.Data) &&
            !MidiHelper.IsNoteOn(inEvent.Data))
        {
            return inEvent;
        }

        byte[] outData = new byte[4];
        inEvent.Data.CopyTo(outData, 0);

        outData[2] += (byte)_parameters.GainMgr.CurrentValue;

        if (outData[2] > 127)
        {
            outData[2] = 127;
        }

        if (outData[2] < 0)
        {
            outData[2] = 0;
        }

        // MidiEvents are immutable, 
        // so we create a new object for the new data.
        VstMidiEvent outEvent = new VstMidiEvent(
            inEvent.DeltaFrames, inEvent.NoteLength, inEvent.NoteOffset,
            outData, inEvent.Detune, inEvent.NoteOffVelocity);

        return outEvent;
    }
}
