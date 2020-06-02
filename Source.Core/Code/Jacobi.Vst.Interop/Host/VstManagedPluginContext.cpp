#include "pch.h"
#include "VstManagedPluginContext.h"
#include "..\Plugin\Configuration.h"
#include "..\Properties\Resources.h"
#include "..\Bootstrapper.h"
#include "..\Utils.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {


	VstManagedPluginContext::VstManagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
		: VstPluginContext(hostCmdStub)
	{
	}

	VstPluginContext^ VstManagedPluginContext::CreateInternal(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		if(System::IO::File::Exists(System::IO::Path::Combine(
			System::IO::Path::GetDirectoryName(pluginPath), 
			System::IO::Path::GetFileNameWithoutExtension(pluginPath) + Jacobi::Vst::Core::Plugin::ManagedPluginFactory::DefaultManagedExtension)))
		{
			return gcnew Jacobi::Vst::Host::Interop::VstManagedPluginContext(hostCmdStub);
		}

		return nullptr;
	}

	void VstManagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ pluginCmdStub = 
			Jacobi::Vst::Interop::Bootstrapper::LoadManagedPlugin(pluginPath, Jacobi::Vst::Plugin::Interop::Configuration::OpenConfig(basePath));

		if(pluginCmdStub == nullptr)
		{
			throw gcnew System::EntryPointNotFoundException(
				System::String::Format(
					Jacobi::Vst::Interop::Properties::Resources::VstManagedPluginContext_PluginCommandStubNotFound,
					pluginPath));
		}

		Jacobi::Vst::Core::Host::VstHostCommandAdapter^ hostAdapter = 
			Jacobi::Vst::Core::Host::VstHostCommandAdapter::Create(HostCommandStub);

		_internalPluginInfo = pluginCmdStub->GetPluginInfo(hostAdapter);
		Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^ deprecatedPluginInfo = 
			dynamic_cast<Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^>(_internalPluginInfo);

		if(deprecatedPluginInfo != nullptr)
		{
			PluginInfo = gcnew Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo();
		}
		else
		{
			PluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();
		}

		AcceptPluginInfoData(false);

		PluginCommandStub = Jacobi::Vst::Core::Host::VstPluginCommandAdapter::Create(pluginCmdStub);
		PluginCommandStub->PluginContext = this;

		Set(VstPluginContext::PluginPathContextVar, pluginPath);
	}

	void VstManagedPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^ deprecatedInfo =
			dynamic_cast<Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^>(PluginInfo);

		Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^ deprecatedInternalInfo =
			dynamic_cast<Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^>(_internalPluginInfo);

		System::Collections::Generic::List<System::String^>^ changedPropNames = 
			gcnew System::Collections::Generic::List<System::String^>();

		if(raiseEvents)
		{
			if(PluginInfo->Flags != safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_internalPluginInfo->Flags))
			{
				changedPropNames->Add("PluginInfo.Flags");
			}

			if(PluginInfo->ProgramCount != _internalPluginInfo->ProgramCount)
			{
				changedPropNames->Add("PluginInfo.ProgramCount");
			}

			if(PluginInfo->ParameterCount != _internalPluginInfo->ParameterCount)
			{
				changedPropNames->Add("PluginInfo.ParameterCount");
			}

			if(PluginInfo->AudioInputCount != _internalPluginInfo->AudioInputCount)
			{
				changedPropNames->Add("PluginInfo.AudioInputCount");
			}

			if(PluginInfo->AudioOutputCount != _internalPluginInfo->AudioOutputCount)
			{
				changedPropNames->Add("PluginInfo.AudioOutputCount");
			}

			if(PluginInfo->InitialDelay != _internalPluginInfo->InitialDelay)
			{
				changedPropNames->Add("PluginInfo.InitialDelay");
			}
			
			if(PluginInfo->PluginID != _internalPluginInfo->PluginID)
			{
				changedPropNames->Add("PluginInfo.PluginID");
			}

			if(PluginInfo->PluginVersion != _internalPluginInfo->PluginVersion)
			{
				changedPropNames->Add("PluginInfo.PluginVersion");
			}

			if(deprecatedInfo != nullptr)
			{
				if(deprecatedInfo->DeprecatedFlags != deprecatedInternalInfo->DeprecatedFlags)
				{
					changedPropNames->Add("PluginInfo.DeprecatedFlags");
				}

				if(deprecatedInfo->RealQualities != deprecatedInternalInfo->RealQualities)
				{
					changedPropNames->Add("PluginInfo.RealQualities");
				}

				if(deprecatedInfo->OfflineQualities != deprecatedInternalInfo->OfflineQualities)
				{
					changedPropNames->Add("PluginInfo.OfflineQualities");
				}

				if(deprecatedInfo->IoRatio != deprecatedInternalInfo->IoRatio)
				{
					changedPropNames->Add("PluginInfo.IoRatio");
				}
			}
		}

		// assign new values
		PluginInfo->Flags = safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_internalPluginInfo->Flags);
		PluginInfo->ProgramCount = _internalPluginInfo->ProgramCount;
		PluginInfo->ParameterCount = _internalPluginInfo->ParameterCount;
		PluginInfo->AudioInputCount = _internalPluginInfo->AudioInputCount;
		PluginInfo->AudioOutputCount = _internalPluginInfo->AudioOutputCount;
		PluginInfo->InitialDelay = _internalPluginInfo->InitialDelay;
		PluginInfo->PluginID = _internalPluginInfo->PluginID;
		PluginInfo->PluginVersion = _internalPluginInfo->PluginVersion;

		// deprecated fields
		if(deprecatedInfo != nullptr)
		{
			deprecatedInfo->DeprecatedFlags = deprecatedInternalInfo->DeprecatedFlags;
			deprecatedInfo->RealQualities = deprecatedInternalInfo->RealQualities;
			deprecatedInfo->OfflineQualities = deprecatedInternalInfo->OfflineQualities;
			deprecatedInfo->IoRatio = deprecatedInternalInfo->IoRatio;
		}

		// raise all the changed property events
		for each(System::String^ propName in changedPropNames)
		{
			RaisePropertyChanged(propName);
		}
	}

}}}} // Jacobi::Vst::Host::Interop