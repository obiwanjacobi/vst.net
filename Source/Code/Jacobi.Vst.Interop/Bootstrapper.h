#pragma once

/// <summary>
/// The Bootstrapper class loads the managed plugin assembly.
/// </summary>
class Bootstrapper
{
public:
	// helper
	static Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ LoadManagedPlugin(System::String^ pluginPath);
};
