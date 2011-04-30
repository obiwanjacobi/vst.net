#include "StdAfx.h"
#include "HostCommandStub.h"
#include "..\TypeConverter.h"
#include "..\UnmanagedString.h"
#include "..\Properties\Resources.h"
#include "..\Utils.h"

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

	_timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();
	_traceCtx = gcnew Jacobi::Vst::Core::Diagnostics::TraceContext(Utils::GetPluginName() + ".Plugin.HostCommandStub", Jacobi::Vst::Core::Plugin::IVstHostCommandStub::typeid);
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
	if(_pluginInfo && pluginInfo)
	{
		// overwrite the AEffect values with the new values supplied by pluginInfo.
		_pluginInfo->numParams = pluginInfo->ParameterCount;
		_pluginInfo->numPrograms = pluginInfo->ProgramCount;

		if (_pluginInfo->numInputs != pluginInfo->AudioInputCount ||
			_pluginInfo->numOutputs != pluginInfo->AudioOutputCount ||
			_pluginInfo->initialDelay != pluginInfo->InitialDelay)
		{
			_pluginInfo->numInputs = pluginInfo->AudioInputCount;
			_pluginInfo->numOutputs = pluginInfo->AudioOutputCount;
			_pluginInfo->initialDelay = pluginInfo->InitialDelay;

			return IoChanged();
		}

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
	VstInt32 version = (::VstInt32)CallHost(audioMasterVersion, 0, 0, 0, 0);

	if(version == 0)	// old host
		version = 1;

	return version;
}

