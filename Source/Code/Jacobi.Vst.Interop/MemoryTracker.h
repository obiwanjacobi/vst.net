#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

ref class MemoryTracker
{
public:
	MemoryTracker(void);

	void RegisterObject(void* memoryObject);
	void RegisterArray(void* arrayObject);
	void ClearAll();

private:
	System::Collections::ObjectModel::Collection<System::IntPtr>^ _memPtrs;
	System::Collections::ObjectModel::Collection<System::IntPtr>^ _arrPtrs;
};

}}} // Jacobi::Vst::Interop