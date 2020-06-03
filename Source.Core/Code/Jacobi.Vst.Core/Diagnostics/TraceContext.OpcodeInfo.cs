namespace Jacobi.Vst.Core.Diagnostics
{
    using System;

    partial class TraceContext
    {
        /// <summary>
        /// A private class that contains information on a dispatcher opcode.
        /// </summary>
        private class OpcodeInfo
        {
            /// <summary>
            /// Constructs a new instance.
            /// </summary>
            /// <param name="description">The human readable description of the opcode.</param>
            /// <param name="legacy">An indication if the opcode is legacy.</param>
            public OpcodeInfo(string description, bool legacy)
            {
                Description = description;
                Legacy = legacy;
            }

            /// <summary>
            /// Gets the opcode description.
            /// </summary>
            public string Description { get; private set; }

            /// <summary>
            /// Gets an indication if the opcode is deprecated.
            /// </summary>
            public bool Legacy { get; private set; }

            /// <summary>
            /// Formats the dispatcher method parameters into a text string.
            /// </summary>
            /// <param name="index">Optional dispatcher parameter.</param>
            /// <param name="value">Optional dispatcher parameter.</param>
            /// <param name="ptr">Optional dispatcher parameter.</param>
            /// <param name="opt">Optional dispatcher parameter.</param>
            /// <returns>Returns the formatted text containing the optional dispatcher method parameters.</returns>
            public string FormatArguments(int index, IntPtr value, IntPtr ptr, float opt)
            {
                string result = Description;

                if (Legacy)
                {
                    result += " (legacy)";
                }

                result += String.Format(", Index={0}, Value={1} Ptr={2} Opt={3}.", index, value, ptr, opt);

                return result;
            }
        }

        // plugin dispatcher opcode definitions
        private static readonly OpcodeInfo[] _dispatchPlugin =
        {
            // VST 1.0
            new OpcodeInfo("effOpen", false),
            new OpcodeInfo("effClose", false),
            new OpcodeInfo("effSetProgram", false),
            new OpcodeInfo("effGetProgram", false),
            new OpcodeInfo("effSetProgramName", false),
            new OpcodeInfo("effGetProgramName", false),
            new OpcodeInfo("effGetParamLabel", false),
            new OpcodeInfo("effGetParamDisplay", false),
            new OpcodeInfo("effGetParamName", false),
            new OpcodeInfo("effGetVu", true),
            new OpcodeInfo("effSetSampleRate", false),
            new OpcodeInfo("effSetBlockSize", false),
            new OpcodeInfo("effMainsChanged", false),
            new OpcodeInfo("effEditGetRect", false),
            new OpcodeInfo("effEditOpen", false),
            new OpcodeInfo("effEditClose", false),
            new OpcodeInfo("effEditDraw", true),
            new OpcodeInfo("effEditMouse", true),
            new OpcodeInfo("effEditKey", true),
            new OpcodeInfo("effEditIdle", false),
            new OpcodeInfo("effEditTop", true),
            new OpcodeInfo("effEditSleep", true),
            new OpcodeInfo("effIdentify", true),
            new OpcodeInfo("effGetChunk", false),
            new OpcodeInfo("effSetChunk", false),
            // VST 2.0
            new OpcodeInfo("effProcessEvents", false),
            new OpcodeInfo("effCanBeAutomated", false),
            new OpcodeInfo("effString2Parameter", false),
            new OpcodeInfo("effGetNumProgramCategories", true),
            new OpcodeInfo("effGetProgramNameIndexed", false),
            new OpcodeInfo("effCopyProgram", true),
            new OpcodeInfo("effConnectInput", true),
            new OpcodeInfo("effConnectOutput", true),
            new OpcodeInfo("effGetInputProperties", false),
            new OpcodeInfo("effGetOutputProperties", false),
            new OpcodeInfo("effGetPlugCategory", false),
            new OpcodeInfo("effGetCurrentPosition", true),
            new OpcodeInfo("effGetDestinationBuffer", true),
            new OpcodeInfo("effOfflineNotify", false),
            new OpcodeInfo("effOfflinePrepare", false),
            new OpcodeInfo("effOfflineRun", false),
            new OpcodeInfo("effProcessVarIo", false),
            new OpcodeInfo("effSetSpeakerArrangement", false),
            new OpcodeInfo("effSetBlockSizeAndSampleRate", true),
            new OpcodeInfo("effSetBypass", false),
            new OpcodeInfo("effGetEffectName", false),
            new OpcodeInfo("effGetErrorText", true),
            new OpcodeInfo("effGetVendorString", false),
            new OpcodeInfo("effGetProductString", false),
            new OpcodeInfo("effGetVendorVersion", false),
            new OpcodeInfo("effVendorSpecific", false),
            new OpcodeInfo("effCanDo", false),
            new OpcodeInfo("effGetTailSize", false),
            new OpcodeInfo("effIdle", true),
            new OpcodeInfo("effGetIcon", true),
            new OpcodeInfo("effSetViewPosition", true),
            new OpcodeInfo("effGetParameterProperties", false),
            new OpcodeInfo("effKeysRequired", true),
            new OpcodeInfo("effGetVstVersion", false),
            // VST 2.1
            new OpcodeInfo("effEditKeyDown", false),
            new OpcodeInfo("effEditKeyUp", false),
            new OpcodeInfo("effSetEditKnobMode", false),
            new OpcodeInfo("effGetMidiProgramName", false),
            new OpcodeInfo("effGetCurrentMidiProgram", false),
            new OpcodeInfo("effGetMidiProgramCategory", false),
            new OpcodeInfo("effHasMidiProgramsChanged", false),
            new OpcodeInfo("effGetMidiKeyName", false),
            new OpcodeInfo("effBeginSetProgram", false),
            new OpcodeInfo("effEndSetProgram", false),
            // VST 2.3
            new OpcodeInfo("effGetSpeakerArrangement", false),
            new OpcodeInfo("effShellGetNextPlugin", false),
            new OpcodeInfo("effStartProcess", false),
            new OpcodeInfo("effStopProcess", false),
            new OpcodeInfo("effSetTotalSampleToProcess", false),
            new OpcodeInfo("effSetPanLaw", false),
            new OpcodeInfo("effBeginLoadBank", false),
            new OpcodeInfo("effBeginLoadProgram", false),
            // VST 2.4
            new OpcodeInfo("effSetProcessPrecision", false),
            new OpcodeInfo("effGetNumMidiInputChannels", false),
            new OpcodeInfo("effGetNumMidiOutputChannels", false),
        };

        // host dispatcher opcode definitions
        private static readonly OpcodeInfo[] _dispatchHost =
        {
            // VST 1.0
            new OpcodeInfo("audioMasterAutomate", false),
            new OpcodeInfo("audioMasterVersion", false),
            new OpcodeInfo("audioMasterCurrentId", false),
            new OpcodeInfo("audioMasterIdle", false),
            new OpcodeInfo("audioMasterPinConnected", true),
            // VST 2.0
            new OpcodeInfo("audioMasterWantMidi", true),
            new OpcodeInfo("audioMasterGetTime", false),
            new OpcodeInfo("audioMasterProcessEvents", false),
            new OpcodeInfo("audioMasterSetTime", true),
            new OpcodeInfo("audioMasterTempoAt", true),
            new OpcodeInfo("audioMasterGetNumAutomatableParameters", true),
            new OpcodeInfo("audioMasterGetParameterQuantization", true),
            new OpcodeInfo("audioMasterIOChanged", false),
            new OpcodeInfo("audioMasterNeedIdle", true),
            new OpcodeInfo("audioMasterSizeWindow", false),
            new OpcodeInfo("audioMasterGetSampleRate", false),
            new OpcodeInfo("audioMasterGetBlockSize", false),
            new OpcodeInfo("audioMasterGetInputLatency", false),
            new OpcodeInfo("audioMasterGetOutputLatency", false),
            new OpcodeInfo("audioMasterGetPreviousPlug", true),
            new OpcodeInfo("audioMasterGetNextPlug", true),
            new OpcodeInfo("audioMasterWillReplaceOrAccumulate", true),
            new OpcodeInfo("audioMasterGetCurrentProcessLevel", false),
            new OpcodeInfo("audioMasterGetAutomationState", false),
            new OpcodeInfo("audioMasterOfflineStart", false),
            new OpcodeInfo("audioMasterOfflineRead", false),
            new OpcodeInfo("audioMasterOfflineWrite", false),
            new OpcodeInfo("audioMasterOfflineGetCurrentPass", false),
            new OpcodeInfo("audioMasterOfflineGetCurrentMetaPass", false),
            new OpcodeInfo("audioMasterSetOutputSampleRate", true),
            new OpcodeInfo("audioMasterGetOutputSpeakerArrangement", true),
            new OpcodeInfo("audioMasterGetVendorString", false),
            new OpcodeInfo("audioMasterGetProductString", false),
            new OpcodeInfo("audioMasterGetVendorVersion", false),
            new OpcodeInfo("audioMasterVendorSpecific", false),
            new OpcodeInfo("audioMasterSetIcon", true),
            new OpcodeInfo("audioMasterCanDo", false),
            new OpcodeInfo("audioMasterGetLanguage", false),
            new OpcodeInfo("audioMasterOpenWindow", true),
            new OpcodeInfo("audioMasterCloseWindow", true),
            new OpcodeInfo("audioMasterGetDirectory", false),
            new OpcodeInfo("audioMasterUpdateDisplay", false),
            new OpcodeInfo("audioMasterBeginEdit", false),
            new OpcodeInfo("audioMasterEndEdit", false),
            new OpcodeInfo("audioMasterOpenFileSelector", false),
            new OpcodeInfo("audioMasterCloseFileSelector", false),
            new OpcodeInfo("audioMasterEditFile", true),
            new OpcodeInfo("audioMasterGetChunkFile", true),
            new OpcodeInfo("audioMasterGetInputSpeakerArrangement", true),
        };
    }
}
