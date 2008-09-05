namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Represent an immutable midi event.
    /// </summary>
    /// <remarks>This Midi event does not represent a System Exclusive midi message. 
    /// Refer to <see cref="VstMidiSysExEvent"/> for Sys.Ex. events.</remarks>
    public class VstMidiEvent : VstEvent
    {
        /// <summary>
        /// Constructs a new immutable instance.
        /// </summary>
        /// <param name="deltaFrames">The number of frame from the start of the current cycle.</param>
        /// <param name="noteLength">The length of the note (when the event is a midi note event).</param>
        /// <param name="noteOffset">The offset of the note.</param>
        /// <param name="midiData">The additional midi event data.</param>
        /// <param name="detune">A detune value.</param>
        /// <param name="noteOffVelocity">Velocity for when the note is done.</param>
        public VstMidiEvent(int deltaFrames,
            int noteLength, int noteOffset, byte[] midiData, sbyte detune, byte noteOffVelocity)
            : base(VstEventTypes.MidiEvent, deltaFrames)
        {
            NoteLength = noteLength;
            NoteOffset = noteOffset;
            MidiData = midiData;
            Detune = detune;
            NoteOffVelocity = noteOffVelocity;
        }

        /// <summary>
        /// Gets the length of the note.
        /// </summary>
        public int NoteLength { get; private set; }
        
        /// <summary>
        /// Gets the offset of the note.
        /// </summary>
        public int NoteOffset { get; private set; }
        
        /// <summary>
        /// Gets the addition midi event data.
        /// </summary>
        public byte[] MidiData { get; private set; }
        
        /// <summary>
        /// Gets the detune value.
        /// </summary>
        public sbyte Detune { get; private set; }
        
        /// <summary>
        /// Gets the velocity when the note was released.
        /// </summary>
        public byte NoteOffVelocity { get; private set; }
    }
}
