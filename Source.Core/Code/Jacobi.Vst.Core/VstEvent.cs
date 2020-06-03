namespace Jacobi.Vst.Core
{
    using System;

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
        /// <param name="data">A byte buffer of event data.</param>
        protected VstEvent(VstEventTypes eventType, int deltaFrames, byte[] data)
        {
            if (eventType == VstEventTypes.Unknown)
            {
                throw new ArgumentException(Properties.Resources.VstEvent_InvalidEventType, nameof(eventType));
            }

            EventType = eventType;
            DeltaFrames = deltaFrames;
            Data = data;
        }

        /// <summary>
        /// Gets the event type.
        /// </summary>
        public VstEventTypes EventType { get; private set; }
        /// <summary>
        /// Gets the number of frames.
        /// </summary>
        public int DeltaFrames { get; private set; }

        /// <summary>
        /// Gets the event data.
        /// </summary>
        public byte[] Data { get; private set; }
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
        /// <summary>Audio event (legacy).</summary>
        LegacyAudioEvent,
        /// <summary>Video event (legacy).</summary>
        LegacyVideoEvent,
        /// <summary>Parameter event (legacy).</summary>
        LegacyParameterEvent,
        /// <summary>Trigger event (legacy).</summary>
        LegacyTriggerEvent,
        /// <summary>Midi System Exclusive event.</summary>
        MidiSysExEvent
    }
}
