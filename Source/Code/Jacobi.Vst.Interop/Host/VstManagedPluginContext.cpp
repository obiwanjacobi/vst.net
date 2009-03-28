#include "StdAfx.h"
#include "VstManagedPluginContext.h"
#include "..\AssemblyLoader.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	VstManagedPluginContext::VstManagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
		: VstPluginContext(hostCmdStub)
	{
	}

	VstPluginContext^ VstManagedPluginContext::Create(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);
		System::String^ baseName = System::IO::Path::GetFileNameWithoutExtension(pluginPath);
		System::String^ fileName = System::IO::Path::Combine(basePath, baseName);
		System::String^ filePath = fileName + Jacobi::Vst::Core::Plugin::ManagedPluginFactory::DefaultManagedExtension;

		if(!System::IO::File::Exists(filePath))
		{
			filePath = fileName + Jacobi::Vst::Core::Plugin::ManagedPluginFactory::AlternateManagedExtension;
		}

		if(System::IO::File::Exists(filePath))
		{
			return gcnew Jacobi::Vst::Interop::Host::VstManagedPluginContext(hostCmdStub);
		}

		return nullptr;
	}

	void VstManagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

		Jacobi::Vst::Interop::AssemblyLoader::Initialize(basePath);

		Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory =
			gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory(pluginPath);

		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ pluginCmdStub = factory->CreatePluginCommandStub();

		if(pluginCmdStub == nullptr)
		{
			throw gcnew System::EntryPointNotFoundException(pluginPath + " does not have a public implementation of the IVstPluginCommandStub interface.");
		}

		Jacobi::Vst::Core::Host::VstHostCommandAdapter^ hostAdapter = 
			gcnew Jacobi::Vst::Core::Host::VstHostCommandAdapter(HostCommandStub);

		PluginInfo = pluginCmdStub->GetPluginInfo(hostAdapter);

		PluginCommandStub = gcnew Jacobi::Vst::Core::Host::VstPluginCommandAdapter(pluginCmdStub);
		PluginCommandStub->PluginContext = this;
	}

	void VstManagedPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		// TODO: can not implement this.
	}

}}}} // namespace Jacobi::Vst::Interop::Host