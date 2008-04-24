#pragma once

using namespace Jacobi::Vst::Core;

ref class PluginCommandProxy
{
public:
	PluginCommandProxy(IVstPluginCommandStub^ cmdStub);

internal:
	VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);

private:
	IVstPluginCommandStub^ _commandStub;
};
