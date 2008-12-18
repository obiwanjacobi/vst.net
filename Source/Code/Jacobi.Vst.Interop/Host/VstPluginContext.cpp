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

		_hostCmdProxy = gcnew VstHostCommandProxy(hostCmdStub);
	}

	VstPluginContext::~VstPluginContext()
	{
		this->!VstPluginContext();
	}

	VstPluginContext::!VstPluginContext()
	{
		CloseLibrary();
	}

	void VstPluginContext::Initialize(System::String^ pluginPath)
	{
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
				throw gcnew System::InvalidOperationException(pluginPath + " cannot be loaded.");
			}

			// check entry point
			VSTPluginMain pluginMain = (VSTPluginMain)::GetProcAddress(_hLib, TEXT("VSTPluginMain"));

			if(pluginMain == NULL)
			{
				throw gcnew System::InvalidOperationException(pluginPath + " has no exported 'VSTPluginMain' function (VST 2.4).");
			}
			
			LoadingPlugin = this;

			// call main and retrieve AEffect*
			_pEffect = pluginMain(&DispatchCallback);

			if(_pEffect == NULL)
			{
				throw gcnew System::InvalidOperationException(pluginPath + " did not return an AEffect structure.");
			}

			System::Runtime::InteropServices::GCHandle ctxHandle = 
					System::Runtime::InteropServices::GCHandle::Alloc(this);

			// maintain the context reference as part of the effect struct
			_pEffect->resvd1 = (VstIntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(ctxHandle).ToPointer();

			_pluginCmdStub = gcnew VstPluginCommandStub(_pEffect);

			// check if the plugin supports our VST version
			if(_pluginCmdStub->GetVstVersion() < 2400)
			{
				throw gcnew System::InvalidOperationException("The Plugin '" + pluginPath + "' does not support VST 2.4.");
			}

			// setup the plugin info
			_pluginInfo = gcnew Jacobi::Vst::Core::Plugin::VstPluginInfo();

			AcceptPluginInfoData(false);
		}
		catch(...)
		{
			CloseLibrary();

			_pluginCmdStub = nullptr;
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

	void VstPluginContext::AcceptPluginInfoData(System::Boolean raiseEvents)
	{
		// TODO: implement raiseEvents;

		_pluginInfo->Flags = safe_cast<Jacobi::Vst::Core::VstPluginFlags>(_pEffect->flags);
		_pluginInfo->ProgramCount = _pEffect->numPrograms;
		_pluginInfo->ParameterCount = _pEffect->numParams;
		_pluginInfo->AudioInputCount = _pEffect->numInputs;
		_pluginInfo->AudioOutputCount = _pEffect->numOutputs;
		_pluginInfo->InitialDelay = _pEffect->initialDelay;
		_pluginInfo->PluginID = _pEffect->uniqueID;
		_pluginInfo->PluginVersion = _pEffect->version;
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