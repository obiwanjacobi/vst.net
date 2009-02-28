#pragma once

#include "VstPluginContext.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	ref class VstManagedPluginContext : public VstPluginContext
	{
	public:
		VstManagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);

		void Initialize(System::String^ pluginPath);

		virtual void AcceptPluginInfoData(System::Boolean raiseEvents) override;

	};

}}}} // namespace Jacobi::Vst::Interop::Host
