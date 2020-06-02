#include "pch.h"
#include "../Bootstrapper.h"
#include "PluginCommandProxy.h"
#include "HostCommandStub.h"
#include "../TimeCriticalScope.h"
#include "../Utils.h"
#include "../Properties/Resources.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

	// fwd ref
	Vst2Plugin* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo);

// main exported method called by host to create the plugin
Vst2Plugin* VSTPluginMain (Vst2HostCommand hostCommandHandler)
{
	// create the host command stub (sends commands to host)
	Jacobi::Vst::Plugin::Interop::HostCommandStub^ hostStub =
		gcnew Jacobi::Vst::Plugin::Interop::HostCommandStub(hostCommandHandler);

	try
	{
		// retrieve the current plugin file name (interop)
		System::String^ interopAssemblyFileName = Utils::GetCurrentFileName();

		// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = Bootstrapper::LoadManagedPlugin(interopAssemblyFileName);
		
		if(commandStub != nullptr)
		{
			// retrieve the plugin info
			Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo = commandStub->GetPluginInfo(hostStub);

			if(pluginInfo)
			{
				// create the native audio effect struct based on the plugin info
				Vst2Plugin* pPlugin = CreateAudioEffectInfo(pluginInfo);

				// initialize host stub with plugin info
				hostStub->Initialize(pPlugin);

				// connect the plugin command stub to the command proxy and construct a handle
				System::Runtime::InteropServices::GCHandle proxyHandle = 
					System::Runtime::InteropServices::GCHandle::Alloc(
						gcnew Jacobi::Vst::Plugin::Interop::PluginCommandProxy(commandStub), 
						System::Runtime::InteropServices::GCHandleType::Normal);

				// maintain the proxy reference as part of the effect struct
				pPlugin->user = System::Runtime::InteropServices::GCHandle::ToIntPtr(proxyHandle).ToPointer();

				return pPlugin;
			}
			else
			{
				Utils::ShowWarning(Jacobi::Vst::Interop::Properties::Resources::VstInteropMain_GetPluginInfoNull);
			}
		}
		else
		{
			Utils::ShowWarning(Jacobi::Vst::Interop::Properties::Resources::VstInteropMain_CouldNotCreatePluginCmdStub);
		}
	}
	catch(System::Exception^ exc)
	{
		delete hostStub;

		Utils::ShowError(exc);
	}

	return NULL;
}

// Dispatcher Procedure called by the host
Vst2IntPtr DispatcherProc(Vst2Plugin* pluginInfo, Vst2PluginCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->Dispatch(safe_cast<int32_t>(command), index, value, ptr, opt);
	}

	return 0;
}

// Audio processing Procedure called by the host
void Process32Proc(Vst2Plugin* pluginInfo, float** inputs, float** outputs, int32_t sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->inputCount, pluginInfo->outputCount);
	}
}

// Audio precision processing Procedure called by the host
void Process64Proc(Vst2Plugin* pluginInfo, double** inputs, double** outputs, int32_t sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->inputCount, pluginInfo->outputCount);
	}
}

// Parameter assignment Procedure called by the host
void SetParameterProc(Vst2Plugin* pluginInfo, int32_t index, float value)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->SetParameter(index, value);
	}
}

// Parameter retrieval Procedure called by the host
float GetParameterProc(Vst2Plugin* pluginInfo, int32_t index)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->GetParameter(index);
	}

	return 0.0;
}

// Audio processing (accumulating)  Procedure called by the host
void Process32AccProc(Vst2Plugin* pluginInfo, float** inputs, float** outputs, int32_t sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Plugin::Interop::PluginCommandProxy^ proxy = (Jacobi::Vst::Plugin::Interop::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->ProcessAcc(inputs, outputs, sampleFrames, pluginInfo->inputCount, pluginInfo->outputCount);
	}
}

// Helper method to create the Vst2Plugin structure based on the pluginInfo.
Vst2Plugin* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo)
{
	// deleted in the Finalizer method of the HostCommandStub
	Vst2Plugin* pEffect = new Vst2Plugin();
	ZeroMemory(pEffect, sizeof(Vst2Plugin));
	pEffect->VstP = Vst2FourCharacterCode;

	// assign function pointers
	pEffect->command = Jacobi::Vst::Interop::DispatcherProc;
	pEffect->replace = Jacobi::Vst::Interop::Process32Proc;
	pEffect->replaceDouble = Jacobi::Vst::Interop::Process64Proc;
	pEffect->parameterSet = Jacobi::Vst::Interop::SetParameterProc;
	pEffect->parameterGet = Jacobi::Vst::Interop::GetParameterProc;

	// assign info data
	pEffect->flags = (Vst2PluginFlags)pluginInfo->Flags;
	pEffect->startupDelay = pluginInfo->InitialDelay;
	pEffect->inputCount = pluginInfo->AudioInputCount;
	pEffect->outputCount = pluginInfo->AudioOutputCount;
	pEffect->parameterCount = pluginInfo->ParameterCount;
	pEffect->programCount = pluginInfo->ProgramCount;
	pEffect->id = pluginInfo->PluginID;
	pEffect->version = pluginInfo->PluginVersion;

	// check for deprecated members
	Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^ deprecatedInfo =
		dynamic_cast<Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^>(pluginInfo);

	if(deprecatedInfo != nullptr)
	{
		// hook up the old accumulating process proc when deprecated PluginInfo is passed in
		pEffect->process = Jacobi::Vst::Interop::Process32AccProc;
		pEffect->realQualities = deprecatedInfo->RealQualities;
		pEffect->offQualities = deprecatedInfo->OfflineQualities;
		pEffect->ioRatio = deprecatedInfo->IoRatio;
	}

	return pEffect;
}

}}} // Jacobi::Vst::Interop