#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

/// <summary>
/// The MemoryTracker class maintains a list of native allocated objects that can be deleted (freed) at another time.
/// </summary>
ref class MemoryTracker
{
public:
	/// <summary>
	/// Constructs a new instance.
	/// </summary>
	MemoryTracker(void);

	/// <summary>
	/// Registers a <paramref name="memoryObject"/> that is not an array.
	/// </summary>
	void RegisterObject(void* memoryObject);
	/// <summary>
	/// Registers an <paramref name="arrayObject"/> that is an array.
	/// </summary>
	void RegisterArray(void* arrayObject);
	/// <summary>
	/// Deletes all 'pointers' tracked.
	/// </summary>
	void ClearAll();

private:
	System::Collections::ObjectModel::Collection<System::IntPtr>^ _memPtrs;
	System::Collections::ObjectModel::Collection<System::IntPtr>^ _arrPtrs;
};

}}} // Jacobi::Vst::Interop