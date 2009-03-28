#pragma once

#include "VstPluginContext.h"
#include "VstHostCommandProxy.h"
#include "VstPluginCommandStub.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	/// <summary>
	/// Implements a PluginContext for an unmanaged Plugin, marshalling the calls between the Context and the Plugin.
	/// </summary>
	ref class VstUnmanagedPluginContext : public VstPluginContext
	{
	public:
		/// <summary>
		/// Disposes managed resources and calls the finalizer.
		/// </summary>
		~VstUnmanagedPluginContext();
		/// <summary>
		/// Disposes unmanaged resources.
		/// </summary>
		!VstUnmanagedPluginContext();
		/// <summary>
		/// Copies the new values from the unmanaged AEffect structure to the <see cref="PluginInfo"/> property.
		/// </summary>
		/// <param name="raiseEvents">When true the <see cref="PropertyChanged"/> event will be raised for each property that has changed.</param>
		/// <remarks>All property names will be prefixed with 'PluginInfo.' to indicate the path to the property.</remarks>
		virtual void AcceptPluginInfoData(System::Boolean raiseEvents) override;

	internal:
		/// <summary>Gets or sets the plugin context of the plugin that is currently loading.</summary>
		/// <remarks>Only set during loading of plugin (Create)</remarks>
		static property VstUnmanagedPluginContext^ LoadingPlugin;

		/// <summary>Gets a reference to the host command proxy.</summary>
		/// <remarks>Used to dispatch incoming requests from the plugin.</remarks>
		property VstHostCommandProxy^ HostCommandProxy 
		{ VstHostCommandProxy^ get() { return _hostCmdProxy; } }

	protected:
		/// <summary>
		/// Constructs a new uninitialized instance using the <paramref name="hostCmdStub"/>.
		/// </summary>
		/// <param name="hostCmdStub">An implementation of the host command stub. Must not be null.</param>
		VstUnmanagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
		/// <summary>
		/// Constructs a new uninitialized instance using the <paramref name="hostCmdStub"/>.
		/// </summary>
		/// <param name="pluginPath">An absolute path the the plugin dll. Must not be null or empty.</param>
		/// <param name="hostCmdStub">An implementation of the host command stub. Must not be null.</param>
		static VstPluginContext^ Create(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);
		/// <summary>
		/// Initializes the PluginContext instance with the plugin pointed to by the <paramref name="pluginPath"/>.
		/// </summary>
		/// <param name="pluginPath">An absolute path the the plugin dll (that contains the exported 
		/// 'VSTPluginMain' function). Must not be null or empty.</param>
		virtual void Initialize(System::String^ pluginPath) override;
		/// <summary>Cleans up unmanaged resources.</summary>
		virtual void Uninitialize() override;

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