#pragma once

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>

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
	/// The loaded configuration object. Can be null.
	/// </summary>
	property Microsoft::Extensions::Configuration::IConfigurationRoot^ PluginConfig
	{ Microsoft::Extensions::Configuration::IConfigurationRoot^ get() { EnsureConfig(); return _config; } }

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
	Microsoft::Extensions::Configuration::IConfigurationRoot^ _config;
	void EnsureConfig();

	System::String^ _basePath;

	System::String^ GetAppSetting(System::String^ key);
};

}}}} // Jacobi::Vst::Interop::Plugin