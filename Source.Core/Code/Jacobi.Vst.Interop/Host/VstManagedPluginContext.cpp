#include "pch.h"
#include "VstManagedPluginContext.h"
#include "..\Plugin\Configuration.h"
#include "..\Properties\Resources.h"
#include "..\Bootstrapper.h"
#include "..\Utils.h"

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

		if(!System::String::IsNullOrEmpty(fileFinder->Find(System::IO::Path::GetFileNameWithoutExtension(pluginPath))))
		{
			return gcnew Jacobi::Vst::Interop::Host::VstManagedPluginContext(hostCmdStub);
		}

		return nullptr;
	}

	void VstManagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		try
		{
			System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

			Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ pluginCmdStub = 
				Bootstrapper::LoadManagedPlugin(pluginPath, gcnew Jacobi::Vst::Interop::Plugin::Configuration(basePath));

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
		finally
		{
			// make sure the private paths are cleared once the plugin is fully loaded.
			Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->PrivateProbePaths->Clear();
		}
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

}}}} // namespace Jacobi::Vst::Interop::Host