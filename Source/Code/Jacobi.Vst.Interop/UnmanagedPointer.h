#pragma once

template<class T>
class UnmanagedPointer
{
public:
	UnmanagedPointer(){ _instance = new T(); }
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

	T* operator->()
	{
		return _instance;
	}

protected:
	T* _instance;
};