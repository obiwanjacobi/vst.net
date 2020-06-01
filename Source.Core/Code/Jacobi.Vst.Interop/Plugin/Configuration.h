#pragma once

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>

namespace Jacobi {
namespace Vst {
namespace Plugin {
namespace Interop {

/// <summary>
/// The Configuration class manages the plugin specific config file.
/// </summary>
ref class Configuration
{
public:
	/// <summary>
	/// Constructs a new instance based on the file path of the plugin assembly (renamed Interop).
	/// </summary>
	Configuration(System::String^ basePath);

	/// <summary>
	/// The loaded configuration object. Can be null.
	/// </summary>
	property Microsoft::Extensions::Configuration::IConfigurationRoot^ PluginConfig
	{ Microsoft::Extensions::Configuration::IConfigurationRoot^ get() { EnsureConfig(); return _config; } }

private:
	System::String^ _basePath;
	Microsoft::Extensions::Configuration::IConfigurationRoot^ _config;

	void EnsureConfig();
	System::String^ GetAppSetting(System::String^ key);
};

}}}} // Jacobi::Vst::Plugin::Interop