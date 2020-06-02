#pragma once

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
	static Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ LoadManagedPlugin(System::String^ pluginPath);
};

}}} // Jacobi::Vst::Interop