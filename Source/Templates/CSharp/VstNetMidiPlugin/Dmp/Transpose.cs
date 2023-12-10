using Jacobi.Vst.Core;
using System;

namespace VstNetMidiPlugin.Dmp;

/// <summary>
/// Change the Note Number of MIDI notes.
/// </summary>
internal sealed class Transpose
{
    private readonly TransposeParameters _parameters;

    public Transpose(TransposeParameters parameters)
    {
        _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
    }

    public VstMidiEvent ProcessEvent(VstMidiEvent inEvent)
    {
        if (!MidiHelper.IsNoteOff(inEvent.Data) && !MidiHelper.IsNoteOn(inEvent.Data))
        {
            return inEvent;
        }

        byte[] outData = new byte[4];
        inEvent.Data.CopyTo(outData, 0);

        outData[1] += (byte)_parameters.TransposeMgr.CurrentValue;

        if (outData[1] > 127)
        {
            outData[1] = 127;
        }

        if (outData[1] < 0)
        {
            outData[1] = 0;
        }

        // MidiEvents are immutable, 
        // so we create a new object for the new data.
        return new VstMidiEvent(
            inEvent.DeltaFrames, inEvent.NoteLength, inEvent.NoteOffset,
            outData, inEvent.Detune, inEvent.NoteOffVelocity);
    }
}
