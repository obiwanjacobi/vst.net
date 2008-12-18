#pragma once

ref class VstHostCommandProxy
{
public:
	VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
	VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);

private:
	Jacobi::Vst::Core::Host::IVstHostCommandStub^ _hostCmdStub;
};