System::Int32 HostCommandStub::GetCurrentPluginID()
{
	return (::VstInt32)CallHost(audioMasterCurrentId, 0, 0, 0, 0);
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

	TypeConverter::ToManagedTimeInfo(_timeInfo, pTimeInfo);

	return _timeInfo;
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
	return (CallHost(audioMasterIOChanged, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SizeWindow(System::Int32 width, System::Int32 height)
{
	return (CallHost(audioMasterSizeWindow, width, height, 0, 0) != 0);
}

System::Single HostCommandStub::GetSampleRate()
{
	return safe_cast<System::Single>(CallHost(audioMasterGetSampleRate, 0, 0, 0, 0));
}

System::Int32 HostCommandStub::GetBlockSize()
{
	return (::VstInt32)CallHost(audioMasterGetBlockSize, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetInputLatency()
{
	return (::VstInt32)CallHost(audioMasterGetInputLatency, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetOutputLatency()
{
	return (::VstInt32)CallHost(audioMasterGetOutputLatency, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstProcessLevels HostCommandStub::GetProcessLevel()
{
	return safe_cast<Jacobi::Vst::Core::VstProcessLevels>(CallHost(audioMasterGetCurrentProcessLevel, 0, 0, 0, 0));
}

Jacobi::Vst::Core::VstAutomationStates HostCommandStub::GetAutomationState()
{
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
	UnmanagedString pText(kVstMaxVendorStrLen);

	if(CallHost(audioMasterGetVendorString, 0, 0, pText, 0) != 0)
	{
		return TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::String^ HostCommandStub::GetProductString()
{
	UnmanagedString pText(kVstMaxProductStrLen);

	if(CallHost(audioMasterGetProductString, 0, 0, pText, 0) != 0)
	{
		return TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::Int32 HostCommandStub::GetVendorVersion()
{
	return (::VstInt32)CallHost(audioMasterGetVendorVersion, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstCanDoResult HostCommandStub::CanDo(System::String^ cando)
{
	UnmanagedString pText(Jacobi::Vst::Core::Constants::MaxCanDoLength);

	TypeConverter::StringToChar(cando, pText, Jacobi::Vst::Core::Constants::MaxCanDoLength);

	return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(CallHost(audioMasterCanDo, 0, 0, pText, 0));
}

Jacobi::Vst::Core::VstHostLanguage HostCommandStub::GetLanguage()
{
	return safe_cast<Jacobi::Vst::Core::VstHostLanguage>(CallHost(audioMasterGetLanguage, 0, 0, 0, 0));
}

System::String^ HostCommandStub::GetDirectory()
{
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
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_VstFileSelectAlreadyInitialized);
	}

	::VstFileSelect* pFileSelect = TypeConverter::AllocUnmanagedFileSelect(fileSelect);

	try
	{
		System::Boolean succeeded = (CallHost(audioMasterOpenFileSelector, 0, 0, pFileSelect, 0) != 0);

		if(succeeded)
		{
			// update the managed type with the users selections.
			TypeConverter::UpdateManagedFileSelect(fileSelect, pFileSelect);
		}

		return succeeded;
	}
	catch(...)
	{
		TypeConverter::DeleteUnmanagedFileSelect(pFileSelect);

		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Error, 
			"Error in Jacobi.Vst.Interop.Plugin.HostCommandStub.OpenFileSelector.");
	}

	return false;
}

System::Boolean HostCommandStub::CloseFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
{
	ThrowIfNotInitialized();

	::VstFileSelect* pFileSelect = (::VstFileSelect*)fileSelect->Reserved.ToPointer();

	if(pFileSelect == NULL)
	{
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_VstFileSelectAlreadyDisposed);
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

//
// Deprecated VST 2.4 methods
//

// IVstPluginCommandsDeprecated10
System::Boolean HostCommandStub::PinConnected(System::Int32 connectionIndex, System::Boolean output)
{
	// Note: retval 0 = true
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterPinConnected), connectionIndex, safe_cast<VstIntPtr>(output), 0, 0) == 0);
}

// IVstPluginCommandsDeprecated20
System::Boolean HostCommandStub::WantMidi()
{
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterWantMidi), 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SetTime(Jacobi::Vst::Core::VstTimeInfo^ timeInfo, Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	::VstTimeInfo* pTimeInfo = TypeConverter::AllocUnmanagedTimeInfo(timeInfo);

	try
	{
		return (CallHost(DECLARE_VST_DEPRECATED (audioMasterSetTime), 0, safe_cast<VstIntPtr>(filterFlags), pTimeInfo, 0) != 0);
	}
	finally
	{
		delete pTimeInfo;
	}
}

System::Int32 HostCommandStub::GetTempoAt(System::Int32 sampleIndex) // bpm * 10000
{
	return safe_cast<System::Int32>(CallHost(DECLARE_VST_DEPRECATED (audioMasterTempoAt), 0, safe_cast<VstIntPtr>(sampleIndex), 0, 0));
}

System::Int32 HostCommandStub::GetAutomatableParameterCount()
{
	return safe_cast<System::Int32>(CallHost(DECLARE_VST_DEPRECATED (audioMasterGetNumAutomatableParameters), 0, 0, 0, 0));
}

System::Int32 HostCommandStub::GetParameterQuantization(System::Int32 parameterIndex)
{
	return safe_cast<System::Int32>(CallHost(DECLARE_VST_DEPRECATED (audioMasterGetParameterQuantization), 0, safe_cast<VstIntPtr>(parameterIndex), 0, 0));
}

System::Boolean HostCommandStub::NeedIdle()
{
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterNeedIdle), 0, 0, 0, 0) != 0);
}

System::IntPtr HostCommandStub::GetPreviousPlugin(System::Int32 pinIndex) // AEffect*
{
	return System::IntPtr(CallHost(DECLARE_VST_DEPRECATED (audioMasterGetPreviousPlug), 0, safe_cast<VstIntPtr>(pinIndex), 0, 0));
}

System::IntPtr HostCommandStub::GetNextPlugin(System::Int32 pinIndex) // AEffect*
{
	return System::IntPtr(CallHost(DECLARE_VST_DEPRECATED (audioMasterGetNextPlug), 0, safe_cast<VstIntPtr>(pinIndex), 0, 0));
}

System::Int32 HostCommandStub::WillReplaceOrAccumulate() // 0=Not Supported, 1=Replace, 2=Accumulate
{
	return safe_cast<System::Int32>(CallHost(DECLARE_VST_DEPRECATED (audioMasterWillReplaceOrAccumulate), 0, 0, 0, 0));
}

System::Boolean HostCommandStub::SetOutputSampleRate(System::Single sampleRate)
{
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterSetOutputSampleRate), 0, 0, 0, sampleRate) != 0);
}

Jacobi::Vst::Core::VstSpeakerArrangement^ HostCommandStub::GetOutputSpeakerArrangement()
{
	::VstSpeakerArrangement* pArrangement = (::VstSpeakerArrangement*)CallHost(DECLARE_VST_DEPRECATED (audioMasterGetOutputSpeakerArrangement), 0, 0, 0, 0);

	return TypeConverter::ToManagedSpeakerArrangement(pArrangement);
}

System::Boolean HostCommandStub::SetIcon(System::Drawing::Icon^ icon)
{
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterSetIcon), 0, 0, icon->Handle.ToPointer(), 0) != 0);
}

System::IntPtr HostCommandStub::OpenWindow()    // HWND
{
	return System::IntPtr(CallHost(DECLARE_VST_DEPRECATED (audioMasterOpenWindow), 0, 0, 0, 0));
}

System::Boolean HostCommandStub::CloseWindow(System::IntPtr wnd)
{
	return (CallHost(DECLARE_VST_DEPRECATED (audioMasterCloseWindow), 0, 0, wnd.ToPointer(), 0) != 0);
}

System::Boolean HostCommandStub::EditFile(System::String^ xml)
{
	char* pXml = TypeConverter::AllocateString(xml);

	try
	{
		return (CallHost(DECLARE_VST_DEPRECATED (audioMasterEditFile), 0, 0, pXml, 0) != 0);
	}
	finally
	{
		TypeConverter::DeallocateString(pXml);
	}
}

System::String^ HostCommandStub::GetChunkFile()
{
	UnmanagedString pFile(2048);

	CallHost(DECLARE_VST_DEPRECATED (audioMasterGetChunkFile), 0, 0, pFile, 0);

	return TypeConverter::CharToString(pFile);
}

Jacobi::Vst::Core::VstSpeakerArrangement^ HostCommandStub::GetInputSpeakerArrangement()
{
	::VstSpeakerArrangement* pArrangement = (::VstSpeakerArrangement*)CallHost(DECLARE_VST_DEPRECATED (audioMasterGetInputSpeakerArrangement), 0, 0, 0, 0);

	return TypeConverter::ToManagedSpeakerArrangement(pArrangement);
}

//-----------------------------------------------------------------------------

// Throws an InvalidOperationException if the host command stub has not been initialized.
inline void HostCommandStub::ThrowIfNotInitialized()
{
	if(IsInitialized() == false)
	{
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_NotInitialized);
	}
}

}}}} // Jacobi::Vst::Interop::Plugin
