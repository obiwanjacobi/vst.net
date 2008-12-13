#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	public ref class VstHostCommandProxy
	{
	public:
		VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);

	internal:
		VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);

	private:
		Jacobi::Vst::Core::Host::IVstHostCommandStub^ _hostCmdStub;
	};

}}}} // namespace Jacobi.Vst.Interop.Host