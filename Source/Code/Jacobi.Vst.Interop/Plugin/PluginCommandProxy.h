#pragma once

#include "..\MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

	/// <summary>
	/// The PluginCommandProxy dispatches calls to the Plugin.
	/// </summary>
	ref class PluginCommandProxy : System::IDisposable
	{
	internal:
		/// <summary>
		/// Constructs a new instance that calls the <paramref name="cmdStub"/>.
		/// </summary>
		PluginCommandProxy(Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ cmdStub);
		~PluginCommandProxy();
		!PluginCommandProxy();

		/// <summary>
		/// Dispatches the opcode to one of the Plugin methods.
		/// </summary>
		VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);
		/// <summary>
		/// Calls the plugin for 32 bit audio processing.
		/// </summary>
		void Process(float** inputs, float** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs);
		/// <summary>
		/// Calls the plugin for 64 bit audio processing.
		/// </summary>
		void Process(double** inputs, double** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs);
		/// <summary>
		/// Calls the plugin to assign a new value to a parameter.
		/// </summary>
		void SetParameter(VstInt32 index, float value);
		/// <summary>
		/// Calls the plugin to retrieve a parameter value.
		/// </summary>
		float GetParameter(VstInt32 index);

		/// <summary>
		/// Calls the plugin for 32 bit accumulating audio processing (deprecated).
		/// </summary>
		void ProcessAcc(float** inputs, float** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs);

	private:
		void Cleanup();

		/// <summary>
		/// Dispatches the opcode to one of the Plugin deprecated methods.
		/// </summary>
		VstIntPtr DispatchDeprecated(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);

		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ _commandStub;
		Jacobi::Vst::Core::Deprecated::IVstPluginCommandsDeprecated20^ _deprecatedCmdStub;

		MemoryTracker^ _memTracker;
		ERect* _pEditorRect;

		Jacobi::Vst::Core::Diagnostics::TraceContext^ _traceCtx;
	};

}}}} // Jacobi::Vst::Interop::Plugin