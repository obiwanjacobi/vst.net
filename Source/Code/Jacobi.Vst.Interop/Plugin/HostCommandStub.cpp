#include "StdAfx.h"
#include "HostCommandStub.h"
#include "..\TypeConverter.h"
#include "..\UnmanagedString.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

// Creates a new instance based on a native callback function pointer.
HostCommandStub::HostCommandStub(::audioMasterCallback hostCallback)
{
	if(hostCallback == NULL)
	{
		throw gcnew System::ArgumentNullException("hostCallback");
	}

	_hostCallback = hostCallback;
	_pluginInfo = NULL;
}

// destructor. See Finalizer
HostCommandStub::~HostCommandStub()
{
	this->!HostCommandStub();
}

// Finalizer deletes the AEffect instance
HostCommandStub::!HostCommandStub()
{
	_hostCallback = NULL;
	
	if(_pluginInfo != NULL)
	{
		delete _pluginInfo;
		_pluginInfo = NULL;
	}
}

// IVstHostCommandStub
// Updates the passed pluginInfo in with the host.
System::Boolean HostCommandStub::UpdatePluginInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo)
{
	if(pluginInfo)
	{
		// overwrite the AEffect values with the new values supplied by pluginInfo.
		_pluginInfo->numInputs = pluginInfo->AudioInputCount;
		_pluginInfo->numOutputs = pluginInfo->AudioOutputCount;
		_pluginInfo->numParams = pluginInfo->ParameterCount;
		_pluginInfo->numPrograms = pluginInfo->ProgramCount;
		return true;
	}

	return false;
}

// IVstHostCommands10
void HostCommandStub::SetParameterAutomated(System::Int32 index, System::Single value)
{
	ThrowIfNotInitialized();

	CallHost(audioMasterAutomate, index, 0, 0, value);
}

System::Int32 HostCommandStub::GetVersion()
{
	//ThrowIfNotInitialized();

	VstInt32 version = CallHost(audioMasterVersion, 0, 0, 0, 0);

	if(version == 0)	// old host
		version = 1;

	return version;
}

System::Int32 HostCommandStub::GetCurrentPluginID()
{
	//ThrowIfNotInitialized();

	return CallHost(audioMasterCurrentId, 0, 0, 0, 0);
}

void HostCommandStub::ProcessIdle()
{
	ThrowIfNotInitialized();

	CallHost(audioMasterIdle, 0, 0, 0, 0);
}

// IVstHostCommands20
Jacobi::Vst::Core::VstTimeInfo^ HostCommandStub::GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	ThrowIfNotInitialized();

	::VstTimeInfo* pTimeInfo = (::VstTimeInfo*)CallHost(audioMasterGetTime, 0, safe_cast<VstInt32>(filterFlags), 0, 0);

	return TypeConverter::ToManagedTimeInfo(pTimeInfo);
}

System::Boolean HostCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
{
	ThrowIfNotInitialized();

	::VstEvents* pEvents = TypeConverter::AllocUnmanagedEvents(events);

	try
	{
		return (CallHost(audioMasterProcessEvents, 0, 0, pEvents, 0) != 0);
	}
	finally
	{
		TypeConverter::DeleteUnmanagedEvents(pEvents);
	}

	return false;
}

