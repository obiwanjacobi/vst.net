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
	_deprecatedCmdStub = dynamic_cast<Jacobi::Vst::Core::Legacy::IVstHostCommandsLegacy20^>(hostCmdStub);

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
		Jacobi::Vst::Interop::TypeConverter::DeallocateString(_directory);
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
				_hostCmdStub->SetParameterAutomated(index, opt);
				result = 1;
				break;
			case Vst2HostCommands::Version:
				result = _hostCmdStub->GetVersion();
				break;
			case Vst2HostCommands::CurrentId:
				result = _hostCmdStub->GetCurrentPluginID();
				break;
			case Vst2HostCommands::Idle:
				_hostCmdStub->ProcessIdle();
				result = 1;
				break;

			// version 2.0 commands
			case Vst2HostCommands::GetTime:
			{
				Jacobi::Vst::Core::VstTimeInfo^ timeInfo = _hostCmdStub->GetTimeInfo(safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value));
				if(timeInfo != nullptr)
				{
					Jacobi::Vst::Interop::TypeConverter::ToUnmanagedTimeInfo(_pTimeInfo, timeInfo);
					result = (Vst2IntPtr)_pTimeInfo;
				}
			}	break;
			case Vst2HostCommands::ProcessEvents:
				_hostCmdStub->ProcessEvents(Jacobi::Vst::Interop::TypeConverter::ToManagedEventArray((::Vst2Events*)ptr));
				result = 1;
				break;
			case Vst2HostCommands::IoChanged:
				result = _hostCmdStub->IoChanged() ? 1 : 0;
				break;
			case Vst2HostCommands::SizeWindow:
				result = _hostCmdStub->SizeWindow(index, (::int32_t)value) ? 1 : 0;
				break;
			case Vst2HostCommands::GetSampleRate:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->GetSampleRate());
				break;
			case Vst2HostCommands::GetBlockSize:
				result = _hostCmdStub->GetBlockSize();
				break;
			case Vst2HostCommands::GetInputLatency:
				result = _hostCmdStub->GetInputLatency();
				break;
			case Vst2HostCommands::GetOutputLatency:
				result = _hostCmdStub->GetOutputLatency();
				break;
			case Vst2HostCommands::GetCurrentProcessLevel:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->GetProcessLevel());
				break;
			case Vst2HostCommands::GetAutomationState:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->GetAutomationState());
				break;
			case Vst2HostCommands::VendorGetString:
				Jacobi::Vst::Interop::TypeConverter::StringToChar(_hostCmdStub->GetVendorString(), (char*)ptr, Vst2MaxVendorStrLen);
				break;
			case Vst2HostCommands::ProductGetString:
				Jacobi::Vst::Interop::TypeConverter::StringToChar(_hostCmdStub->GetProductString(), (char*)ptr, Vst2MaxProductStrLen);
				break;
			case Vst2HostCommands::VendorGetVersion:
				result = _hostCmdStub->GetVendorVersion();
				break;
			case Vst2HostCommands::CanDo:
			{
				System::String^ cando = Jacobi::Vst::Interop::TypeConverter::CharToString((char*)ptr);
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->CanDo(cando));
			}	break;
			case Vst2HostCommands::GetLanguage:
				result = safe_cast<Vst2IntPtr>(_hostCmdStub->GetLanguage());
				break;
			case Vst2HostCommands::GetDirectory:
				if(_directory == NULL)
				{
					_directory = Jacobi::Vst::Interop::TypeConverter::AllocateString(_hostCmdStub->GetDirectory());
				}
				// return cached value
				result = (Vst2IntPtr)_directory;
				break;
			case Vst2HostCommands::UpdateDisplay:
				result = _hostCmdStub->UpdateDisplay() ? 1 : 0;
				break;
			case Vst2HostCommands::EditBegin:
				result = _hostCmdStub->BeginEdit(index) ? 1 : 0;
				break;
			case Vst2HostCommands::EditEnd:
				result = _hostCmdStub->EndEdit(index) ? 1 : 0;
				break;
			case Vst2HostCommands::FileSelectorOpen:
			{
				Jacobi::Vst::Core::VstFileSelect^ fileSelect = Jacobi::Vst::Interop::TypeConverter::ToManagedFileSelect((::Vst2FileSelect*)ptr);
				result = _hostCmdStub->OpenFileSelector(fileSelect) ? 1 : 0;
				Jacobi::Vst::Interop::TypeConverter::AllocUpdateUnmanagedFileSelect((::Vst2FileSelect*)ptr, fileSelect);
			}	break;
			case Vst2HostCommands::FileSelectorClose:
			{
				Jacobi::Vst::Core::VstFileSelect^ fileSelect = Jacobi::Vst::Interop::TypeConverter::GetManagedFileSelect((::Vst2FileSelect*)ptr);
				result = _hostCmdStub->CloseFileSelector(fileSelect) ? 1 : 0;
				Jacobi::Vst::Interop::TypeConverter::DeleteUpdateUnmanagedFileSelect((::Vst2FileSelect*)ptr);
			}	break;
			default:
				result = DispatchLegacy(command, index, value, ptr, opt);
				break;
			}
		}
		catch(System::Exception^ e)
		{
			_traceCtx->WriteError(e);

			Jacobi::Vst::Interop::Utils::ShowError(e);
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

	if(_deprecatedCmdStub != nullptr)
	{
		switch(command)
		{
		// VST 1.0
		case Vst2HostCommands::PinConnected:
			result = _deprecatedCmdStub->PinConnected(index, value != 0) ? 1 : 0;
			break;

		// VST 2.0
		case Vst2HostCommands::SetTime:
		{
			Jacobi::Vst::Core::VstTimeInfo^ timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();
			Jacobi::Vst::Interop::TypeConverter::ToManagedTimeInfo(timeInfo, (::Vst2TimeInfo*)ptr);
			Jacobi::Vst::Core::VstTimeInfoFlags filterFlags = safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value);

			result = _deprecatedCmdStub->SetTime(timeInfo, filterFlags) ? 1 : 0;
		}	break;
		case Vst2HostCommands::TempoAt:
			result = safe_cast<Vst2IntPtr>(_deprecatedCmdStub->GetTempoAt(safe_cast<System::Int32>(value)));
			break;
		case Vst2HostCommands::GetAutomatableParameterCount:
			result = safe_cast<Vst2IntPtr>(_deprecatedCmdStub->GetAutomatableParameterCount());
			break;
		case Vst2HostCommands::GetParameterQuantization:
			result = safe_cast<Vst2IntPtr>(_deprecatedCmdStub->GetParameterQuantization(safe_cast<System::Int32>(value)));
			break;
		case Vst2HostCommands::NeedIdle:
			result = _deprecatedCmdStub->NeedIdle() ? 1 : 0;
			break;
		case Vst2HostCommands::PluginGetPrevious:
			result = (Vst2IntPtr)_deprecatedCmdStub->GetPreviousPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case Vst2HostCommands::PluginGetNext:
			result = (Vst2IntPtr)_deprecatedCmdStub->GetNextPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case Vst2HostCommands::WillReplace:
			result = safe_cast<Vst2IntPtr>(_deprecatedCmdStub->WillReplaceOrAccumulate());
			break;
		case Vst2HostCommands::SetOutputSampleRate:
			result = _deprecatedCmdStub->SetOutputSampleRate(opt) ? 1 : 0;
			break;
		case Vst2HostCommands::GetOutputSpeakerArrangement:
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _deprecatedCmdStub->GetOutputSpeakerArrangement();
		Jacobi::Vst::Interop::TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
			result = (Vst2IntPtr)_pArrangement;
		}	break;
		case Vst2HostCommands::SetIcon:
		{
			System::IntPtr hIcon(ptr);
			result = _deprecatedCmdStub->SetIcon(hIcon) ? 1 : 0;
		}	break;
		case Vst2HostCommands::WindowOpen:
			result = (Vst2IntPtr)_deprecatedCmdStub->OpenWindow().ToPointer();
			break;
		case Vst2HostCommands::WindowClose:
			result = _deprecatedCmdStub->CloseWindow(System::IntPtr(ptr)) ? 1 : 0;
			break;
		case Vst2HostCommands::EditFile:
			result = _deprecatedCmdStub->EditFile(Jacobi::Vst::Interop::TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
			break;
		case Vst2HostCommands::GetChunkFile:
		{	System::String^ path = _deprecatedCmdStub->GetChunkFile();
		Jacobi::Vst::Interop::TypeConverter::StringToChar(path, (char*)ptr, 2047);
			result = System::String::IsNullOrEmpty(path) ? 1 : 0;
		}	break;
		case Vst2HostCommands::GetInputSpeakerArrangement:
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _deprecatedCmdStub->GetInputSpeakerArrangement();
		Jacobi::Vst::Interop::TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
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