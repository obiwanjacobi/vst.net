#include "StdAfx.h"
#include "Bootstrapper.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

Bootstrapper::Bootstrapper(System::String^ basePath)
{
	_paths = gcnew System::Collections::Generic::List<System::String^>();
	_paths->Add(basePath);

	System::String^ paths = System::Configuration::ConfigurationManager::AppSettings["vstnetProbePaths"];

	if(!System::String::IsNullOrEmpty(paths))
	{
		_paths->AddRange(paths->Split(';'));
	}

	System::AppDomain::CurrentDomain->AssemblyResolve += gcnew System::ResolveEventHandler(this, &Bootstrapper::ResolveAssembly);
}

Bootstrapper::~Bootstrapper()
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