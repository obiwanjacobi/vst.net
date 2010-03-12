#include "StdAfx.h"
#include "Utils.h"
#include "Bootstrapper.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

Bootstrapper::Bootstrapper(System::String^ basePath, Jacobi::Vst::Interop::Plugin::Configuration^ config)
{
	// Note: cannot use the Throw helper class, because that's in Core which isn't loaded yet.
	if(System::String::IsNullOrEmpty(basePath))
	{
		throw gcnew System::ArgumentNullException("basePath", "Argument can not be null or empty.");
	}
	if(config == nullptr)
	{
		throw gcnew System::ArgumentNullException("config");
	}

	_config = config;

	// add base path of plugin location
	_paths = gcnew System::Collections::Generic::List<System::String^>();
	_paths->Add(basePath);

	// add (optional) plugin config probe paths
	System::String^ paths = _config->ProbePaths;
	Jacobi::Vst::Interop::Utils::AddPaths(_paths, paths, basePath);

	// add (optional) global (exe)config probe paths
	paths = System::Configuration::ConfigurationManager::AppSettings[Jacobi::Vst::Interop::Plugin::Configuration::VstNetProbePaths];
	Jacobi::Vst::Interop::Utils::AddPaths(_paths, paths, basePath);

	System::AppDomain::CurrentDomain->AssemblyResolve += gcnew System::ResolveEventHandler(this, &Bootstrapper::ResolveAssembly);
}

Bootstrapper::~Bootstrapper()
{
	this->!Bootstrapper();
}

Bootstrapper::!Bootstrapper()
{
	System::AppDomain::CurrentDomain->AssemblyResolve -= gcnew System::ResolveEventHandler(this, &Bootstrapper::ResolveAssembly);
}

System::Reflection::Assembly^ Bootstrapper::ResolveAssembly(System::Object^ sender, System::ResolveEventArgs^ e)
{
	System::Reflection::AssemblyName^ assemblyName = gcnew System::Reflection::AssemblyName(e->Name);

	return LoadAssembly(assemblyName->Name + ".dll");
}

System::Reflection::Assembly^ Bootstrapper::LoadAssembly(System::String^ fileName)
{
	for each(System::String^ path in _paths)
	{
		System::String^ filePath = System::IO::Path::Combine(path, fileName);

		if(System::IO::File::Exists(filePath))
		{
			return System::Reflection::Assembly::LoadFile(filePath);
		}
	}

	return nullptr;
}

}}} // Jacobi::Vst::Interop