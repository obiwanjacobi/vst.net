#pragma once

#include "VstPluginContext.h"
#include "VstHostCommandProxy.h"
#include "VstPluginCommandStub.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	ref class VstUnmanagedPluginContext : public VstPluginContext
	{
	public:
		VstUnmanagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
		~VstUnmanagedPluginContext();
		!VstUnmanagedPluginContext();

		void Initialize(System::String^ pluginPath);

		virtual void AcceptPluginInfoData(System::Boolean raiseEvents) override;

	internal:
		/// <summary>Gets or sets the plugin context of the plugin that is currently loading.</summary>
		/// <remarks>Only set during loading of plugin (Create)</remarks>
		static property VstUnmanagedPluginContext^ LoadingPlugin;

		/// <summary>Gets a reference to the host command proxy.</summary>
		/// <remarks>Used to dispatch incoming requests from the plugin.</remarks>
		property VstHostCommandProxy^ HostCommandProxy 
		{ VstHostCommandProxy^ get() { return _hostCmdProxy; } }

	private:
		HMODULE _hLib;
		::AEffect* _pEffect;

		VstHostCommandProxy^ _hostCmdProxy;

		void CloseLibrary()
		{ if(_hLib != NULL) { ::FreeLibrary(_hLib); _hLib = NULL; } }
	};

}}}} // namespace Jacobi::Vst::Interop::Host

// typedef for the main exported function from a plugin dll
typedef ::AEffect* (*VSTPluginMain)(::audioMasterCallback);

// static callback function
static VstIntPtr DispatchCallback(AEffect* pEff, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);