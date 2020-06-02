#include "pch.h"
#include "HostCommandStub.h"
#include "../TypeConverter.h"
#include "../UnmanagedString.h"
#include "../Properties\Resources.h"
#include "../Utils.h"

namespace Jacobi {
namespace Vst {
namespace Plugin {
namespace Interop {

// Creates a new instance based on a native callback function pointer.
HostCommandStub::HostCommandStub(::Vst2HostCommand hostCommand)
{
	if(hostCommand == NULL)
	{
		throw gcnew System::ArgumentNullException("hostCallback");
	}

	_hostCommand = hostCommand;
	_pluginInfo = NULL;

	_timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();
	_traceCtx = gcnew Jacobi::Vst::Core::Diagnostics::TraceContext(
		Jacobi::Vst::Interop::Utils::GetPluginName() + ".Plugin.HostCommandStub", Jacobi::Vst::Core::Plugin::IVstHostCommandStub::typeid);
}

// destructor. See Finalizer
HostCommandStub::~HostCommandStub()
{
	this->!HostCommandStub();
}

// Finalizer deletes the Vst2Plugin instance
HostCommandStub::!HostCommandStub()
{
	_hostCommand = NULL;
	
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
		// overwrite the Vst2Plugin values with the new values supplied by pluginInfo.
		_pluginInfo->parameterCount = pluginInfo->ParameterCount;
		_pluginInfo->programCount = pluginInfo->ProgramCount;

		if (_pluginInfo->inputCount != pluginInfo->AudioInputCount ||
			_pluginInfo->outputCount != pluginInfo->AudioOutputCount ||
			_pluginInfo->startupDelay != pluginInfo->InitialDelay)
		{
			_pluginInfo->inputCount = pluginInfo->AudioInputCount;
			_pluginInfo->outputCount = pluginInfo->AudioOutputCount;
			_pluginInfo->startupDelay = pluginInfo->InitialDelay;

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

	CallHost(Vst2HostCommands::Automate, index, 0, 0, value);
}

System::Int32 HostCommandStub::GetVersion()
{
	int32_t version = (::int32_t)CallHost(Vst2HostCommands::Version, 0, 0, 0, 0);

	if(version == 0)	// old host
		version = 1;

	return version;
}

System::Int32 HostCommandStub::GetCurrentPluginID()
{
	return (::int32_t)CallHost(Vst2HostCommands::CurrentId, 0, 0, 0, 0);
}

void HostCommandStub::ProcessIdle()
{
	ThrowIfNotInitialized();

	CallHost(Vst2HostCommands::Idle, 0, 0, 0, 0);
}

// IVstHostCommands20
Jacobi::Vst::Core::VstTimeInfo^ HostCommandStub::GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	ThrowIfNotInitialized();

	::Vst2TimeInfo* pTimeInfo = (::Vst2TimeInfo*)
		CallHost(Vst2HostCommands::GetTime, 0, safe_cast<int32_t>(filterFlags), 0, 0);

	Jacobi::Vst::Interop::TypeConverter::ToManagedTimeInfo(_timeInfo, pTimeInfo);

	return _timeInfo;
}

System::Boolean HostCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
{
	ThrowIfNotInitialized();

	::Vst2Events* pEvents = Jacobi::Vst::Interop::TypeConverter::AllocUnmanagedEvents(events);

	try
	{
		return (CallHost(Vst2HostCommands::ProcessEvents, 0, 0, pEvents, 0) != 0);
	}
	finally
	{
		Jacobi::Vst::Interop::TypeConverter::DeleteUnmanagedEvents(pEvents);
	}

	return false;
}

System::Boolean HostCommandStub::IoChanged()
{
	return (CallHost(Vst2HostCommands::IoChanged, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SizeWindow(System::Int32 width, System::Int32 height)
{
	return (CallHost(Vst2HostCommands::SizeWindow, width, height, 0, 0) != 0);
}

System::Single HostCommandStub::GetSampleRate()
{
	return safe_cast<System::Single>(CallHost(Vst2HostCommands::GetSampleRate, 0, 0, 0, 0));
}

System::Int32 HostCommandStub::GetBlockSize()
{
	return (::int32_t)CallHost(Vst2HostCommands::GetBlockSize, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetInputLatency()
{
	return (::int32_t)CallHost(Vst2HostCommands::GetInputLatency, 0, 0, 0, 0);
}

System::Int32 HostCommandStub::GetOutputLatency()
{
	return (::int32_t)CallHost(Vst2HostCommands::GetOutputLatency, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstProcessLevels HostCommandStub::GetProcessLevel()
{
	return safe_cast<Jacobi::Vst::Core::VstProcessLevels>(
		CallHost(Vst2HostCommands::GetCurrentProcessLevel, 0, 0, 0, 0));
}

Jacobi::Vst::Core::VstAutomationStates HostCommandStub::GetAutomationState()
{
	return safe_cast<Jacobi::Vst::Core::VstAutomationStates>(
		CallHost(Vst2HostCommands::GetAutomationState, 0, 0, 0, 0));
}

System::String^ HostCommandStub::GetVendorString()
{
	Jacobi::Vst::Interop::UnmanagedString pText(Vst2MaxVendorStrLen + 1);

	if(CallHost(Vst2HostCommands::VendorGetString, 0, 0, pText, 0) != 0)
	{
		return Jacobi::Vst::Interop::TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::String^ HostCommandStub::GetProductString()
{
	Jacobi::Vst::Interop::UnmanagedString pText(Vst2MaxProductStrLen + 1);

	if(CallHost(Vst2HostCommands::ProductGetString, 0, 0, pText, 0) != 0)
	{
		return Jacobi::Vst::Interop::TypeConverter::CharToString(pText);
	}

	return nullptr;
}

System::Int32 HostCommandStub::GetVendorVersion()
{
	return (::int32_t)CallHost(Vst2HostCommands::VendorGetVersion, 0, 0, 0, 0);
}

Jacobi::Vst::Core::VstCanDoResult HostCommandStub::CanDo(System::String^ cando)
{
	Jacobi::Vst::Interop::UnmanagedString pText(Jacobi::Vst::Core::Constants::MaxCanDoLength + 1);

	Jacobi::Vst::Interop::TypeConverter::StringToChar(cando, pText, Jacobi::Vst::Core::Constants::MaxCanDoLength);

	return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(
		CallHost(Vst2HostCommands::CanDo, 0, 0, pText, 0));
}

Jacobi::Vst::Core::VstHostLanguage HostCommandStub::GetLanguage()
{
	return safe_cast<Jacobi::Vst::Core::VstHostLanguage>(
		CallHost(Vst2HostCommands::GetLanguage, 0, 0, 0, 0));
}

System::String^ HostCommandStub::GetDirectory()
{
	return Jacobi::Vst::Interop::TypeConverter::CharToString((char*)
		CallHost(Vst2HostCommands::GetDirectory, 0, 0, 0, 0));
}

System::Boolean HostCommandStub::UpdateDisplay()
{
	ThrowIfNotInitialized();

	return (CallHost(Vst2HostCommands::UpdateDisplay, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::BeginEdit(System::Int32 index)
{
	ThrowIfNotInitialized();
	
	return (CallHost(Vst2HostCommands::EditBegin, index, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::EndEdit(System::Int32 index)
{
	ThrowIfNotInitialized();
	
	return (CallHost(Vst2HostCommands::EditEnd, index, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::OpenFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
{
	ThrowIfNotInitialized();

	if(fileSelect->Reserved != System::IntPtr::Zero)
	{
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_VstFileSelectAlreadyInitialized);
	}

	::Vst2FileSelect* pFileSelect = Jacobi::Vst::Interop::TypeConverter::AllocUnmanagedFileSelect(fileSelect);

	try
	{
		System::Boolean succeeded = (CallHost(Vst2HostCommands::FileSelectorOpen, 0, 0, pFileSelect, 0) != 0);

		if(succeeded)
		{
			// update the managed type with the users selections.
			Jacobi::Vst::Interop::TypeConverter::UpdateManagedFileSelect(fileSelect, pFileSelect);
		}

		return succeeded;
	}
	catch(...)
	{
		Jacobi::Vst::Interop::TypeConverter::DeleteUnmanagedFileSelect(pFileSelect);

		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Error, 
			"Error in Jacobi.Vst.Interop.Plugin.HostCommandStub.OpenFileSelector.");
	}

	return false;
}

System::Boolean HostCommandStub::CloseFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
{
	ThrowIfNotInitialized();

	::Vst2FileSelect* pFileSelect = (::Vst2FileSelect*)fileSelect->Reserved.ToPointer();

	if(pFileSelect == NULL)
	{
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_VstFileSelectAlreadyDisposed);
	}

	try
	{
		System::Boolean succeeded = (CallHost(Vst2HostCommands::FileSelectorClose, 0, 0, pFileSelect, 0) != 0);

		return succeeded;
	}
	finally
	{
		Jacobi::Vst::Interop::TypeConverter::DeleteUnmanagedFileSelect(pFileSelect);
		fileSelect->Reserved = System::IntPtr::Zero;
	}
}

//
// Legacy VST 2.4 methods
//

// IVstPluginCommandsLegacy10
System::Boolean HostCommandStub::PinConnected(System::Int32 connectionIndex, System::Boolean output)
{
	// Note: retval 0 = true
	return (CallHost(Vst2HostCommands::PinConnected, connectionIndex, safe_cast<Vst2IntPtr>(output), 0, 0) == 0);
}

// IVstPluginCommandsLegacy20
System::Boolean HostCommandStub::WantMidi()
{
	return (CallHost(Vst2HostCommands::WantMidi, 0, 0, 0, 0) != 0);
}

System::Boolean HostCommandStub::SetTime(Jacobi::Vst::Core::VstTimeInfo^ timeInfo, Jacobi::Vst::Core::VstTimeInfoFlags filterFlags)
{
	::Vst2TimeInfo* pTimeInfo = Jacobi::Vst::Interop::TypeConverter::AllocUnmanagedTimeInfo(timeInfo);

	try
	{
		return (CallHost(Vst2HostCommands::SetTime, 0, safe_cast<Vst2IntPtr>(filterFlags), pTimeInfo, 0) != 0);
	}
	finally
	{
		delete pTimeInfo;
	}
}

System::Int32 HostCommandStub::GetTempoAt(System::Int32 sampleIndex) // bpm * 10000
{
	return safe_cast<System::Int32>(
		CallHost(Vst2HostCommands::TempoAt, 0, safe_cast<Vst2IntPtr>(sampleIndex), 0, 0));
}

System::Int32 HostCommandStub::GetAutomatableParameterCount()
{
	return safe_cast<System::Int32>(
		CallHost(Vst2HostCommands::GetAutomatableParameterCount, 0, 0, 0, 0));
}

System::Int32 HostCommandStub::GetParameterQuantization(System::Int32 parameterIndex)
{
	return safe_cast<System::Int32>(
		CallHost(Vst2HostCommands::GetParameterQuantization, 0, safe_cast<Vst2IntPtr>(parameterIndex), 0, 0));
}

System::Boolean HostCommandStub::NeedIdle()
{
	return (CallHost(Vst2HostCommands::NeedIdle, 0, 0, 0, 0) != 0);
}

System::IntPtr HostCommandStub::GetPreviousPlugin(System::Int32 pinIndex) // Vst2Plugin*
{
	return System::IntPtr(CallHost(Vst2HostCommands::PluginGetPrevious, 0, safe_cast<Vst2IntPtr>(pinIndex), 0, 0));
}

System::IntPtr HostCommandStub::GetNextPlugin(System::Int32 pinIndex) // Vst2Plugin*
{
	return System::IntPtr(CallHost(Vst2HostCommands::PluginGetNext, 0, safe_cast<Vst2IntPtr>(pinIndex), 0, 0));
}

System::Int32 HostCommandStub::WillReplaceOrAccumulate() // 0=Not Supported, 1=Replace, 2=Accumulate
{
	return safe_cast<System::Int32>(CallHost(Vst2HostCommands::WillReplace, 0, 0, 0, 0));
}

System::Boolean HostCommandStub::SetOutputSampleRate(System::Single sampleRate)
{
	return (CallHost(Vst2HostCommands::SetOutputSampleRate, 0, 0, 0, sampleRate) != 0);
}

Jacobi::Vst::Core::VstSpeakerArrangement^ HostCommandStub::GetOutputSpeakerArrangement()
{
	::Vst2SpeakerArrangement* pArrangement = (::Vst2SpeakerArrangement*)
		CallHost(Vst2HostCommands::GetOutputSpeakerArrangement, 0, 0, 0, 0);

	return Jacobi::Vst::Interop::TypeConverter::ToManagedSpeakerArrangement(pArrangement);
}

System::Boolean HostCommandStub::SetIcon(System::IntPtr icon)
{
	return (CallHost(Vst2HostCommands::SetIcon, 0, 0, icon.ToPointer(), 0) != 0);
}

System::IntPtr HostCommandStub::OpenWindow()    // HWND
{
	return System::IntPtr(CallHost(Vst2HostCommands::WindowOpen, 0, 0, 0, 0));
}

System::Boolean HostCommandStub::CloseWindow(System::IntPtr wnd)
{
	return (CallHost(Vst2HostCommands::WindowClose, 0, 0, wnd.ToPointer(), 0) != 0);
}

System::Boolean HostCommandStub::EditFile(System::String^ xml)
{
	char* pXml = Jacobi::Vst::Interop::TypeConverter::AllocateString(xml);

	try
	{
		return (CallHost(Vst2HostCommands::EditFile, 0, 0, pXml, 0) != 0);
	}
	finally
	{
		Jacobi::Vst::Interop::TypeConverter::DeallocateString(pXml);
	}
}

System::String^ HostCommandStub::GetChunkFile()
{
	Jacobi::Vst::Interop::UnmanagedString pFile(2048);

	CallHost(Vst2HostCommands::GetChunkFile, 0, 0, pFile, 0);

	return Jacobi::Vst::Interop::TypeConverter::CharToString(pFile);
}

Jacobi::Vst::Core::VstSpeakerArrangement^ HostCommandStub::GetInputSpeakerArrangement()
{
	::Vst2SpeakerArrangement* pArrangement = (::Vst2SpeakerArrangement*)
		CallHost(Vst2HostCommands::GetInputSpeakerArrangement, 0, 0, 0, 0);

	return Jacobi::Vst::Interop::TypeConverter::ToManagedSpeakerArrangement(pArrangement);
}

// Throws an InvalidOperationException if the host command stub has not been initialized.
inline void HostCommandStub::ThrowIfNotInitialized()
{
	if (IsInitialized() == false)
	{
		throw gcnew System::InvalidOperationException(
			Jacobi::Vst::Interop::Properties::Resources::HostCommandStub_NotInitialized);
	}
}

}}}} // Jacobi::Vst::Plugin::Interop
