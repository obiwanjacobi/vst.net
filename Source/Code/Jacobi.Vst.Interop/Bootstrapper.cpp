#include "pch.h"
#include "Utils.h"
#include "Bootstrapper.h"

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>
#using <Microsoft.Extensions.Configuration.FileExtensions.dll>
#using <Microsoft.Extensions.Configuration.Json.dll>

Microsoft::Extensions::Configuration::IConfigurationRoot^ CreateConfiguration(System::String^ pluginPath)
{
	auto basePath = System::IO::Path::GetDirectoryName(pluginPath);
	auto name = System::IO::Path::GetFileNameWithoutExtension(pluginPath);

	auto builder = gcnew Microsoft::Extensions::Configuration::ConfigurationBuilder();
	Microsoft::Extensions::Configuration::FileConfigurationExtensions::SetBasePath(builder, basePath);
	Microsoft::Extensions::Configuration::JsonConfigurationExtensions::AddJsonFile(
		builder, name + ".appsettings.json", /*optional*/ true);

	return builder->Build();
}

// static helper method
Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ Bootstrapper::LoadManagedPlugin(System::String^ pluginPath)
{
	auto configuration = CreateConfiguration(pluginPath);
	auto probePaths = configuration->default["vstnetProbePaths"];

	// create the plugin (command stub) factory
	auto factory = gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory(probePaths);

	// load the managed plugin assembly by its default name
	factory->LoadAssemblyByDefaultName(pluginPath);

	// create the managed type that implements the Plugin Command Stub interface (sends commands to plugin)
	Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ commandStub = factory->CreatePluginCommandStub();
	
	if(commandStub != nullptr)
	{
		// assign config to commandStub
		commandStub->PluginConfiguration = configuration;
	}

	return commandStub;
}
