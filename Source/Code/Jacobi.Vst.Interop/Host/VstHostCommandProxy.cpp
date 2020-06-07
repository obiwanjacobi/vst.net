#include "pch.h"
#include "VstHostCommandProxy.h"
#include "..\TypeConverter.h"
#include "..\Utils.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {

VstHostCommandProxy::VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
{
	Jacobi::Vst::Core::Throw::IfArgumentIsNull(hostCmdStub, "hostCmdStub");

	_hostCmdStub = hostCmdStub;
	_legacyCmdStub = dynamic_cast<Jacobi::Vst::Core::Legacy::IVstHostCommandsLegacy20^>(hostCmdStub);

	// unmanaged structures
	_pTimeInfo = new ::Vst2TimeInfo();
	_directory = NULL;
	_pArrangement = new ::Vst2SpeakerArrangement();

	_traceCtx = gcnew Jacobi::Vst::Core::Diagnostics::TraceContext("Host.HostCommandProxy", Jacobi::Vst::Core::Host::IVstHostCommandStub::typeid);
}

VstHostCommandProxy::~VstHostCommandProxy()
{
	this->!VstHostCommandProxy();
}

VstHostCommandProxy::!VstHostCommandProxy()
{
	if(_pTimeInfo != NULL)
	{
		delete _pTimeInfo;
		_pTimeInfo = NULL;
	}

	if(_directory != NULL)
	{
		TypeConverter::DeallocateString(_directory);
		_directory = NULL;
	}

	if(_pArrangement != NULL)
	{
		delete _pArrangement;
		_pArrangement = NULL;
	}
}

Vst2IntPtr VstHostCommandProxy::Dispatch(int32_t opcode, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	Vst2IntPtr result = 0;

	_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

	if(_hostCmdStub != nullptr)
	{
		try
		{
			Vst2HostCommands command = safe_cast<Vst2HostCommands>(opcode);
			switch(command)
			{
			// version 1.0 commands
			case Vst2HostCommands::Automate:
				_hostCmdStub->Commands->SetParameterAutomated(index, opt);
				result = 1;
				break;
			case Vst2HostCommands::Version:
				result = _hostCmdStub->Commands->GetVersion();
				break;
			case Vst2HostCommands::CurrentId:
				result = _hostCmdStub->Commands->GetCurrentPluginID();
				break;
			case Vst2HostCommands::Idle:
				_hostCmdStub->Commands->ProcessIdle();
				result = 1;
				break;

			// version 2.0 commands
			case Vst2HostCommands::GetTime:
			{
				Jacobi::Vst::Core::VstTimeInfo^ timeInfo = _hostCmdStub->Commands->GetTimeInfo(safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value));
				if(timeInfo != nullptr)
				{
					TypeConverter::ToUnmanagedTimeInfo(_pTimeInfo, timeInfo);
					result = (Vst2IntPtr)_pTimeInfo;
				}
			}	break;
			case Vst2HostCommands::ProcessEvents:
				_hostCmdStub->Commands->ProcessEvents(TypeConverter::ToManagedEventArray((::Vst2Events*)ptr));
				result = 1;
				break;
			case Vst2HostCommands::IoChanged:
				result = _hostCmdStub->Commands->IoChanged() ? 1 : 0;
				break;
			case Vst2HostCommands::SizeWindow:
				result = _hostCmdStub->Commands->SizeWindow(index, (::int32_t)value) ? 1 : 0;
				break;
			case Vst2HostCommands::GetSampleRate:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->Commands->GetSampleRate());
				break;
			case Vst2HostCommands::GetBlockSize:
				result = _hostCmdStub->Commands->GetBlockSize();
				break;
			case Vst2HostCommands::GetInputLatency:
				result = _hostCmdStub->Commands->GetInputLatency();
				break;
			case Vst2HostCommands::GetOutputLatency:
				result = _hostCmdStub->Commands->GetOutputLatency();
				break;
			case Vst2HostCommands::GetCurrentProcessLevel:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->Commands->GetProcessLevel());
				break;
			case Vst2HostCommands::GetAutomationState:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->Commands->GetAutomationState());
				break;
			case Vst2HostCommands::VendorGetString:
				TypeConverter::StringToChar(_hostCmdStub->Commands->GetVendorString(), (char*)ptr, Vst2MaxVendorStrLen);
				break;
			case Vst2HostCommands::ProductGetString:
				TypeConverter::StringToChar(_hostCmdStub->Commands->GetProductString(), (char*)ptr, Vst2MaxProductStrLen);
				break;
			case Vst2HostCommands::VendorGetVersion:
				result = _hostCmdStub->Commands->GetVendorVersion();
				break;
			case Vst2HostCommands::CanDo:
			{
				System::String^ cando = TypeConverter::CharToString((char*)ptr);
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->Commands->CanDo(cando));
			}	break;
			case Vst2HostCommands::GetLanguage:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->Commands->GetLanguage());
				break;
			case Vst2HostCommands::GetDirectory:
				if(_directory == NULL)
				{
					_directory = TypeConverter::AllocateString(_hostCmdStub->Commands->GetDirectory());
				}
				// return cached value
				result = (Vst2IntPtr)_directory;
				break;
			case Vst2HostCommands::UpdateDisplay:
				result = _hostCmdStub->Commands->UpdateDisplay() ? 1 : 0;
				break;
			case Vst2HostCommands::EditBegin:
				result = _hostCmdStub->Commands->BeginEdit(index) ? 1 : 0;
				break;
			case Vst2HostCommands::EditEnd:
				result = _hostCmdStub->Commands->EndEdit(index) ? 1 : 0;
				break;
			case Vst2HostCommands::FileSelectorOpen:
			{
				Jacobi::Vst::Core::VstFileSelect^ fileSelect = TypeConverter::ToManagedFileSelect((::Vst2FileSelect*)ptr);
				result = _hostCmdStub->Commands->OpenFileSelector(fileSelect) ? 1 : 0;
				TypeConverter::AllocUpdateUnmanagedFileSelect((::Vst2FileSelect*)ptr, fileSelect);
			}	break;
			case Vst2HostCommands::FileSelectorClose:
			{
				Jacobi::Vst::Core::VstFileSelect^ fileSelect = TypeConverter::GetManagedFileSelect((::Vst2FileSelect*)ptr);
				result = _hostCmdStub->Commands->CloseFileSelector(fileSelect) ? 1 : 0;
				TypeConverter::DeleteUpdateUnmanagedFileSelect((::Vst2FileSelect*)ptr);
			}	break;
			default:
				result = DispatchLegacy(command, index, value, ptr, opt);
				break;
			}
		}
		catch(System::Exception^ e)
		{
			_traceCtx->WriteError(e);

			Utils::ShowError(e);
		}
	}
	else
	{
		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Warning, "The Host Command Stub was not set.");
	}

	_traceCtx->WriteDispatchEnd(System::IntPtr(result));

	return result;
}

