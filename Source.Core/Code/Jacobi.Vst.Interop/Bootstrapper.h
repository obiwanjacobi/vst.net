#pragma once

#include "Plugin/Configuration.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The Bootstrapper class loads the managed plugin assembly.
/// </summary>
ref class Bootstrapper
{
public:
	// helper
	static Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ LoadManagedPlugin(System::String^ pluginPath, 
		Microsoft::Extensions::Configuration::IConfiguration^ config);
};

}}} // Jacobi::Vst::Interop