namespace Jacobi.Vst.Core.Deprecated
{
    using System;

    /// <summary>
    /// Contains all the operations from VST 1.0 that were deprecated in VST 2.4.
    /// </summary>
    public interface IVstHostCommandsDeprecated10
    {
        /// <summary>
        /// Reports whether the spefied pin at the <paramref name="connectionIndex"/> is connected.
        /// </summary>
        /// <param name="connectionIndex">A zero-based index of the connection pin.</param>
        /// <param name="output">Report in output pins when True, otherwise (False) report on input pins.</param>
        /// <returns>Returns True when the pin is connected, otherwise False is returned.</returns>
        bool PinConnected(int connectionIndex, bool output);
    }

    /// <summary>
    /// Contains all the operations from VST 2.0 that were deprecated in VST 2.4.
    /// </summary>
    public interface IVstHostCommandsDeprecated20 : IVstHostCommandsDeprecated10
    {
        /// <summary>
        /// Indicates to the Host that the Plugin wants to process Midi events.
        /// </summary>
        /// <returns>Returns True when the call was successful.</returns>
        bool WantMidi();
        /// <summary>
        /// Sets a new time for the Host.
        /// </summary>
        /// <param name="timeInfo">Must not be null.</param>
        /// <param name="filterFlags">Unclear what the purpose is for these flags.</param>
        /// <returns>Returns True when the call was successful.</returns>
        bool SetTime(VstTimeInfo timeInfo, VstTimeInfoFlags filterFlags);
        /// <summary>
        /// Retrieves the tempo at specified <paramref name="sampleIndex"/> location.
        /// </summary>
        /// <param name="sampleIndex">A zero-based sample index.</param>
        /// <returns>Returns the tempo in bmp * 10000.</returns>
        int GetTempoAt(int sampleIndex); // bpm * 10000
        /// <summary>
        /// Returns the number of parameters that support automation.
        /// </summary>
        /// <returns>Returns the number of parameters that support automation.</returns>
        int GetAutomatableParameterCount();
        /// <summary>
        /// Returns the integer value for +1.0 representation,
        /// or 1 if full single float precision is maintained in automation.
        /// </summary>
        /// <param name="parameterIndex">A zero-based index into the parmeter collection or -1 for all/any.</param>
        /// <returns>Returns the integer value for +1.0 representation, or 1 if full single float precision is maintained in automation.</returns>
        int GetParameterQuantization(int parameterIndex);
        /// <summary>
        /// Indicates to the host that the Plugin needs idle calls (outside its editor window).
        /// </summary>
        /// <returns>Returns True when the call was successful.</returns>
        bool NeedIdle();
        /// <summary>
        /// Retrieves the previous Plugin based on the specified <paramref name="pinIndex"/>.
        /// </summary>
        /// <param name="pinIndex">A zero-based pin index. Specify -1 for next.</param>
        /// <returns>Return System.IntPtr.Zero when unsuccessful.</returns>
        IntPtr GetPreviousPlugin(int pinIndex); // AEffect*
        /// <summary>
        /// Retrieves the next Plugin based on the specified <paramref name="pinIndex"/>.
        /// </summary>
        /// <param name="pinIndex">A zero-based pin index. Specify -1 for next.</param>
        /// <returns>Return System.IntPtr.Zero when unsuccessful.</returns>
        IntPtr GetNextPlugin(int pinIndex); // AEffect*
        /// <summary>
        /// Returns an indication how the Host processes audio.
        /// </summary>
        /// <returns>Returns 0=Not Supported, 1=Replace, 2=Accumulate.</returns>
        int WillReplaceOrAccumulate();
        /// <summary>
        /// For variable IO. Sets the output sample rate.
        /// </summary>
        /// <param name="sampleRate">The sample rate.</param>
        /// <returns>Returns True when the call was successful.</returns>
        bool SetOutputSampleRate(float sampleRate);
        /// <summary>
        /// Gets the output speaker arrangement.
        /// </summary>
        /// <returns>Returns the speaker arrangement.</returns>
        VstSpeakerArrangement GetOutputSpeakerArrangement();
        /// <summary>
        /// Provides the host with an icon representation of the plugin.
        /// </summary>
        /// <param name="icon">Passes the icon Handle to the Host. Must not be null.</param>
        /// <returns>Returns True when the call was successful.</returns>
        bool SetIcon(IntPtr icon);
        /// <summary>
        /// Opens a new host window.
        /// </summary>
        /// <returns>Returns the Win32 HWND window handle.</returns>
        IntPtr OpenWindow();    // HWND
        /// <summary>
        /// Closes a window previously opened by <see cref="OpenWindow"/>.
        /// </summary>
        /// <param name="wnd">The window handle.</param>
        /// <returns>Returns True when the call was successful.</returns>
        bool CloseWindow(IntPtr wnd);
        /// <summary>
        /// Opens an audio editor window; defined by <paramref name="xml"/>.
        /// </summary>
        /// <param name="xml">Must not be null or empty.</param>
        /// <returns>Returns True when the call was successful.</returns>
        bool EditFile(string xml);
        /// <summary>
        /// Gets the native path of currently loading bank or project.
        /// </summary>
        /// <returns>Return the file path to the chunk file.</returns>
        /// <remarks>Call from within GetChunk.</remarks>
        string GetChunkFile();
        /// <summary>
        /// Gets the input speaker arrangement.
        /// </summary>
        /// <returns>Returns the speaker arrangement.</returns>
        VstSpeakerArrangement GetInputSpeakerArrangement();
    }
}
