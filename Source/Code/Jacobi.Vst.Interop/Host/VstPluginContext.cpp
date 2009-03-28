#include "StdAfx.h"
#include "VstHostCommandProxy.h"
#include "UnmanagedArray.h"
#include "VstPluginCommandStub.h"
#include "VstPluginContext.h"
#include "VstManagedPluginContext.h"
#include "VstUnmanagedPluginContext.h"
#include "..\TypeConverter.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	// static factory method
	VstPluginContext^ VstPluginContext::Create(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNullOrEmpty(pluginPath, "pluginPath");
		Jacobi::Vst::Core::Throw::IfArgumentIsNull(hostCmdStub, "hostCmdStub");

		// verify file exist
		if(!System::IO::File::Exists(pluginPath))
		{
			throw gcnew System::IO::FileNotFoundException(pluginPath);
		}

		VstPluginContext^ pluginCtx = VstManagedPluginContext::Create(pluginPath, hostCmdStub);

		if(pluginCtx == nullptr)
		{
			pluginCtx = VstUnmanagedPluginContext::Create(pluginPath, hostCmdStub);
		}

		if(pluginCtx != nullptr)
		{
			try
			{
				pluginCtx->Initialize(pluginPath);
			}
			catch(...)
			{
				delete pluginCtx;

				throw;
			}
		}

		return pluginCtx;
	}

	//-------------------------------------------------------------------------

	VstPluginContext::VstPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		Jacobi::Vst::Core::Throw::IfArgumentIsNull(hostCmdStub, "hostCmdStub");

		_props = gcnew System::Collections::Generic::Dictionary<System::String^, System::Object^>();

		_hostCmdStub = hostCmdStub;
		_hostCmdStub->PluginContext = this;
	}

	VstPluginContext::~VstPluginContext()
	{
		if(PluginCommandStub != nullptr)
		{
			PluginCommandStub->Close();
		}

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
		if(_pluginCmdStub != nullptr)
		{
			delete _pluginCmdStub;
			_pluginCmdStub = nullptr;
		}

		_pluginInfo = nullptr;

		// cleanup 'derived' resources
		Uninitialize();
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


}}}} // namespace Jacobi::Vst::Interop::Host
