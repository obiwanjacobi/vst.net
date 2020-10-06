using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework.Common;
using System;
using System.Drawing;

namespace Jacobi.Vst.Samples.HotReloadWrapper
{
    internal class PluginCommands : IVstPluginCommands24
    {
        private readonly WinFormsControlWrapper<UI.PluginFrame> _uiWrapper =
            new WinFormsControlWrapper<UI.PluginFrame>();

        // commands to the loaded plugin
        private IVstPluginCommands24? _commands;
        // path of the loaded plugin
        private string _pluginPath = String.Empty;

        public delegate IVstPluginCommands24 LoadPlugin(string pluginPath);

        public PluginCommands()
        {
            _uiWrapper.SafeInstance.OnReload += Plugin_OnReload;
        }

        private void Plugin_OnReload(string pluginPath)
        {
            if (OnLoadPlugin != null)
            {
                _commands = OnLoadPlugin(pluginPath);
                if (_commands != null)
                {
                    _pluginPath = pluginPath;
                    _commands.EditorOpen(_uiWrapper.SafeInstance.PluginWnd);
                }
            }
        }

        // callback to trigger plugin load logic
        public LoadPlugin? OnLoadPlugin { get; set; }

        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            if (_commands != null)
                _commands.BeginLoadBank(chunkInfo);

            return VstCanDoResult.No;
        }

        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            if (_commands != null)
                _commands.BeginLoadProgram(chunkInfo);

