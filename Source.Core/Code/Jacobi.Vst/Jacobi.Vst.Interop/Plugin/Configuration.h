#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

/// <summary>
/// The Configuration class manages the plugin specific config file.
/// </summary>
ref class Configuration
{
public:
	/// <summary>
	/// Constructs a new instance based on the file path of the plugin assembly (renamed Interop).
	/// </summary>
	Configuration(System::String^ exeFilePath);

	/// <summary>
	/// 
	/// </summary>
	property System::Boolean IsValid 
	{ System::Boolean get() { return (_config != nullptr); } }

	/// <summary>
	/// The loaded configuration object. Can be null.
	/// </summary>
	property System::Configuration::Configuration^ PluginConfig
	{ System::Configuration::Configuration^ get() { return _config; } }

	/// <summary>
	/// The probe path config setting. Can be null.
	/// </summary>
	property System::String^ ProbePaths
	{ System::String^ get() { return GetAppSetting(VstNetProbePaths); } }

	/// <summary>
	/// The managed assembly name override. Can be null.
	/// </summary>
	property System::String^ ManagedAssemblyName
	{ System::String^ get() { return GetAppSetting(VstNetManagedAssemblyName); } }

	/// <summary>
	/// The appSettgins config setting 'vstnetProbePaths'.
	/// </summary>
	static System::String^ VstNetProbePaths = "vstnetProbePaths";

	/// <summary>
	/// The appSettgins config setting 'vstnetManagedAssemblyName'.
	/// </summary>
	static System::String^ VstNetManagedAssemblyName = "vstnetManagedAssemblyName";

private:
	System::Configuration::Configuration^ _config;

	System::String^ GetAppSetting(System::String^ key)
	{
		if(_config != nullptr)
		{
			System::Configuration::KeyValueConfigurationElement^ element = _config->AppSettings->Settings[key];

			if(element != nullptr)
			{
				return element->Value;
			}
		}

		return nullptr;
	}
};

}}}} // Jacobi::Vst::Interop::Plugin