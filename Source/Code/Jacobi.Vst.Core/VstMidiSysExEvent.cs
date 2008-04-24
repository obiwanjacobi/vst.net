namespace Jacobi.Vst.Core
{
    public class VstMidiSysExEvent : VstEvent
    {
        public VstMidiSysExEvent(VstEventTypes eventType, int deltaFrames, int flags,
            byte[] sysexData)
            : base(eventType, deltaFrames, flags)
        {
            SysExData = sysexData;
        }

        public byte[] SysExData;
    }
}
