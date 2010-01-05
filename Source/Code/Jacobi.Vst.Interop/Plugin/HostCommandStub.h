#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

	/// <summary>
	/// The HostCommandStub calls the host callback function.
	/// </summary>
	ref class HostCommandStub : Jacobi::Vst::Core::Plugin::IVstHostCommandStub, 
		Jacobi::Vst::Core::Deprecated::IVstHostCommandsDeprecated20
	{
	public:
		/// <summary>
		/// Disposes the managed resources and calls the finalizer.
		/// </summary>
		~HostCommandStub();
		/// <summary>
		/// Disposes the unmanaged resources.
		/// </summary>
		!HostCommandStub();

		// IVstHostCommandStub
		/// <summary>
		/// Returns true when the HostCommandStub has been fully initialized (the execution path has left the VSTPluginMain).
		/// </summary>
		virtual System::Boolean IsInitialized() { return (_pluginInfo != NULL); }
		/// <summary>
		/// Updates the unmanaged <b>AEffect</b> structure with the new values in the <paramref name="pluginInfo"/>.
		/// </summary>
		/// <param name="pluginInfo">Must not be null.</param>
		virtual System::Boolean UpdatePluginInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo);
		
		// IVstHostCommands10
		/// <summary>
        /// Notifies the Host that the value of the parameter at <paramref name="index"/> has a new <paramref name="value"/>.
        /// </summary>
        /// <param name="index">Must be greater than zero and smaller than the parameter count.</param>
        /// <param name="value">The new value assigned to the parameter.</param>
        /// <remarks>The plugin can call this method to allow the parameter value change to be automated by the host.</remarks>
		virtual void SetParameterAutomated(System::Int32 index, System::Single value);
		/// <summary>
        /// Retrieves the version number of the host.
        /// </summary>
        /// <returns>Usually the version number is in thousends. For example 1100 means version 1.1.0.0.</returns>
		virtual System::Int32 GetVersion();
		/// <summary>
        /// Retrieves the unique plugin ID of the current plugin.
        /// </summary>
        /// <returns>Returns the Four Character Code as an integer.</returns>
		virtual System::Int32 GetCurrentPluginID();
		/// <summary>
        /// Yield execution control to the host.
        /// </summary>
		virtual void ProcessIdle();
		
		// IVstHostCommands20
		/// <summary>
        /// Retrieves time info in a specific format.
        /// </summary>
        /// <param name="filterFlags">Indicates the preferred time information format.</param>
        /// <returns>Returns time information but not necessarilly in the format specified by <paramref name="filterFlags"/>.</returns>
		virtual Jacobi::Vst::Core::VstTimeInfo^ GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags);
		/// <summary>
        /// Requests the host to process the <paramref name="events"/>.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events);
		/// <summary>
        /// Notifies the host that the plugin IO has changed.
        /// </summary>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean IoChanged();
		/// <summary>
        /// Sizes the Host window to the specified dimensions.
        /// </summary>
        /// <param name="width">Width of the window in pixels.</param>
        /// <param name="height">Height of the window in pixels.</param>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean SizeWindow(System::Int32 width, System::Int32 height);
		/// <summary>
        /// Retrieves the current sample rate from the host.
        /// </summary>
        /// <returns>Returns the number of samples per second.</returns>
		virtual System::Single GetSampleRate();
		/// <summary>
        /// Retrieves the number of samples passed to the plugin during the audio processing cycles.
        /// </summary>
        /// <returns>Returns the number of samples.</returns>
		virtual System::Int32 GetBlockSize();
		/// <summary>
        /// Retrieves the latency concerning audio input.
        /// </summary>
        /// <returns>Returns the latency in number of samples?</returns>
		virtual System::Int32 GetInputLatency();
		/// <summary>
        /// Retrieves the latency concerning audio output.
        /// </summary>
        /// <returns>Returns the latency in number of samples?</returns>
		virtual System::Int32 GetOutputLatency();
		/// <summary>
        /// Returns an indication of what Host Thread is currently calling into the plugin.
        /// </summary>
        /// <returns>Returns a thread identifier.</returns>
		virtual Jacobi::Vst::Core::VstProcessLevels GetProcessLevel();
		/// <summary>
        /// Retrieves the level of automation supported by the host.
        /// </summary>
        /// <returns>Returns a value indicating the automation level.</returns>
		virtual Jacobi::Vst::Core::VstAutomationStates GetAutomationState();
		//virtual System::Boolean OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource);
		//virtual System::Boolean OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option);
		//virtual System::Boolean OfflineStart(array<Jacobi::Vst::Core::VstAudioFile^>^ files, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles);
		//virtual System::Int32 OfflineGetCurrentPass();
		//virtual System::Int32 OfflineGetCurrentMetaPass();
		/// <summary>
        /// Retrieves the host vendor string.
        /// </summary>
        /// <returns>Never returns null?</returns>
		virtual System::String^ GetVendorString();
		/// <summary>
        /// Retrieves the host product infotmation.
        /// </summary>
        /// <returns>Never returns null?</returns>
		virtual System::String^ GetProductString();
		/// <summary>
        /// Retrieves the host version.
        /// </summary>
        /// <returns>Never returns 0 (zero).</returns>
		virtual System::Int32 GetVendorVersion();
		/// <summary>
        /// Queries the host for specific support.
        /// </summary>
        /// <param name="cando">A host capability.</param>
		/// <returns>Returns <see cref="Jacobi::Vst::Core::VstCanDoResult"/><b>.Yes</b> if the host supports the capability.</returns>
		virtual Jacobi::Vst::Core::VstCanDoResult CanDo(System::String^ cando);
		/// <summary>
        /// Retrieves the localized langauge of the host.
        /// </summary>
        /// <returns>Returns an value indicating the host UI language.</returns>
		virtual Jacobi::Vst::Core::VstHostLanguage GetLanguage();
		/// <summary>
        /// Retieves the base directory for the plugin.
        /// </summary>
        /// <returns>Returns a rooted path.</returns>
		virtual System::String^ GetDirectory();
		/// <summary>
        /// Request the host to update its display.
        /// </summary>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean UpdateDisplay();
		/// <summary>
        /// Notifies the host that the parameter at <paramref name="index"/> is about to be edited.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean BeginEdit(System::Int32 index);
		/// <summary>
        /// Notifies the host that the parameter at <paramref name="index"/> was edited.
        /// </summary>
        /// <param name="index">A zero-based index into the parameter collection.</param>
        /// <returns>Returns true if supported by the host.</returns>
		virtual System::Boolean EndEdit(System::Int32 index);
		/// <summary>
        /// Opens the file selector in the host.
        /// </summary>
        /// <param name="fileSelect">A structure describing the options and settings.</param>
        /// <returns>Returns true if supported by the host.</returns>
        /// <remarks>On return (if true) the selected paths are filled in <paramref name="fileSelect"/>.</remarks>
		virtual System::Boolean OpenFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect);
		/// <summary>
        /// Cleans up the unmanaged resources consumed by a call to <see cref="OpenFileSelector"/>.
        /// </summary>
        /// <param name="fileSelect">The exact same instance that was also passed to <see cref="OpenFileSelector"/>.</param>
        /// <returns>Returns true if supported by the host.</returns>
        /// <remarks>This method must always be called when <see cref="OpenFileSelector"/> returned true. 
        /// Otherwise unmanaged memory will leak.</remarks>
		virtual System::Boolean CloseFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect);

		//
		// Deprecated method support (VST 2.4)
		//

		// IVstHostCommandsDeprecated10
		/// <summary>
		/// Reports whether the spefied pin at the <paramref name="connectionIndex"/> is connected.
		/// </summary>
		/// <param name="connectionIndex">A zero-based index of the connection pin.</param>
		/// <param name="output">Report in output pins when True, otherwise (False) report on input pins.</param>
		/// <returns>Returns True when the pin is connected, otherwise False is returned.</returns>
		virtual System::Boolean PinConnected(System::Int32 connectionIndex, System::Boolean output);

		// IVstHostCommandsDeprecated20
		/// <summary>
		/// Indicates to the Host that the Plugin wants to process Midi events.
		/// </summary>
		/// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean WantMidi();
		/// <summary>
		/// Sets a new time for the Host.
		/// </summary>
		/// <param name="timeInfo">Must not be null.</param>
		/// <param name="filterFlags">Unclear what the purpose is for these flags.</param>
		/// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean SetTime(Jacobi::Vst::Core::VstTimeInfo^ timeInfo, Jacobi::Vst::Core::VstTimeInfoFlags filterFlags);
		/// <summary>
		/// Retrieves the tempo at specified <paramref name="sampleIndex"/> location.
		/// </summary>
		/// <param name="sampleIndex">A zero-based sample index.</param>
		/// <returns>Returns the tempo in bmp * 10000.</returns>
        virtual System::Int32 GetTempoAt(System::Int32 sampleIndex); // bpm * 10000
		/// <summary>
		/// Returns the number of parameters that support automation.
		/// </summary>
		/// <returns>Returns the number of parameters that support automation.</returns>
        virtual System::Int32 GetAutomatableParameterCount();
		/// <summary>
		/// Returns the integer value for +1.0 representation,
		/// or 1 if full single float precision is maintained in automation.
		/// </summary>
		/// <param name="parameterIndex">A zero-based index into the parmeter collection or -1 for all/any.</param>
		/// <returns>Returns the integer value for +1.0 representation, or 1 if full single float precision is maintained in automation.</returns>
        virtual System::Int32 GetParameterQuantization(System::Int32 parameterIndex);
		/// <summary>
		/// Indicates to the host that the Plugin needs idle calls (outside its editor window).
		/// </summary>
		/// <returns>Returns True when the call was successful.</returns>
        virtual System::Boolean NeedIdle();
		/// <summary>
		/// Retrieves the previous Plugin based on the specified <paramref name="pinIndex"/>.
		/// </summary>
		/// <param name="pinIndex">A zero-based pin index. Specify -1 for next.</param>
		/// <returns>Return System.IntPtr.Zero when unsuccessful.</returns>
        virtual System::IntPtr GetPreviousPlugin(System::Int32 pinIndex); // AEffect*
		/// <summary>
		/// Retrieves the next Plugin based on the specified <paramref name="pinIndex"/>.
		/// </summary>
		/// <param name="pinIndex">A zero-based pin index. Specify -1 for next.</param>
		/// <returns>Return System.IntPtr.Zero when unsuccessful.</returns>
        virtual System::IntPtr GetNextPlugin(System::Int32 pinIndex); // AEffect*
		/// <summary>
		/// Returns an indication how the Host processes audio.
		/// </summary>
		/// <returns>Returns 0=Not Supported, 1=Replace, 2=Accumulate.</returns>
        virtual System::Int32 WillReplaceOrAccumulate();
		/// <summary>
        /// For variable IO. Sets the output sample rate.
        /// </summary>
        /// <param name="sampleRate">The sample rate.</param>
        /// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean SetOutputSampleRate(System::Single sampleRate);
		/// <summary>
        /// Gets the output speaker arrangement.
        /// </summary>
        /// <returns>Returns the speaker arrangement.</returns>
        virtual Jacobi::Vst::Core::VstSpeakerArrangement^ GetOutputSpeakerArrangement();
		/// <summary>
        /// Provides the host with an icon representation of the plugin.
        /// </summary>
        /// <param name="icon">Passes the icon Handle to the Host. Must not be null.</param>
        /// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean SetIcon(System::Drawing::Icon^ icon);
		/// <summary>
        /// Opens a new host window.
        /// </summary>
        /// <returns>Returns the Win32 HWND window handle.</returns>
        virtual System::IntPtr OpenWindow();    // HWND
		/// <summary>
        /// Closes a window previously opened by <see cref="OpenWindow"/>.
        /// </summary>
        /// <param name="wnd">The window handle.</param>
        /// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean CloseWindow(System::IntPtr wnd);
		/// <summary>
        /// Opens an audio editor window; defined by <paramref name="xml"/>.
        /// </summary>
        /// <param name="xml">Must not be null or empty.</param>
        /// <returns>Returns True when the call was successful.</returns>
		virtual System::Boolean EditFile(System::String^ xml);
		/// <summary>
        /// Gets the native path of currently loading bank or project.
        /// </summary>
        /// <returns>Return the file path to the chunk file.</returns>
        /// <remarks>Call from within GetChunk.</remarks>
		virtual System::String^ GetChunkFile();
		/// <summary>
        /// Gets the input speaker arrangement.
        /// </summary>
        /// <returns>Returns the speaker arrangement.</returns>
		virtual Jacobi::Vst::Core::VstSpeakerArrangement^ GetInputSpeakerArrangement();

	internal:
		HostCommandStub(::audioMasterCallback hostCallback);
		void Initialize(AEffect* pluginInfo) { if(pluginInfo == NULL) { throw gcnew System::ArgumentNullException("pluginInfo"); } _pluginInfo = pluginInfo; }

	private:
		AEffect* _pluginInfo;
		audioMasterCallback _hostCallback;

		void ThrowIfNotInitialized();
		VstIntPtr CallHost(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
		{
			_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

			VstIntPtr result = _hostCallback(_pluginInfo, opcode, index, value, ptr, opt);

			_traceCtx->WriteDispatchEnd(System::IntPtr(result));

			return result;
		}

		Jacobi::Vst::Core::Diagnostics::TraceContext^ _traceCtx;
	};

}}}} // Jacobi::Vst::Interop::Plugin