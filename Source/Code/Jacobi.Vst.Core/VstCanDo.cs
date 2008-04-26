namespace Jacobi.Vst.Core
{
    public enum VstHostCanDo
    {
        None,
        SendVstEvents,
        SendVstMidiEvent,
        SendVstTimeInfo,
        ReceiveVstEvents,
        ReceiveVstMidiEvent,
        ReportConnectionChanges,
        AcceptIOChanges,
        SizeWindow,
        Offline,
        OpenFileSelector,
        CloseFileSelector,
        StartStopProcess,
        ShellCategory,
        SendVstMidiEventFlagIsRealtime,
    }

    public enum VstPluginCanDo
    {
        None,
        SendVstEvents,
        SendVstMidiEvent,
        ReceiveVstEvents,
        ReceiveVstMidiEvent,
        ReceiveVstTimeInfo,
        Offline,
        MidiProgramNames,
        Bypass,
    }
}
