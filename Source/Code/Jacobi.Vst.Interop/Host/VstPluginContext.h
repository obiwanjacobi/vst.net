#pragma once

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

		// TODO: make type and prop internal
		property VstHostCommandProxy^ HostCommandProxy { VstHostCommandProxy^ get() { return _hostCmdProxy; } }
		// TODO: make type internal and change prop to commands interface type
		property VstPluginCommandStub^ PluginCommandStub { VstPluginCommandStub^ get() { return _pluginCmdStub; } }

	internal:
		// only set during loading of plugin
		static property VstPluginContext^ LoadingPlugin;

		::AEffect* GetEffectStruct() { return _pEffect; }

	private:
		HMODULE _hLib;
		::AEffect* _pEffect;

		VstHostCommandProxy^ _hostCmdProxy;
		VstPluginCommandStub^ _pluginCmdStub;

		void CloseLibrary() { if(_hLib != NULL) { ::FreeLibrary(_hLib); _hLib = NULL; } }
	};

}}}} // namespace Jacobi.Vst.Interop.Host

// typedef for the main exported function from a plugin dll
typedef ::AEffect* (*VSTPluginMain)(::audioMasterCallback);

// static callback function
static VstIntPtr DispatchCallback(AEffect* pEff, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);