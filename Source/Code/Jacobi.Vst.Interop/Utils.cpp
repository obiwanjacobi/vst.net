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

}}} // Jacobi::Vst::Interop