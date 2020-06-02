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
	/// Finds and opens the vstsettings.json.
	/// </summary>
	/// <returns>Can return null if settings are not found.</returns>
	static Microsoft::Extensions::Configuration::IConfiguration^ OpenConfig(System::String^ basePath);

};

}}}} // Jacobi::Vst::Plugin::Interop