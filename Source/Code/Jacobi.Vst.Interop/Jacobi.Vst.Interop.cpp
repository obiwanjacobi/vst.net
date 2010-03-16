// Jacobi.Vst.Interop.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Bootstrapper.h"
#include "Plugin/PluginCommandProxy.h"
#include "Plugin/HostCommandStub.h"
#include "TimeCriticalScope.h"
#include "Utils.h"
#include "Properties/Resources.h"
#include<vcclr.h>

namespace Jacobi {
namespace Vst {
namespace Interop {

// fwd ref
AEffect* VSTPluginMainInternal (Bootstrapper^ bootstrapper, audioMasterCallback hostCallback);

// main exported method called by host to create the plugin
AEffect* VSTPluginMain (audioMasterCallback hostCallback)
{
	Bootstrapper^ bootstrapper = nullptr;

	try
	{
		// retrieve the current plugin file name (interop)
		System::String^ interopAssemblyFileName = Utils::GetCurrentFileName();

		// try to locate the plugin specific config file
		Jacobi::Vst::Interop::Plugin::Configuration^ config = 
			gcnew Jacobi::Vst::Interop::Plugin::Configuration(interopAssemblyFileName);

		// create the bootstrapper and register with the AssemlbyResolve event
		bootstrapper = gcnew Bootstrapper(System::IO::Path::GetDirectoryName(interopAssemblyFileName), config);

		//
		// We have boot-strapped (above).
		// Now call the actual VST main.
		//
		return VSTPluginMainInternal(bootstrapper, hostCallback);
	}
	catch(System::Exception^ exc)
	{
		if(bootstrapper != nullptr)
		{
			delete bootstrapper;
		}

		// cannot use the Utils::ShowError method here, cause it depends on Core.
		System::Windows::Forms::MessageBox::Show(nullptr, exc->ToString(), "VST.NET Bootstrapper Error", 
			System::Windows::Forms::MessageBoxButtons::OK, System::Windows::Forms::MessageBoxIcon::Error);
	}

	return NULL;
}

// fwd ref
AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo);

AEffect* VSTPluginMainInternal (Bootstrapper^ bootstrapper, audioMasterCallback hostCallback)
{
	// retrieve the plugin config
	Jacobi::Vst::Interop::Plugin::Configuration^ config = bootstrapper->Configuration;

	// The method dependencies has brought in the Core assembly. 
	// Now we unregister the bootstrapper and turn it over to the AssemblyLoader (located in the Core Assembly).
	delete bootstrapper;

	// retrieve the current plugin file name (interop)
	System::String^ interopAssemblyFileName = Utils::GetCurrentFileName();
	//System::String^ basePath = System::IO::Path::GetDirectoryName(interopAssemblyFileName);

	// create the host command stub (sends commands to host)
	Jacobi::Vst::Interop::Plugin::HostCommandStub^ hostStub = 
		gcnew Jacobi::Vst::Interop::Plugin::HostCommandStub(hostCallback);

	try
	{
		// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = Bootstrapper::LoadManagedPlugin(interopAssemblyFileName, config);
		
		if(commandStub)
		{
			// retrieve the plugin info
			Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo = commandStub->GetPluginInfo(hostStub);

			if(pluginInfo)
			{
				// create the native audio effect struct based on the plugin info
				AEffect* pEffect = CreateAudioEffectInfo(pluginInfo);

				// initialize host stub with plugin info
				hostStub->Initialize(pEffect);

				// connect the plugin command stub to the command proxy and construct a handle
				System::Runtime::InteropServices::GCHandle proxyHandle = 
					System::Runtime::InteropServices::GCHandle::Alloc(
						gcnew Jacobi::Vst::Interop::Plugin::PluginCommandProxy(commandStub), System::Runtime::InteropServices::GCHandleType::Normal);

				// maintain the proxy reference as part of the effect struct
				pEffect->user = System::Runtime::InteropServices::GCHandle::ToIntPtr(proxyHandle).ToPointer();

				return pEffect;
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
	finally
	{
		// make sure the private paths are cleared once the plugin is fully loaded.
		Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->PrivateProbePaths->Clear();
	}

	return NULL;
}

// Dispatcher Procedure called by the host
VstIntPtr DispatcherProc(AEffect* pluginInfo, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->Dispatch(opcode, index, value, ptr, opt);
	}

	return 0;
}

// Audio processing Procedure called by the host
void Process32Proc(AEffect* pluginInfo, float** inputs, float** outputs, VstInt32 sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

// Audio precision processing Procedure called by the host
void Process64Proc(AEffect* pluginInfo, double** inputs, double** outputs, VstInt32 sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

// Parameter assignment Procedure called by the host
void SetParameterProc(AEffect* pluginInfo, VstInt32 index, float value)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->SetParameter(index, value);
	}
}

// Parameter retrieval Procedure called by the host
float GetParameterProc(AEffect* pluginInfo, VstInt32 index)
{
	if(pluginInfo && pluginInfo->user)
	{
		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->GetParameter(index);
	}

	return 0.0;
}

// Audio processing (accumulating)  Procedure called by the host
void Process32AccProc(AEffect* pluginInfo, float** inputs, float** outputs, VstInt32 sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		// Tell the GC we are doing real-time processing here
		TimeCriticalScope scope;

		Jacobi::Vst::Interop::Plugin::PluginCommandProxy^ proxy = (Jacobi::Vst::Interop::Plugin::PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->ProcessAcc(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

// Helper method to create the AEffect structure based on the pluginInfo.
AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo)
{
	// deleted in the Finalizer method of the HostCommandStub
	AEffect* pEffect = new AEffect();
	ZeroMemory(pEffect, sizeof(AEffect));
	pEffect->magic = kEffectMagic;

	// assign function pointers
	pEffect->dispatcher = Jacobi::Vst::Interop::DispatcherProc;
	pEffect->processReplacing = Jacobi::Vst::Interop::Process32Proc;
	pEffect->processDoubleReplacing = Jacobi::Vst::Interop::Process64Proc;
	pEffect->setParameter = Jacobi::Vst::Interop::SetParameterProc;
	pEffect->getParameter = Jacobi::Vst::Interop::GetParameterProc;

	// assign info data
	pEffect->flags = (int)pluginInfo->Flags;
	pEffect->initialDelay = pluginInfo->InitialDelay;
	pEffect->numInputs = pluginInfo->AudioInputCount;
	pEffect->numOutputs = pluginInfo->AudioOutputCount;
	pEffect->numParams = pluginInfo->ParameterCount;
	pEffect->numPrograms = pluginInfo->ProgramCount;
	pEffect->uniqueID = pluginInfo->PluginID;
	pEffect->version = pluginInfo->PluginVersion;

	// check for deprecated members
	Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^ deprecatedInfo =
		dynamic_cast<Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^>(pluginInfo);

	if(deprecatedInfo != nullptr)
	{
		// hook up the old accumulating process proc when deprecated PluginInfo is passed in
		pEffect->DECLARE_VST_DEPRECATED (process) = Jacobi::Vst::Interop::Process32AccProc;

		pEffect->DECLARE_VST_DEPRECATED (realQualities) = deprecatedInfo->RealQualities;
		pEffect->DECLARE_VST_DEPRECATED (offQualities) = deprecatedInfo->OfflineQualities;
		pEffect->DECLARE_VST_DEPRECATED (ioRatio) = deprecatedInfo->IoRatio;
	}

	return pEffect;
}

}}} // Jacobi::Vst::Interop