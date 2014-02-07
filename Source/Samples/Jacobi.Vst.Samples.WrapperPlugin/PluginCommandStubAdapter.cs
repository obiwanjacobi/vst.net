using System;
using System.Configuration;
using System.Drawing;

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Interop.Host;

namespace Jacobi.Vst.Samples.WrapperPlugin
{
    public class PluginCommandStubAdapter : IVstPluginCommandStub
    {
        private VstPluginContext _pluginCtx;

        #region IVstPluginCommandStub Members

        public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
        {
            //
            // get the path to the wrapped plugin from config
            //

            if (PluginConfiguration == null)
            {
                throw new ApplicationException("No plugin configuration found.");
            }

            KeyValueConfigurationElement configElem = PluginConfiguration.AppSettings.Settings["PluginPath"];

            if(configElem == null)
            {
                throw new ApplicationException("The 'PluginPath' configuration (app) setting was not found.");
            }

            Host.HostCommandStubAdapter hostCmdAdapter = new Host.HostCommandStubAdapter(hostCmdStub);
            _pluginCtx = VstPluginContext.Create(configElem.Value, hostCmdAdapter);

            return _pluginCtx.PluginInfo;
        }

        public Configuration PluginConfiguration 
        { get; set; }

        #endregion

        #region IVstPluginCommands24 Members

        public int GetNumberOfMidiInputChannels()
        {
            return _pluginCtx.PluginCommandStub.GetNumberOfMidiInputChannels();
        }

        public int GetNumberOfMidiOutputChannels()
        {
            return _pluginCtx.PluginCommandStub.GetNumberOfMidiOutputChannels();
        }

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            return _pluginCtx.PluginCommandStub.SetProcessPrecision(precision);
        }

        #endregion

