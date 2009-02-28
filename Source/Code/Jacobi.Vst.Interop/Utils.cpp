#include "StdAfx.h"
#include "Utils.h"

namespace Jacobi {
namespace Vst {
namespace Interop {

// Shows a MessageBox with the exception information.
void Utils::ShowError(System::Exception^ e)
{
	System::Text::StringBuilder^ txt = gcnew System::Text::StringBuilder();

	while(e != nullptr)
	{
		FormatException(txt, e);

		//
		// Special cases for more info on exceptions
		//

		System::Reflection::ReflectionTypeLoadException^ rtle = dynamic_cast<System::Reflection::ReflectionTypeLoadException^>(e);
		if(rtle != nullptr)
		{
			for each(System::Exception^ le in rtle->LoaderExceptions)
			{
				txt->AppendLine("Loader Exception ------------------------ ");
				FormatException(txt, le);
			}

			if(rtle->Types->Length > 0)
			{
				txt->Append("Types: ");

				for each(System::Type^ type in rtle->Types)
				{
					txt->Append("\t");
					txt->Append(type->FullName);
					txt->AppendLine();
				}
			}
		}

		if(e->InnerException != nullptr)
		{
			txt->AppendLine();
			txt->AppendLine("Inner Exception ------------------------ ");
		}

		e = e->InnerException;
	}

	System::Windows::Forms::MessageBox::Show(nullptr, txt->ToString(), "VST.NET Error", 
		System::Windows::Forms::MessageBoxButtons::OK, System::Windows::Forms::MessageBoxIcon::Error);
}

void Utils::FormatException(System::Text::StringBuilder^ txt, System::Exception^ e)
{
	txt->AppendFormat("{0}: {1}", e->GetType(), e->Message);
	txt->AppendLine();

	txt->Append(e->StackTrace->ToString());
	txt->AppendLine();

	if(e->Data->Count > 0)
	{
		
	}

	if(!System::String::IsNullOrEmpty(e->HelpLink))
	{
		txt->AppendFormat("Help Link: ", e->HelpLink);
		txt->AppendLine();
	}
}

// Shows a MessageBox with the warning message
void Utils::ShowWarning(System::String^ message)
{
	System::Windows::Forms::MessageBox::Show(nullptr, message, "VST.NET Warning", 
		System::Windows::Forms::MessageBoxButtons::OK, System::Windows::Forms::MessageBoxIcon::Exclamation);
}

}}} // Jacobi::Vst::Interop