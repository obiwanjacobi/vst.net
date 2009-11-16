namespace Jacobi.Vst.Core.Deprecated
{
    using System;

    /// <summary>
    /// The VstGenericEvent represents an event of one of the deprecated event types.
    /// </summary>
    public class VstGenericEvent : VstEvent
    {
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
