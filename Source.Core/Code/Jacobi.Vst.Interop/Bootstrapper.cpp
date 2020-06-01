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

	_config = config;

	// add base path of plugin location
	_paths = gcnew System::Collections::Generic::List<System::String^>();
	_paths->Add(basePath);

	// add (optional) plugin config probe paths
	System::String^ paths = _config->ProbePaths;
	Jacobi::Vst::Interop::Utils::AddPaths(_paths, paths, basePath);
}

// static helper method
Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ Bootstrapper::LoadManagedPlugin(System::String^ pluginPath, Jacobi::Vst::Plugin::Interop::Configuration^ config)
{
	System::String^ basePath = System::IO::Path::GetDirectoryName(pluginPath);

	// add the vst plugin directory to the assembly loader
	Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->PrivateProbePaths->Add(basePath);

	// add the probe paths from the plugin config to the assembly loader
	Utils::AddPaths(Jacobi::Vst::Core::Plugin::AssemblyLoader::Current->PrivateProbePaths, config->ProbePaths, basePath);

	// create the plugin (command stub) factory
	Jacobi::Vst::Core::Plugin::ManagedPluginFactory^ factory = 
		gcnew Jacobi::Vst::Core::Plugin::ManagedPluginFactory();
	
	// load the managed plugin assembly either by a specific name from config or default name
	if(!System::String::IsNullOrEmpty(config->ManagedAssemblyName))
	{
		factory->LoadAssembly(config->ManagedAssemblyName);
	}
	else
	{
		factory->LoadAssemblyByDefaultName(pluginPath);
	}

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