#pragma once

#include "..\MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	public ref class VstPluginContext : public System::IDisposable
	{
	public:
		VstPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
		~VstPluginContext();
		!VstPluginContext();

		void Initialize(System::String^ pluginPath);
		void AcceptPluginInfoData(System::Boolean raiseEvents);

		property Jacobi::Vst::Core::Host::IVstPluginCommandStub^ PluginCommandStub 
		{ Jacobi::Vst::Core::Host::IVstPluginCommandStub^ get() { return _pluginCmdStub; } }

		property Jacobi::Vst::Core::Plugin::VstPluginInfo^ PluginInfo 
		{ Jacobi::Vst::Core::Plugin::VstPluginInfo^ get() { return _pluginInfo; } }

	internal:
		// only set during loading of plugin
		static property VstPluginContext^ LoadingPlugin;

		property VstHostCommandProxy^ HostCommandProxy 
		{ VstHostCommandProxy^ get() { return _hostCmdProxy; } }

	private:
		HMODULE _hLib;
		::AEffect* _pEffect;

		VstHostCommandProxy^ _hostCmdProxy;
		VstPluginCommandStub^ _pluginCmdStub;
		Jacobi::Vst::Core::Plugin::VstPluginInfo^ _pluginInfo;

		void CloseLibrary() { if(_hLib != NULL) { ::FreeLibrary(_hLib); _hLib = NULL; } }
	};

}}}} // namespace Jacobi.Vst.Interop.Host

// typedef for the main exported function from a plugin dll
typedef ::AEffect* (*VSTPluginMain)(::audioMasterCallback);

// static callback function
static VstIntPtr DispatchCallback(AEffect* pEff, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);