namespace Jacobi.Vst.Core
{
    /// <summary>
    /// The Vst 1.0 Host commands
    /// </summary>
    public interface IVstHostCommands10
    {
        void SetParameterAutomated(int index, float value);
        int GetVersion();
        int GetCurrentPluginID();
        void ProcessIdle();
    }

    /// <summary>
    /// The Vst 2.0 Host commands
    /// </summary>
    public interface IVstHostCommands20 : IVstHostCommands10
    {
        VstTimeInfo GetTimeInfo(VstTimeInfoFlags filterFlags);
        bool ProcessEvents(VstEvent[] events);
        bool IoChanged();
        bool SizeWindow(int width, int height);
        double GetSampleRate();
        int GetBlockSize();
        int GetInputLatency();
        int GetOutputLatency();
        VstProcessLevels GetProcessLevel();
        VstAutomationStates GetAutomationState();
        bool OfflineRead(VstOfflineTask task, VstOfflineOption option, bool readSource);
        bool OfflineWrite(VstOfflineTask task, VstOfflineOption option);
        bool OfflineStart(VstAudioFile[] files, int numberOfAudioFiles, int numberOfNewAudioFiles);
        int OfflineGetCurrentPass();
        int OfflineGetCurrentMetaPass();
        string GetVendorString();
        string GetProductString();
        int GetVendorVersion();
        VstCanDoResult CanDo(VstHostCanDo cando);
        VstHostLanguage GetLanguage();
        string GetDirectory();
        bool UpdateDisplay();
        bool BeginEdit(int index);
        bool EndEdit(int index);
        bool OpenFileSelector(/*VstFileSelect*/);
        bool CloseFileSelector(/*VstFileSelect*/);
    }
}
