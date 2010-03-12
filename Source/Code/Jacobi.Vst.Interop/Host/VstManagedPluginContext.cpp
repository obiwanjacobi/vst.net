#include "StdAfx.h"
#include "VstManagedPluginContext.h"
//#include "..\AssemblyLoader.h"
#include "..\Properties\Resources.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	VstManagedPluginContext::VstManagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
		: VstPluginContext(hostCmdStub)
	{
	}

	VstPluginContext^ VstManagedPluginContext::CreateInternal(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		Jacobi::Vst::Core::Plugin::FileFinder^ fileFinder = Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->CreateFileFinder();
		fileFinder->Paths->Insert(0, System::IO::Path::GetDirectoryName(pluginPath));
		fileFinder->Extensions->Add(Jacobi::Vst::Core::Plugin::ManagedPluginFactory::DefaultManagedExtension);
		fileFinder->Extensions->Add(Jacobi::Vst::Core::Plugin::ManagedPluginFactory::AlternateManagedExtension);

		if(!System::String::IsNullOrEmpty(fileFinder->Find(System::IO::Path::GetFileNameWithoutExtension(pluginPath))))
		{
			return gcnew Jacobi::Vst::Interop::Host::VstManagedPluginContext(hostCmdStub);
		}

		return nullptr;
	}

	void VstManagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

		Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->PrivateProbePaths->Add(basePath);

		// TODO: add use of plugin config probe paths

		Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory =
			gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory();
		factory->LoadAssemblyByDefaultName(pluginPath);

		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ pluginCmdStub = factory->CreatePluginCommandStub();

		if(pluginCmdStub == nullptr)
		{
			throw gcnew System::EntryPointNotFoundException(
				System::String::Format(
					Jacobi::Vst::Interop::Properties::Resources::VstManagedPluginContext_PluginCommandStubNotFound,
					pluginPath));
		}

		Jacobi::Vst::Core::Host::VstHostCommandAdapter^ hostAdapter = 
			Jacobi::Vst::Core::Host::VstHostCommandAdapter::Create(HostCommandStub);

		PluginInfo = pluginCmdStub->GetPluginInfo(hostAdapter);

		PluginCommandStub = Jacobi::Vst::Core::Host::VstPluginCommandAdapter::Create(pluginCmdStub);
		PluginCommandStub->PluginContext = this;
	}

	void VstManagedPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		// TODO: can not implement this.
	}

}}}} // namespace Jacobi::Vst::Interop::Host