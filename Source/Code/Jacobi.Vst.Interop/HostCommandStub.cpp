#include "StdAfx.h"
#include "HostCommandStub.h"
#include "TypeConverter.h"

HostCommandStub::HostCommandStub(::audioMasterCallback hostCallback)
{
	if(hostCallback == NULL)
	{
		throw gcnew System::ArgumentNullException("hostCallback");
	}

	_hostCallback = hostCallback;
}

// IVstHostCommandStub10
void HostCommandStub::SetParameterAutomated(System::Int32 index, System::Single value)
{
	ThrowIfNotInitialized();

	CallHost(audioMasterAutomate, index, 0, 0, value);
}

System::Int32 HostCommandStub::GetVersion()
{
	ThrowIfNotInitialized();

	VstInt32 version = CallHost(audioMasterVersion, 0, 0, 0, 0);

	if(version == 0)	// old host
		version = 1;

	return version;
}

System::Int32 HostCommandStub::GetCurrentPluginID()
{
	ThrowIfNotInitialized();

	return CallHost(audioMasterCurrentId, 0, 0, 0, 0);
}

void HostCommandStub::ProcessIdle()
{
	ThrowIfNotInitialized();

	CallHost(audioMasterIdle, 0, 0, 0, 0);
}

// IVstHostCommandStub
Jacobi::Vst::Core::VstTimeInfo^ HostCommandStub::GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	ThrowIfNotInitialized();

	::VstTimeInfo* pTimeInfo = (::VstTimeInfo*)CallHost(audioMasterGetTime, 0, (VstInt32)filterFlags, 0, 0);

	return TypeConverter::ToTimeInfo(pTimeInfo);
}

System::Boolean HostCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
{
	ThrowIfNotInitialized();

	::VstEvents* pEvents = TypeConverter::FromEventsArray(events);

	try
	{
		return (CallHost(audioMasterProcessEvents, 0, 0, pEvents, 0) != 0);
	}
	finally
	{
		TypeConverter::DeleteVstEvents(pEvents);
	}

	return false;
}

System::Boolean HostCommandStub::IoChanged()
{
	ThrowIfNotInitialized();

	return (CallHost(audioMasterIOChanged, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SizeWindow(System::Int32 width, System::Int32 height)
{
	ThrowIfNotInitialized();
	
	return (CallHost(audioMasterSizeWindow, width, height, 0, 0) != 0);
}

System::Double HostCommandStub::GetSampleRate()
{
	ThrowIfNotInitialized();
	
	return (System::Double)CallHost(audioMasterGetSampleRate, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetBlockSize()
{
	ThrowIfNotInitialized();

	return CallHost(audioMasterGetBlockSize, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetInputLatency()
{
	ThrowIfNotInitialized();
	
	return CallHost(audioMasterGetInputLatency, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetOutputLatency()
{
	ThrowIfNotInitialized();

	return CallHost(audioMasterGetOutputLatency, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstProcessLevels HostCommandStub::GetProcessLevel()
{
	ThrowIfNotInitialized();
	
	return (Jacobi::Vst::Core::VstProcessLevels)CallHost(audioMasterGetCurrentProcessLevel, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstAutomationStates HostCommandStub::GetAutomationState()
{
	ThrowIfNotInitialized();
	
	return (Jacobi::Vst::Core::VstAutomationStates)CallHost(audioMasterGetAutomationState, 0, 0, 0, 0);
}

System::Boolean HostCommandStub::OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource)
{
	ThrowIfNotInitialized();

	// TODO: implement
	return false;
}

System::Boolean HostCommandStub::OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option)
{
	ThrowIfNotInitialized();

	// TODO: implement
	return false;
}

System::Boolean HostCommandStub::OfflineStart(Jacobi::Vst::Core::VstAudioFile^ file, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles)
{
	ThrowIfNotInitialized();

	// TODO: implement
	return false;
}

// bool?
System::Int32 HostCommandStub::OfflineGetCurrentPass()
{
	ThrowIfNotInitialized();
	
	return CallHost(audioMasterOfflineGetCurrentPass, 0, 0, 0, 0);
}

// bool?
System::Int32 HostCommandStub::OfflineGetCurrentMetaPass()
{
	ThrowIfNotInitialized();
	
	return CallHost(audioMasterOfflineGetCurrentMetaPass, 0, 0, 0, 0);
}

System::String^ HostCommandStub::GetVendorString()
{
	ThrowIfNotInitialized();

	System::String^ str = nullptr;
	char* pText = new char[64];

	if(CallHost(audioMasterGetVendorString, 0, 0, pText, 0) != 0)
	{
		str = TypeConverter::CharToString(pText);
	}

	delete[] pText;

	return str;
}

System::String^ HostCommandStub::GetProductString()
{
	ThrowIfNotInitialized();

	System::String^ str = nullptr;
	char* pText = new char[64];

	if(CallHost(audioMasterGetProductString, 0, 0, pText, 0) != 0)
	{
		str = TypeConverter::CharToString(pText);
	}

	delete[] pText;

	return str;
}

System::Int32 HostCommandStub::GetVendorVersion()
{
	ThrowIfNotInitialized();
	
	return CallHost(audioMasterGetVendorVersion, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstCanDoResult HostCommandStub::CanDo(Jacobi::Vst::Core::VstHostCanDo cando)
{
	ThrowIfNotInitialized();
	
	char* pText = new char[64];
	TypeConverter::StringToChar(cando.ToString(), pText, 64);
	pText[0] += 32;	// tolower

	Jacobi::Vst::Core::VstCanDoResult result = 
		(Jacobi::Vst::Core::VstCanDoResult)CallHost(audioMasterCanDo, 0, 0, pText, 0);

	delete[] pText;

	return result;
}

Jacobi::Vst::Core::VstHostLanguage HostCommandStub::GetLanguage()
{
	ThrowIfNotInitialized();
	
	return (Jacobi::Vst::Core::VstHostLanguage)CallHost(audioMasterGetLanguage, 0, 0, 0, 0);
}

System::String^ HostCommandStub::GetDirectory()
{
	ThrowIfNotInitialized();
	
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

System::Boolean HostCommandStub::OpenFileSelector(/*VstFileSelect*/)
{
	ThrowIfNotInitialized();

	// TODO: implement
	return false;
}

System::Boolean HostCommandStub::CloseFileSelector(/*VstFileSelect*/)
{
	ThrowIfNotInitialized();

	// TODO: implement
	return false;
}

inline void HostCommandStub::ThrowIfNotInitialized()
{
	if(IsInitialized() == false)
	{
		throw gcnew System::InvalidOperationException("The HostCommandStub is not Initialized yet.");
	}
}