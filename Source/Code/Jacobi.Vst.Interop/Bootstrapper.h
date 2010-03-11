#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The Bootstrapper class temporarily subscribes to the AssemlbyResolve event of the current AppDomain.
/// </summary>
/// <remarks>
/// It is only used to load the Jacobi.Vst.Core assembly.
/// </remarks>
ref class Bootstrapper
{
public:
	/// <summary>
	/// Constructs a new instance initialized with the specified <paramref name="basePath"/>
	/// as well as the paths loaded from the "vstnetProbePaths" config appSettings.
	/// </summary>
	Bootstrapper(System::String^ basePath);
	/// <summary>
	/// Unregisters the bootstrapper from the AssemlbyResolve event.
	/// </summary>
	~Bootstrapper();

	

private:
	// contains the private probe paths
	System::Collections::Generic::List<System::String^>^ _paths;

	// AppDomain::AssemlbyResolve event handler
	System::Reflection::Assembly^ ResolveAssembly(System::Object^ sender, System::ResolveEventArgs^ e);
	// helper method to load the assembly using the probe paths
	System::Reflection::Assembly^ LoadAssembly(System::String^ fileName);
};

}}} // Jacobi::Vst::Interop