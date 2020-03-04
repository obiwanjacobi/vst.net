#include "pch.h"
#include "Configuration.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

Configuration::Configuration(System::String^ exeFilePath)
{
	_config = System::Configuration::ConfigurationManager::OpenExeConfiguration(exeFilePath);

	if(_config != nullptr && !_config->HasFile)
	{
		_config = nullptr;
	}
}

System::String^ Configuration::GetAppSetting(System::String^ key)
{
	if (_config != nullptr)
	{
		System::Configuration::KeyValueConfigurationElement^ element = _config->AppSettings->Settings[key];

		if (element != nullptr)
		{
			return element->Value;
		}
	}

	return nullptr;
}

}}}} // Jacobi::Vst::Interop::Plugin