namespace Jacobi.Vst.Core
{
    public class VstMidiEvent : VstEvent
    {
        public VstMidiEvent(int deltaFrames, int flags,
            int noteLength, int noteOffset, byte[] midiData, char detune, byte noteOffVelocity)
            : base(VstEventTypes.MidiEvent, deltaFrames, flags)
        {
            NoteLength = noteLength;
            NoteOffset = noteOffset;
            MidiData = midiData;
            Detune = detune;
            NoteOffVelocity = noteOffVelocity;
        }

        public int NoteLength { get; private set; }
        public int NoteOffset { get; private set; }
        public byte[] MidiData { get; private set; }
        public char Detune { get; private set; }
        public byte NoteOffVelocity { get; private set; }
    }
}
