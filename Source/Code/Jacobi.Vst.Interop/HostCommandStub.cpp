#include "StdAfx.h"
#include "HostCommandStub.h"

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
}

System::Int32 HostCommandStub::GetVersion()
{
	ThrowIfNotInitialized();
	return 0;
}

System::Int32 HostCommandStub::GetCurrentPluginID()
{
	ThrowIfNotInitialized();
	return 0;
}

void HostCommandStub::ProcessIdle()
{
	ThrowIfNotInitialized();
}

// IVstHostCommandStub
Jacobi::Vst::Core::VstTimeInfo^ HostCommandStub::GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	ThrowIfNotInitialized();
	return nullptr;
}

System::Boolean HostCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::IoChanged()
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::SizeWindow(System::Int32 width, System::Int32 height)
{
	ThrowIfNotInitialized();
	return false;
}

double HostCommandStub::GetSampleRate()
{
	ThrowIfNotInitialized();
	return 0.0;
}

System::Int32 HostCommandStub::GetBlockSize()
{
	ThrowIfNotInitialized();
	return 0;
}

System::Int32 HostCommandStub::GetInputLatency()
{
	ThrowIfNotInitialized();
	return 0;
}

System::Int32 HostCommandStub::GetOutputLatency()
{
	ThrowIfNotInitialized();
	return 0;
}

Jacobi::Vst::Core::VstProcessLevels HostCommandStub::GetProcessLevel()
{
	ThrowIfNotInitialized();
	return Jacobi::Vst::Core::VstProcessLevels::Unknown;
}

Jacobi::Vst::Core::VstAutomationStates HostCommandStub::GetAutomationState()
{
	ThrowIfNotInitialized();
	return Jacobi::Vst::Core::VstAutomationStates::Unsupported;
}

System::Boolean HostCommandStub::OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource)
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option)
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::OfflineStart(Jacobi::Vst::Core::VstAudioFile^ file, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles)
{
	ThrowIfNotInitialized();
	return false;
}

System::Int32 HostCommandStub::OfflineGetCurrentPass()
{
	ThrowIfNotInitialized();
	return 0;
}

System::Int32 HostCommandStub::OfflineGetCurrentMetaPass()
{
	ThrowIfNotInitialized();
	return 0;
}

System::String^ HostCommandStub::GetVendorString()
{
	ThrowIfNotInitialized();
	return nullptr;
}

System::String^ HostCommandStub::GetProductString()
{
	ThrowIfNotInitialized();
	return nullptr;
}

System::Int32 HostCommandStub::GetVendorVersion()
{
	ThrowIfNotInitialized();
	return 0;
}

System::Boolean HostCommandStub::CanDo(System::String^ cando)
{
	ThrowIfNotInitialized();
	return false;
}

Jacobi::Vst::Core::VstHostLanguage HostCommandStub::GetLanguage()
{
	ThrowIfNotInitialized();
	return Jacobi::Vst::Core::VstHostLanguage::NotSupported;
}

System::String^ HostCommandStub::GetDirectory()
{
	ThrowIfNotInitialized();
	return nullptr;
}

System::Boolean HostCommandStub::UpdateDisplay()
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::BeginEdit()
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::EndEdit()
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::OpenFileSelector(/*VstFileSelect*/)
{
	ThrowIfNotInitialized();
	return false;
}

System::Boolean HostCommandStub::CloseFileSelector(/*VstFileSelect*/)
{
	ThrowIfNotInitialized();
	return false;
}

inline void HostCommandStub::ThrowIfNotInitialized()
{
	if(IsInitialized() == false)
	{
		throw gcnew System::InvalidOperationException("The HostCommandStub is not Initialized yet.");
	}
}