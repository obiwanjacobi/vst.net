namespace Jacobi.Vst.Core
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The Vst Plugin callback functions
    /// </summary>
    public interface IVstPluginCommandsBase
    {
        /// <summary>
        /// Called by the host once every cycle to process incoming audio as well as output audio.
        /// </summary>
        /// <param name="inputs">An array with audio input buffers.</param>
        /// <param name="outputs">An array with audio output buffers.</param>
        void ProcessReplacing(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs);

        /// <summary>
        /// Called by the host once every cycle to process incoming audio as well as output audio.
        /// </summary>
        /// <param name="inputs">An array with audio input buffers.</param>
        /// <param name="outputs">An array with audio output buffers.</param>
        void ProcessReplacing(VstAudioPrecisionBuffer[] inputs, VstAudioPrecisionBuffer[] outputs);

        /// <summary>
        /// Called by the host to assign a new <paramref name="value"/> to the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-base index into the parameters collection.</param>
        /// <param name="value">The new value for the parameter.</param>
        void SetParameter(int index, float value);

        /// <summary>
        /// Called by the host to retrieve the current value of the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-base index into the parameters collection.</param>
        /// <returns>Returns 0.0 the value for the parameter at <paramref name="index"/>.</returns>
        float GetParameter(int index);
    }

    /// <summary>
    /// The Vst 1.0 Plugin commands
    /// </summary>
    public interface IVstPluginCommands10 : IVstPluginCommandsBase
    {
        /// <summary>
        /// This is the first method called by the host right after the assembly is loaded.
        /// </summary>
        void Open();

        /// <summary>
        /// This is the last method the host calls. Dispose your resources.
        /// </summary>
        void Close();

        /// <summary>
        /// The plugin should activate the Program at <paramref name="programNumber"/>.
        /// </summary>
        /// <param name="programNumber">A zero-based program number (index).</param>
        void SetProgram(int programNumber);

        /// <summary>
        /// Retrieve the current program index.
        /// </summary>
        /// <returns>Returns the current program index or 0 as a default.</returns>
        int GetProgram();

        /// <summary>
        /// Assign a new name to the current/active program.
        /// </summary>
        /// <param name="name">The new program name.</param>
        void SetProgramName(string name);

        /// <summary>
        /// Retrieves the name of the current/active program.
        /// </summary>
        /// <returns>Can return null or an empty string.</returns>
        string GetProgramName();

        /// <summary>
        /// Retrieves the label for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Can return null or an empty string.</returns>
        string GetParameterLabel(int index);

        /// <summary>
        /// Retrieves the display value for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Can return null or an empty string.</returns>
        string GetParameterDisplay(int index);

        /// <summary>
        /// Retrieves the name for the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Can return null or an empty string.</returns>
        string GetParameterName(int index);

        /// <summary>
        /// Assigns the <paramref name="sampleRate"/> to the plugin.
        /// </summary>
        /// <param name="sampleRate">The number of audio samples per second.</param>
        void SetSampleRate(float sampleRate);

        /// <summary>
        /// Assigns the <paramref name="blockSize"/> to the plugin.
        /// </summary>
        /// <param name="blockSize">The number samples per cycle.</param>
        void SetBlockSize(int blockSize);

        /// <summary>
        /// Called by the host when the users has turned the plugin on or off.
        /// </summary>
        /// <param name="onoff">True when on, false when off.</param>
        void MainsChanged(bool onoff);

        /// <summary>
        /// Called by the host to retrieve the bounding rectangle of the editor.
        /// </summary>
        /// <param name="rect">The rectangle receiving the bounds.</param>
        /// <returns>Returns true when the <paramref name="rect"/> was set.</returns>
        bool EditorGetRect(out Rectangle rect);

        /// <summary>
        /// Called by the host to open the plugin custom editor.
        /// </summary>
        /// <param name="hWnd">The handle to the parent window.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool EditorOpen(IntPtr hWnd);

        /// <summary>
        /// Called by the host to close (and destroy) the plugin custom editor.
        /// </summary>
        void EditorClose();

        /// <summary>
        /// Called by the host when the editor is idle.
        /// </summary>
        /// <remarks>Keep your processing short.</remarks>
        void EditorIdle();

        /// <summary>
        /// Called by the host to retrieve a buffer with Program (and Parameter) content.
        /// </summary>
        /// <param name="isPreset">True if only the current/active program should be serialized,
        /// otherwise (false) the complete program bank should be serialized.</param>
        /// <returns>Returns null when not implemented.</returns>
        byte[] GetChunk(bool isPreset);

        /// <summary>
        /// Called by the host to load in a previously serialized program buffer.
        /// </summary>
        /// <param name="data">The buffer provided by the host that contains the program data.</param>
        /// <param name="isPreset">True if only the current/active program should be deserialized,
        /// otherwise (false) the complete program bank should be deserialized.</param>
        /// <returns>Returns the number of bytes read from the <paramref name="data"/> buffer or
        /// zero not implemented.</returns>
        int SetChunk(byte[] data, bool isPreset);
    }

    /// <summary>
    /// The Vst 2.0 Plugin commands
    /// </summary>
    public interface IVstPluginCommands20 : IVstPluginCommands10
    {
        /// <summary>
        /// Called by the host when the plugin has specified the <see cref="VstPluginCanDo.ReceiveVstMidiEvent"/> flag.
        /// </summary>
        /// <param name="events">The (Midi) events for the current 'block'.</param>
        /// <returns>Returns false if not implemented.</returns>
        bool ProcessEvents(VstEvent[] events);

        /// <summary>
        /// Called by the host to query the plugin whether the parameter at <paramref name="index"/> can be automated.
        /// </summary>
        /// <param name="index">The zero-based index into the parameters.</param>
        /// <returns>Returns true if the parameter can be automated or false if not implemented.</returns>
        bool CanParameterBeAutomated(int index);

        /// <summary>
        /// Parses the <paramref name="str"/> value to assign to the parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-base parameter index.</param>
        /// <param name="str">The value for the parameter.</param>
        /// <returns>Returns true when the parameter was successfully parsed or
        /// false if not implemented.</returns>
        bool String2Parameter(int index, string str);

        /// <summary>
        /// Retrieves the name of the program at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-base index into the plugin Programs.</param>
        /// <returns>Returns null if not implemented.</returns>
        string GetProgramNameIndexed(int index);

        /// <summary>
        /// Retrieves the pin properties for the input at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin inputs.</param>
        /// <returns>Returns null if not implemented.</returns>
        VstPinProperties? GetInputProperties(int index);

        /// <summary>
        /// Retrieves the pin properties for the output at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin outputs.</param>
        /// <returns>Returns null if not implemented.</returns>
        VstPinProperties? GetOutputProperties(int index);

        /// <summary>
        /// Retrieves a categorization value for the plugin.
        /// </summary>
        /// <returns>Returns the plugin category.</returns>
        VstPluginCategory GetCategory();

        /// <summary>
        /// Under Construction
        /// </summary>
        /// <param name="saInput">Must not be null.</param>
        /// <param name="saOutput">Must not be null.</param>
        /// <returns>Returns false if not implemented.</returns>
        bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput);

        /// <summary>
        /// Called by the host to bypass plugin processing.
        /// </summary>
        /// <param name="bypass">True to bypass, false to process.</param>
        /// <returns>Returns false if not implemented.</returns>
        bool SetBypass(bool bypass);

        /// <summary>
        /// Called by the host to retrieve the name of plugin.
        /// </summary>
        /// <returns>Returns the name. Must not be null.</returns>
        /// <remarks>The plugin name should not exceed 31 characters.</remarks>
        string GetEffectName();

        /// <summary>
        /// Called to retrieve the plugin vendor information.
        /// </summary>
        /// <returns>Returns the Vendor name.</returns>
        string GetVendorString();

        /// <summary>
        /// Called to retrieve the plugin product information.
        /// </summary>
        /// <returns>Returns the Product name.</returns>
        string GetProductString();

        /// <summary>
        /// Called to retrieve the plugin version information.
        /// </summary>
        /// <returns>Returns the Version number.</returns>
        int GetVendorVersion();

        /// <summary>
        /// Called by the host to query the plugin if a certain behavior or aspect is supported.
        /// </summary>
        /// <param name="cando">The string containing the can-do string, which can be host specific.</param>
        /// <returns>Returns an indication if the capability is supported.</returns>
        VstCanDoResult CanDo(string cando);

        /// <summary>
        /// Called by the host to retrieve the number of samples that the plugin outputs after the input has gone silent.
        /// </summary>
        /// <returns>Returns zero if not implemented.</returns>
        int GetTailSize();

        /// <summary>
        /// Called by the host to retrieve information about a plugin parameter at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zero-based index into the plugin parameters.</param>
        /// <returns>Returns null if not implemented.</returns>
        VstParameterProperties? GetParameterProperties(int index);

        /// <summary>
        /// Called by the host to query the plugin what VST version it supports.
        /// </summary>
        /// <returns>Return 2400 for VST 2.4.</returns>
        int GetVstVersion();
    }

    /// <summary>
    /// The Vst 2.1 Plugin commands
    /// </summary>
    public interface IVstPluginCommands21 : IVstPluginCommands20
    {
        /// <summary>
        /// Called by the host when the user presses a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);

        /// <summary>
        /// Called by the host when the user releases a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);

        /// <summary>
        /// Called by the host to set the mode for turning knobs.
        /// </summary>
        /// <param name="mode">The mode to use for turning knobs.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool SetEditorKnobMode(VstKnobMode mode);

        /// <summary>
        /// Retrieves information about a midi program for a specific Midi <paramref name="channel"/>.
        /// </summary>
        /// <param name="midiProgramName">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the number of implemented Midi programs or 0 if not implemented.</returns>
        int GetMidiProgramName(VstMidiProgramName midiProgramName, int channel);

        /// <summary>
        /// Retrieves information about the current midi program for a specific Midi <paramref name="channel"/>.
        /// </summary>
        /// <param name="midiProgramName">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the number of implemented Midi programs or 0 if not implemented.</returns>
        int GetCurrentMidiProgramName(VstMidiProgramName midiProgramName, int channel);

        /// <summary>
        /// Retrieves information about a Midi Program Category.
        /// </summary>
        /// <param name="midiCat">Must not be null.</param>
        /// <param name="channel">The zero-based Midi channel.</param>
        /// <returns>Returns the total number of Midi program categories or 0 if not implemented.</returns>
        int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel);

        /// <summary>
        /// Indicates if the program for the specified Midi <paramref name="channel"/> has changed.
        /// </summary>
        /// <param name="channel">The zero-base Midi channel.</param>
        /// <returns>Returns true if the Midi Program has changed, otherwise false is returned.</returns>
        bool HasMidiProgramsChanged(int channel);

        /// <summary>
        /// Retrieves information about a Midi Key (or note).
        /// </summary>
        /// <param name="midiKeyName">Must not be null.</param>
        /// <param name="channel">The zero-base Midi channel.</param>
        /// <returns>Returns true when the <paramref name="midiKeyName"/>.Name was filled.</returns>
        bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel);

        /// <summary>
        /// Called by the host just before a new Program is set.
        /// </summary>
        /// <returns>Returns false when not implemented.</returns>
        bool BeginSetProgram();

        /// <summary>
        /// Called by the host just after a new Program is set.
        /// </summary>
        /// <returns>Returns false when not implemented.</returns>
        bool EndSetProgram();
    }

    /// <summary>
    /// The Vst 2.3 Plugin commands
    /// </summary>
    public interface IVstPluginCommands23 : IVstPluginCommands21
    {
        /// <summary>
        /// Returns the speaker arrangements for the input and output of the plugin.
        /// </summary>
        /// <param name="input">Filled with the speaker arrangement for the plugin inputs.</param>
        /// <param name="output">Filled with the speaker arrangement for the plugin outputs.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool GetSpeakerArrangement(out VstSpeakerArrangement? input, out VstSpeakerArrangement? output);

        /// <summary>
        /// Retrieves the unique plugin Id and the name of the next (sub) plugin.
        /// </summary>
        /// <param name="name">Filled with the name of the next sub-plugin</param>
        /// <returns>Returns the unique plugin id.</returns>
        int GetNextPlugin(out string name);

        /// <summary>
        /// Called just before the first call to Process is made.
        /// </summary>
        /// <returns>It is unclear what this return value represents.</returns>
        int StartProcess();

        /// <summary>
        /// Called just after the last call to Process is made.
        /// </summary>
        /// <returns>It is unclear what this return value represents.</returns>
        int StopProcess();

        /// <summary>
        /// Informs the plugin of the pan algorithm to use.
        /// </summary>
        /// <param name="type">The pan algorithm type.</param>
        /// <param name="gain">A gain factor.</param>
        /// <returns>Returns false when not implemented.</returns>
        bool SetPanLaw(VstPanLaw type, float gain);

        /// <summary>
        /// Called by the host to query the plugin that supports persistence if the chunk can be read.
        /// </summary>
        /// <param name="chunkInfo">Must not be null.</param>
        /// <returns>Returns <see cref="VstCanDoResult.Yes"/> if the plugin can read the data.</returns>
        VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo);

        /// <summary>
        /// Called by the host to query the plugin that supports persistence if the chunk can be read.
        /// </summary>
        /// <param name="chunkInfo">Must not be null.</param>
        /// <returns>Returns <see cref="VstCanDoResult.Yes"/> if the plugin can read the data.</returns>
        VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo);
    }

    /// <summary>
    /// The Vst 2.4 Plugin commands
    /// </summary>
    public interface IVstPluginCommands24 : IVstPluginCommands23
    {
        /// <summary>
        /// Called by the host query inform the plugin on the precision of audio processing it supports.
        /// </summary>
        /// <param name="precision">An indication of either 32 bit or 64 bit samples.</param>
        /// <returns>Returns true when the requested <paramref name="precision"/> is supported.</returns>
        bool SetProcessPrecision(VstProcessPrecision precision);

        /// <summary>
        /// Called by the host to retrieve the number of Midi In channels the plugin supports.
        /// </summary>
        /// <returns>Returns the number of Midi In channels, or 0 (zero) if not supported.</returns>
        int GetNumberOfMidiInputChannels();

        /// <summary>
        /// Called by the host to retrieve the number of Midi Out channels the plugin supports.
        /// </summary>
        /// <returns>Returns the number of Midi Out channels, or 0 (zero) if not supported.</returns>
        int GetNumberOfMidiOutputChannels();
    }
}