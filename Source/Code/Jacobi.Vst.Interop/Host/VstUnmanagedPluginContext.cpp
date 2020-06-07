#include "pch.h"
#include "VstUnmanagedPluginContext.h"
#include "..\TypeConverter.h"
#include "..\Properties\Resources.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {


	VstUnmanagedPluginContext::VstUnmanagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
		: VstPluginContext(hostCmdStub)
	{
		_hostCmdProxy = gcnew VstHostCommandProxy(hostCmdStub);
	}

	VstUnmanagedPluginContext::~VstUnmanagedPluginContext()
	{
		// we dont call the finalizer because otherwise the Dll is unloaded before Plugin.Close could be called.
		//this->!VstUnmanagedPluginContext();
	}

	VstUnmanagedPluginContext::!VstUnmanagedPluginContext()
	{
		// close the loaded library.
		CloseLibrary();
	}
	
	VstPluginContext^ VstUnmanagedPluginContext::CreateInternal(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		return gcnew VstUnmanagedPluginContext(hostCmdStub);
	}

	VstPluginContext^ VstUnmanagedPluginContext::ShellCreate(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		auto newCtx = gcnew VstUnmanagedPluginContext(hostCmdStub);
		auto pluginPath = Find<System::String^>(VstPluginContext::PluginPathContextVar);

		try
		{
			newCtx->Initialize(pluginPath);
		}
		catch(...)
		{
			delete newCtx;

			throw;
		}

		return newCtx;
	}

	void VstUnmanagedPluginContext::Uninitialize()
	{
		this->!VstUnmanagedPluginContext();
	}

	void VstUnmanagedPluginContext::Initialize(System::String^ pluginPath)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");

		// method called more than once?
		if(_hLib != NULL)
		{
			throw gcnew System::InvalidOperationException(
				Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_AlreadyInitialized);
		}

		char* pPluginPath = NULL;

		try
		{
			pPluginPath = TypeConverter::AllocateString(pluginPath);

			// Load plugin dll
			_hLib = ::LoadLibraryA(pPluginPath);

			if(_hLib == NULL)
			{
				throw gcnew System::ArgumentException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_LoadPluginFailed,
						pluginPath));
			}
				
			// check entry point
			_pluginMain = (Vst2PluginMain)::GetProcAddress(_hLib, "VSTPluginMain");

			if(_pluginMain == NULL)
			{
				// check old entry point
				_pluginMain = (Vst2PluginMain)::GetProcAddress(_hLib, "main");
			}

			if(_pluginMain == NULL)
			{
				throw gcnew System::EntryPointNotFoundException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_EntryPointNotFound,
						pluginPath));
			}
			
			LoadingPlugin = this;

			// call main and retrieve Vst2Plugin*
			_pEffect = _pluginMain(&HostCommandHandler);

			if(_pEffect == NULL)
			{
				throw gcnew System::OperationCanceledException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_PluginReturnedNull,
						pluginPath));
			}

			if(_pEffect->VstP != Vst2FourCharacterCode)
			{
				throw gcnew System::OperationCanceledException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_MagicNumberMismatch,
						pluginPath));
			}

			auto ctxHandle = System::Runtime::InteropServices::GCHandle::Alloc(this);

			// maintain the context reference as part of the effect struct
			_pEffect->reserved1 = (Vst2IntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(ctxHandle).ToPointer();

			PluginCommandStub = gcnew VstPluginCommandStub(_pEffect);
			PluginCommandStub->PluginContext = this;

			// setup the plugin info
			if(PluginCommandStub->Commands->GetVstVersion() < 2400)
			{
				// use structure with extra legacy fields for older versions
				PluginInfo = gcnew Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo();
			}
			else
			{
				PluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();
			}

			AcceptPluginInfoData(false);

			Set(VstPluginContext::PluginPathContextVar, pluginPath);
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
		Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^ legacyInfo =
			dynamic_cast<Jacobi::Vst::Core::Legacy::VstPluginLegacyInfo^>(PluginInfo);

		System::Collections::Generic::List<System::String^>^ changedPropNames = 
			gcnew System::Collections::Generic::List<System::String^>();

		if(raiseEvents)
		{
			if(PluginInfo->Flags != safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags))
			{
				changedPropNames->Add("PluginInfo.Flags");
			}

			if(PluginInfo->ProgramCount != _pEffect->programCount)
			{
				changedPropNames->Add("PluginInfo.ProgramCount");
			}

			if(PluginInfo->ParameterCount != _pEffect->parameterCount)
			{
				changedPropNames->Add("PluginInfo.ParameterCount");
			}

			if(PluginInfo->AudioInputCount != _pEffect->inputCount)
			{
				changedPropNames->Add("PluginInfo.AudioInputCount");
			}

			if(PluginInfo->AudioOutputCount != _pEffect->outputCount)
			{
				changedPropNames->Add("PluginInfo.AudioOutputCount");
			}

			if(PluginInfo->InitialDelay != _pEffect->startupDelay)
			{
				changedPropNames->Add("PluginInfo.InitialDelay");
			}
			
			if(PluginInfo->PluginID != _pEffect->id)
			{
				changedPropNames->Add("PluginInfo.PluginID");
			}

			if(PluginInfo->PluginVersion != _pEffect->version)
			{
				changedPropNames->Add("PluginInfo.PluginVersion");
			}

			if(legacyInfo != nullptr)
			{
				if(legacyInfo->RealQualities != _pEffect->realQualities)
				{
					changedPropNames->Add("PluginInfo.RealQualities");
				}

				if(legacyInfo->OfflineQualities != _pEffect->offQualities)
				{
					changedPropNames->Add("PluginInfo.OfflineQualities");
				}

				if(legacyInfo->IoRatio != _pEffect->ioRatio)
				{
					changedPropNames->Add("PluginInfo.IoRatio");
				}
			}
		}

		// assign new values
		PluginInfo->Flags = safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags);
		PluginInfo->ProgramCount = _pEffect->programCount;
		PluginInfo->ParameterCount = _pEffect->parameterCount;
		PluginInfo->AudioInputCount = _pEffect->inputCount;
		PluginInfo->AudioOutputCount = _pEffect->outputCount;
		PluginInfo->InitialDelay = _pEffect->startupDelay;
		PluginInfo->PluginID = _pEffect->id;
		PluginInfo->PluginVersion = _pEffect->version;

		// legacy fields
		if(legacyInfo != nullptr)
		{
			legacyInfo->RealQualities = _pEffect->realQualities;
			legacyInfo->OfflineQualities = _pEffect->offQualities;
			legacyInfo->IoRatio = _pEffect->ioRatio;
		}

		// raise all the changed property events
		for each(System::String^ propName in changedPropNames)
		{
			RaisePropertyChanged(propName);
		}
	}


}}}} // Jacobi::Vst::Host::Interop


Vst2IntPtr HostCommandHandler(::Vst2Plugin* pPlugin, ::int32_t opcode, ::int32_t index, ::Vst2IntPtr value, void* ptr, float opt)
{
	Jacobi::Vst::Host::Interop::VstUnmanagedPluginContext^ context = nullptr;

	if(pPlugin != NULL && pPlugin->reserved1 != 0)
	{
		// extract the reference to the VstPluginContext from the effect struct.
		context = safe_cast<Jacobi::Vst::Host::Interop::VstUnmanagedPluginContext^>(
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr((void*)pPlugin->reserved1)).Target);
	}

	// fallback to the current loading plugin.
	if(context == nullptr)
	{
		context = Jacobi::Vst::Host::Interop::VstUnmanagedPluginContext::LoadingPlugin;
	}

	// dispatch call to plugin context and its Host Proxy.
	if(context != nullptr)
	{
		return context->HostCommandProxy->Dispatch(opcode, index, value, ptr, opt);
	}

	// no-one there to answer...
	System::Diagnostics::Debug::WriteLine(
		"Warning: No VstUnmanagedPluginContext instance was found to dispatch opcode: {0}.", opcode);
	return 0;
}