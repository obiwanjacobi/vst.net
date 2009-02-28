#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The AssemblyLoader registers the AppDomain event when an assembly is not found
/// and then tries to load it from the plugin folder.
/// </summary>
ref class AssemblyLoader
{
public:
	/// <summary>
	/// Initializes the one and only instance with the plugin folder. Can be called multiple times.
	/// </summary>
	static void Initialize(System::String^ baseDir);

private:
	System::String^ _baseDir;
	static AssemblyLoader^ _instance;

	AssemblyLoader(System::String^ baseDir);
	System::Reflection::Assembly^ LoadAssembly(System::Object^ sender, System::ResolveEventArgs^ e);
};

}}} // Jacobi::Vst::Interop