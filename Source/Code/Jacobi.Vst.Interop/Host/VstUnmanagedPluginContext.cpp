#include "StdAfx.h"
#include "VstUnmanagedPluginContext.h"
#include "..\TypeConverter.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	VstUnmanagedPluginContext::VstUnmanagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
		: VstPluginContext(hostCmdStub)
	{
		_hostCmdProxy = gcnew VstHostCommandProxy(hostCmdStub);
	}

	VstUnmanagedPluginContext::~VstUnmanagedPluginContext()
	{
		this->!VstUnmanagedPluginContext();
	}

	VstUnmanagedPluginContext::!VstUnmanagedPluginContext()
	{
		// close the loaded library.
		CloseLibrary();
	}

	void VstUnmanagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		// method called more than once?
		if(_hLib != NULL)
		{
			throw gcnew System::InvalidOperationException("This instance of the VstPluginContext is already initialized.");
		}

		char* pPluginPath = NULL;

		try
		{
			pPluginPath = TypeConverter::AllocateString(pluginPath);

			// Load plugin dll
			_hLib = ::LoadLibraryA(pPluginPath);

			if(_hLib == NULL)
			{
				throw gcnew System::ArgumentException(pluginPath + " cannot be loaded.");
			}

			// check entry point
			VSTPluginMain pluginMain = (VSTPluginMain)::GetProcAddress(_hLib, "VSTPluginMain");

			if(pluginMain == NULL)
			{
				throw gcnew System::EntryPointNotFoundException(pluginPath + " has no exported 'VSTPluginMain' function (VST 2.4).");
			}
			
			LoadingPlugin = this;

			// call main and retrieve AEffect*
			_pEffect = pluginMain(&DispatchCallback);

			if(_pEffect == NULL)
			{
				throw gcnew System::OperationCanceledException(pluginPath + " did not return an AEffect structure.");
			}

			if(_pEffect->magic != kEffectMagic)
			{
				throw gcnew System::OperationCanceledException(pluginPath + " did not return an AEffect structure with the correct 'Magic' number.");
			}

			System::Runtime::InteropServices::GCHandle ctxHandle = 
					System::Runtime::InteropServices::GCHandle::Alloc(this);

			// maintain the context reference as part of the effect struct
			_pEffect->resvd1 = (VstIntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(ctxHandle).ToPointer();

			PluginCommandStub = gcnew VstPluginCommandStub(_pEffect);
			PluginCommandStub->PluginContext = this;

			// check if the plugin supports our VST version
			if(PluginCommandStub->GetVstVersion() < 2400)
			{
				throw gcnew System::NotSupportedException("The Plugin '" + pluginPath + "' does not support VST 2.4.");
			}

			// setup the plugin info
			PluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();

			AcceptPluginInfoData(false);
		}
		catch(...)
		{
			CloseLibrary();

			PluginCommandStub = nullptr;
			PluginInfo = nullptr;
			_pEffect = NULL;

			throw;
		}
		finally
		{
			if(pPluginPath != NULL)
			{
				TypeConverter::DeallocateString(pPluginPath);
			}

			LoadingPlugin = nullptr;
		}
	}

	void VstUnmanagedPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		System::Collections::Generic::List<System::String^> changedPropNames = 
			gcnew System::Collections::Generic::List<System::String^>();

		if(raiseEvents)
		{
			if(PluginInfo->Flags != safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags))
			{
				changedPropNames.Add("PluginInfo.Flags");
			}

			if(PluginInfo->ProgramCount != _pEffect->numPrograms)
			{
				changedPropNames.Add("PluginInfo.ProgramCount");
			}

			if(PluginInfo->ParameterCount != _pEffect->numParams)
			{
				changedPropNames.Add("PluginInfo.ParameterCount");
			}

			if(PluginInfo->AudioInputCount != _pEffect->numInputs)
			{
				changedPropNames.Add("PluginInfo.AudioInputCount");
			}

			if(PluginInfo->AudioOutputCount != _pEffect->numOutputs)
			{
				changedPropNames.Add("PluginInfo.AudioOutputCount");
			}

			if(PluginInfo->InitialDelay != _pEffect->initialDelay)
			{
				changedPropNames.Add("PluginInfo.InitialDelay");
			}
			
			if(PluginInfo->PluginID != _pEffect->uniqueID)
			{
				changedPropNames.Add("PluginInfo.PluginID");
			}

			if(PluginInfo->PluginVersion != _pEffect->version)
			{
				changedPropNames.Add("PluginInfo.PluginVersion");
			}
		}

		// assign new values
		PluginInfo->Flags = safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags);
		PluginInfo->ProgramCount = _pEffect->numPrograms;
		PluginInfo->ParameterCount = _pEffect->numParams;
		PluginInfo->AudioInputCount = _pEffect->numInputs;
		PluginInfo->AudioOutputCount = _pEffect->numOutputs;
		PluginInfo->InitialDelay = _pEffect->initialDelay;
		PluginInfo->PluginID = _pEffect->uniqueID;
		PluginInfo->PluginVersion = _pEffect->version;

		// raise all the changed property events
		for each(System::String^ propName in changedPropNames)
		{
			RaisePropertyChanged(propName);
		}
	}


}}}} // namespace Jacobi::Vst::Interop::Host


VstIntPtr DispatchCallback(::AEffect* pEffect, ::VstInt32 opcode, ::VstInt32 index, ::VstIntPtr value, void* ptr, float opt)
{
	Jacobi::Vst::Interop::Host::VstUnmanagedPluginContext^ context = nullptr;

	if(pEffect != NULL && pEffect->resvd1 != 0)
	{
		// extract the reference to the VstPluginContext from the effect struct.
		context = safe_cast<Jacobi::Vst::Interop::Host::VstUnmanagedPluginContext^>(
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr((void*)pEffect->resvd1)).Target);
	}

	// fallback to the current loading plugin.
	if(context == nullptr)
	{
		context = Jacobi::Vst::Interop::Host::VstUnmanagedPluginContext::LoadingPlugin;
	}

	// dispatch call to plugin context and its Host Proxy.
	if(context != nullptr)
	{
		return context->HostCommandProxy->Dispatch(opcode, index, value, ptr, opt);
	}

	// no-one there to answer...
	System::Diagnostics::Debug::WriteLine("Warning: No VstUnmanagedPluginContext instance was found to dispatch opcode:" + opcode);
	return 0;
}