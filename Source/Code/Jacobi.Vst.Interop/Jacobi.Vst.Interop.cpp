// Jacobi.Vst.Interop.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "AssemblyLoader.h"
#include "PluginCommandProxy.h"
#include "HostCommandStub.h"
#include "Utils.h"
#include<vcclr.h>

// fwd refs
AEffect* VSTPluginMainInternal (audioMasterCallback hostCallback);

// main exported method called by host to create the plugin
AEffect* VSTPluginMain (audioMasterCallback hostCallback)
{
	// retrieve the current plugin file name (interop)
	System::String^ interopAssemblyFileName = Utils::GetCurrentFileName();

	// pass the assembly loader the vst plugin directory
	AssemblyLoader::Initialize(System::IO::Path::GetDirectoryName(interopAssemblyFileName));

	//
	// We have boostrapped the AssemblyLoader (above).
	// Now call the actual VST main.
	//
	return VSTPluginMainInternal(hostCallback);
}

// fwd refs
AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo);

AEffect* VSTPluginMainInternal (audioMasterCallback hostCallback)
{
	// create the host command stub (sends commands to host)
	HostCommandStub^ hostStub = gcnew HostCommandStub(hostCallback);

	try
	{
		// retrieve the current plugin file name (interop)
		System::String^ interopAssemblyFileName = Utils::GetCurrentFileName();

		// create the plugin (command stub) factory
		Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory = 
			gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory(interopAssemblyFileName);
		
		// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
		Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = factory->CreatePluginCommandStub();
		
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
						gcnew PluginCommandProxy(commandStub), System::Runtime::InteropServices::GCHandleType::Normal);

				// maintain the proxy reference as part of the effect struct
				pEffect->user = System::Runtime::InteropServices::GCHandle::ToIntPtr(proxyHandle).ToPointer();

				return pEffect;
			}
			else
			{
				Utils::ShowWarning("The Plugin Command Stub did not return a Plugin Info instance. Loading will be cancelled.");
			}
		}
		else
		{
			Utils::ShowWarning("The Plugin Factory was unable to create a Plugin Command Stub. Loading will be cancelled.");
		}
	}
	catch(System::Exception^ exc)
	{
		delete hostStub;

		Utils::ShowError(exc);
	}

	return NULL;
}

VstIntPtr DispatcherProc(AEffect* pluginInfo, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	if(pluginInfo && pluginInfo->user)
	{
		PluginCommandProxy^ proxy = (PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->Dispatch(opcode, index, value, ptr, opt);
	}

	return 0;
}

void Process32Proc(AEffect* pluginInfo, float** inputs, float** outputs, VstInt32 sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		PluginCommandProxy^ proxy = (PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

void Process64Proc(AEffect* pluginInfo, double** inputs, double** outputs, VstInt32 sampleFrames)
{
	if(pluginInfo && pluginInfo->user)
	{
		PluginCommandProxy^ proxy = (PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

void SetParameterProc(AEffect* pluginInfo, VstInt32 index, float value)
{
	if(pluginInfo && pluginInfo->user)
	{
		PluginCommandProxy^ proxy = (PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		proxy->SetParameter(index, value);
	}
}

float GetParameterProc(AEffect* pluginInfo, VstInt32 index)
{
	if(pluginInfo && pluginInfo->user)
	{
		PluginCommandProxy^ proxy = (PluginCommandProxy^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pluginInfo->user)).Target;

		return proxy->GetParameter(index);
	}

	return 0.0;
}

AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo)
{
	// deleted in the Finalizer method of the HostCommandStub
	AEffect* pEffect = new AEffect();
	pEffect->magic = kEffectMagic;

	// assign function pointers
	pEffect->dispatcher = ::DispatcherProc;
	pEffect->processReplacing = ::Process32Proc;
	pEffect->processDoubleReplacing = ::Process64Proc;
	pEffect->setParameter = ::SetParameterProc;
	pEffect->getParameter = ::GetParameterProc;

	// assign info data
	pEffect->flags = (int)pluginInfo->Flags;
	pEffect->initialDelay = pluginInfo->InitialDelay;
	pEffect->numInputs = pluginInfo->AudioInputCount;
	pEffect->numOutputs = pluginInfo->AudioOutputCount;
	pEffect->numParams = pluginInfo->ParameterCount;
	pEffect->numPrograms = pluginInfo->ProgramCount;
	pEffect->uniqueID = pluginInfo->PluginID;
	pEffect->version = pluginInfo->PluginVersion;

	return pEffect;
}
