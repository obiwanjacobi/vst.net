#include "pch.h"
#include "MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

// default ctor.
MemoryTracker::MemoryTracker(void)
{
	_memPtrs = gcnew System::Collections::ObjectModel::Collection<System::IntPtr>();
	_arrPtrs = gcnew System::Collections::ObjectModel::Collection<System::IntPtr>();
}

// add a memoryObject to the tracker.
// memoryObject must NOT be an array.
void MemoryTracker::RegisterObject(void* memoryObject)
{
	_memPtrs->Add(System::IntPtr(memoryObject));
}

// add an arrayObject to the tracker.
// arrayObject must be an array.
void MemoryTracker::RegisterArray(void* arrayObject)
{
	_arrPtrs->Add(System::IntPtr(arrayObject));
}

// deletes all tracked memory pointers.
void MemoryTracker::ClearAll()
{
	for each(System::IntPtr ptr in _memPtrs)
	{
		void* p = ptr.ToPointer();
		if(p != NULL)
		{
			delete p;
		}
	}

	_memPtrs->Clear();

	for each(System::IntPtr ptr in _arrPtrs)
	{
		void* p = ptr.ToPointer();
		if(p != NULL)
		{
			delete[] p;
		}
	}

	_arrPtrs->Clear();
}

}}} // Jacobi::Vst::Interop