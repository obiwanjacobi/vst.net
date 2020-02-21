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

}}}} // Jacobi::Vst::Interop::Plugin