// Jacobi.Vst.Interop.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PluginCommandProxy.h"
#include "HostCommandStub.h"
#include<vcclr.h>

// fwd refs
System::String^ getPluginFileName();
AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::VstPluginInfo^ pluginInfo);
void ShowError(System::Exception^ e);

// global reference to the plugin cmd stub
gcroot<PluginCommandProxy^> _pluginCommandProxy;

// main exported method called by host to create the plugin
AEffect* VSTPluginMain (audioMasterCallback hostCallback)
{
	// create the host command stub (sends commands to host)
	HostCommandStub^ hostStub = gcnew HostCommandStub(hostCallback);

	try
	{
		// retrieve the current plugin file name
		System::String^ interopAssemblyFileName = getPluginFileName();
		
		// create the plugin (command stub) factory
		Jacobi::Vst::Core::ManagedPluginFactory^ factory = gcnew Jacobi::Vst::Core::ManagedPluginFactory(interopAssemblyFileName);
		
		// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
		Jacobi::Vst::Core::IVstPluginCommandStub^ commandStub = factory->CreatePluginCommandStub();
		
		if(commandStub)
		{
			// retrieve the plugin info
			Jacobi::Vst::Core::VstPluginInfo^ pluginInfo = commandStub->GetPluginInfo(hostStub);

			if(pluginInfo)
			{
				// create the native audio effect struct based on the plugin info
				AEffect* pEffect = CreateAudioEffectInfo(pluginInfo);

				// initialize host stub with plugin info
				hostStub->Initialize(pEffect);

				// connect the plugin command stub to the command proxy
				_pluginCommandProxy = gcnew PluginCommandProxy(commandStub);

				return pEffect;
			}
		}
	}
	catch(System::Exception^ exc)
	{
		delete hostStub;

		ShowError(exc);
	}

	return NULL;
}

VstIntPtr DispatcherProc(AEffect* pluginInfo, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	if(_pluginCommandProxy)
	{
		return _pluginCommandProxy->Dispatch(opcode, index, value, ptr, opt);
	}

	return 0;
}

void Process32Proc(AEffect* pluginInfo, float** inputs, float** outputs, VstInt32 sampleFrames)
{
	if(_pluginCommandProxy)
	{
		_pluginCommandProxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

void Process64Proc(AEffect* pluginInfo, double** inputs, double** outputs, VstInt32 sampleFrames)
{
	if(_pluginCommandProxy)
	{
		_pluginCommandProxy->Process(inputs, outputs, sampleFrames, pluginInfo->numInputs, pluginInfo->numOutputs);
	}
}

void SetParameterProc(AEffect* pluginInfo, VstInt32 index, float value)
{
	if(_pluginCommandProxy)
	{
		_pluginCommandProxy->SetParameter(index, value);
	}
}

float GetParameterProc(AEffect* pluginInfo, VstInt32 index)
{
	if(_pluginCommandProxy)
	{
		return _pluginCommandProxy->GetParameter(index);
	}

	return 0.0;
}

AEffect* CreateAudioEffectInfo(Jacobi::Vst::Core::VstPluginInfo^ pluginInfo)
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
	pEffect->numInputs = pluginInfo->NumberOfAudioInputs;
	pEffect->numOutputs = pluginInfo->NumberOfAudioOutputs;
	pEffect->numParams = pluginInfo->NumberOfParameters;
	pEffect->numPrograms = pluginInfo->NumberOfPrograms;
	pEffect->uniqueID = pluginInfo->PluginID;
	pEffect->version = pluginInfo->PluginVersion;

	return pEffect;
}

System::String^ getPluginFileName()
{
	System::Reflection::Assembly^ ass = System::Reflection::Assembly::GetExecutingAssembly();
	return ass->Location;
}

void ShowError(System::Exception^ e)
{
	System::String^ text = e->ToString();

	System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(text);

	MessageBox(NULL, (LPCSTR)mem.ToPointer(), LPCSTR("VST.NET Error"), MB_ICONERROR | MB_OK);

	System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
}