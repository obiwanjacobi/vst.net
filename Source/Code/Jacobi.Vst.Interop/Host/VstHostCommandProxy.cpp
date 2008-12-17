#include "StdAfx.h"
#include "VstHostCommandProxy.h"
#include "..\TypeConverter.h"
#include "..\Utils.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{
	VstHostCommandProxy::VstHostCommandProxy(Jacobi::Vst::Core::Host::IVstHostCommandStub^ hostCmdStub)
	{
		if(hostCmdStub == nullptr)
		{
			throw gcnew System::ArgumentNullException("hostCommandStub");
		}

		_hostCmdStub = hostCmdStub;
	}

	VstIntPtr VstHostCommandProxy::Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
	{
		VstIntPtr result = 0;

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
					//TODO:
					//result = VstTimeInfo* pTimeInfo;
				}
				}
				break;
			case audioMasterProcessEvents:
				// [ptr]: pointer to #VstEvents  @see VstEvents @see AudioEffectX::sendVstEventsToHost
				_hostCmdStub->ProcessEvents(TypeConverter::ToEventArray((::VstEvents*)ptr));
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
					System::Enum::Parse(Jacobi::Vst::Core::VstHostCanDo::typeid, candoStr));
				result = safe_cast<VstIntPtr>(_hostCmdStub->CanDo(cando));
				}
				break;
			case audioMasterGetLanguage:
				// [return value]: language code  @see VstHostLanguage
				result = safe_cast<VstIntPtr>(_hostCmdStub->GetLanguage());
				break;
			case audioMasterGetDirectory:
				// [return value]: FSSpec on MAC, else char*  @see AudioEffectX::getDirectory
				//TODO:
				//result = _hostCmdStub->GetDirectory();
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
				// [ptr]: VstFileSelect* [return value]: 1 if supported  @see AudioEffectX::openFileSelector
				//TODO:
				//result = _hostCmdStub->OpenFileSelector() ? 1 : 0;
				break;
			case audioMasterCloseFileSelector:
				// [ptr]: VstFileSelect*  @see AudioEffectX::closeFileSelector
				//TODO:
				//result = _hostCmdStub->CloseFileSelector() ? 1 : 0;
				break;
			default:
				break;
			}
		}
		catch(System::Exception^ e)
		{
			Utils::ShowError(e);
		}

		return result;
	}

}}}} // namespace Jacobi.Vst.Interop.Host