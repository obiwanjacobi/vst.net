#include "StdAfx.h"
#include "MemoryTracker.h"

MemoryTracker::MemoryTracker(void)
{
	_memPtrs = gcnew System::Collections::ObjectModel::Collection<System::IntPtr>();
	_arrPtrs = gcnew System::Collections::ObjectModel::Collection<System::IntPtr>();
}

void MemoryTracker::RegisterObject(void* memoryObject)
{
	_memPtrs->Add(System::IntPtr(memoryObject));
}

void MemoryTracker::RegisterArray(void* arrayObject)
{
	_arrPtrs->Add(System::IntPtr(arrayObject));
}

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
