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
        /// <param name="detune">A detune value. Must lie within [-64,63] range.</param>
        /// <param name="noteOffVelocity">Velocity for when the note is done.</param>
        public VstMidiEvent(int deltaFrames,
            int noteLength, int noteOffset, byte[] midiData, short detune, byte noteOffVelocity)
            : this(deltaFrames, noteLength, noteOffset, midiData, detune, noteOffVelocity, false)
        { }

        /// <summary>
        /// Constructs a new immutable instance.
        /// </summary>
        /// <param name="deltaFrames">The number of frame from the start of the current cycle.</param>
        /// <param name="noteLength">The length of the note (when the event is a midi note event).</param>
        /// <param name="noteOffset">The offset of the note.</param>
        /// <param name="midiData">The additional midi event data.</param>
        /// <param name="detune">A detune value. Should lie within [-64,63] range (not checked).</param>
        /// <param name="noteOffVelocity">Velocity for when the note is done.</param>
        /// <param name="isRealtime">True if the Midi Event was received in real time.</param>
        public VstMidiEvent(int deltaFrames,
            int noteLength, int noteOffset, byte[] midiData, short detune, byte noteOffVelocity, bool isRealtime)
            : base(VstEventTypes.MidiEvent, deltaFrames, midiData)
        {
            Throw.IfArgumentIsNull(midiData, nameof(midiData));
            //Throw.IfArgumentNotInRange<short>(detune, -64, 63, "detune"); // caused problems in Sonar X2. Issue: 10054

            NoteLength = noteLength;
            NoteOffset = noteOffset;
            Detune = detune;
            NoteOffVelocity = noteOffVelocity;
            IsRealtime = isRealtime;
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
        /// Gets the detune value.
        /// </summary>
        public short Detune { get; private set; }

        /// <summary>
        /// Gets the velocity when the note was released.
        /// </summary>
        public byte NoteOffVelocity { get; private set; }

        /// <summary>
        /// Gets an indication if this midi event was played live, not played back from a track.
        /// </summary>
        public bool IsRealtime { get; private set; }
    }
}