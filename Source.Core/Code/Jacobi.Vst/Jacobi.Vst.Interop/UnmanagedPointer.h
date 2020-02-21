#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

template<class T>
class UnmanagedPointer
{
public:
	UnmanagedPointer()
	{ 
		_instance = new T();
		_owned = true;
	}
	
	UnmanagedPointer(T* instance)
	{
		_instance = instance;
		_owned = (instance != NULL);
	}
	
	virtual ~UnmanagedPointer()
	{
		if(_owned)
		{
			delete _instance;
		}

		_instance = NULL;
	}

	T** operator &()
	{
		return &_instance;
	}

	operator T*()
	{
		return _instance;
	}

	T* operator->()
	{
		return _instance;
	}

protected:
	T* _instance;
	bool _owned;
};

}}} // Jacobi::Vst::Interop