System::Boolean HostCommandStub::IoChanged()
{
	//ThrowIfNotInitialized();

	return (CallHost(audioMasterIOChanged, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SizeWindow(System::Int32 width, System::Int32 height)
{
	//ThrowIfNotInitialized();
	
	return (CallHost(audioMasterSizeWindow, width, height, 0, 0) != 0);
}

System::Single HostCommandStub::GetSampleRate()
{
	//ThrowIfNotInitialized();
	
	return safe_cast<System::Single>(CallHost(audioMasterGetSampleRate, 0, 0, 0, 0));
}

System::Int32 HostCommandStub::GetBlockSize()
{
	//ThrowIfNotInitialized();

	return CallHost(audioMasterGetBlockSize, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetInputLatency()
{
	//ThrowIfNotInitialized();
	
	return CallHost(audioMasterGetInputLatency, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetOutputLatency()
{
	//ThrowIfNotInitialized();

	return CallHost(audioMasterGetOutputLatency, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstProcessLevels HostCommandStub::GetProcessLevel()
{
	//ThrowIfNotInitialized();
	
	return safe_cast<Jacobi::Vst::Core::VstProcessLevels>(CallHost(audioMasterGetCurrentProcessLevel, 0, 0, 0, 0));
}

Jacobi::Vst::Core::VstAutomationStates HostCommandStub::GetAutomationState()
{
	//ThrowIfNotInitialized();
	
	return safe_cast<Jacobi::Vst::Core::VstAutomationStates>(CallHost(audioMasterGetAutomationState, 0, 0, 0, 0));
}

//System::Boolean HostCommandStub::OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource)
//{
//	ThrowIfNotInitialized();
//
//	// TODO: implement
//	return false;
//}
//
//System::Boolean HostCommandStub::OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option)
//{
//	ThrowIfNotInitialized();
//
//	// TODO: implement
//	return false;
//}
//
//System::Boolean HostCommandStub::OfflineStart(array<Jacobi::Vst::Core::VstAudioFile^>^ files, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles)
//{
//	ThrowIfNotInitialized();
//
//	// TODO: implement
//	return false;
//}
//
//// bool?
//System::Int32 HostCommandStub::OfflineGetCurrentPass()
//{
//	ThrowIfNotInitialized();
//	
//	return CallHost(audioMasterOfflineGetCurrentPass, 0, 0, 0, 0);
//}
//
//// bool?
//System::Int32 HostCommandStub::OfflineGetCurrentMetaPass()
//{
//	ThrowIfNotInitialized();
//	
//	return CallHost(audioMasterOfflineGetCurrentMetaPass, 0, 0, 0, 0);
//}

System::String^ HostCommandStub::GetVendorString()
{
	//ThrowIfNotInitialized();

	UnmanagedString pText(kVstMaxVendorStrLen);

	if(CallHost(audioMasterGetVendorString, 0, 0, pText, 0) != 0)
	{
		return TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::String^ HostCommandStub::GetProductString()
{
	//ThrowIfNotInitialized();

	UnmanagedString pText(kVstMaxProductStrLen);

	if(CallHost(audioMasterGetProductString, 0, 0, pText, 0) != 0)
	{
		return TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::Int32 HostCommandStub::GetVendorVersion()
{
	//ThrowIfNotInitialized();
	
	return CallHost(audioMasterGetVendorVersion, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstCanDoResult HostCommandStub::CanDo(Jacobi::Vst::Core::VstHostCanDo cando)
{
	//ThrowIfNotInitialized();
	
	UnmanagedString pText(Jacobi::Vst::Core::Constants::MaxCanDoLength + 1);

	TypeConverter::StringToChar(cando.ToString(), pText, Jacobi::Vst::Core::Constants::MaxCanDoLength + 1);
	pText[0] += 32;	// tolower

	return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(CallHost(audioMasterCanDo, 0, 0, pText, 0));
}

Jacobi::Vst::Core::VstHostLanguage HostCommandStub::GetLanguage()
{
	//ThrowIfNotInitialized();
	
	return safe_cast<Jacobi::Vst::Core::VstHostLanguage>(CallHost(audioMasterGetLanguage, 0, 0, 0, 0));
}

System::String^ HostCommandStub::GetDirectory()
{
	//ThrowIfNotInitialized();
	
	return TypeConverter::CharToString((char*)CallHost(audioMasterGetDirectory, 0, 0, 0, 0));
}

System::Boolean HostCommandStub::UpdateDisplay()
{
	ThrowIfNotInitialized();

	return (CallHost(audioMasterUpdateDisplay, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::BeginEdit(System::Int32 index)
{
	ThrowIfNotInitialized();
	
	return (CallHost(audioMasterBeginEdit, index, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::EndEdit(System::Int32 index)
{
	ThrowIfNotInitialized();
	
	return (CallHost(audioMasterEndEdit, index, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::OpenFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
{
	ThrowIfNotInitialized();

	if(fileSelect->Reserved != System::IntPtr::Zero)
	{
		throw gcnew System::InvalidOperationException("The argument is already initialized with an unmanaged VstFileSelect structure.");
	}

	::VstFileSelect* pFileSelect = TypeConverter::AllocUnmanagedFileSelect(fileSelect);

	try
	{
		System::Boolean succeeded = (CallHost(audioMasterOpenFileSelector, 0, 0, pFileSelect, 0) != 0);

		if(succeeded)
		{
			// update the managed type with the users selections.
			TypeConverter::ToManagedFileSelect(fileSelect, pFileSelect);
		}

		return succeeded;
	}
	catch(...)
	{
		TypeConverter::DeleteUnmanagedFileSelect(pFileSelect);
	}

	return false;
}

System::Boolean HostCommandStub::CloseFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
{
	ThrowIfNotInitialized();

	::VstFileSelect* pFileSelect = (::VstFileSelect*)fileSelect->Reserved.ToPointer();

	if(pFileSelect == NULL)
	{
		throw gcnew System::InvalidOperationException("The unmanaged VstFileSelect structure has been disposed.");
	}

	try
	{
		System::Boolean succeeded = (CallHost(audioMasterCloseFileSelector, 0, 0, pFileSelect, 0) != 0);

		return succeeded;
	}
	finally
	{
		TypeConverter::DeleteUnmanagedFileSelect(pFileSelect);
		fileSelect->Reserved = System::IntPtr::Zero;
	}
}

// Throws an InvalidOperationException if the host command stub has not been initialized.
inline void HostCommandStub::ThrowIfNotInitialized()
{
	if(IsInitialized() == false)
	{
		throw gcnew System::InvalidOperationException("The HostCommandStub is not Initialized yet.");
	}
}

}}}} // Jacobi::Vst::Interop::Plugin