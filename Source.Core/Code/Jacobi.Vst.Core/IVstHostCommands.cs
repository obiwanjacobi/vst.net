namespace Jacobi.Vst.Core
{
    /// <summary>
    /// The Vst 1.0 Host commands
    /// </summary>
    public interface IVstHostCommands10
    {
        /// <summary>
        /// Notifies the Host that the value of the parameter at <paramref name="index"/> has a new <paramref name="value"/>.
        /// </summary>
        /// <param name="index">Must be greater/equal to zero and smaller than the parameter count.</param>
        /// <param name="value">The new value assigned to the parameter.</param>
        /// <remarks>The plugin can call this method to allow the parameter value change to be automated by the host.</remarks>
        void SetParameterAutomated(int index, float value);
        /// <summary>
        /// Retrieves the version number of the host.
        /// </summary>
        /// <returns>Usually the version number is in thousends. For example 1100 means version 1.1.0.0.</returns>
        int GetVersion();
        /// <summary>
        /// Retrieves the unique plugin ID of the current plugin.
        /// </summary>
        /// <returns>Returns the Four Character Code as an integer.</returns>
        int GetCurrentPluginID();
        /// <summary>
        /// Yield execution control to the host.
        /// </summary>
        void ProcessIdle();
    }

    /// <summary>
    /// The Vst 2.0 Host commands
    /// </summary>
    public interface IVstHostCommands20 : IVstHostCommands10
    {
        /// <summary>
        /// Retrieves time info in a specific format.
        /// </summary>
        /// <param name="filterFlags">Indicates the preferred time information format.</param>
        /// <returns>Returns time information but not necessarilly in the format specified by <paramref name="filterFlags"/>.</returns>
        VstTimeInfo GetTimeInfo(VstTimeInfoFlags filterFlags);
        /// <summary>
        /// Requests the host to process the <paramref name="events"/>.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        /// <returns>Returns true if supported by the host.</returns>
        bool ProcessEvents(VstEvent[] events);
        /// <summary>
        /// Notifies the host that the plugin IO has changed.
        /// </summary>
        /// <returns>Returns true if supported by the host.</returns>
        bool IoChanged();
        /// <summary>
        /// Sizes the Host window to the specified dimensions.
        /// </summary>
        /// <param name="width">Width of the window in pixels.</param>
        /// <param name="height">Height of the window in pixels.</param>
        /// <returns>Returns true if supported by the host.</returns>
        bool SizeWindow(int width, int height);
        /// <summary>
        /// Retrieves the current sample rate from the host.
        /// </summary>
        /// <returns>Returns the number of samples per second.</returns>
        float GetSampleRate();
        /// <summary>
        /// Retrieves the number of samples passed to the plugin during the audio processing cycles.
        /// </summary>
        /// <returns>Returns the number of samples.</returns>
        int GetBlockSize();
        /// <summary>
        /// Retrieves the latency concerning audio input.
        /// </summary>
        /// <returns>Returns the latency in number of samples?</returns>
        int GetInputLatency();
        /// <summary>
        /// Retrieves the latency concerning audio output.
        /// </summary>
        /// <returns>Returns the latency in number of samples?</returns>
        int GetOutputLatency();
        /// <summary>
        /// Returns an indication of what Host Thread is currently calling into the plugin.
        /// </summary>
        /// <returns>Returns a thread identifier.</returns>
        VstProcessLevels GetProcessLevel();
        /// <summary>
        /// Retrieves the level of automation supported by the host.
        /// </summary>
        /// <returns>Returns a value indicating the automation level.</returns>
        VstAutomationStates GetAutomationState();
        /// <summary>
        /// Retrieves the host vendor string.
        /// </summary>
        /// <returns>Never returns null?</returns>
        string GetVendorString();
        /// <summary>
        /// Retrieves the host product infotmation.
        /// </summary>
        /// <returns>Never returns null?</returns>
        string GetProductString();
        /// <summary>
        /// Retrieves the host version.
        /// </summary>
        /// <returns>Never returns 0 (zero).</returns>
        int GetVendorVersion();
        /// <summary>
        /// Queries the host for specific support.
        /// </summary>
        /// <param name="cando">A host capability.</param>
        /// <returns>Returns <see cref="VstCanDoResult.Yes"/> if the host supports the capability.</returns>
        VstCanDoResult CanDo(string cando);
        /// <summary>
        /// Retrieves the localized langauge of the host.
        /// </summary>
        /// <returns>Returns an value indicating the host UI language.</returns>
        VstHostLanguage GetLanguage();
        /// <summary>
        /// Retieves the base directory for the plugin.
        /// </summary>
        /// <returns>Returns a rooted path.</returns>
        string GetDirectory();
        /// <summary>
        /// Request the host to update its display.
        /// </summary>
        /// <returns>Returns true if supported by the host.</returns>
        bool UpdateDisplay();
        /// <summary>
        /// Notifies the host that the parameter at <paramref name="index"/> is about to be edited.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns true if supported by the host.</returns>
        bool BeginEdit(int index);
        /// <summary>
        /// Notifies the host that the parameter at <paramref name="index"/> was edited.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns true if supported by the host.</returns>
        bool EndEdit(int index);
        /// <summary>
        /// Opens the file selector in the host.
        /// </summary>
        /// <param name="fileSelect">A structure describing the options and settings.</param>
        /// <returns>Returns true if supported by the host.</returns>
        /// <remarks>On return (if true) the selected paths are filled in <paramref name="fileSelect"/>.</remarks>
        bool OpenFileSelector(VstFileSelect fileSelect);
        /// <summary>
        /// Cleans up the unmanaged resources consumed by a call to <see cref="OpenFileSelector"/>.
        /// </summary>
        /// <param name="fileSelect">The exact same instance that was also passed to <see cref="OpenFileSelector"/>.</param>
        /// <returns>Returns true if supported by the host.</returns>
        /// <remarks>This method must always be called when <see cref="OpenFileSelector"/> returned true. 
        /// Otherwise unmanaged memory will leak.</remarks>
        bool CloseFileSelector(VstFileSelect fileSelect);
    }
}
