namespace VstHostCanDos
{
	const char* canDoSendVstEvents = "sendVstEvents"; ///< Host supports send of Vst events to plug-in
	const char* canDoSendVstMidiEvent = "sendVstMidiEvent"; ///< Host supports send of MIDI events to plug-in
	const char* canDoSendVstTimeInfo = "sendVstTimeInfo"; ///< Host supports send of VstTimeInfo to plug-in
	const char* canDoReceiveVstEvents = "receiveVstEvents"; ///< Host can receive Vst events from plug-in
	const char* canDoReceiveVstMidiEvent = "receiveVstMidiEvent"; ///< Host can receive MIDI events from plug-in 
	const char* canDoReportConnectionChanges = "reportConnectionChanges"; ///< Host will indicates the plug-in when something change in plug-in´s routing/connections with #suspend/#resume/#setSpeakerArrangement 
	const char* canDoAcceptIOChanges = "acceptIOChanges"; ///< Host supports #ioChanged ()
	const char* canDoSizeWindow = "sizeWindow"; ///< used by VSTGUI
	const char* canDoOffline = "offline"; ///< Host supports offline feature
	const char* canDoOpenFileSelector = "openFileSelector"; ///< Host supports function #openFileSelector ()
	const char* canDoCloseFileSelector = "closeFileSelector"; ///< Host supports function #closeFileSelector ()
	const char* canDoStartStopProcess = "startStopProcess"; ///< Host supports functions #startProcess () and #stopProcess ()
	const char* canDoShellCategory = "shellCategory"; ///< 'shell' handling via uniqueID. If supported by the Host and the Plug-in has the category #kPlugCategShell
	const char* canDoSendVstMidiEventFlagIsRealtime = "sendVstMidiEventFlagIsRealtime"; ///< Host supports flags for #VstMidiEvent
}

namespace VstPluginCanDos
{
	const char* canDoSendVstEvents = "sendVstEvents"; ///< plug-in will send Vst events to Host
	const char* canDoSendVstMidiEvent = "sendVstMidiEvent"; ///< plug-in will send MIDI events to Host
	const char* canDoReceiveVstEvents = "receiveVstEvents"; ///< plug-in can receive MIDI events from Host
	const char* canDoReceiveVstMidiEvent = "receiveVstMidiEvent"; ///< plug-in can receive MIDI events from Host 
	const char* canDoReceiveVstTimeInfo = "receiveVstTimeInfo"; ///< plug-in can receive Time info from Host 
	const char* canDoOffline = "offline"; ///< plug-in supports offline functions (#offlineNotify, #offlinePrepare, #offlineRun)
	const char* canDoMidiProgramNames = "midiProgramNames"; ///< plug-in supports function #getMidiProgramName ()
	const char* canDoBypass = "bypass"; ///< plug-in supports function #setBypass ()
}
