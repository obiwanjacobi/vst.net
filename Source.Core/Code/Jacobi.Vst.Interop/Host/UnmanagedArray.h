#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

/// <summary>
/// caches an array of T and grows on demand
/// </summary>
template<class T>
ref class UnmanagedArray : System::IDisposable
{
public:
	/// <summary>Constructs an empty array.</summary>
	UnmanagedArray(){ _array = NULL; _length = 0; }
	/// <summary>Wraps an existing array pointer to by <paramref name="instance"/> that contains '<paramref name="length"/>' elements.</summary>
	/// <param name="instance">An array of elements of type <typeparamref name="T"/> or NULL.</param>
	/// <param name="length">The number of elements in <paramref name="instance"/>.</param>
	UnmanagedArray(T* instance, long length){ _array = instance; _length = length; }
	~UnmanagedArray() { DeleteArray(); }

	/// <summary>Returns the number of elements in the array.</summary>
	long GetLength()
	{
		return _length;
	}

	/// <summary>Returns the size of the array in bytes.</summary>
	long GetByteLength()
	{
		return _length * sizeof(T);
	}

	/// <summary>Returns a pointer to the start of the array.</summary>
	T* GetArray()
	{
		return _array;
	}

	/// <summary>Returns a pointer to the start of the array that has at least '<paramref name="length"/>' elements.</summary>
	/// <param name="length">The number of elements the array should have.</param>
	/// <remarks>When the array needs to grow, its content is not preserved!</remakrs>
	T* GetArray(int length)
	{
		// check if length is ok
		if(_array != NULL &&
			_length < length)
		{
			DeleteArray();
		}

		if(_array == NULL && length > 0)
		{
			AllocateArray(length);
		}

		return _array;
	}

	operator T*()
	{
		return _array;
	}

private:
	long _length;
	T* _array;

	void AllocateArray(long length)
	{
		_array = new T[length];
		_length = length;
	}

	void DeleteArray()
	{
		if(_array != NULL)
		{
			delete[] _array;
			_array = NULL;
			_length = 0;
		}
	}
};

}}}} // Jacobi::Vst::Interop::Host