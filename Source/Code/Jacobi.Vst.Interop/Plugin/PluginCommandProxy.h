#pragma once

#include "..\MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

/// <summary>
/// The PluginCommandProxy dispatches calls to the Plugin.
/// </summary>
ref class PluginCommandProxy
{
internal:
	/// <summary>
	/// Constructs a new instance that calls the <paramref name="cmdStub"/>.
	/// </summary>
	PluginCommandProxy(Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ cmdStub);

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

private:
	void Cleanup();

	Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ _commandStub;
	MemoryTracker^ _memTracker;
};

}}}} // Jacobi::Vst::Interop::Plugin