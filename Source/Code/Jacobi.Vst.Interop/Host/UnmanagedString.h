#pragma once

class UnmanagedString
{
public:
	UnmanagedString(int maxLength)
	{
		_buffer = new char[maxLength];
	}

	~UnmanagedString()
	{
		delete [] _buffer;
	}

	operator char*()
	{
		return _buffer;
	}

private:
	char* _buffer;

};