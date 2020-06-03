#include "pch.h"
#include "Utils.h"
#include "Bootstrapper.h"

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>
#using <Microsoft.Extensions.Configuration.FileExtensions.dll>
#using <Microsoft.Extensions.Configuration.Json.dll>

namespace Jacobi {
namespace Vst {
namespace Interop {

// static helper method
Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ Bootstrapper::LoadManagedPlugin(System::String^ pluginPath)
{
	// create the plugin (command stub) factory
	Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory = 
		gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory();
	
	// load the managed plugin assembly by its default name
	factory->LoadAssemblyByDefaultName(pluginPath);

	// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
	Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = factory->CreatePluginCommandStub();
	
	if(commandStub != nullptr)
	{
		System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

		auto builder = gcnew Microsoft::Extensions::Configuration::ConfigurationBuilder();
		Microsoft::Extensions::Configuration::FileConfigurationExtensions::SetBasePath(builder, basePath);
		Microsoft::Extensions::Configuration::JsonConfigurationExtensions::AddJsonFile(builder, "vstsettings.json", true);
		// assign config to commandStub
		commandStub->PluginConfiguration = builder->Build();
	}

	return commandStub;
}

}}} // Jacobi::Vst::Interop