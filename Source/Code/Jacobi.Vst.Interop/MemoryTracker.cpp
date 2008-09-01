#include "StdAfx.h"
#include "MemoryTracker.h"

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
// memoryObject must be an array.
void MemoryTracker::RegisterArray(void* arrayObject)
{
	_arrPtrs->Add(System::IntPtr(arrayObject));
}

// deletes all tracked memory pointers.
void MemoryTracker::ClearAll()
{
	for each(System::IntPtr ptr in _memPtrs)
	{
		delete ptr.ToPointer();
	}

	_memPtrs->Clear();

	for each(System::IntPtr ptr in _arrPtrs)
	{
		delete[] ptr.ToPointer();
	}

	_arrPtrs->Clear();
}
