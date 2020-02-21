#pragma once

#include "VstPluginContext.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

	/// <summary>
	/// Implements a PluginContext for a Managed Plugin, bypassing the double interop that would occur.
	/// </summary>
	ref class VstManagedPluginContext : public VstPluginContext
	{
	public:
		/// <summary>
		/// Copies the new values provided by the Plugin to the <see cref="PluginInfo"/> property.
		/// </summary>
		/// <param name="raiseEvents">When true the <see cref="PropertyChanged"/> event will be raised for each property that has changed.</param>
		/// <remarks>All property names will be prefixed with 'PluginInfo.' to indicate the path to the property.</remarks>
		virtual void AcceptPluginInfoData(System::Boolean raiseEvents) override;

	internal:
		/// <summary>
		/// Constructs a new uninitialized instance using the <paramref name="hostCmdStub"/>.
		/// </summary>
		/// <param name="pluginPath">An absolute path the the plugin dll. Must not be null or empty.</param>
		/// <param name="hostCmdStub">An implementation of the host command stub. Must not be null.</param>
		/// <remarks>Returns null when not successful.</remarks>
		static VstPluginContext^ CreateInternal(System::String^ pluginPath, Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);

	protected:
		/// <summary>
		/// Constructs a new uninitialized instance using the <paramref name="hostCmdStub"/>.
		/// </summary>
		/// <param name="hostCmdStub">An implementation of the host command stub. Must not be null.</param>
		VstManagedPluginContext(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub);

		/// <summary>
		/// Initializes the PluginContext instance with the plugin pointed to by the <paramref name="pluginPath"/>.
		/// </summary>
		/// <param name="pluginPath">An absolute path the the plugin dll (that contains the exported 
		/// 'VSTPluginMain' function). Must not be null or empty.</param>
		virtual void Initialize(System::String^ pluginPath) override;

	private:
		Jacobi::Vst::Core::Plugin::VstPluginInfo^ _internalPluginInfo;
	};

}}}} // namespace Jacobi::Vst::Interop::Host
