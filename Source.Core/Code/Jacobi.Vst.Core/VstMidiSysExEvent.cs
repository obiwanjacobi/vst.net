namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Represents an immutable Midi System Exclusive event.
    /// </summary>
    public class VstMidiSysExEvent : VstEvent
    {
        /// <summary>
        /// Constructs a new immutable instance.
        /// </summary>
        /// <param name="deltaFrames">The number of frame from the start of the current cycle.</param>
        /// <param name="sysexData">The raw system exclusive data.</param>
        public VstMidiSysExEvent(int deltaFrames, byte[] sysexData)
            : base(VstEventTypes.MidiSysExEvent, deltaFrames, sysexData)
        {
            Throw.IfArgumentIsNull(sysexData, nameof(sysexData));
        }
    }
}
