#include "StdAfx.h"
#include "Utils.h"

void Utils::ShowError(System::Exception^ e)
{
	System::String^ text = e->ToString();

	System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(text);

	::MessageBox(NULL, (LPCTSTR)mem.ToPointer(), LPCTSTR("VST.NET Error"), MB_ICONERROR | MB_OK);

	System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
}

void Utils::ShowWarning(System::String^ message)
{
	System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(message);

	::MessageBox(NULL, (LPCTSTR)mem.ToPointer(), LPCTSTR("VST.NET Warning"), MB_ICONEXCLAMATION | MB_OK);

	System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
}

