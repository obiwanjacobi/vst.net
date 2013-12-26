#include "StdAfx.h"
#include "VstUnmanagedPluginContext.h"
#include "..\TypeConverter.h"
#include "..\Properties\Resources.h"

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
		VstUnmanagedPluginContext^ newCtx = gcnew VstUnmanagedPluginContext(hostCmdStub);
		System::String^ pluginPath = Find<System::String^>(VstPluginContext::PluginPathContextVar);

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
			_pluginMain = (VSTPluginMain)::GetProcAddress(_hLib, "VSTPluginMain");

			if(_pluginMain == NULL)
			{
				// check old entry point
				_pluginMain = (VSTPluginMain)::GetProcAddress(_hLib, "main");
			}

			if(_pluginMain == NULL)
			{
				throw gcnew System::EntryPointNotFoundException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_EntryPointNotFound,
						pluginPath));
			}
			
			LoadingPlugin = this;

			// call main and retrieve AEffect*
			_pEffect = _pluginMain(&DispatchCallback);

			if(_pEffect == NULL)
			{
				throw gcnew System::OperationCanceledException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_PluginReturnedNull,
						pluginPath));
			}

			if(_pEffect->magic != kEffectMagic)
			{
				throw gcnew System::OperationCanceledException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_MagicNumberMismatch,
						pluginPath));
			}

			System::Runtime::InteropServices::GCHandle ctxHandle = 
					System::Runtime::InteropServices::GCHandle::Alloc(this);

			// maintain the context reference as part of the effect struct
			_pEffect->resvd1 = (VstIntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(ctxHandle).ToPointer();

			PluginCommandStub = gcnew VstPluginCommandStub(_pEffect);
			PluginCommandStub->PluginContext = this;

			/*// check if the plugin supports our VST version
			if(PluginCommandStub->GetVstVersion() < 2400)
			{
				throw gcnew System::NotSupportedException(
					System::String::Format(
						Jacobi::Vst::Interop::Properties::Resources::VstUnmanagedPluginContext_VstVersionMismatch,
						pluginPath));
			}*/

			// setup the plugin info
			if(PluginCommandStub->GetVstVersion() < 2400)
			{
				// use structure with extra deprecated fields for older versions
				PluginInfo = gcnew Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo();
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
		Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^ deprecatedInfo =
			dynamic_cast<Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedInfo^>(PluginInfo);

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

			if(deprecatedInfo != nullptr)
			{
				if(deprecatedInfo->RealQualities != _pEffect->DECLARE_VST_DEPRECATED (realQualities))
				{
					changedPropNames.Add("PluginInfo.RealQualities");
				}

				if(deprecatedInfo->OfflineQualities != _pEffect->DECLARE_VST_DEPRECATED (offQualities))
				{
					changedPropNames.Add("PluginInfo.OfflineQualities");
				}

				if(deprecatedInfo->IoRatio != _pEffect->DECLARE_VST_DEPRECATED (ioRatio))
				{
					changedPropNames.Add("PluginInfo.IoRatio");
				}
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

		// deprecated fields
		if(deprecatedInfo != nullptr)
		{
			deprecatedInfo->RealQualities = _pEffect->DECLARE_VST_DEPRECATED (realQualities);
			deprecatedInfo->OfflineQualities = _pEffect->DECLARE_VST_DEPRECATED (offQualities);
			deprecatedInfo->IoRatio = _pEffect->DECLARE_VST_DEPRECATED (ioRatio);
		}

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