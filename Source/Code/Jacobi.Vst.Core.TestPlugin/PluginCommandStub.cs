namespace Jacobi.Vst.Core.TestPlugin
{
    using Jacobi.Vst.Core;

    public class PluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginInfo _pluginInfo;
        private IVstHostCommandStub _hostStub;
        
        private EditorControl1 _editorCtrl;

        #region IVstPluginCommandStub Members

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            return false;
        }

        public int GetNumberOfMidiInputChannels()
        {
            return 0;
        }

        public int GetNumberOfMidiOutputChannels()
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStub23 Members

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            input = null;
            output = null;

            return false;
        }

        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            return 0;
        }

        public int GetNextPlugin(out string name)
        {
            name = null;
            return 0;
        }

        public int StartProcess()
        {
            return 0;
        }

        public int StopProcess()
        {
            return 0;
        }

        public bool SetPanLaw(VstPanLaw type, float value)
        {
            return false;
        }

        public int BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            return 0;
        }

        public int BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStub21 Members

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return false;
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return false;
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            return false;
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return 0;
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return 0;
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            return 0;
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            return false;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            return false;
        }

        public bool BeginSetProgram()
        {
            return false;
        }

        public bool EndSetProgram()
        {
            return false;
        }

        #endregion

        #region IVstPluginCommandStub20 Members

        public bool ProcessEvents(VstEvent[] events)
        {
            return false;
        }

        public bool CanParameterBeAutomated(int index)
        {
            return false;
        }

        public bool String2Parameter(int index, string str)
        {
            return false;
        }

        public bool GetProgramNameIndexed(int index, out string name)
        {
            name = null;
            return false;
        }

        public bool GetInputProperties(int index, VstPinProperties pinProps)
        {
            return false;
        }

        public bool GetOutputProperties(int index, VstPinProperties pinProps)
        {
            return false;
        }

        public VstPluginCategory GetCategory()
        {
            return VstPluginCategory.Unknown;
        }

        public bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        {
            return false;
        }

        public bool OfflinePrepare(int count)
        {
            return false;
        }

        public bool OfflineRun(int count)
        {
            return false;
        }

        public bool ProcessVariableIO(VstVariableIO variableIO)
        {
            return false;
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            return false;
        }

        public bool SetBypass(bool bypass)
        {
            return false;
        }

        public bool GetEffectName(out string name)
        {
            name = "VST.NET TestPlugin";
            return false;
        }

        public bool GetVendorString(out string vendor)
        {
            vendor = "Jacobi Software (c) 2008";
            return false;
        }

        public bool GetProductString(out string product)
        {
            product = "VST.NET";
            return false;
        }

        public int GetVendorVersion()
        {
            return 1000;
        }

        public VstCanDoResult CanDo(VstPluginCanDo cando)
        {
            return VstCanDoResult.No;
        }

        public int GetTailSize()
        {
            return 0;
        }

        public bool GetParameterProperties(int index, VstParameterProperties paramProps)
        {
            return false;
        }

        public int GetVstVersion()
        {
            return 2400;
        }

        #endregion

        #region IVstPluginCommandStub10 Members

        public void Open()
        {
        }

        public void Close()
        {
        }

        public void SetProgram(int programNumber)
        {
        }

        public int GetProgram()
        {
            return 0;
        }

        public void SetProgramName(string name)
        {
            
        }

        public string GetProgramName()
        {
            return null;
        }

        public string GetParameterLabel(int index)
        {
            return null;
        }

        public string GetParameterDisplay(int index)
        {
            return null;
        }

        public string GetParameterName(int index)
        {
            return null;
        }

        public void SetSampleRate(float sampleRate)
        {

        }

        public void SetBlockSize(int blockSize)
        {
        }

        public void MainsChanged(bool onoff)
        {
        }

        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            if (_editorCtrl != null)
            {
                rect = _editorCtrl.Bounds;
                return true;
            }
            else
            {
                rect = new System.Drawing.Rectangle();
            }

            return false;
        }

        public bool EditorOpen(System.IntPtr hWnd)
        {
            _editorCtrl = new EditorControl1();
            _editorCtrl.CreateControl();
            NativeMethods.SetParent(_editorCtrl.Handle, hWnd);
            _editorCtrl.Location = new System.Drawing.Point(0, 0);
            _editorCtrl.Show();

            return true;
        }

        public void EditorClose()
        {
            if (_editorCtrl != null)
            {
                _editorCtrl.Dispose();
                _editorCtrl = null;
            }
        }

        public void EditorIdle()
        {
            
        }

        public int GetChunk(out byte[] data, bool isPreset)
        {
            data = null;
            return 0;
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            return 0;
        }

        #endregion

        #region IVstPluginCommandStubBase Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            _hostStub = hostCmdStub;
            _pluginInfo = new VstPluginInfo();
            
            _pluginInfo.Flags = VstPluginInfoFlags.HasEditor;
            _pluginInfo.PluginID = 1234;
            _pluginInfo.PluginVersion = 1000;

            return _pluginInfo;
        }

        public void ProcessReplacing(VstAudioBuffer[] input, VstAudioBuffer[] outputs)
        {
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] input, VstAudioPrecisionBuffer[] outputs)
        {
        }

        public void SetParameter(int index, float value)
        {
        }

        public float GetParameter(int index)
        {
            return 0.0f;
        }

        #endregion

    }
}
