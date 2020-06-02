#include "pch.h"
#include "Configuration.h"

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>
#using <Microsoft.Extensions.Configuration.FileExtensions.dll>
#using <Microsoft.Extensions.Configuration.Json.dll>

namespace Jacobi {
namespace Vst {
namespace Plugin {
namespace Interop {

Microsoft::Extensions::Configuration::IConfiguration^ Configuration::OpenConfig(System::String^ basePath)
{
	auto builder = gcnew Microsoft::Extensions::Configuration::ConfigurationBuilder();
	Microsoft::Extensions::Configuration::FileConfigurationExtensions::SetBasePath(builder, basePath);
	Microsoft::Extensions::Configuration::JsonConfigurationExtensions::AddJsonFile(builder, "vstsettings.json", true);
	return builder->Build();
}

}}}} // Jacobi::Vst::Plugin::Interop