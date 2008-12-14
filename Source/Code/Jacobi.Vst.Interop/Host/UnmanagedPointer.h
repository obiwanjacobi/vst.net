#pragma once

template<class T>
class UnmanagedPointer
{
public:
	UnmanagedPointer(bool allocate){ if(allocate) _instance = new T(); else _instance = NULL; }
	UnmanagedPointer(T* instance){ _instance = instance; }
	virtual ~UnmanagedPointer(){ delete _instance; }

	T** operator &()
	{
		return &_instance;
	}

	operator T*()
	{
		return _instance;
	}

protected:
	T* _instance;
};