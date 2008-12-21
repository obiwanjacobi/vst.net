#pragma once

class UnmanagedString
{
public:
	UnmanagedString(int maxLength)
	{
		_buffer = new char[maxLength + 1];
		ZeroMemory(_buffer, maxLength + 1);
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