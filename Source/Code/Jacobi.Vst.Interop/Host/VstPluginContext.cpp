#include "StdAfx.h"
#include "VstHostCommandProxy.h"
#include "UnmanagedArray.h"
#include "VstPluginCommandStub.h"
#include "VstPluginContext.h"
#include "..\TypeConverter.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	VstPluginContext::VstPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		if(hostCmdStub == nullptr)
		{
			throw gcnew System::ArgumentNullException("hostCmdStub");
		}

		_hostCmdStub = hostCmdStub;
		_hostCmdProxy = gcnew VstHostCommandProxy(hostCmdStub);

		hostCmdStub->PluginContext = this;

		_props = gcnew System::Collections::Generic::Dictionary<System::String^, System::Object^>();
	}

	VstPluginContext::~VstPluginContext()
	{
		this->!VstPluginContext();
	}

	VstPluginContext::!VstPluginContext()
	{
		System::IDisposable^ disposable = nullptr;

		// dispose all content
		for each(System::Collections::Generic::KeyValuePair<System::String^, System::Object^> item in _props)
		{
			disposable = dynamic_cast<System::IDisposable^>(item.Value);

			if(disposable != nullptr)
			{
				delete disposable;
			}
		}

		// if the host command stub implements IDisposable, it is disposed too.
		disposable = dynamic_cast<System::IDisposable^>(_hostCmdStub);
		
		if(disposable != nullptr)
		{
			delete disposable;
		}

		// dispose the plugin command stub (unmanaged)
		_pluginCmdStub->~VstPluginCommandStub();
		_pluginCmdStub = nullptr;
		_pluginInfo = nullptr;

		// close the loaded library.
		CloseLibrary();
	}

	void VstPluginContext::Initialize(System::String^ pluginPath)
	{
		// method called more than once?
		if(_hLib != NULL)
		{
			throw gcnew System::InvalidOperationException("This instance of the VstPluginContext is already initialized.");
		}

		// verify file exist
		if(!System::IO::File::Exists(pluginPath))
		{
			throw gcnew System::IO::FileNotFoundException(pluginPath);
		}

		char* pPluginPath = NULL;

		try
		{
			pPluginPath = TypeConverter::AllocateString(pluginPath);

			// Load plugin dll
			_hLib = ::LoadLibrary(pPluginPath);

			if(_hLib == NULL)
			{
				throw gcnew System::ArgumentException(pluginPath + " cannot be loaded.");
			}

			// check entry point
			VSTPluginMain pluginMain = (VSTPluginMain)::GetProcAddress(_hLib, TEXT("VSTPluginMain"));

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

			_pluginCmdStub = gcnew VstPluginCommandStub(_pEffect);
			_pluginCmdStub->PluginContext = this;

			// check if the plugin supports our VST version
			if(_pluginCmdStub->GetVstVersion() < 2400)
			{
				throw gcnew System::NotSupportedException("The Plugin '" + pluginPath + "' does not support VST 2.4.");
			}

			// setup the plugin info
			_pluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();

			AcceptPluginInfoData(false);
		}
		catch(...)
		{
			CloseLibrary();

			_pluginCmdStub = nullptr;
			_pluginInfo = nullptr;
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

	generic<typename T> 
	void VstPluginContext::Set(System::String^ keyName, T value)
	{
		if(_props->ContainsKey(keyName))
		{
			_props->default[keyName] = value;
			RaisePropertyChanged(keyName);
		}
		else
		{
			_props->Add(keyName, value);
		}
	}

	generic<typename T> 
	T VstPluginContext::Find(System::String^ keyName)
	{
		if(_props->ContainsKey(keyName))
		{
			return (T)(_props->default[keyName]);
		}

		return T();
	}

	void VstPluginContext::Delete(System::String^ keyName)
	{
		System::IDisposable^ prop = dynamic_cast<System::IDisposable^>(Find<System::Object^>(keyName));

		if(prop != nullptr)
		{
			delete prop;
		}

		Remove(keyName);
	}

	void VstPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		System::Collections::Generic::List<System::String^> changedPropNames = 
			gcnew System::Collections::Generic::List<System::String^>();

		if(raiseEvents)
		{
			if(_pluginInfo->Flags != safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags))
			{
				changedPropNames.Add("PluginInfo.Flags");
			}

			if(_pluginInfo->ProgramCount != _pEffect->numPrograms)
			{
				changedPropNames.Add("PluginInfo.ProgramCount");
			}

			if(_pluginInfo->ParameterCount != _pEffect->numParams)
			{
				changedPropNames.Add("PluginInfo.ParameterCount");
			}

			if(_pluginInfo->AudioInputCount != _pEffect->numInputs)
			{
				changedPropNames.Add("PluginInfo.AudioInputCount");
			}

			if(_pluginInfo->AudioOutputCount != _pEffect->numOutputs)
			{
				changedPropNames.Add("PluginInfo.AudioOutputCount");
			}

			if(_pluginInfo->InitialDelay != _pEffect->initialDelay)
			{
				changedPropNames.Add("PluginInfo.InitialDelay");
			}
			
			if(_pluginInfo->PluginID != _pEffect->uniqueID)
			{
				changedPropNames.Add("PluginInfo.PluginID");
			}

			if(_pluginInfo->PluginVersion != _pEffect->version)
			{
				changedPropNames.Add("PluginInfo.PluginVersion");
			}
		}

		// assign new values
		_pluginInfo->Flags = safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags);
		_pluginInfo->ProgramCount = _pEffect->numPrograms;
		_pluginInfo->ParameterCount = _pEffect->numParams;
		_pluginInfo->AudioInputCount = _pEffect->numInputs;
		_pluginInfo->AudioOutputCount = _pEffect->numOutputs;
		_pluginInfo->InitialDelay = _pEffect->initialDelay;
		_pluginInfo->PluginID = _pEffect->uniqueID;
		_pluginInfo->PluginVersion = _pEffect->version;

		// raise all the changed property events
		for each(System::String^ propName in changedPropNames)
		{
			RaisePropertyChanged(propName);
		}
	}

}}}} // namespace Jacobi.Vst.Interop.Host


VstIntPtr DispatchCallback(::AEffect* pEffect, ::VstInt32 opcode, ::VstInt32 index, ::VstIntPtr value, void* ptr, float opt)
{
	Jacobi::Vst::Interop::Host::VstPluginContext^ context = nullptr;

	if(pEffect != NULL)
	{
		// extract the reference to the VstPluginContext from the effect struct.
		context = (Jacobi::Vst::Interop::Host::VstPluginContext^)
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr((void*)pEffect->resvd1)).Target;
	}

	// fallback to the current loading plugin.
	if(context == nullptr)
	{
		context = Jacobi::Vst::Interop::Host::VstPluginContext::LoadingPlugin;
	}

	// dispatch call to plugin context and its Host Proxy.
	if(context != nullptr)
	{
		return context->HostCommandProxy->Dispatch(opcode, index, value, ptr, opt);
	}

	// no-one there to answer...
	return 0;
}