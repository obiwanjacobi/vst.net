namespace Jacobi.Vst.Core.Deprecated
{
    using System.Drawing;

    /// <summary>
    /// Contains all the operations from VST 1.0 that were deprecated in VST 2.4.
    /// </summary>
    public interface IVstPluginCommandsDeprecatedBase
    {
        /// <summary>
        /// Processes audio in an accumulating fashion.
        /// </summary>
        /// <param name="inputs">Audio input buffers. Must not be null.</param>
        /// <param name="outputs">Audio output buffers. Must not be null.</param>
        void ProcessAcc(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs);
    }

    /// <summary>
    /// Contains all the operations from VST 1.0 that were deprecated in VST 2.4.
    /// </summary>
    public interface IVstPluginCommandsDeprecated10 : IVstPluginCommandsDeprecatedBase
    {
        // VST 1.0

        /// <summary>
        /// Called if the VstPluginDeprecatedInfo.DeprecatedFlags has the "HasClip" or "HasVu" flags set.
        /// </summary>
        /// <returns>Returns the current Vu value.</returns>
        float GetVu();
        //void EditorDrawMac();
        //void EditorMouseMac();
        /// <summary>
        /// Called when a key stroke occurs in the editor.
        /// </summary>
        /// <param name="keycode">The key code value.</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool EditorKey(int keycode);
        /// <summary>
        /// The window that hosts the plugin editor is put on top of other windows.
        /// </summary>
        /// <returns>Returns true if the call was successful.</returns>
        bool EditorTop();
        /// <summary>
        /// The window that hosts the plugin editor is put in the background.
        /// </summary>
        /// <returns>Returns true if the call was successful.</returns>
        bool EditorSleep();
        /// <summary>
        /// Returns an identifaction code.
        /// </summary>
        /// <returns>Returns 'NvEf' as an integer.</returns>
        int Identify();
    }

    /// <summary>
    /// Contains all the operations from VST 2.0 that were deprecated in VST 2.4.
    /// </summary>
    public interface IVstPluginCommandsDeprecated20 : IVstPluginCommandsDeprecated10
    {
        // VST 2.0

        /// <summary>
        /// Retrieves the number of program categories.
        /// </summary>
        /// <returns>Returns the number of program categories.</returns>
        int GetProgramCategoriesCount();
        /// <summary>
        /// Copy the current program to the program at <paramref name="programIndex"/>.
        /// </summary>
        /// <param name="programIndex">A zero-based index into the program collection.</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool CopyCurrentProgramTo(int programIndex);
        /// <summary>
        /// Notifies the plugin of the fact that an input pin was dis/connected.
        /// </summary>
        /// <param name="inputIndex">A zero-based index into the input connection collection.</param>
        /// <param name="connected">Indicates if the pin was connected (True) or disconnected (False).</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool ConnectInput(int inputIndex, bool connected);
        /// <summary>
        /// Notifies the plugin of the fact that an output pin was dis/connected.
        /// </summary>
        /// <param name="inputIndex">A zero-based index into the output connection collection.</param>
        /// <param name="connected">Indicates if the pin was connected (True) or disconnected (False).</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool ConnectOutput(int outputIndex, bool connected);
        /// <summary>
        /// For external DSP.
        /// </summary>
        /// <returns>Returns the current position.</returns>
        /// <remarks>The <see cref="Jacobi.Vst.Core.Deprecated.VstPluginDeprecatedFlags"/>.ExtIsAsync 
        /// must be set in order for this method to be called.</remarks>
        int GetCurrentPosition();
        /// <summary>
        /// For external DSP.
        /// </summary>
        /// <returns>Returns the destination audio buffer.</returns>
        /// <remarks>The <see cref="Jacobi.Vst.Core.Deprecated.VstPluginDeprecatedFlags"/>.ExtHasBuffer 
        /// must be set in order for this method to be called.</remarks>
        VstAudioBuffer GetDestinationBuffer();
        /// <summary>
        /// Assigns a new block size and sample rate value to the plugin.
        /// </summary>
        /// <param name="blockSize">The number of samples per frame (cycle).</param>
        /// <param name="sampleRate">The new sample rate.</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool SetBlockSizeAndSampleRate(int blockSize, float sampleRate);
        /// <summary>
        /// Retrieves an error text from the plugin.
        /// </summary>
        /// <returns>Returns the error text.</returns>
        /// <remarks>The length of the text must not exceed 256 characters.</remarks>
        string GetErrorText(); // max 256 chars
        /// <summary>
        /// Called by the host to allow some light idle processing by the plugin.
        /// </summary>
        /// <returns>Returns True when subsequent Idle calls should follow. 
        /// False is returned when no further Idle processing is required.</returns>
        bool Idle();
        /// <summary>
        /// Retrieves an iconic representation of the plugin.
        /// </summary>
        /// <returns>Returns null when not supported.</returns>
        /// <remarks>The VST specs are not final for this method. Not supported.</remarks>
        Icon GetIcon();
        /// <summary>
        /// Moves the view to a new position inside the window.
        /// </summary>
        /// <param name="position">The x and y coordinates.</param>
        /// <returns>Returns true if the call was successful.</returns>
        bool SetViewPosition(ref Point position);
        /// <summary>
        /// Indicates if keys are required by the plugin.
        /// </summary>
        /// <returns>Returns true if keys are required.</returns>
        bool KeysRequired();
    }
}
