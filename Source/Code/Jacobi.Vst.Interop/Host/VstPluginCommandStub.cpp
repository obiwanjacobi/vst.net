#include "pch.h"
#include "UnmanagedArray.h"
#include "VstPluginCommandStub.h"
#include "..\TypeConverter.h"
#include "..\UnmanagedString.h"
#include "..\UnmanagedPointer.h"
#include "..\Properties\Resources.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {

VstPluginCommandStub::VstPluginCommandStub(::Vst2Plugin* plugin)
{
	if(plugin == NULL)
	{
		throw gcnew System::ArgumentNullException("plugin");
	}

	_cmdImpl = gcnew Jacobi::Vst::Host::Interop::VstPluginCommandsImpl(plugin);
}


}}}} // Jacobi::Vst::Host::Interop
