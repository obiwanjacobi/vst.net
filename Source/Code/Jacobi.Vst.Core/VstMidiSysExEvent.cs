namespace Jacobi.Vst.Core
{
    public class VstMidiSysExEvent : VstEvent
    {
        public VstMidiSysExEvent(int deltaFrames, int flags,
            byte[] sysexData)
            : base(VstEventTypes.MidiSysExEvent, deltaFrames, flags)
        {
            SysExData = sysexData;
        }

        public byte[] SysExData { get; private set; }
    }
}
