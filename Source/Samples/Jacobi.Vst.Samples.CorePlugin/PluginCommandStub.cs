namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;

    /// <summary>
    /// Custom implementation for the Plugin command stub.
    /// </summary>
    /// <remarks>Most methods just log to the UI.</remarks>
    public class PluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginInfo _pluginInfo;
        private IVstHostCommandStub _hostStub;
        
        private WinFormsWrapper<EditorControl> _editorCtrl = new WinFormsWrapper<EditorControl>();

        #region IVstPluginCommandStub Members

        /// <inheritdoc />
        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            _hostStub = hostCmdStub;
            _pluginInfo = new VstPluginInfo();

            _pluginInfo.AudioInputCount = 1;
            _pluginInfo.AudioOutputCount = 2;
            _pluginInfo.ProgramCount = 1;
            _pluginInfo.Flags = VstPluginFlags.HasEditor | VstPluginFlags.CanReplacing;
            _pluginInfo.PluginID = 1234;
            _pluginInfo.PluginVersion = 1000;

            return _pluginInfo;
        }

        #endregion

        #region IVstPluginCommands24 Members

        /// <inheritdoc />
        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            _editorCtrl.Instance.AddLine("SetProcessPrecision:" + precision);
            return false;
        }

        /// <inheritdoc />
        public int GetNumberOfMidiInputChannels()
        {
            _editorCtrl.Instance.AddLine("GetNumberOfMidiInputChannels");
            return 0;
        }

        /// <inheritdoc />
        public int GetNumberOfMidiOutputChannels()
        {
            _editorCtrl.Instance.AddLine("GetNumberOfMidiOutputChannels");
            return 0;
        }

        #endregion

        #region IVstPluginCommands23 Members

        /// <inheritdoc />
        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            _editorCtrl.Instance.AddLine("GetSpeakerArrangement");
            input = null;
            output = null;

            return false;
        }

        /// <inheritdoc />
        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            _editorCtrl.Instance.AddLine("SetTotalSamplesToProcess: " + numberOfSamples);
            return 0;
        }

        /// <inheritdoc />
        public int GetNextPlugin(out string name)
        {
            _editorCtrl.Instance.AddLine("GetNextPlugin");
            name = null;
            return 0;
        }

        /// <inheritdoc />
        public int StartProcess()
        {
            _editorCtrl.Instance.AddLine("StartProcess");
            return 0;
        }

        /// <inheritdoc />
        public int StopProcess()
        {
            _editorCtrl.Instance.AddLine("StopProcess");
            return 0;
        }

        /// <inheritdoc />
        public bool SetPanLaw(VstPanLaw type, float value)
        {
            _editorCtrl.Instance.AddLine("SetPanLaw: " + type);
            return false;
        }

        /// <inheritdoc />
        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            _editorCtrl.Instance.AddLine("BeginLoadBank");
            return VstCanDoResult.Unknown;
        }
        
        /// <inheritdoc />
        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            _editorCtrl.Instance.AddLine("BeginLoadProgram");
            return VstCanDoResult.Unknown;
        }

        #endregion

        #region IVstPluginCommands21 Members

        /// <inheritdoc />
        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            _editorCtrl.Instance.AddLine("EditorKeyDown: " + ascii);
            return false;
        }

        /// <inheritdoc />
        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            _editorCtrl.Instance.AddLine("EditorKeyUp: " + ascii);
            return false;
        }

        /// <inheritdoc />
        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            _editorCtrl.Instance.AddLine("SetEditorKnobMode: " + mode);
            return false;
        }

        /// <inheritdoc />
        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiProgramName: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            _editorCtrl.Instance.AddLine("GetCurrentMidiProgramName: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiProgramCategory: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public bool HasMidiProgramsChanged(int channel)
        {
            _editorCtrl.Instance.AddLine("HasMidiProgramsChanged: " + channel);
            return false;
        }

        /// <inheritdoc />
        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiKeyName: " + channel);
            return false;
        }

        /// <inheritdoc />
        public bool BeginSetProgram()
        {
            _editorCtrl.Instance.AddLine("BeginSetProgram");
            return false;
        }

        /// <inheritdoc />
        public bool EndSetProgram()
        {
            _editorCtrl.Instance.AddLine("EndSetProgram");
            return false;
        }

        #endregion

        #region IVstPluginCommands20 Members

        /// <inheritdoc />
        public bool ProcessEvents(VstEvent[] events)
        {
            _editorCtrl.Instance.AddLine("ProcessEvents: " + events.Length);
            return false;
        }

        /// <inheritdoc />
        public bool CanParameterBeAutomated(int index)
        {
            _editorCtrl.Instance.AddLine("CanParameterBeAutomated: " + index);
            return false;
        }

        /// <inheritdoc />
        public bool String2Parameter(int index, string str)
        {
            _editorCtrl.Instance.AddLine("String2Parameter: " + index + ", " + str);
            return false;
        }

        /// <inheritdoc />
        public string GetProgramNameIndexed(int index)
        {
            _editorCtrl.Instance.AddLine("GetProgramNameIndexed: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPinProperties GetInputProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetInputProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPinProperties GetOutputProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetOutputProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPluginCategory GetCategory()
        {
            _editorCtrl.Instance.AddLine("GetCategory");
            return VstPluginCategory.Unknown;
        }

        /// <inheritdoc />
        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            _editorCtrl.Instance.AddLine("SetSpeakerArrangement");
            return false;
        }

        /// <inheritdoc />
        public bool SetBypass(bool bypass)
        {
            _editorCtrl.Instance.AddLine("SetBypass: " + bypass);
            return false;
        }

        /// <inheritdoc />
        public string GetEffectName()
        {
            _editorCtrl.Instance.AddLine("GetEffectName");
            return "VST.NET Core Plugin";
        }

        /// <inheritdoc />
        public string GetVendorString()
        {
            _editorCtrl.Instance.AddLine("GetVendorString");
            return "Jacobi Software (c) 2009";
        }

        /// <inheritdoc />
        public string GetProductString()
        {
            _editorCtrl.Instance.AddLine("GetProductString");
            return "VST.NET Code Samples";
        }

        /// <inheritdoc />
        public int GetVendorVersion()
        {
            _editorCtrl.Instance.AddLine("GetVendorVersion");
            return 1000;
        }

        /// <inheritdoc />
        public VstCanDoResult CanDo(string cando)
        {
            _editorCtrl.Instance.AddLine("CanDo: " + cando);
            return VstCanDoResult.No;
        }

        /// <inheritdoc />
        public int GetTailSize()
        {
            _editorCtrl.Instance.AddLine("GetTailSize");
            return 0;
        }

        /// <inheritdoc />
        public VstParameterProperties GetParameterProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public int GetVstVersion()
        {
            _editorCtrl.Instance.AddLine("GetVstVersion");
            return 2400;
        }

        #endregion

        #region IVstPluginCommands10 Members

        /// <inheritdoc />
        public void Open()
        {
            _editorCtrl.Instance.AddLine("Open");
        }

        /// <inheritdoc />
        public void Close()
        {
            // Calling back into the Form at this point can cause a dead-lock.
            //_editorCtrl.Instance.AddLine("Close");
        }

        /// <inheritdoc />
        public void SetProgram(int programNumber)
        {
            _editorCtrl.Instance.AddLine("SetProgram: " + programNumber);
        }

        /// <inheritdoc />
        public int GetProgram()
        {
            _editorCtrl.Instance.AddLine("GetProgram");
            return 0;
        }

        /// <inheritdoc />
        public void SetProgramName(string name)
        {
            _editorCtrl.Instance.AddLine("SetProgramName: " + name);
        }

        /// <inheritdoc />
        public string GetProgramName()
        {
            _editorCtrl.Instance.AddLine("GetProgramName");
            return "NoProgram";
        }

        /// <inheritdoc />
        public string GetParameterLabel(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterLabel: " + index);
            return null;
        }

        /// <inheritdoc />
        public string GetParameterDisplay(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterDisplay: " + index);
            return null;
        }

        /// <inheritdoc />
        public string GetParameterName(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterName: " + index);
            return null;
        }

        /// <inheritdoc />
        public void SetSampleRate(float sampleRate)
        {
            _editorCtrl.Instance.AddLine("SetSampleRate: " + sampleRate);
        }

        /// <inheritdoc />
        public void SetBlockSize(int blockSize)
        {
            _editorCtrl.Instance.AddLine("SetBlockSize: " + blockSize);
        }
        
        /// <inheritdoc />
        public void MainsChanged(bool onoff)
        {
            _editorCtrl.Instance.AddLine("MainsChanged: " + onoff);
        }

        /// <inheritdoc />
        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            _editorCtrl.GetBounds(out rect);
            return true;
        }

        /// <inheritdoc />
        public bool EditorOpen(System.IntPtr hWnd)
        {
            _editorCtrl.Instance.Host = _hostStub;
            _editorCtrl.Open(hWnd);
            return true;
        }

        /// <inheritdoc />
        public void EditorClose()
        {
            _editorCtrl.Close();
        }

        /// <inheritdoc />
        public void EditorIdle()
        {
            
        }

        /// <inheritdoc />
        public byte[] GetChunk(bool isPreset)
        {
            _editorCtrl.Instance.AddLine("GetChunk");
            return null;
        }

        /// <inheritdoc />
        public int SetChunk(byte[] data, bool isPreset)
        {
            _editorCtrl.Instance.AddLine("SetChunk");
            return 0;
        }

        #endregion

        #region IVstPluginCommandsBase Members

        /// <inheritdoc />
        public void ProcessReplacing(VstAudioBuffer[] input, VstAudioBuffer[] outputs)
        {
            foreach (VstAudioBuffer audioBuffer in outputs)
            {
                for (int n = 0; n < audioBuffer.SampleCount; n++)
                {
                    audioBuffer[n] = input[0][n];
                }
            }
        }

        /// <inheritdoc />
        public void ProcessReplacing(VstAudioPrecisionBuffer[] input, VstAudioPrecisionBuffer[] outputs)
        {
            foreach (VstAudioPrecisionBuffer audioBuffer in outputs)
            {
                for (int n = 0; n < audioBuffer.SampleCount; n++)
                {
                    audioBuffer[n] = input[0][n];
                }
            }
        }

        /// <inheritdoc />
        public void SetParameter(int index, float value)
        {
            _editorCtrl.Instance.AddLine("SetParameter");
        }

        /// <inheritdoc />
        public float GetParameter(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameter");
            return 0.0f;
        }

        #endregion

    }
}
