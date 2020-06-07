#include "pch.h"
#include "VstManagedPluginContext.h"
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

		auto basePath = System::IO::Path::GetDirectoryName(pluginPath);
		auto pluginCmdStub = Bootstrapper::LoadManagedPlugin(pluginPath);

		if(pluginCmdStub == nullptr)
		{
			throw gcnew System::EntryPointNotFoundException(
				System::String::Format(
					Jacobi::Vst::Interop::Properties::Resources::VstManagedPluginContext_PluginCommandStubNotFound,
					pluginPath));
		}

		auto hostAdapter = gcnew Jacobi::Vst::Core::Host::VstHostCommandAdapter(HostCommandStub);

		_internalPluginInfo = pluginCmdStub->GetPluginInfo(hostAdapter);
		Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^ legacyPluginInfo = 
			dynamic_cast<Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^>(_internalPluginInfo);

		if(legacyPluginInfo != nullptr)
		{
			PluginInfo = gcnew Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo();
		}
		else
		{
			PluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();
		}

		AcceptPluginInfoData(false);

		PluginCommandStub = gcnew Jacobi::Vst::Core::Host::VstPluginCommandAdapter(pluginCmdStub);
		PluginCommandStub->PluginContext = this;

		Set(VstPluginContext::PluginPathContextVar, pluginPath);
	}

	void VstManagedPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		auto legacyInfo = dynamic_cast<Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^>(PluginInfo);
		auto legacyInternalInfo = dynamic_cast<Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^>(_internalPluginInfo);
		auto changedPropNames = gcnew System::Collections::Generic::List<System::String^>();

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

			if(legacyInfo != nullptr)
			{
				if(legacyInfo->LegacyFlags != legacyInternalInfo->LegacyFlags)
				{
					changedPropNames->Add("PluginInfo.LegacyFlags");
				}

				if(legacyInfo->RealQualities != legacyInternalInfo->RealQualities)
				{
					changedPropNames->Add("PluginInfo.RealQualities");
				}

				if(legacyInfo->OfflineQualities != legacyInternalInfo->OfflineQualities)
				{
					changedPropNames->Add("PluginInfo.OfflineQualities");
				}

				if(legacyInfo->IoRatio != legacyInternalInfo->IoRatio)
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

		// legacy fields
		if(legacyInfo != nullptr)
		{
			legacyInfo->LegacyFlags = legacyInternalInfo->LegacyFlags;
			legacyInfo->RealQualities = legacyInternalInfo->RealQualities;
			legacyInfo->OfflineQualities = legacyInternalInfo->OfflineQualities;
			legacyInfo->IoRatio = legacyInternalInfo->IoRatio;
		}

		// raise all the changed property events
		for each(System::String^ propName in changedPropNames)
		{
			RaisePropertyChanged(propName);
		}
	}

}}}} // Jacobi::Vst::Host::Interop