        #region IVstPluginCommands23 Members

        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCtx.PluginCommandStub.BeginLoadBank(chunkInfo);
        }

        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCtx.PluginCommandStub.BeginLoadProgram(chunkInfo);
        }

        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            return _pluginCtx.PluginCommandStub.GetSpeakerArrangement(out input, out output);
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return _pluginCtx.PluginCommandStub.SetPanLaw(type, gain);
        }

        public int StartProcess()
        {
            return _pluginCtx.PluginCommandStub.StartProcess();
        }

        public int StopProcess()
        {
            return _pluginCtx.PluginCommandStub.StopProcess();
        }

        public int GetNextPlugin(out string name)
        {
            return _pluginCtx.PluginCommandStub.GetNextPlugin(out name);
        }

        #endregion

        #region IVstPluginCommands21 Members

        public bool BeginSetProgram()
        {
            return _pluginCtx.PluginCommandStub.BeginSetProgram();
        }

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCtx.PluginCommandStub.EditorKeyDown(ascii, virtualKey, modifers);
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCtx.PluginCommandStub.EditorKeyUp(ascii, virtualKey, modifers);
        }

        public bool EndSetProgram()
        {
            return _pluginCtx.PluginCommandStub.EndSetProgram();
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCtx.PluginCommandStub.GetCurrentMidiProgramName(midiProgram, channel);
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            return _pluginCtx.PluginCommandStub.GetMidiKeyName(midiKeyName, channel);
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            return _pluginCtx.PluginCommandStub.GetMidiProgramCategory(midiCat, channel);
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCtx.PluginCommandStub.GetMidiProgramName(midiProgram, channel);
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            return _pluginCtx.PluginCommandStub.HasMidiProgramsChanged(channel);
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            return _pluginCtx.PluginCommandStub.SetEditorKnobMode(mode);
        }

        #endregion

        #region IVstPluginCommands20 Members

        public VstCanDoResult CanDo(string cando)
        {
            return _pluginCtx.PluginCommandStub.CanDo(cando);
        }

        public bool CanParameterBeAutomated(int index)
        {
            return _pluginCtx.PluginCommandStub.CanParameterBeAutomated(index);
        }

        public VstPluginCategory GetCategory()
        {
            return _pluginCtx.PluginCommandStub.GetCategory();
        }

        public string GetEffectName()
        {
            return _pluginCtx.PluginCommandStub.GetEffectName();
        }

        public VstPinProperties GetInputProperties(int index)
        {
            return _pluginCtx.PluginCommandStub.GetInputProperties(index);
        }

        public VstPinProperties GetOutputProperties(int index)
        {
            return _pluginCtx.PluginCommandStub.GetOutputProperties(index);
        }

        public VstParameterProperties GetParameterProperties(int index)
        {
            return _pluginCtx.PluginCommandStub.GetParameterProperties(index);
        }

        public string GetProductString()
        {
            return _pluginCtx.PluginCommandStub.GetProductString();
        }

        public string GetProgramNameIndexed(int index)
        {
            return _pluginCtx.PluginCommandStub.GetProgramNameIndexed(index);
        }

        public int GetTailSize()
        {
            return _pluginCtx.PluginCommandStub.GetTailSize();
        }

        public string GetVendorString()
        {
            return _pluginCtx.PluginCommandStub.GetVendorString();
        }

        public int GetVendorVersion()
        {
            return _pluginCtx.PluginCommandStub.GetVendorVersion();
        }

        public int GetVstVersion()
        {
            return _pluginCtx.PluginCommandStub.GetVstVersion();
        }

        public bool ProcessEvents(VstEvent[] events)
        {
            return _pluginCtx.PluginCommandStub.ProcessEvents(events);
        }

        public bool SetBypass(bool bypass)
        {
            return _pluginCtx.PluginCommandStub.SetBypass(bypass);
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            return _pluginCtx.PluginCommandStub.SetSpeakerArrangement(saInput, saOutput);
        }

        public bool String2Parameter(int index, string str)
        {
            return _pluginCtx.PluginCommandStub.String2Parameter(index, str);
        }

        #endregion

        #region IVstPluginCommands10 Members

        public void Close()
        {
            _pluginCtx.PluginCommandStub.Close();
        }

        public void EditorClose()
        {
            _pluginCtx.PluginCommandStub.EditorClose();
        }

        public bool EditorGetRect(out Rectangle rect)
        {
            return _pluginCtx.PluginCommandStub.EditorGetRect(out rect);
        }

        public void EditorIdle()
        {
            _pluginCtx.PluginCommandStub.EditorIdle();
        }

        public bool EditorOpen(IntPtr hWnd)
        {
            return _pluginCtx.PluginCommandStub.EditorOpen(hWnd);
        }

        public byte[] GetChunk(bool isPreset)
        {
            return _pluginCtx.PluginCommandStub.GetChunk(isPreset);
        }

        public string GetParameterDisplay(int index)
        {
            return _pluginCtx.PluginCommandStub.GetParameterDisplay(index);
        }

        public string GetParameterLabel(int index)
        {
            return _pluginCtx.PluginCommandStub.GetParameterLabel(index);
        }

        public string GetParameterName(int index)
        {
            return _pluginCtx.PluginCommandStub.GetParameterName(index);
        }

        public int GetProgram()
        {
            return _pluginCtx.PluginCommandStub.GetProgram();
        }

        public string GetProgramName()
        {
            return _pluginCtx.PluginCommandStub.GetProgramName();
        }

        public void MainsChanged(bool onoff)
        {
            _pluginCtx.PluginCommandStub.MainsChanged(onoff);
        }

        public void Open()
        {
            _pluginCtx.PluginCommandStub.Open();
        }

        public void SetBlockSize(int blockSize)
        {
            _pluginCtx.PluginCommandStub.SetBlockSize(blockSize);
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            return _pluginCtx.PluginCommandStub.SetChunk(data, isPreset);
        }

        public void SetProgram(int programNumber)
        {
            _pluginCtx.PluginCommandStub.SetProgram(programNumber);
        }

        public void SetProgramName(string name)
        {
            _pluginCtx.PluginCommandStub.SetProgramName(name);
        }

        public void SetSampleRate(float sampleRate)
        {
            _pluginCtx.PluginCommandStub.SetSampleRate(sampleRate);
        }

        #endregion

        #region IVstPluginCommandsBase Members

        public float GetParameter(int index)
        {
            return _pluginCtx.PluginCommandStub.GetParameter(index);
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            _pluginCtx.PluginCommandStub.ProcessReplacing(inputs, outputs);
        }

        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            _pluginCtx.PluginCommandStub.ProcessReplacing(inputs, outputs);
        }

        public void SetParameter(int index, float value)
        {
            _pluginCtx.PluginCommandStub.SetParameter(index, value);
        }

        #endregion

    }
}