Vst2IntPtr VstHostCommandProxy::DispatchLegacy(Vst2HostCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	Vst2IntPtr result = 0;

	if(_legacyCmdStub != nullptr)
	{
		switch(command)
		{
		// VST 1.0
		case Vst2HostCommands::PinConnected:
			result = _legacyCmdStub->PinConnected(index, value != 0) ? 1 : 0;
			break;

		// VST 2.0
		case Vst2HostCommands::SetTime:
		{
			Jacobi::Vst::Core::VstTimeInfo^ timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();
			TypeConverter::ToManagedTimeInfo(timeInfo, (::Vst2TimeInfo*)ptr);
			Jacobi::Vst::Core::VstTimeInfoFlags filterFlags = safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value);

			result = _legacyCmdStub->SetTime(timeInfo, filterFlags) ? 1 : 0;
		}	break;
		case Vst2HostCommands::TempoAt:
			result = safe_cast<Vst2IntPtr>(_legacyCmdStub->GetTempoAt(safe_cast<System::Int32>(value)));
			break;
		case Vst2HostCommands::GetAutomatableParameterCount:
			result = safe_cast<Vst2IntPtr>(_legacyCmdStub->GetAutomatableParameterCount());
			break;
		case Vst2HostCommands::GetParameterQuantization:
			result = safe_cast<Vst2IntPtr>(_legacyCmdStub->GetParameterQuantization(safe_cast<System::Int32>(value)));
			break;
		case Vst2HostCommands::NeedIdle:
			result = _legacyCmdStub->NeedIdle() ? 1 : 0;
			break;
		case Vst2HostCommands::PluginGetPrevious:
			result = (Vst2IntPtr)_legacyCmdStub->GetPreviousPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case Vst2HostCommands::PluginGetNext:
			result = (Vst2IntPtr)_legacyCmdStub->GetNextPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case Vst2HostCommands::WillReplace:
			result = safe_cast<Vst2IntPtr>(_legacyCmdStub->WillReplaceOrAccumulate());
			break;
		case Vst2HostCommands::SetOutputSampleRate:
			result = _legacyCmdStub->SetOutputSampleRate(opt) ? 1 : 0;
			break;
		case Vst2HostCommands::GetOutputSpeakerArrangement:
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _legacyCmdStub->GetOutputSpeakerArrangement();
		TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
			result = (Vst2IntPtr)_pArrangement;
		}	break;
		case Vst2HostCommands::SetIcon:
		{
			System::IntPtr hIcon(ptr);
			result = _legacyCmdStub->SetIcon(hIcon) ? 1 : 0;
		}	break;
		case Vst2HostCommands::WindowOpen:
			result = (Vst2IntPtr)_legacyCmdStub->OpenWindow().ToPointer();
			break;
		case Vst2HostCommands::WindowClose:
			result = _legacyCmdStub->CloseWindow(System::IntPtr(ptr)) ? 1 : 0;
			break;
		case Vst2HostCommands::EditFile:
			result = _legacyCmdStub->EditFile(TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
			break;
		case Vst2HostCommands::GetChunkFile:
		{	System::String^ path = _legacyCmdStub->GetChunkFile();
		TypeConverter::StringToChar(path, (char*)ptr, 2047);
			result = System::String::IsNullOrEmpty(path) ? 1 : 0;
		}	break;
		case Vst2HostCommands::GetInputSpeakerArrangement:
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _legacyCmdStub->GetInputSpeakerArrangement();
		TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
			result = (Vst2IntPtr)_pArrangement;
		}	break;
		default:
			// unknown command
			_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Information, 
				System::String::Format("Unhandled dispatcher opcode: {0}.", safe_cast<System::Int32>(command)));
			break;
		}
	}
	else
	{
		// unhandled command
		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Information, 
			System::String::Format("Unhandled dispatcher opcode: {0}.", safe_cast<System::Int32>(command)));
	}

	return result;
}

}}}} // Jacobi::Vst::Host::Interop