#pragma once

ref class AssemblyLoader
{
public:
	static void Initialize(System::String^ baseDir);

private:
	System::String^ _baseDir;
	static AssemblyLoader^ _instance;

	AssemblyLoader(System::String^ baseDir);
	System::Reflection::Assembly^ LoadAssembly(System::Object^ sender, System::ResolveEventArgs^ e);
};
