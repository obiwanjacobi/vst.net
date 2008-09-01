#include "StdAfx.h"
#include "AssemblyLoader.h"

// constructs a new instance and registers a handler to resolve assemblies.
// The baseDir points to the folder that contains the assemblies to resolve.
AssemblyLoader::AssemblyLoader(System::String^ baseDir)
{
	System::AppDomain::CurrentDomain->AssemblyResolve += gcnew System::ResolveEventHandler(this, &AssemblyLoader::LoadAssembly);

	_baseDir = baseDir;
}

// The handler that resolves assemblies.
System::Reflection::Assembly^ AssemblyLoader::LoadAssembly(System::Object^ sender, System::ResolveEventArgs^ e)
{
	array<System::String^>^ parts = e->Name->Split(',');
	System::String^ fileName = parts[0] + ".dll";

	return System::Reflection::Assembly::LoadFile(System::IO::Path::Combine(_baseDir, fileName));
}

// Static helper method that initializes the loader only once, even when called multiple times.
void AssemblyLoader::Initialize(System::String^ baseDir)
{
	if(_instance == nullptr)
	{
		_instance = gcnew AssemblyLoader(baseDir);
	}
}