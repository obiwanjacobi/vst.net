﻿namespace Jacobi.Vst.Core.Deprecated
{
    using System;

    /// <summary>
    /// The VstGenericEvent represents an event of one of the deprecated event types.
    /// </summary>
    public class VstGenericEvent : VstEvent
    {
        /// <summary>
        /// Constructs a new immutable instance.
        /// </summary>
        /// <param name="eventType">The type of event. Cannot be <see cref="VstEventTypes.MidiEvent"/> or <see cref="VstEventTypes.MidiSysExEvent"/>.</param>
        /// <param name="deltaFrames">The start of this event in the number of delta frames from the current cycle.</param>
        /// <param name="data">The associated data for this event.</param>
        public VstGenericEvent(VstEventTypes eventType, int deltaFrames, byte[] data)
            : base(eventType, deltaFrames)
        {
            if (eventType == VstEventTypes.MidiEvent || 
                eventType == VstEventTypes.MidiSysExEvent)
            {
                throw new ArgumentException(Properties.Resources.VstGenericEvent_InvalidEventType, "eventType");
            }

            Data = data;
        }

        /// <summary>
        /// Gets the generic data.
        /// </summary>
        public byte[] Data { get; private set; }
    }
}
