#include "pch.h"
#include "Utils.h"
#include "Bootstrapper.h"
#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>

namespace Jacobi {
namespace Vst {
namespace Interop {

Bootstrapper::Bootstrapper(System::String^ basePath, Jacobi::Vst::Plugin::Interop::Configuration^ config)
{
	// Note: cannot use the Throw helper class, because that's in Core which isn't loaded yet.
	if(System::String::IsNullOrEmpty(basePath))
	{
		throw gcnew System::ArgumentNullException("basePath", "Argument can not be null or empty.");
	}
	if(config == nullptr)
	{
		throw gcnew System::ArgumentNullException("config");
	}

	_basePath = basePath;
	_config = config;
}

// static helper method
Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ Bootstrapper::LoadManagedPlugin(System::String^ pluginPath, Jacobi::Vst::Plugin::Interop::Configuration^ config)
{
	System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

	// set the base path to the assembly loader
	Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->BasePath = basePath;

	// create the plugin (command stub) factory
	Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory = 
		gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory();
	
	// load the managed plugin assembly by its default name
	factory->LoadAssemblyByDefaultName(pluginPath);

	// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
	Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = factory->CreatePluginCommandStub();
	
	if(commandStub != nullptr)
	{
		// assign config to commandStub (can be null)
		commandStub->PluginConfiguration = config->PluginConfig;
	}

	return commandStub;
}

}}} // Jacobi::Vst::Interop