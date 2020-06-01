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
	// helper
	static Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ LoadManagedPlugin(System::String^ pluginPath, 
		Jacobi::Vst::Plugin::Interop::Configuration^ config);
};

}}} // Jacobi::Vst::Interop