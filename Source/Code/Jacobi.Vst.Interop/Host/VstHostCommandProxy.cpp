#include "StdAfx.h"
#include "VstHostCommandProxy.h"
#include "..\TypeConverter.h"
#include "..\Utils.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

VstHostCommandProxy::VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
{
	Jacobi::Vst::Core::Throw::IfArgumentIsNull(hostCmdStub, "hostCmdStub");

	_hostCmdStub = hostCmdStub;
	_deprecatedCmdStub = dynamic_cast<Jacobi::Vst::Core::Deprecated::IVstHostCommandsDeprecated20^>(hostCmdStub);

	// unmanaged structures
	_pTimeInfo = new VstTimeInfo();
	_directory = NULL;
	_pArrangement = new ::VstSpeakerArrangement();

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

VstIntPtr VstHostCommandProxy::Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	VstIntPtr result = 0;

	_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

	try
	{
		switch(opcode)
		{
		// version 1.0 commands
		case audioMasterAutomate:
			// [index]: parameter index [opt]: parameter value  @see AudioEffect::setParameterAutomated
			_hostCmdStub->SetParameterAutomated(index, opt);
			result = 1;
			break;
		case audioMasterVersion:
			// [return value]: Host VST version (for example 2400 for VST 2.4) @see AudioEffect::getMasterVersion
			result = _hostCmdStub->GetVersion();
			break;
		case audioMasterCurrentId:
			// [return value]: current unique identifier on shell plug-in  @see AudioEffect::getCurrentUniqueId
			result = _hostCmdStub->GetCurrentPluginID();
			break;
		case audioMasterIdle:
			// no arguments  @see AudioEffect::masterIdle
			_hostCmdStub->ProcessIdle();
			result = 1;
			break;

		// version 2.0 commands
		case audioMasterGetTime:
		{
			// [return value]: #VstTimeInfo* or null if not supported [value]: request mask  @see VstTimeInfoFlags @see AudioEffectX::getTimeInfo
			Jacobi::Vst::Core::VstTimeInfo^ timeInfo = _hostCmdStub->GetTimeInfo(safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value));
			if(timeInfo != nullptr)
			{
				TypeConverter::ToUnmanagedTimeInfo(_pTimeInfo, timeInfo);
				result = (VstIntPtr)_pTimeInfo;
			}
		}	break;
		case audioMasterProcessEvents:
			// [ptr]: pointer to #VstEvents  @see VstEvents @see AudioEffectX::sendVstEventsToHost
			_hostCmdStub->ProcessEvents(TypeConverter::ToManagedEventArray((::VstEvents*)ptr));
			result = 1;
			break;
		case audioMasterIOChanged:
			// [return value]: 1 if supported  @see AudioEffectX::ioChanged
			result = _hostCmdStub->IoChanged() ? 1 : 0;
			break;
		case audioMasterSizeWindow:
			// [index]: new width [value]: new height [return value]: 1 if supported  @see AudioEffectX::sizeWindow
			result = _hostCmdStub->SizeWindow(index, value) ? 1 : 0;
			break;
		case audioMasterGetSampleRate:
			// [return value]: current sample rate  @see AudioEffectX::updateSampleRate
			result = safe_cast<VstIntPtr>(_hostCmdStub->GetSampleRate());
			break;
		case audioMasterGetBlockSize:
			// [return value]: current block size  @see AudioEffectX::updateBlockSize
			result = _hostCmdStub->GetBlockSize();
			break;
		case audioMasterGetInputLatency:
			// [return value]: input latency in audio samples  @see AudioEffectX::getInputLatency
			result = _hostCmdStub->GetInputLatency();
			break;
		case audioMasterGetOutputLatency:
			// [return value]: output latency in audio samples  @see AudioEffectX::getOutputLatency
			result = _hostCmdStub->GetOutputLatency();
			break;
		case audioMasterGetCurrentProcessLevel:
			// [return value]: current process level  @see VstProcessLevels
			result = safe_cast<VstIntPtr>(_hostCmdStub->GetProcessLevel());
			break;
		case audioMasterGetAutomationState:
			// [return value]: current automation state  @see VstAutomationStates
			result = safe_cast<VstIntPtr>(_hostCmdStub->GetAutomationState());
			break;
		//case audioMasterOfflineStart:
		//	// [index]: numNewAudioFiles [value]: numAudioFiles [ptr]: #VstAudioFile*  @see AudioEffectX::offlineStart
		//	break;
		//case audioMasterOfflineRead:
		//	// [index]: bool readSource [value]: #VstOfflineOption* @see VstOfflineOption [ptr]: #VstOfflineTask*  @see VstOfflineTask @see AudioEffectX::offlineRead
		//	break;
		//case audioMasterOfflineWrite:
		//	// @see audioMasterOfflineRead @see AudioEffectX::offlineRead
		//	break;
		//case audioMasterOfflineGetCurrentPass:
		//	// @see AudioEffectX::offlineGetCurrentPass
		//	break;
		//case audioMasterOfflineGetCurrentMetaPass:
		//	// @see AudioEffectX::offlineGetCurrentMetaPass
		//	break;
		case audioMasterGetVendorString:
			// [ptr]: char buffer for vendor string, limited to #kVstMaxVendorStrLen  @see AudioEffectX::getHostVendorString
			TypeConverter::StringToChar(_hostCmdStub->GetVendorString(), (char*)ptr, kVstMaxVendorStrLen);
			break;
		case audioMasterGetProductString:
			// [ptr]: char buffer for vendor string, limited to #kVstMaxProductStrLen  @see AudioEffectX::getHostProductString
			TypeConverter::StringToChar(_hostCmdStub->GetProductString(), (char*)ptr, kVstMaxProductStrLen);
			break;
		case audioMasterGetVendorVersion:
			// [return value]: vendor-specific version  @see AudioEffectX::getHostVendorVersion
			result = _hostCmdStub->GetVendorVersion();
			break;
		//case audioMasterVendorSpecific:
		//	// no definition, vendor specific handling  @see AudioEffectX::hostVendorSpecific
		//	break;
		case audioMasterCanDo:
		{
			// [ptr]: "can do" string [return value]: 1 for supported
			System::String^ candoStr = TypeConverter::CharToString((char*)ptr);
			Jacobi::Vst::Core::VstHostCanDo cando = safe_cast<Jacobi::Vst::Core::VstHostCanDo>(
				System::Enum::Parse(Jacobi::Vst::Core::VstHostCanDo::typeid, candoStr, true));
			result = safe_cast<VstIntPtr>(_hostCmdStub->CanDo(cando));
		}	break;
		case audioMasterGetLanguage:
			// [return value]: language code  @see VstHostLanguage
			result = safe_cast<VstIntPtr>(_hostCmdStub->GetLanguage());
			break;
		case audioMasterGetDirectory:
			// [return value]: FSSpec on MAC, else char*  @see AudioEffectX::getDirectory
			if(_directory == NULL)
			{
				_directory = TypeConverter::AllocateString(_hostCmdStub->GetDirectory());
			}
			// return cached value
			result = (VstIntPtr)_directory;
			break;
		case audioMasterUpdateDisplay:
			// no arguments	
			result = _hostCmdStub->UpdateDisplay() ? 1 : 0;
			break;
		case audioMasterBeginEdit:
			// [index]: parameter index  @see AudioEffectX::beginEdit
			result = _hostCmdStub->BeginEdit(index) ? 1 : 0;
			break;
		case audioMasterEndEdit:
			// [index]: parameter index  @see AudioEffectX::endEdit
			result = _hostCmdStub->EndEdit(index) ? 1 : 0;
			break;
		case audioMasterOpenFileSelector:
		{
			// [ptr]: VstFileSelect* [return value]: 1 if supported  @see AudioEffectX::openFileSelector
			Jacobi::Vst::Core::VstFileSelect^ fileSelect = TypeConverter::ToManagedFileSelect((::VstFileSelect*)ptr);
			result = _hostCmdStub->OpenFileSelector(fileSelect) ? 1 : 0;
			TypeConverter::AllocUpdateUnmanagedFileSelect((::VstFileSelect*)ptr, fileSelect);
		}	break;
		case audioMasterCloseFileSelector:
		{
			// [ptr]: VstFileSelect*  @see AudioEffectX::closeFileSelector
			Jacobi::Vst::Core::VstFileSelect^ fileSelect = TypeConverter::GetManagedFileSelect((::VstFileSelect*)ptr);
			result = _hostCmdStub->CloseFileSelector(fileSelect) ? 1 : 0;
			TypeConverter::DeleteUpdateUnmanagedFileSelect((::VstFileSelect*)ptr);
		}	break;
		default:
			result = DispatchDeprecated(opcode, index, value, ptr, opt);
			break;
		}
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}

	_traceCtx->WriteDispatchEnd(System::IntPtr(result));

	return result;
}

