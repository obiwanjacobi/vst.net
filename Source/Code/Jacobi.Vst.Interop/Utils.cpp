#include "StdAfx.h"
#include "Utils.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

// Shows a MessageBox with the exception information.
void Utils::ShowError(System::Exception^ e)
{
	System::String^ text = Jacobi::Vst::Core::Diagnostics::ErrorHelper::FormatException(e);

	System::Windows::Forms::MessageBox::Show(nullptr, text, "VST.NET Error", 
		System::Windows::Forms::MessageBoxButtons::OK, System::Windows::Forms::MessageBoxIcon::Error);
}

// Shows a MessageBox with the warning message
void Utils::ShowWarning(System::String^ message)
{
	System::Windows::Forms::MessageBox::Show(nullptr, message, "VST.NET Warning", 
		System::Windows::Forms::MessageBoxButtons::OK, System::Windows::Forms::MessageBoxIcon::Exclamation);
}

System::Boolean Utils::AddPaths(System::Collections::Generic::List<System::String^>^ pathList, System::String^ paths, System::String^ basePath)
{
	if(pathList != nullptr && !System::String::IsNullOrEmpty(paths))
	{
		if(!System::String::IsNullOrEmpty(basePath))
		{
			for each(System::String^ path in paths->Split(';'))
			{
				if(!System::String::IsNullOrEmpty(path) && !System::IO::Path::IsPathRooted(path) && path->StartsWith(".\\"))
				{
					path = System::IO::Path::Combine(basePath, path->Substring(2));
				}

				pathList->Add(path);
			}
		}
		else
		{
			pathList->AddRange(paths->Split(';'));
		}

		return true;
	}

	return false;
}

}}} // Jacobi::Vst::Interop