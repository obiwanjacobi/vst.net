namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
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
        //private WpfControlWrapper<EditorControl> _editorCtrl = new WpfControlWrapper<EditorControl>(300, 300);

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

        /// <inheritdoc />
        public Configuration PluginConfiguration { get; set; }

        private void Log(string text)
        {
            Debug.WriteLine(text);

            if (_editorCtrl.Instance != null)
            {
                _editorCtrl.Instance.AddLine(text);
            }
        }

        #endregion

        #region IVstPluginCommands24 Members

        /// <inheritdoc />
        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            Log("SetProcessPrecision:" + precision);
            return false;
        }

        /// <inheritdoc />
        public int GetNumberOfMidiInputChannels()
        {
            Log("GetNumberOfMidiInputChannels");
            return 0;
        }

        /// <inheritdoc />
        public int GetNumberOfMidiOutputChannels()
        {
            Log("GetNumberOfMidiOutputChannels");
            return 0;
        }

        #endregion

        #region IVstPluginCommands23 Members

        /// <inheritdoc />
        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            Log("GetSpeakerArrangement");
            input = null;
            output = null;

            return false;
        }

        /// <inheritdoc />
        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            Log("SetTotalSamplesToProcess: " + numberOfSamples);
            return 0;
        }

        /// <inheritdoc />
        public int GetNextPlugin(out string name)
        {
            Log("GetNextPlugin");
            name = null;
            return 0;
        }

        /// <inheritdoc />
        public int StartProcess()
        {
            Log("StartProcess");
            return 0;
        }

        /// <inheritdoc />
        public int StopProcess()
        {
            Log("StopProcess");
            return 0;
        }

        /// <inheritdoc />
        public bool SetPanLaw(VstPanLaw type, float value)
        {
            Log("SetPanLaw: " + type);
            return false;
        }

        /// <inheritdoc />
        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            Log("BeginLoadBank");
            return VstCanDoResult.Unknown;
        }
        
        /// <inheritdoc />
        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            Log("BeginLoadProgram");
            return VstCanDoResult.Unknown;
        }

        #endregion

        #region IVstPluginCommands21 Members

        /// <inheritdoc />
        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            Log("EditorKeyDown: " + ascii);
            return false;
        }

        /// <inheritdoc />
        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            Log("EditorKeyUp: " + ascii);
            return false;
        }

        /// <inheritdoc />
        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            Log("SetEditorKnobMode: " + mode);
            return false;
        }

        /// <inheritdoc />
        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            Log("GetMidiProgramName: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            Log("GetCurrentMidiProgramName: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            Log("GetMidiProgramCategory: " + channel);
            return 0;
        }

        /// <inheritdoc />
        public bool HasMidiProgramsChanged(int channel)
        {
            Log("HasMidiProgramsChanged: " + channel);
            return false;
        }

        /// <inheritdoc />
        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            Log("GetMidiKeyName: " + channel);
            return false;
        }

        /// <inheritdoc />
        public bool BeginSetProgram()
        {
            Log("BeginSetProgram");
            return false;
        }

        /// <inheritdoc />
        public bool EndSetProgram()
        {
            Log("EndSetProgram");
            return false;
        }

        #endregion

        #region IVstPluginCommands20 Members

        /// <inheritdoc />
        public bool ProcessEvents(VstEvent[] events)
        {
            Log("ProcessEvents: " + events.Length);
            return false;
        }

        /// <inheritdoc />
        public bool CanParameterBeAutomated(int index)
        {
            Log("CanParameterBeAutomated: " + index);
            return false;
        }

        /// <inheritdoc />
        public bool String2Parameter(int index, string str)
        {
            Log("String2Parameter: " + index + ", " + str);
            return false;
        }

        /// <inheritdoc />
        public string GetProgramNameIndexed(int index)
        {
            Log("GetProgramNameIndexed: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPinProperties GetInputProperties(int index)
        {
            Log("GetInputProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPinProperties GetOutputProperties(int index)
        {
            Log("GetOutputProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public VstPluginCategory GetCategory()
        {
            Log("GetCategory");
            return VstPluginCategory.Unknown;
        }

        /// <inheritdoc />
        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            Log("SetSpeakerArrangement");
            return false;
        }

        /// <inheritdoc />
        public bool SetBypass(bool bypass)
        {
            Log("SetBypass: " + bypass);
            return false;
        }

        /// <inheritdoc />
        public string GetEffectName()
        {
            Log("GetEffectName");
            return "VST.NET Core Plugin";
        }

        /// <inheritdoc />
        public string GetVendorString()
        {
            Log("GetVendorString");
            return "Jacobi Software (c) 2011";
        }

        /// <inheritdoc />
        public string GetProductString()
        {
            Log("GetProductString");
            return "VST.NET Code Samples";
        }

        /// <inheritdoc />
        public int GetVendorVersion()
        {
            Log("GetVendorVersion");
            return 1000;
        }

        /// <inheritdoc />
        public VstCanDoResult CanDo(string cando)
        {
            Log("CanDo: " + cando);
            return VstCanDoResult.No;
        }

        /// <inheritdoc />
        public int GetTailSize()
        {
            Log("GetTailSize");
            return 0;
        }

        /// <inheritdoc />
        public VstParameterProperties GetParameterProperties(int index)
        {
            Log("GetParameterProperties: " + index);
            return null;
        }

        /// <inheritdoc />
        public int GetVstVersion()
        {
            Log("GetVstVersion");
            return 2400;
        }

        #endregion

        #region IVstPluginCommands10 Members

        /// <inheritdoc />
        public void Open()
        {
            Log("Open");
        }

        /// <inheritdoc />
        public void Close()
        {
            // Calling back into the Form at this point can cause a dead-lock.
            //Log("Close");

            // perform cleanup of the host stub
            if (_hostStub != null)
            {
                _hostStub.Dispose();
                _hostStub = null;

                _pluginInfo = null;
            }
        }

        /// <inheritdoc />
        public void SetProgram(int programNumber)
        {
            Log("SetProgram: " + programNumber);
        }

        /// <inheritdoc />
        public int GetProgram()
        {
            Log("GetProgram");
            return 0;
        }

        /// <inheritdoc />
        public void SetProgramName(string name)
        {
            Log("SetProgramName: " + name);
        }

        /// <inheritdoc />
        public string GetProgramName()
        {
            Log("GetProgramName");
            return "NoProgram";
        }

        /// <inheritdoc />
        public string GetParameterLabel(int index)
        {
            Log("GetParameterLabel: " + index);
            return null;
        }

        /// <inheritdoc />
        public string GetParameterDisplay(int index)
        {
            Log("GetParameterDisplay: " + index);
            return null;
        }

        /// <inheritdoc />
        public string GetParameterName(int index)
        {
            Log("GetParameterName: " + index);
            return null;
        }

        /// <inheritdoc />
        public void SetSampleRate(float sampleRate)
        {
            Log("SetSampleRate: " + sampleRate);
        }

        /// <inheritdoc />
        public void SetBlockSize(int blockSize)
        {
            Log("SetBlockSize: " + blockSize);
        }
        
        /// <inheritdoc />
        public void MainsChanged(bool onoff)
        {
            Log("MainsChanged: " + onoff);
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
            _editorCtrl.Open(hWnd);
            _editorCtrl.Instance.Host = _hostStub;

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
            Log("GetChunk");
            return null;
        }

        /// <inheritdoc />
        public int SetChunk(byte[] data, bool isPreset)
        {
            Log("SetChunk");
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
            Log("SetParameter");
        }

        /// <inheritdoc />
        public float GetParameter(int index)
        {
            Log("GetParameter");
            return 0.0f;
        }

        #endregion

    }
}
