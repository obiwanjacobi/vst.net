namespace Jacobi.Vst.Core
{
    using System;
    using System.Drawing;

    public interface IVstPluginCommandStubBase
    {
        VstPluginInfo GetPluginInfo();

        void ProcessReplacing(VstAudioBuffer[] input);
        void ProcessReplacing(VstPrecisionAudioBuffer[] input);
        void SetParameter(int index, float value);
        float GetParameter(int index);
    }

    public interface IVstPluginCommandStub10 : IVstPluginCommandStubBase
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
        bool EditorGetRect(ref Rectangle rect);
        bool EditorOpen(IntPtr hWnd);
        void EditorClose();
        void EditorIdle();
        int GetChunk(out byte[] data, bool isPreset);
        int SetChunk(byte[] data, bool isPreset);
    }

    public interface IVstPluginCommandStub20 : IVstPluginCommandStub10
    {
        bool ProcessEvents(VstEvent[] events);
        bool CanParameterBeAutomated(int index);
        bool String2Parameter(int index, string str);
        bool GetProgramNameIndexed(int index, out string name);
        bool GetInputProperties(int index, VstPinProperties pinProps);
        bool GetOutputProperties(int index, VstPinProperties pinProps);
        int GetCategory();
        bool OfflineNotify(VstAudioFile[] audioFiles, int count, int startFlag);
        bool OfflinePrepare(/*VstOfflineTask[]*/ int count);
        bool OfflineRun(/*VstOfflineTask[]*/ int count);
        bool ProcessVariableIO(/*VstVariableIO* pVarIO*/);
        bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput);
        bool SetBypass(bool bypass);
        bool GetEffectName(out string name);
        bool GetVendorString(out string vendor);
        bool GetProductString(out string product);
        int GetVendorVersion();
        int CanDo(string cando);
        int GetTailSize();
        bool GetParameterProperties(int index, VstParameterProperties paramProps);
        int GetVstVersion();
    }

    public interface IVstPluginCommandStub21 : IVstPluginCommandStub20
    {
        bool EditorKeyDown(byte ascii, byte key, byte modifers);
        bool EditorKeyUp(byte ascii, byte key, byte modifers);
        bool SetEditorKnobMode(int mode);
        int GetMidiProgramName(VstMidiProgramName midiProgram, int channel);
        int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel);
        int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel);
        bool HasMidiProgramsChanged(int channel);
        bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel);
        bool BeginSetProgram();
        bool EndSetProgram();
    }

    public interface IVstPluginCommandStub23 : IVstPluginCommandStub21
    {
        bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output);
        int SetTotalSamplesToProcess(int numberOfSamples);
        int GetNextPlugin(out string name);
        int StartProcess();
        int StopProcess();
        bool SetPanLaw(int type, float value);
        int BeginLoadBank(VstPatchChunkInfo chunkInfo);
        int BeginLoadProgram(VstPatchChunkInfo chunkInfo);
    }

    public interface IVstPluginCommandStub : IVstPluginCommandStub23
    {
        bool SetProcessPrecision(VstProcessPrecision precision);
        int GetNumberOfMidiInputChannels();
        int GetNumberOfMidiOutputChannels();
    }
}
