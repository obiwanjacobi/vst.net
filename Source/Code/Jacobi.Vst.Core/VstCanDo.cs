namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Host capabilities.
    /// </summary>
    public enum VstHostCanDo
    {
        /// <summary>Null value.</summary>
        None,
        /// <summary>Host supports send of Vst events to plug-in.</summary>
        SendVstEvents,
        /// <summary>Host supports send of MIDI events to plug-in.</summary>
        SendVstMidiEvent,
        /// <summary>Host supports send of VstTimeInfo to plug-in.</summary>
        SendVstTimeInfo,
        /// <summary>Host can receive Vst events from plug-in.</summary>
        ReceiveVstEvents,
        /// <summary>Host can receive MIDI events from plug-in .</summary>
        ReceiveVstMidiEvent,
        /// <summary>Host will indicates the plug-in when something changes in plug-in´s routing/connections with #suspend/#resume/#setSpeakerArrangement.</summary>
        ReportConnectionChanges,
        /// <summary>Host supports <see cref="IVstHostCommands20.IoChanged"/>.</summary>
        AcceptIOChanges,
        /// <summary></summary>
        SizeWindow,
        /// <summary>Host supports offline feature.</summary>
        Offline,
        /// <summary>Host supports function #openFileSelector().</summary>
        OpenFileSelector,
        /// <summary>Host supports function #closeFileSelector().</summary>
        CloseFileSelector,
        /// <summary>Host supports functions #startProcess() and #stopProcess().</summary>
        StartStopProcess,
        /// <summary>'shell' handling via uniqueID. If supported by the Host and the Plug-in has the category #kPlugCategShell.</summary>
        ShellCategory,
        /// <summary>Host supports flags for <see cref="VstMidiEvent"/>.</summary>
        SendVstMidiEventFlagIsRealtime,
    }

    /// <summary>
    /// Plugin capabilities.
    /// </summary>
    public enum VstPluginCanDo
    {
        /// <summary>Null value.</summary>
        Unknown,
        /// <summary>plug-in will send Vst events to Host.</summary>
        SendVstEvents,
        /// <summary>plug-in will send MIDI events to Host.</summary>
        SendVstMidiEvent,
        /// <summary>plug-in can receive MIDI events from Host.</summary>
        ReceiveVstEvents,
        /// <summary>plug-in can receive MIDI events from Host.</summary>
        ReceiveVstMidiEvent,
        /// <summary>plug-in can receive Time info from Host.</summary>
        ReceiveVstTimeInfo,
        /// <summary>plug-in supports offline functions (#offlineNotify, #offlinePrepare, #offlineRun).</summary>
        Offline,
        /// <summary>plug-in supports function #getMidiProgramName().</summary>
        MidiProgramNames,
        /// <summary>plug-in supports function #setBypass().</summary>
        Bypass,
    }
}
