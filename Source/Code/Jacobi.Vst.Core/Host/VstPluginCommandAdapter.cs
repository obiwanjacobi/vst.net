using System;

namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The VstPluginCommandAdapter class implements the Plugin <see cref="Jacobi.Vst.Core.Host.IVstPluginCommandStub"/>
    /// interface and forwards those calls to a <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation
    /// provided when the adapter class is constructed.
    /// </summary>
    public class VstPluginCommandAdapter : IVstPluginCommandStub
    {
        private Plugin.IVstPluginCommandStub _pluginCmdStub;

        /// <summary>
        /// Constructs a new instance based on the <paramref name="pluginCmdStub"/>
        /// </summary>
        /// <param name="pluginCmdStub">Will be used to forward calls to. Must not be null.</param>
        public VstPluginCommandAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
        {
            Throw.IfArgumentIsNull(pluginCmdStub, "pluginCmdStub");

            _pluginCmdStub = pluginCmdStub;
        }

        #region IVstPluginCommandStub Members

        /// <inheritdoc />
        public IVstPluginContext PluginContext { get; set; }

        #endregion

        #region IVstPluginCommands24 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="precision">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetProcessPrecision(VstProcessPrecision precision)
        {
            return _pluginCmdStub.SetProcessPrecision(precision);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetNumberOfMidiInputChannels()
        {
            return _pluginCmdStub.GetNumberOfMidiInputChannels();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetNumberOfMidiOutputChannels()
        {
            return _pluginCmdStub.GetNumberOfMidiOutputChannels();
        }

        #endregion

        #region IVstPluginCommands23 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="input">Passed with the forwarded call and back.</param>
        /// <param name="output">Passed with the forwarded call and back.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
        {
            return _pluginCmdStub.GetSpeakerArrangement(out input, out output);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int StartProcess()
        {
            return _pluginCmdStub.StartProcess();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int StopProcess()
        {
            return _pluginCmdStub.StopProcess();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="type">Passed with the forwarded call.</param>
        /// <param name="gain">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return _pluginCmdStub.SetPanLaw(type, gain);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="chunkInfo">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCmdStub.BeginLoadBank(chunkInfo);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="chunkInfo"></param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
        {
            return _pluginCmdStub.BeginLoadProgram(chunkInfo);
        }

        #endregion

        #region IVstPluginCommands21 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="ascii">Passed with the forwarded call.</param>
        /// <param name="virtualKey">Passed with the forwarded call.</param>
        /// <param name="modifers">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCmdStub.EditorKeyDown(ascii, virtualKey, modifers);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="ascii">Passed with the forwarded call.</param>
        /// <param name="virtualKey">Passed with the forwarded call.</param>
        /// <param name="modifers">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            return _pluginCmdStub.EditorKeyUp(ascii, virtualKey, modifers);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="mode">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetEditorKnobMode(VstKnobMode mode)
        {
            return _pluginCmdStub.SetEditorKnobMode(mode);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="midiProgram">Passed with the forwarded call.</param>
        /// <param name="channel">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCmdStub.GetMidiProgramName(midiProgram, channel);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="midiProgram">Passed with the forwarded call.</param>
        /// <param name="channel">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
        {
            return _pluginCmdStub.GetCurrentMidiProgramName(midiProgram, channel);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="midiCat">Passed with the forwarded call.</param>
        /// <param name="channel">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
        {
            return _pluginCmdStub.GetMidiProgramCategory(midiCat, channel);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="channel">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool HasMidiProgramsChanged(int channel)
        {
            return _pluginCmdStub.HasMidiProgramsChanged(channel);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="midiKeyName">Passed with the forwarded call.</param>
        /// <param name="channel">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
        {
            return _pluginCmdStub.GetMidiKeyName(midiKeyName, channel);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool BeginSetProgram()
        {
            return _pluginCmdStub.BeginSetProgram();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EndSetProgram()
        {
            return _pluginCmdStub.EndSetProgram();
        }

        #endregion

        #region IVstPluginCommands20 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="events">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool ProcessEvents(VstEvent[] events)
        {
            return _pluginCmdStub.ProcessEvents(events);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool CanParameterBeAutomated(int index)
        {
            return _pluginCmdStub.CanParameterBeAutomated(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <param name="str">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool String2Parameter(int index, string str)
        {
            return _pluginCmdStub.String2Parameter(index, str);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetProgramNameIndexed(int index)
        {
            return _pluginCmdStub.GetProgramNameIndexed(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstPinProperties GetInputProperties(int index)
        {
            return _pluginCmdStub.GetInputProperties(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstPinProperties GetOutputProperties(int index)
        {
            return _pluginCmdStub.GetOutputProperties(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstPluginCategory GetCategory()
        {
            return _pluginCmdStub.GetCategory();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="saInput">Passed with the forwarded call.</param>
        /// <param name="saOutput">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
        {
            return _pluginCmdStub.SetSpeakerArrangement(saInput, saOutput);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="bypass">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool SetBypass(bool bypass)
        {
            return _pluginCmdStub.SetBypass(bypass);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetEffectName()
        {
            return _pluginCmdStub.GetEffectName();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetVendorString()
        {
            return _pluginCmdStub.GetVendorString();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetProductString()
        {
            return _pluginCmdStub.GetProductString();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetVendorVersion()
        {
            return _pluginCmdStub.GetVendorVersion();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="cando">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstCanDoResult CanDo(string cando)
        {
            return _pluginCmdStub.CanDo(cando);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetTailSize()
        {
            return _pluginCmdStub.GetTailSize();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public VstParameterProperties GetParameterProperties(int index)
        {
            return _pluginCmdStub.GetParameterProperties(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetVstVersion()
        {
            return _pluginCmdStub.GetVstVersion();
        }

        #endregion

        #region IVstPluginCommands10 Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        public void Open()
        {
            _pluginCmdStub.Open();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        public void Close()
        {
            _pluginCmdStub.Close();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="programNumber">Passed with the forwarded call.</param>
        public void SetProgram(int programNumber)
        {
            _pluginCmdStub.SetProgram(programNumber);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int GetProgram()
        {
            return _pluginCmdStub.GetProgram();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="name">Passed with the forwarded call.</param>
        public void SetProgramName(string name)
        {
            _pluginCmdStub.SetProgramName(name);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetProgramName()
        {
            return _pluginCmdStub.GetProgramName();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetParameterLabel(int index)
        {
            return _pluginCmdStub.GetParameterLabel(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetParameterDisplay(int index)
        {
            return _pluginCmdStub.GetParameterDisplay(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public string GetParameterName(int index)
        {
            return _pluginCmdStub.GetParameterName(index);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="sampleRate">Passed with the forwarded call.</param>
        public void SetSampleRate(float sampleRate)
        {
            _pluginCmdStub.SetSampleRate(sampleRate);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="blockSize">Passed with the forwarded call.</param>
        public void SetBlockSize(int blockSize)
        {
            _pluginCmdStub.SetBlockSize(blockSize);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="onoff">Passed with the forwarded call.</param>
        public void MainsChanged(bool onoff)
        {
            _pluginCmdStub.MainsChanged(onoff);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="rect">Passed with the forwarded call and back.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorGetRect(out System.Drawing.Rectangle rect)
        {
            return _pluginCmdStub.EditorGetRect(out rect);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="hWnd">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool EditorOpen(IntPtr hWnd)
        {
            return _pluginCmdStub.EditorOpen(hWnd);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        public void EditorClose()
        {
            _pluginCmdStub.EditorClose();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        public void EditorIdle()
        {
            _pluginCmdStub.EditorIdle();
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="isPreset">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public byte[] GetChunk(bool isPreset)
        {
            return _pluginCmdStub.GetChunk(isPreset);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="data">Passed with the forwarded call.</param>
        /// <param name="isPreset">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public int SetChunk(byte[] data, bool isPreset)
        {
            return _pluginCmdStub.SetChunk(data, isPreset);
        }

        #endregion

        #region IVstPluginCommandsBase Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="inputs">Passed with the forwarded call.</param>
        /// <param name="outputs">Passed with the forwarded call.</param>
        public void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
        {
            _pluginCmdStub.ProcessReplacing(inputs, outputs);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="inputs">Passed with the forwarded call.</param>
        /// <param name="outputs">Passed with the forwarded call.</param>
        public void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs)
        {
            _pluginCmdStub.ProcessReplacing(inputs, outputs);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <param name="value">Passed with the forwarded call.</param>
        public void SetParameter(int index, float value)
        {
            _pluginCmdStub.SetParameter(index, value);
        }

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation.
        /// </summary>
        /// <param name="index">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public float GetParameter(int index)
        {
            return _pluginCmdStub.GetParameter(index);
        }

        #endregion
    }
}