            return VstCanDoResult.No;
        }

        public bool BeginSetProgram()
        {
            if (_commands != null)
                return _commands.BeginSetProgram();

            return false;
        }

        public VstCanDoResult CanDo(string cando)
        {
            if (_commands != null)
                return _commands.CanDo(cando);

            return VstCanDoResult.No;
        }

        public bool CanParameterBeAutomated(int index)
        {
            if (_commands != null)
                return _commands.CanParameterBeAutomated(index);

            return false;
        }

        public void Close()
        {
            if (_commands != null)
                _commands.Close();
        }

        public void EditorClose()
        {
            if (_commands != null)
            {
                _uiWrapper.SafeInstance.DetachPluginUI();
                _commands.EditorClose();
            }
            _uiWrapper.Close();
        }

        public bool EditorGetRect(out Rectangle rect)
        {
            if (_commands != null &&
                _commands.EditorGetRect(out Rectangle pluginRect))
            {
                _uiWrapper.SafeInstance.SizeForPlugin(ref pluginRect);
            }

            rect = _uiWrapper.Bounds;
            return true;
        }

        public void EditorIdle()
        {
            if (_commands != null)
                _commands.EditorIdle();
        }

        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            if (_commands != null)
                return _commands.EditorKeyDown(ascii, virtualKey, modifers);

            return false;
        }

        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            if (_commands != null)
                return _commands.EditorKeyUp(ascii, virtualKey, modifers);

            return false;
        }

        public bool EditorOpen(IntPtr hWnd)
        {
            if (_commands != null)
                _commands.EditorOpen(_uiWrapper.SafeInstance.PluginWnd);

            _uiWrapper.SafeInstance.LoadedPluginPath = _pluginPath;
            _uiWrapper.Open(hWnd);
            return true;
        }

        public bool EndSetProgram()
        {
            if (_commands != null)
                return _commands.EndSetProgram();

            return false;
        }

        public VstPluginCategory GetCategory()
        {
            if (_commands != null)
                return _commands.GetCategory();

            return VstPluginCategory.Unknown;
        }

        public byte[] GetChunk(bool isPreset)
        {
            if (_commands != null)
                return _commands.GetChunk(isPreset);

            return Array.Empty<byte>();
        }

        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgramName, int channel)
        {
            if (_commands != null)
                return _commands.GetCurrentMidiProgramName(midiProgramName, channel);

            return 0;
        }

        public string GetEffectName()
        {
            if (_commands != null)
                return _commands.GetEffectName();

            return "VST.NET Hot-Reload Plugin Wrapper";
        }

        public VstPinProperties? GetInputProperties(int index)
        {
            if (_commands != null)
                return _commands.GetInputProperties(index);

            return null;
        }

        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            if (_commands != null)
                return _commands.GetMidiKeyName(midiKeyName, channel);

            return false;
        }

        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            if (_commands != null)
                return _commands.GetMidiProgramCategory(midiCat, channel);

            return 0;
        }

        public int GetMidiProgramName(VstMidiProgramName midiProgramName, int channel)
        {
            if (_commands != null)
                return _commands.GetMidiProgramName(midiProgramName, channel);

            return 0;
        }

        public int GetNextPlugin(out string name)
        {
            if (_commands != null)
                return _commands.GetNextPlugin(out name);

            name = String.Empty;
            return 0;
        }

        public int GetNumberOfMidiInputChannels()
        {
            if (_commands != null)
                return _commands.GetNumberOfMidiInputChannels();

            return 0;
        }

        public int GetNumberOfMidiOutputChannels()
        {
            if (_commands != null)
                return _commands.GetNumberOfMidiOutputChannels();

            return 0;
        }

        public VstPinProperties? GetOutputProperties(int index)
        {
            if (_commands != null)
                return _commands.GetOutputProperties(index);

            return null;
        }

        public float GetParameter(int index)
        {
            if (_commands != null)
                return _commands.GetParameter(index);

            return 0.0f;
        }

        public string GetParameterDisplay(int index)
        {
            if (_commands != null)
                return _commands.GetParameterDisplay(index);

            return String.Empty;
        }

        public string GetParameterLabel(int index)
        {
            if (_commands != null)
                return _commands.GetParameterLabel(index);

            return String.Empty;
        }

        public string GetParameterName(int index)
        {
            if (_commands != null)
                return _commands.GetParameterName(index);

            return String.Empty;
        }

        public VstParameterProperties? GetParameterProperties(int index)
        {
            if (_commands != null)
                return _commands.GetParameterProperties(index);

            return null;
        }

        public string GetProductString()
        {
            if (_commands != null)
                return _commands.GetProductString();

            return "VST.NET 2";
        }

        public int GetProgram()
        {
            if (_commands != null)
                return _commands.GetProgram();

            return 0;
        }

        public string GetProgramName()
        {
            if (_commands != null)
                return _commands.GetProgramName();

            return String.Empty;
        }

        public string GetProgramNameIndexed(int index)
        {
            if (_commands != null)
                return _commands.GetProgramNameIndexed(index);

            return String.Empty;
        }

        public bool GetSpeakerArrangement(out VstSpeakerArrangement? input, out VstSpeakerArrangement? output)
        {
            if (_commands != null)
                return _commands.GetSpeakerArrangement(out input, out output);

            input = null;
            output = null;
            return false;
        }

        public int GetTailSize()
        {
            if (_commands != null)
                return _commands.GetTailSize();

            return 0;
        }

        public string GetVendorString()
        {
            if (_commands != null)
                return _commands.GetVendorString();

            return "Jacobi Software";
        }

        public int GetVendorVersion()
        {
            if (_commands != null)
                return _commands.GetVendorVersion();

            return 2000;
        }

        public int GetVstVersion()
        {
            if (_commands != null)
                return _commands.GetVstVersion();

            return 2400;
        }

        public bool HasMidiProgramsChanged(int channel)
        {
            if (_commands != null)
                return _commands.HasMidiProgramsChanged(channel);

            return false;
        }

        public void MainsChanged(bool onoff)
        {
            if (_commands != null)
                _commands.MainsChanged(onoff);
        }

        public void Open()
        {
            if (_commands != null)
                _commands.Open();
        }

        public bool ProcessEvents(VstEvent[] events)
        {
            if (_commands != null)
                return _commands.ProcessEvents(events);

            return false;
        }

        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            if (_commands != null)
                _commands.ProcessReplacing(inputs, outputs);
        }

        public void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            if (_commands != null)
                _commands.ProcessReplacing(inputs, outputs);
        }

        public void SetBlockSize(int blockSize)
        {
            if (_commands != null)
                _commands.SetBlockSize(blockSize);
        }

        public bool SetBypass(bool bypass)
        {
            if (_commands != null)
                return _commands.SetBypass(bypass);

            return false;
        }

        public int SetChunk(byte[] data, bool isPreset)
        {
            if (_commands != null)
                return _commands.SetChunk(data, isPreset);

            return 0;
        }

        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            if (_commands != null)
                return _commands.SetEditorKnobMode(mode);

            return false;
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            if (_commands != null)
                return _commands.SetPanLaw(type, gain);

            return false;
        }

        public void SetParameter(int index, float value)
        {
            if (_commands != null)
                _commands.SetParameter(index, value);
        }

        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            if (_commands != null)
                return _commands.SetProcessPrecision(precision);

            return false;
        }

        public void SetProgram(int programNumber)
        {
            if (_commands != null)
                _commands.SetProgram(programNumber);
        }

        public void SetProgramName(string name)
        {
            if (_commands != null)
                _commands.SetProgramName(name);
        }

        public void SetSampleRate(float sampleRate)
        {
            if (_commands != null)
                _commands.SetSampleRate(sampleRate);
        }

        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            if (_commands != null)
                return _commands.SetSpeakerArrangement(saInput, saOutput);

            return false;
        }

        public int StartProcess()
        {
            if (_commands != null)
                return _commands.StartProcess();

            return 0;
        }

        public int StopProcess()
        {
            if (_commands != null)
                return _commands.StopProcess();

            return 0;
        }

        public bool String2Parameter(int index, string str)
        {
            if (_commands != null)
                return _commands.String2Parameter(index, str);

            return false;
        }
    }
}
