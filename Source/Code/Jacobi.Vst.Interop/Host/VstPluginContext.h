#pragma once

#include "..\MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	/// <summary>
	/// 
	/// </summary>
	public ref class VstPluginContext : public Jacobi::Vst::Core::Host::IVstPluginContext, public System::IDisposable
	{
	public:
		VstPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
		// IDisposable interface implementation
		~VstPluginContext();
		!VstPluginContext();

		void Initialize(System::String^ pluginPath);
		
		// IVstPluginContext interface implementation
		generic<typename T> 
		virtual void Set(System::String^ keyName, T value);
		generic<typename T> 
		virtual T Find(System::String^ keyName);
		virtual void Remove(System::String^ keyName)
		{ _props->Remove(keyName); }

		virtual property Jacobi::Vst::Core::Host::IVstHostCommandStub^ HostCommandStub 
		{ Jacobi::Vst::Core::Host::IVstHostCommandStub^ get() { return _hostCmdStub; } }

		virtual property Jacobi::Vst::Core::Host::IVstPluginCommandStub^ PluginCommandStub 
		{ Jacobi::Vst::Core::Host::IVstPluginCommandStub^ get() { return _pluginCmdStub; } }

		virtual property Jacobi::Vst::Core::Plugin::VstPluginInfo^ PluginInfo 
		{ Jacobi::Vst::Core::Plugin::VstPluginInfo^ get() { return _pluginInfo; } }

		virtual void AcceptPluginInfoData(System::Boolean raiseEvents);

		// INotifyPropertyChanged interface implementation
		virtual event System::ComponentModel::PropertyChangedEventHandler^ PropertyChanged;

	internal:
		// only set during loading of plugin
		static property VstPluginContext^ LoadingPlugin;

		property VstHostCommandProxy^ HostCommandProxy 
		{ VstHostCommandProxy^ get() { return _hostCmdProxy; } }

	private:
		HMODULE _hLib;
		::AEffect* _pEffect;

		Jacobi::Vst::Core::Host::IVstHostCommandStub^ _hostCmdStub;
		VstHostCommandProxy^ _hostCmdProxy;
		VstPluginCommandStub^ _pluginCmdStub;
		Jacobi::Vst::Core::Plugin::VstPluginInfo^ _pluginInfo;

		System::Collections::Generic::Dictionary<System::String^, System::Object^>^ _props;

		void CloseLibrary()
		{ if(_hLib != NULL) { ::FreeLibrary(_hLib); _hLib = NULL; } }

		void RaisePropertyChanged(System::String^ propName)
		{ PropertyChanged(this, gcnew	System::ComponentModel::PropertyChangedEventArgs(propName));}
	};

}}}} // namespace Jacobi.Vst.Interop.Host

// typedef for the main exported function from a plugin dll
typedef ::AEffect* (*VSTPluginMain)(::audioMasterCallback);

// static callback function
static VstIntPtr DispatchCallback(AEffect* pEff, VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt);