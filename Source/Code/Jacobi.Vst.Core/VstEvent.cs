namespace Jacobi.Vst.Core
{
    /// <summary>
    /// The VstEvent represents a base class common to both 
    /// <see cref="VstMidiEvent"/> and <see cref="VstMidiSysExEvent"/> classes.
    /// </summary>
    public abstract class VstEvent
    {
        /// <summary>
        /// For derived classes only.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="deltaFrames">The start of this event in the number of delta frames from the current cycle.</param>
        protected VstEvent(VstEventTypes eventType, int deltaFrames)
        {
            EventType = eventType;
            DeltaFrames = deltaFrames;
        }

        /// <summary>
        /// Gets the event type.
        /// </summary>
        public VstEventTypes EventType { get; private set; }
        /// <summary>
        /// Gets the number of frames.
        /// </summary>
        public int DeltaFrames { get; private set; }
    }

    /// <summary>
    /// The type of event.
    /// </summary>
    public enum VstEventTypes
    {
        /// <summary>Null value.</summary>
        Unknown = 0,
        /// <summary>Midi event.</summary>
        MidiEvent = 1,
        /// <summary>Midi System Exclusive event.</summary>
        MidiSysExEvent = 6
    }
}
