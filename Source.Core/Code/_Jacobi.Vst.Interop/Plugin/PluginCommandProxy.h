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
		Vst2IntPtr Dispatch(int32_t opcode, int32_t index, Vst2IntPtr value, void* ptr, float opt);
		/// <summary>
		/// Calls the plugin for 32 bit audio processing.
		/// </summary>
		void Process(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs);
		/// <summary>
		/// Calls the plugin for 64 bit audio processing.
		/// </summary>
		void Process(double** inputs, double** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs);
		/// <summary>
		/// Calls the plugin to assign a new value to a parameter.
		/// </summary>
		void SetParameter(int32_t index, float value);
		/// <summary>
		/// Calls the plugin to retrieve a parameter value.
		/// </summary>
		float GetParameter(int32_t index);

		/// <summary>
		/// Calls the plugin for 32 bit accumulating audio processing (deprecated).
		/// </summary>
		void ProcessAcc(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs);

	private:
		void Cleanup();

		/// <summary>
		/// Dispatches the opcode to one of the Plugin deprecated methods.
		/// </summary>
		Vst2IntPtr DispatchDeprecated(Vst2PluginCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt);

		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ _commandStub;
		Jacobi::Vst::Core::Deprecated::IVstPluginCommandsDeprecated20^ _deprecatedCmdStub;

		MemoryTracker^ _memTracker;
		Vst2Rectangle* _pEditorRect;

		Jacobi::Vst::Core::Diagnostics::TraceContext^ _traceCtx;
	};

}}}} // Jacobi::Vst::Interop::Plugin