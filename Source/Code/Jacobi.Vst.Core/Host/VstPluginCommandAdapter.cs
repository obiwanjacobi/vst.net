using System;

namespace Jacobi.Vst.Core.Host
{
    public class VstPluginCommandAdapter : IVstPluginCommandStub
    {
        private Plugin.IVstPluginCommandStub _pluginCmdStub;

        public VstPluginCommandAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
        {
            Throw.IfArgumentIsNull(pluginCmdStub, "pluginCmdStub");

            _pluginCmdStub = pluginCmdStub;
        }

        #region IVstPluginCommandStub Members

        public IVstPluginContext PluginContext { get; set; }

        #endregion

        #region IVstPluginCommands24 Members

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            return _pluginCmdStub.SetProcessPrecision(precision);
        }

        public int GetNumberOfMidiInputChannels()
        {
            return _pluginCmdStub.GetNumberOfMidiInputChannels();
        }

        public int GetNumberOfMidiOutputChannels()
        {
            return _pluginCmdStub.GetNumberOfMidiOutputChannels();
        }

        #endregion

        #region IVstPluginCommands23 Members

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            return _pluginCmdStub.GetSpeakerArrangement(out input, out output);
        }

        public int StartProcess()
        {
            return _pluginCmdStub.StartProcess();
        }

        public int StopProcess()
        {
            return _pluginCmdStub.StopProcess();
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return _pluginCmdStub.SetPanLaw(type, gain);
        }

        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCmdStub.BeginLoadBank(chunkInfo);
        }

        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCmdStub.BeginLoadProgram(chunkInfo);
        }

        #endregion

        #region IVstPluginCommands21 Members

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCmdStub.EditorKeyDown(ascii, virtualKey, modifers);
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCmdStub.EditorKeyUp(ascii, virtualKey, modifers);
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            return _pluginCmdStub.SetEditorKnobMode(mode);
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCmdStub.GetMidiProgramName(midiProgram, channel);
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCmdStub.GetCurrentMidiProgramName(midiProgram, channel);
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            return _pluginCmdStub.GetMidiProgramCategory(midiCat, channel);
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            return _pluginCmdStub.HasMidiProgramsChanged(channel);
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            return _pluginCmdStub.GetMidiKeyName(midiKeyName, channel);
        }

        public bool BeginSetProgram()
        {
            return _pluginCmdStub.BeginSetProgram();
        }

        public bool EndSetProgram()
        {
            return _pluginCmdStub.EndSetProgram();
        }

        #endregion

        #region IVstPluginCommands20 Members

        public bool ProcessEvents(VstEvent[] events)
        {
            return _pluginCmdStub.ProcessEvents(events);
        }

        public bool CanParameterBeAutomated(int index)
        {
            return _pluginCmdStub.CanParameterBeAutomated(index);
        }

        public bool String2Parameter(int index, string str)
        {
            return _pluginCmdStub.String2Parameter(index, str);
        }

        public string GetProgramNameIndexed(int index)
        {
            return _pluginCmdStub.GetProgramNameIndexed(index);
        }

        public VstPinProperties GetInputProperties(int index)
        {
            return _pluginCmdStub.GetInputProperties(index);
        }

        public VstPinProperties GetOutputProperties(int index)
        {
            return _pluginCmdStub.GetOutputProperties(index);
        }

        public VstPluginCategory GetCategory()
        {
            return _pluginCmdStub.GetCategory();
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            return _pluginCmdStub.SetSpeakerArrangement(saInput, saOutput);
        }

        public bool SetBypass(bool bypass)
        {
            return _pluginCmdStub.SetBypass(bypass);
        }

        public string GetEffectName()
        {
            return _pluginCmdStub.GetEffectName();
        }

        public string GetVendorString()
        {
            return _pluginCmdStub.GetVendorString();
        }

        public string GetProductString()
        {
            return _pluginCmdStub.GetProductString();
        }

        public int GetVendorVersion()
        {
            return _pluginCmdStub.GetVendorVersion();
        }

        public VstCanDoResult CanDo(string cando)
        {
            return _pluginCmdStub.CanDo(cando);
        }

        public int GetTailSize()
        {
            return _pluginCmdStub.GetTailSize();
        }

        public VstParameterProperties GetParameterProperties(int index)
        {
            return _pluginCmdStub.GetParameterProperties(index);
        }

        public int GetVstVersion()
        {
            return _pluginCmdStub.GetVstVersion();
        }

        #endregion

        #region IVstPluginCommands10 Members

        public void Open()
        {
            _pluginCmdStub.Open();
        }

        public void Close()
        {
            _pluginCmdStub.Close();
        }

        public void SetProgram(int programNumber)
        {
            _pluginCmdStub.SetProgram(programNumber);
        }

        public int GetProgram()
        {
            return _pluginCmdStub.GetProgram();
        }

        public void SetProgramName(string name)
        {
            _pluginCmdStub.SetProgramName(name);
        }

        public string GetProgramName()
        {
            return _pluginCmdStub.GetProgramName();
        }

        public string GetParameterLabel(int index)
        {
            return _pluginCmdStub.GetParameterLabel(index);
        }

        public string GetParameterDisplay(int index)
        {
            return _pluginCmdStub.GetParameterDisplay(index);
        }

        public string GetParameterName(int index)
        {
            return _pluginCmdStub.GetParameterName(index);
        }

        public void SetSampleRate(float sampleRate)
        {
            _pluginCmdStub.SetSampleRate(sampleRate);
        }

        public void SetBlockSize(int blockSize)
        {
            _pluginCmdStub.SetBlockSize(blockSize);
        }

        public void MainsChanged(bool onoff)
        {
            _pluginCmdStub.MainsChanged(onoff);
        }

        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            return _pluginCmdStub.EditorGetRect(out rect);
        }

        public bool EditorOpen(IntPtr hWnd)
        {
            return _pluginCmdStub.EditorOpen(hWnd);
        }

        public void EditorClose()
        {
            _pluginCmdStub.EditorClose();
        }

        public void EditorIdle()
        {
            _pluginCmdStub.EditorIdle();
        }

        public byte[] GetChunk(bool isPreset)
        {
            return _pluginCmdStub.GetChunk(isPreset);
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            return _pluginCmdStub.SetChunk(data, isPreset);
        }

        #endregion

        #region IVstPluginCommandsBase Members

        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            _pluginCmdStub.ProcessReplacing(inputs, outputs);
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            _pluginCmdStub.ProcessReplacing(inputs, outputs);
        }

        public void SetParameter(int index, float value)
        {
            _pluginCmdStub.SetParameter(index, value);
        }

        public float GetParameter(int index)
        {
            return _pluginCmdStub.GetParameter(index);
        }

        #endregion
    }
}
