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

        public VstEventTypes EventType;
        //public int ByteSize;
        public int DeltaFrames;
        public int Flags;
    }

    public enum VstEventTypes
    {
        Unknown = 0,
        MidiEvent = 1,
        MidiSysExEvent = 6
    }
}
