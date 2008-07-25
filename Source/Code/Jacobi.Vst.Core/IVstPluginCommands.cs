namespace Jacobi.Vst.Core
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The Vst Plugin callback functions
    /// </summary>
    public interface IVstPluginCommandsBase
    {
        void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs);
        void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs);
        void SetParameter(int index, float value);
        float GetParameter(int index);
    }

    /// <summary>
    /// The Vst 1.0 Plugin commands
    /// </summary>
    public interface IVstPluginCommands10 : IVstPluginCommandsBase
    {
        void Open();
        void Close();
        void SetProgram(int programNumber);
        int GetProgram();
        void SetProgramName(string name);
        string GetProgramName();
        string GetParameterLabel(int index);
        string GetParameterDisplay(int index);
        string GetParameterName(int index);
        void SetSampleRate(float sampleRate);
        void SetBlockSize(int blockSize);
        void MainsChanged(bool onoff);
        bool EditorGetRect(out Rectangle rect);
        bool EditorOpen(IntPtr hWnd);
        void EditorClose();
        void EditorIdle();
        byte[] GetChunk(bool isPreset);
        int SetChunk(byte[] data, bool isPreset);
    }

    /// <summary>
    /// The Vst 2.0 Plugin commands
    /// </summary>
    public interface IVstPluginCommands20 : IVstPluginCommands10
    {
        bool ProcessEvents(VstEvent[] events);
        bool CanParameterBeAutomated(int index);
        bool String2Parameter(int index, string str);
        string GetProgramNameIndexed(int index);
        VstPinProperties GetInputProperties(int index);
        VstPinProperties GetOutputProperties(int index);
        VstPluginCategory GetCategory();
        bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag);
        bool OfflinePrepare(VstOfflineTask[] tasks, int count);
        bool OfflineRun(VstOfflineTask[] tasks, int count);
        bool ProcessVariableIO(VstVariableIO variableIO);
        bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput);
        bool SetBypass(bool bypass);
        string GetEffectName();
        string GetVendorString();
        string GetProductString();
        int GetVendorVersion();
        VstCanDoResult CanDo(string cando);
        int GetTailSize();
        VstParameterProperties GetParameterProperties(int index);
        int GetVstVersion();
    }

    /// <summary>
    /// The Vst 2.1 Plugin commands
    /// </summary>
    public interface IVstPluginCommands21 : IVstPluginCommands20
    {
        bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
        bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
        bool SetEditorKnobMode(VstKnobMode mode);
        int GetMidiProgramName(VstMidiProgramName midiProgram, int channel);
        int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel);
        int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel);
        bool HasMidiProgramsChanged(int channel);
        bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel);
        bool BeginSetProgram();
        bool EndSetProgram();
    }

    /// <summary>
    /// The Vst 2.3 Plugin commands
    /// </summary>
    public interface IVstPluginCommands23 : IVstPluginCommands21
    {
        bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output);
        int SetTotalSamplesToProcess(int numberOfSamples);
        int GetNextPlugin(out string name);
        int StartProcess();
        int StopProcess();
        bool SetPanLaw(VstPanLaw type, float value);
        VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo);
        VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo);
    }

    /// <summary>
    /// The Vst 2.4 Plugin commands
    /// </summary>
    public interface IVstPluginCommands24 : IVstPluginCommands23
    {
        bool SetProcessPrecision(VstProcessPrecision precision);
        int GetNumberOfMidiInputChannels();
        int GetNumberOfMidiOutputChannels();
    }

}
