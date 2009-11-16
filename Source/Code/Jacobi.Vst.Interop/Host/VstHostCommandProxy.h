#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

/// <summary>
/// The VstHostCommandProxy class dispatches incoming requests from the plugin to an implementation of
/// the <see cref="Jacobi::Vst::Core::Host::IVstHostCommandStub"/> interface.
/// </summary>
ref class VstHostCommandProxy
{
public:
	/// <summary>Construcs a new instance based on a reference to the <paramref name="hostCmdStub"/>.</summary>
	/// <param name="hostCmdStub">Must not be null.</param>
	VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
	/// <summary>Disposes managed resources and calls the finalizer.</summary>
	~VstHostCommandProxy();
	/// <summary>Disposes unmanaged resources.</summary>
	!VstHostCommandProxy();

	/// <summary>Dispatches the <paramref name="opcode"/> and its parameters to one of the methods on the
	/// <see cref="Jacobi::Vst::Core::Host::IVstHostCommandStub"/> interface.</summary>
	/// <param name="opcode">Indicates the type of message.</param>
	/// <param name="index">Optional argument.</param>
	/// <param name="value">Optional argument.</param>
	/// <param name="ptr">Optional argument.</param>
	/// <param name="opt">Optional argument.</param>
	/// <returns>Returns the return value of the method called on the 
	/// <see cref="Jacobi::Vst::Core::Host::IVstHostCommandStub"/> interface.</returns>
	VstIntPtr Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);

private:
	Jacobi::Vst::Core::Host::IVstHostCommandStub^ _hostCmdStub;
	Jacobi::Vst::Core::Deprecated::IVstHostCommandsDeprecated20^ _deprecatedCmdStub;

	::VstTimeInfo* _pTimeInfo;
	char* _directory;
	::VstSpeakerArrangement* _pArrangement;

	VstIntPtr DispatchDeprecated(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);
};

}}}} // Jacobi::Vst::Interop::Host