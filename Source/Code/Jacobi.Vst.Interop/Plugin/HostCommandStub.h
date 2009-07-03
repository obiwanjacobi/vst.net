#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

	/// <summary>
	/// The HostCommandStub calls the host callback function.
	/// </summary>
	ref class HostCommandStub : Jacobi::Vst::Core::Plugin::IVstHostCommandStub, 
		Jacobi::Vst::Core::IVstHostCommandsDeprecated
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
		virtual Jacobi::Vst::Core::VstCanDoResult CanDo(Jacobi::Vst::Core::VstHostCanDo cando);
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

		/// <summary>
		/// Indicates to the Host that the Plugin wants to process Midi events.
		/// </summary>
		virtual System::Void WantMidi();

	internal:
		HostCommandStub(::audioMasterCallback hostCallback);
		void Initialize(AEffect* pluginInfo) { if(pluginInfo == NULL) { throw gcnew System::ArgumentNullException("pluginInfo"); } _pluginInfo = pluginInfo; }

	private:
		AEffect* _pluginInfo;
		audioMasterCallback _hostCallback;

		void ThrowIfNotInitialized();
		VstIntPtr CallHost(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt) { return _hostCallback(_pluginInfo, opcode, index, value, ptr, opt); }
	};

}}}} // Jacobi::Vst::Interop::Plugin