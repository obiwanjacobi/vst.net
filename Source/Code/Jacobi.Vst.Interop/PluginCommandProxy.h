#pragma once

ref class PluginCommandProxy
{
internal:
	PluginCommandProxy(Jacobi::Vst::Core::IVstPluginCommandStub^ cmdStub);

	VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);
	void Process(float** inputs, float** outputs, VstInt32 sampleFrames, int numInputs, int numOutputs);
	void Process(double** inputs, double** outputs, VstInt32 sampleFrames, int numInputs, int numOutputs);
	void SetParameter(VstInt32 index, float value);
	float GetParameter(VstInt32 index);

private:
	Jacobi::Vst::Core::IVstPluginCommandStub^ _commandStub;
};
