#include "pch.h"
#include "HostCommandStub.h"
#include "../TypeConverter.h"
#include "../UnmanagedString.h"
#include "../Properties\Resources.h"
#include "../Utils.h"

namespace Jacobi {
namespace Vst {
namespace Plugin {
namespace Interop {

// Creates a new instance based on a native callback function pointer.
HostCommandStub::HostCommandStub(::Vst2HostCommand hostCommand)
{
	if(hostCommand == NULL)
	{
		throw gcnew System::ArgumentNullException("hostCallback");
	}

	_hostCommand = hostCommand;
	_commands = gcnew Jacobi::Vst::Plugin::Interop::HostCommandsImpl(_hostCommand);
}

// destructor. See Finalizer
HostCommandStub::~HostCommandStub()
{
	this->!HostCommandStub();
}

// Finalizer deletes the Vst2Plugin instance
HostCommandStub::!HostCommandStub()
{
	_hostCommand = NULL;
	
	if(_commands != nullptr)
	{
		delete _commands;
		_commands = nullptr;
	}
}

// IVstHostCommandStub
// Updates the passed pluginInfo in with the host.
System::Boolean HostCommandStub::UpdatePluginInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo)
{
	return _commands->UpdatePluginInfo(pluginInfo);
}

}}}} // Jacobi::Vst::Plugin::Interop