VstIntPtr VstHostCommandProxy::DispatchDeprecated(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	VstIntPtr result = 0;

	if(_deprecatedCmdStub != nullptr)
	{
		switch(opcode)
		{
		// VST 1.0
		case DECLARE_VST_DEPRECATED (audioMasterPinConnected):
			result = _deprecatedCmdStub->PinConnected(index, value != 0) ? 1 : 0;
			break;

		// VST 2.0
		case DECLARE_VST_DEPRECATED (audioMasterSetTime):
		{
			Jacobi::Vst::Core::VstTimeInfo^ timeInfo = TypeConverter::ToManagedTimeInfo((::VstTimeInfo*)ptr);
			Jacobi::Vst::Core::VstTimeInfoFlags filterFlags = safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(value);

			result = _deprecatedCmdStub->SetTime(timeInfo, filterFlags) ? 1 : 0;
		}	break;
		case DECLARE_VST_DEPRECATED (audioMasterTempoAt):
			result = safe_cast<VstIntPtr>(_deprecatedCmdStub->GetTempoAt(safe_cast<System::Int32>(value)));
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetNumAutomatableParameters):
			result = safe_cast<VstIntPtr>(_deprecatedCmdStub->GetAutomatableParameterCount());
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetParameterQuantization):
			result = safe_cast<VstIntPtr>(_deprecatedCmdStub->GetParameterQuantization(safe_cast<System::Int32>(value)));
			break;
		case DECLARE_VST_DEPRECATED (audioMasterNeedIdle):
			result = _deprecatedCmdStub->NeedIdle() ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetPreviousPlug):
			result = (VstIntPtr)_deprecatedCmdStub->GetPreviousPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetNextPlug):
			result = (VstIntPtr)_deprecatedCmdStub->GetNextPlugin(safe_cast<System::Int32>(value)).ToPointer();
			break;
		case DECLARE_VST_DEPRECATED (audioMasterWillReplaceOrAccumulate):
			result = safe_cast<VstIntPtr>(_deprecatedCmdStub->WillReplaceOrAccumulate());
			break;
		case DECLARE_VST_DEPRECATED (audioMasterSetOutputSampleRate):
			result = _deprecatedCmdStub->SetOutputSampleRate(opt) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetOutputSpeakerArrangement):
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _deprecatedCmdStub->GetOutputSpeakerArrangement();
			TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
			result = (VstIntPtr)_pArrangement;
		}	break;
		case DECLARE_VST_DEPRECATED (audioMasterSetIcon):
		{
			System::IntPtr hIcon(ptr);
			System::Drawing::Icon^ icon = System::Drawing::Icon::FromHandle(hIcon);
			result = _deprecatedCmdStub->SetIcon(icon) ? 1 : 0;
		}	break;
		case DECLARE_VST_DEPRECATED (audioMasterOpenWindow):
			result = (VstIntPtr)_deprecatedCmdStub->OpenWindow().ToPointer();
			break;
		case DECLARE_VST_DEPRECATED (audioMasterCloseWindow):
			result = _deprecatedCmdStub->CloseWindow(System::IntPtr(ptr)) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (audioMasterEditFile):
			result = _deprecatedCmdStub->EditFile(TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (audioMasterGetChunkFile):
		{	System::String^ path = _deprecatedCmdStub->GetChunkFile();
			TypeConverter::StringToChar(path, (char*)ptr, 2047);
			result = System::String::IsNullOrEmpty(path) ? 1 : 0;
		}	break;
		case DECLARE_VST_DEPRECATED (audioMasterGetInputSpeakerArrangement):
		{	Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement = _deprecatedCmdStub->GetInputSpeakerArrangement();
			TypeConverter::ToUnmanagedSpeakerArrangement(_pArrangement, arrangement);
			result = (VstIntPtr)_pArrangement;
		}	break;
		default:
			// unknown command
			_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Information, "Unhandled dispatcher opcode:" + opcode);
			break;
		}
	}
	else
	{
		// unhandled command
		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Information, "Unhandled dispatcher opcode:" + opcode);
	}

	return result;
}

}}}} // Jacobi::Vst::Interop::Host