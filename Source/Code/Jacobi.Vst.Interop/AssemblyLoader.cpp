#include "StdAfx.h"
#include "AssemblyLoader.h"

AssemblyLoader::AssemblyLoader(System::String^ baseDir)
{
	System::AppDomain::CurrentDomain->AssemblyResolve += gcnew System::ResolveEventHandler(this, &AssemblyLoader::LoadAssembly);

	_baseDir = baseDir;
}

System::Reflection::Assembly^ AssemblyLoader::LoadAssembly(System::Object^ sender, System::ResolveEventArgs^ e)
{
	array<System::String^>^ parts = e->Name->Split(',');
	System::String^ fileName = parts[0] + ".dll";

	return System::Reflection::Assembly::LoadFile(System::IO::Path::Combine(_baseDir, fileName));
}

void AssemblyLoader::Initialize(System::String^ baseDir)
{
	if(_instance == nullptr)
	{
		_instance = gcnew AssemblyLoader(baseDir);
	}
}