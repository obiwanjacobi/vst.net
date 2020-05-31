#include "pch.h"
#include "Configuration.h"

#using <Microsoft.Extensions.Configuration.dll>
#using <Microsoft.Extensions.Configuration.Abstractions.dll>
#using <Microsoft.Extensions.Configuration.FileExtensions.dll>
#using <Microsoft.Extensions.Configuration.Json.dll>

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

Configuration::Configuration(System::String^ filePath)
{
	_basePath = filePath;
}

void Configuration::EnsureConfig()
{
	if (_config == nullptr)
	{
		auto builder = gcnew Microsoft::Extensions::Configuration::ConfigurationBuilder();
		Microsoft::Extensions::Configuration::FileConfigurationExtensions::SetBasePath(builder, _basePath);
		Microsoft::Extensions::Configuration::JsonConfigurationExtensions::AddJsonFile(builder, "vstsettings.json", true);
		_config = builder->Build();
	}
}

System::String^ Configuration::GetAppSetting(System::String^ key)
{
	EnsureConfig();

	if (_config != nullptr)
	{
		return _config->default[key];
	}

	return nullptr;
}

}}}} // Jacobi::Vst::Interop::Plugin