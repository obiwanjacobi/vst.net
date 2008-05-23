namespace Jacobi.Vst.Core.TestPlugin
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;

    public class PluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginInfo _pluginInfo;
        private IVstHostCommandStub _hostStub;
        
        private WinFormsWrapper<EditorControl1> _editorCtrl = new WinFormsWrapper<EditorControl1>();

        #region IVstPluginCommandStub Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            _hostStub = hostCmdStub;
            _pluginInfo = new VstPluginInfo();

            _pluginInfo.NumberOfAudioInputs = 1;
            _pluginInfo.NumberOfAudioOutputs = 2;
            _pluginInfo.NumberOfPrograms = 1;
            _pluginInfo.Flags = VstPluginFlags.HasEditor | VstPluginFlags.CanReplacing;
            _pluginInfo.PluginID = 1234;
            _pluginInfo.PluginVersion = 1000;

            return _pluginInfo;
        }

        #endregion

        #region IVstPluginCommands24 Members

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            _editorCtrl.Instance.AddLine("SetProcessPrecision:" + precision);
            return false;
        }

        public int GetNumberOfMidiInputChannels()
        {
            _editorCtrl.Instance.AddLine("GetNumberOfMidiInputChannels");
            return 0;
        }

        public int GetNumberOfMidiOutputChannels()
        {
            _editorCtrl.Instance.AddLine("GetNumberOfMidiOutputChannels");
            return 0;
        }

        #endregion

        #region IVstPluginCommands23 Members

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            _editorCtrl.Instance.AddLine("GetSpeakerArrangement");
            input = null;
            output = null;

            return false;
        }

        public int SetTotalSamplesToProcess(int numberOfSamples)
        {
            _editorCtrl.Instance.AddLine("SetTotalSamplesToProcess: " + numberOfSamples);
            return 0;
        }

        public int GetNextPlugin(out string name)
        {
            _editorCtrl.Instance.AddLine("GetNextPlugin");
            name = null;
            return 0;
        }

        public int StartProcess()
        {
            _editorCtrl.Instance.AddLine("StartProcess");
            return 0;
        }

        public int StopProcess()
        {
            _editorCtrl.Instance.AddLine("StopProcess");
            return 0;
        }

        public bool SetPanLaw(VstPanLaw type, float value)
        {
            _editorCtrl.Instance.AddLine("SetPanLaw: " + type);
            return false;
        }

        public int BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            _editorCtrl.Instance.AddLine("BeginLoadBank");
            return 0;
        }

        public int BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            _editorCtrl.Instance.AddLine("BeginLoadProgram");
            return 0;
        }

        #endregion

        #region IVstPluginCommands21 Members

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            _editorCtrl.Instance.AddLine("EditorKeyDown: " + ascii);
            return false;
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            _editorCtrl.Instance.AddLine("EditorKeyUp: " + ascii);
            return false;
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            _editorCtrl.Instance.AddLine("SetEditorKnobMode: " + mode);
            return false;
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiProgramName: " + channel);
            return 0;
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            _editorCtrl.Instance.AddLine("GetCurrentMidiProgramName: " + channel);
            return 0;
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiProgramCategory: " + channel);
            return 0;
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            _editorCtrl.Instance.AddLine("HasMidiProgramsChanged: " + channel);
            return false;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            _editorCtrl.Instance.AddLine("GetMidiKeyName: " + channel);
            return false;
        }

        public bool BeginSetProgram()
        {
            _editorCtrl.Instance.AddLine("BeginSetProgram");
            return false;
        }

        public bool EndSetProgram()
        {
            _editorCtrl.Instance.AddLine("EndSetProgram");
            return false;
        }

        #endregion

        #region IVstPluginCommands20 Members

        public bool ProcessEvents(VstEvent[] events)
        {
            _editorCtrl.Instance.AddLine("ProcessEvents: " + events.Length);
            return false;
        }

        public bool CanParameterBeAutomated(int index)
        {
            _editorCtrl.Instance.AddLine("CanParameterBeAutomated: " + index);
            return false;
        }

        public bool String2Parameter(int index, string str)
        {
            _editorCtrl.Instance.AddLine("String2Parameter: " + index + ", " + str);
            return false;
        }

        public string GetProgramNameIndexed(int index)
        {
            _editorCtrl.Instance.AddLine("GetProgramNameIndexed: " + index);
            return null;
        }

        public VstPinProperties GetInputProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetInputProperties: " + index);
            return null;
        }

        public VstPinProperties GetOutputProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetOutputProperties: " + index);
            return null;
        }

        public VstPluginCategory GetCategory()
        {
            _editorCtrl.Instance.AddLine("GetCategory");
            return VstPluginCategory.Unknown;
        }

        public bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag)
        {
            _editorCtrl.Instance.AddLine("OfflineNotify");
            return false;
        }

        public bool OfflinePrepare(VstOfflineTask[] tasks, int count)
        {
            _editorCtrl.Instance.AddLine("OfflinePrepare");
            return false;
        }

        public bool OfflineRun(VstOfflineTask[] tasks, int count)
        {
            _editorCtrl.Instance.AddLine("OfflineRun");
            return false;
        }

        public bool ProcessVariableIO(VstVariableIO variableIO)
        {
            _editorCtrl.Instance.AddLine("ProcessVariableIO");
            return false;
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            _editorCtrl.Instance.AddLine("SetSpeakerArrangement");
            return false;
        }

        public bool SetBypass(bool bypass)
        {
            _editorCtrl.Instance.AddLine("SetBypass: " + bypass);
            return false;
        }

        public string GetEffectName()
        {
            _editorCtrl.Instance.AddLine("GetEffectName");
            return "VST.NET TestPlugin";
        }

        public string GetVendorString()
        {
            _editorCtrl.Instance.AddLine("GetVendorString");
            return "Jacobi Software (c) 2008";
        }

        public string GetProductString()
        {
            _editorCtrl.Instance.AddLine("GetProductString");
            return "VST.NET";
        }

        public int GetVendorVersion()
        {
            _editorCtrl.Instance.AddLine("GetVendorVersion");
            return 1000;
        }

        public VstCanDoResult CanDo(VstPluginCanDo cando)
        {
            _editorCtrl.Instance.AddLine("CanDo: " + cando);
            return VstCanDoResult.No;
        }

        public int GetTailSize()
        {
            _editorCtrl.Instance.AddLine("GetTailSize");
            return 0;
        }

        public VstParameterProperties GetParameterProperties(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterProperties: " + index);
            return null;
        }

        public int GetVstVersion()
        {
            _editorCtrl.Instance.AddLine("GetVstVersion");
            return 2400;
        }

        #endregion

        #region IVstPluginCommands10 Members

        public void Open()
        {
            _editorCtrl.Instance.AddLine("Open");
        }

        public void Close()
        {
            _editorCtrl.Instance.AddLine("Close");
        }

        public void SetProgram(int programNumber)
        {
            _editorCtrl.Instance.AddLine("SetProgram: " + programNumber);
        }

        public int GetProgram()
        {
            _editorCtrl.Instance.AddLine("GetProgram");
            return 0;
        }

        public void SetProgramName(string name)
        {
            _editorCtrl.Instance.AddLine("SetProgramName: " + name);
        }

        public string GetProgramName()
        {
            _editorCtrl.Instance.AddLine("GetProgramName");
            return "NoProgram";
        }

        public string GetParameterLabel(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterLabel: " + index);
            return null;
        }

        public string GetParameterDisplay(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterDisplay: " + index);
            return null;
        }

        public string GetParameterName(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameterName: " + index);
            return null;
        }

        public void SetSampleRate(float sampleRate)
        {
            _editorCtrl.Instance.AddLine("SetSampleRate: " + sampleRate);
        }

        public void SetBlockSize(int blockSize)
        {
            _editorCtrl.Instance.AddLine("SetBlockSize: " + blockSize);
        }

        public void MainsChanged(bool onoff)
        {
            _editorCtrl.Instance.AddLine("MainsChanged: " + onoff);
        }

        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            _editorCtrl.GetBounds(out rect);
            return true;
        }

        public bool EditorOpen(System.IntPtr hWnd)
        {
            _editorCtrl.Open(hWnd);
            return true;
        }

        public void EditorClose()
        {
            _editorCtrl.Close();
        }

        public void EditorIdle()
        {
            
        }

        public int GetChunk(out byte[] data, bool isPreset)
        {
            _editorCtrl.Instance.AddLine("GetChunk");
            data = null;
            return 0;
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            _editorCtrl.Instance.AddLine("SetChunk");
            return 0;
        }

        #endregion

        #region IVstPluginCommandsBase Members

        public void ProcessReplacing(VstAudioBuffer[] input, VstAudioBuffer[] outputs)
        {
            foreach (VstAudioBuffer audioBuffer in outputs)
            {
                for (int n = 0; n < audioBuffer.Count; n++)
                {
                    unsafe
                    {
                        audioBuffer.Buffer[n] = input[0].Buffer[n];
                    }
                }
            }
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] input, VstAudioPrecisionBuffer[] outputs)
        {
            foreach (VstAudioPrecisionBuffer audioBuffer in outputs)
            {
                for (int n = 0; n < audioBuffer.Count; n++)
                {
                    unsafe
                    {
                        audioBuffer.Buffer[n] = input[0].Buffer[n];
                    }
                }
            }
        }

        public void SetParameter(int index, float value)
        {
            _editorCtrl.Instance.AddLine("SetParameter");
        }

        public float GetParameter(int index)
        {
            _editorCtrl.Instance.AddLine("GetParameter");
            return 0.0f;
        }

        #endregion

    }
}
