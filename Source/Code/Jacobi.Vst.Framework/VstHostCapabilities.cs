namespace Jacobi.Vst.Framework
{
    using System;

    [Flags]
    public enum VstHostCapabilities
    {
        None = 0x0000,
        SendMidiEvents = 0x0001,
        ReceiveMidiEvents = 0x0002,
        RealtimeMidiFlag = 0x0004,
        ReportConnectionChanges = 0x0008,
        AcceptIoChanges = 0x0010,
        SizeWindow = 0x0020,
        Offline = 0x0040,
        OpenFileSelector = 0x0080,
        StartStopProcess = 0x0100,
        PluginHost = 0x0200,
        SendTimeInfo = 0x0400,
    }
}
