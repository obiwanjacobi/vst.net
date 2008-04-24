namespace Jacobi.Vst.Core
{
    public class VstMidiEvent : VstEvent
    {
        public VstMidiEvent(VstEventTypes eventType, int deltaFrames, int flags,
            int noteLength, int noteOffset, byte[] midiData, char detune, byte noteOffVelocity)
            : base(eventType, deltaFrames, flags)
        {
            NoteLength = noteLength;
            NoteOffset = noteOffset;
            MidiData = midiData;
            Detune = detune;
            NoteOffVelocity = noteOffVelocity;
        }

        public int NoteLength;
        public int NoteOffset;
        public byte[] MidiData;
        public char Detune;
        public byte NoteOffVelocity;
    }
}
