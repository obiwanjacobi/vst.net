#pragma once

#include "Plugin/Configuration.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The Bootstrapper class temporarily subscribes to the AssemlbyResolve event of the current AppDomain.
/// </summary>
/// <remarks>
/// It is only used to load the Jacobi.Vst.Core assembly.
/// </remarks>
ref class Bootstrapper
{
public:
	/// <summary>
	/// Constructs a new instance initialized with the specified <paramref name="basePath"/>.
	/// </summary>
	Bootstrapper(System::String^ basePath, Jacobi::Vst::Plugin::Interop::Configuration^ config);

	property Jacobi::Vst::Plugin::Interop::Configuration^ Configuration
	{ Jacobi::Vst::Plugin::Interop::Configuration^ get() { return _config; } }

	// helper
	static Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ LoadManagedPlugin(System::String^ pluginPath, 
		Jacobi::Vst::Plugin::Interop::Configuration^ config);

private:
	// plugin specific config
	Jacobi::Vst::Plugin::Interop::Configuration^ _config;

	// the deployment folder
	System::String^ _basePath;
};

}}} // Jacobi::Vst::Interop