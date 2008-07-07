namespace Jacobi.Vst.Core
{
    public abstract class VstEvent
    {
        protected VstEvent(VstEventTypes eventType, int deltaFrames, int flags)
        {
            EventType = eventType;
            DeltaFrames = deltaFrames;
            Flags = flags;
        }

        public VstEventTypes EventType { get; private set; }
        public int DeltaFrames { get; private set; }
        public int Flags { get; private set; }
    }

    public enum VstEventTypes
    {
        Unknown = 0,
        MidiEvent = 1,
        MidiSysExEvent = 6
    }
}
