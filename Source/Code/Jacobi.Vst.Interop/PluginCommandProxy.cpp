#include "StdAfx.h"
#include "PluginCommandProxy.h"
#include "TypeConverter.h"
#include<vcclr.h>

PluginCommandProxy::PluginCommandProxy(IVstPluginCommandStub^ cmdStub)
{
	_commandStub = cmdStub;
}

VstIntPtr PluginCommandProxy::Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	if(!_commandStub) return 0;

	VstIntPtr result = 0;

	switch(opcode)
	{
	case effOpen:
		_commandStub->Open();
		break;
	case effClose:
		_commandStub->Close();
		break;
	case effSetProgram:
		_commandStub->SetProgram((VstInt32)value);
		break;
	case effGetProgram:
		result = _commandStub->GetProgram();
		break;
	case effSetProgramName:
		_commandStub->SetProgramName(TypeConverter::CharToString((char*)ptr));
		break;
	case effGetProgramName:
		TypeConverter::StringToChar(_commandStub->GetProgramName(), (char*)ptr, kVstMaxProgNameLen);
		break;
	case effGetParamLabel:
		TypeConverter::StringToChar(_commandStub->GetParameterLabel(index), (char*)ptr, kVstMaxParamStrLen);
		break;
	case effGetParamDisplay:
		TypeConverter::StringToChar(_commandStub->GetParameterDisplay(index), (char*)ptr, kVstMaxParamStrLen);
		break;
	case effGetParamName:
		TypeConverter::StringToChar(_commandStub->GetParameterName(index), (char*)ptr, kVstMaxParamStrLen);
		break;
	case effSetSampleRate:
		_commandStub->SetSampleRate(opt);
		break;
	case effSetBlockSize:
		_commandStub->SetBlockSize((VstInt32)value);
		break;
	case effMainsChanged:
		_commandStub->MainsChanged(value == 1);
		break;
	case effEditGetRect:
		{
		System::Drawing::Rectangle rect;
		result = (_commandStub->EditorGetRect(rect) ? 1 : 0);
		TypeConverter::RectangleToERect(rect, (ERect**)ptr);
		}
		break;
	case effEditOpen:
		result = _commandStub->EditorOpen(IntPtr(ptr));
		break;
	case effEditClose:
		_commandStub->EditorClose();
		break;
	case effEditIdle:
		_commandStub->EditorIdle();
		break;
	case effGetChunk:
		{
		array<System::Byte>^ buffer;
		result = _commandStub->GetChunk(buffer, index == 1) ? 1 : 0;
		TypeConverter::ByteArrayToPtr(buffer, (void**)ptr);
		//result = buffer->Length;
		}
		break;
	case effSetChunk:
		{
		array<System::Byte>^ buffer = TypeConverter::PtrToByteArray(ptr, value);
		result = _commandStub->SetChunk(buffer, index == 1) ? 1 : 0;
		}
		break;
	case effProcessEvents:
		result = _commandStub->ProcessEvents(TypeConverter::ToEventArray((VstEvents*)ptr)) ? 1 : 0;
		break;
	case effCanBeAutomated:
		result = _commandStub->CanParameterBeAutomated(index) ? 1 : 0;
		break;
	case effString2Parameter:
		result = _commandStub->String2Parameter(index, TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
		break;
	case effGetProgramNameIndexed:
		{
		System::String^ name;
		result = _commandStub->GetProgramNameIndexed(index, name);
		TypeConverter::StringToChar(name, (char*)ptr, kVstMaxProgNameLen);
		}
		break;
	case effGetInputProperties:
		result = _commandStub->GetInputProperties(index, TypeConverter::ToPinProperties((::VstPinProperties*)ptr)) ? 1 : 0;
		break;
	case effGetOutputProperties:
		result = _commandStub->GetOutputProperties(index, TypeConverter::ToPinProperties((::VstPinProperties*)ptr)) ? 1 : 0;
		break;
	case effGetPlugCategory:
		result = _commandStub->GetCategory();
		break;
	case effOfflineNotify:
		// ptr -> VstAudioFile*
		//result = _commandStub->OfflineNotify(value, index != 0);
		break;
	case effOfflinePrepare:
		// ptr -> VstOfflineTask*
		//result = _commandStub->OfflinePrepare(value);
		break;
	case effOfflineRun:
		// ptr -> VstOfflineTask*
		//result = _commandStub->OfflineRun(value);
		break;
	case effProcessVarIo:
		// ptr -> VstVariableIo*
		//result = _commandStub->ProcessVariableIO();
		break;
	case effSetSpeakerArrangement:
		result = _commandStub->SetSpeakerArrangement(TypeConverter::ToSpeakerArrangement((::VstSpeakerArrangement*)value),
			TypeConverter::ToSpeakerArrangement((::VstSpeakerArrangement*)ptr));
		break;
	case effSetBypass:
		result = _commandStub->SetBypass(value ? true : false) ? 1 : 0;
		break;
	case effGetEffectName:
		{
		System::String^ name;
		result = _commandStub->GetEffectName(name) ? 1 : 0;
		TypeConverter::StringToChar(name, (char*)ptr, kVstMaxEffectNameLen);
		}
		break;
	case effGetVendorString:
		{
		System::String^ str;
		result = _commandStub->GetVendorString(str) ? 1 : 0;
		TypeConverter::StringToChar(str, (char*)ptr, kVstMaxEffectNameLen);
		}
		break;
	case effGetProductString:
		{
		System::String^ str;
		result = _commandStub->GetProductString(str) ? 1 : 0;
		TypeConverter::StringToChar(str, (char*)ptr, kVstMaxEffectNameLen);
		}
		break;
	case effGetVendorVersion:
		result = _commandStub->GetVendorVersion();
		break;
	case effCanDo:
		result = _commandStub->CanDo(TypeConverter::CharToString((char*)ptr));
		break;
	case effGetTailSize:
		result = _commandStub->GetTailSize();
		break;
	case effGetParameterProperties:
		result = _commandStub->GetParameterProperties(index, 
			TypeConverter::ToParameterProperties((::VstParameterProperties*)ptr)) ? 1 : 0;
		break;
	case effGetVstVersion:
		result = _commandStub->GetVstVersion();
		break;
	case effEditKeyDown:
		result = _commandStub->EditorKeyDown((System::Byte)index, (System::Byte)value, (System::Byte)opt) ? 1 : 0;
		break;
	case effEditKeyUp:
		result = _commandStub->EditorKeyUp((System::Byte)index, (System::Byte)value, (System::Byte)opt) ? 1 : 0;
		break;
	case effSetEditKnobMode:
		result = _commandStub->SetEditorKnobMode(value) ? 1 : 0;
		break;
	case effGetMidiProgramName:
		{
		::MidiProgramName* pProgName = (::MidiProgramName*)ptr;
		Jacobi::Vst::Core::VstMidiProgramName^ progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
		progName->CurrentProgramIndex = pProgName->thisProgramIndex;
		result = _commandStub->GetMidiProgramName(progName, index);
		TypeConverter::FromMidiProgramName(progName, pProgName);
		}
		break;
	case effGetCurrentMidiProgram:
		{
		::MidiProgramName* pProgName = (::MidiProgramName*)ptr;
		Jacobi::Vst::Core::VstMidiProgramName^ progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
		result = _commandStub->GetCurrentMidiProgramName(progName, index);
		TypeConverter::FromMidiProgramName(progName, pProgName);
		}
		break;
	case effGetMidiProgramCategory:
		{
		::MidiProgramCategory* pProgCat = (::MidiProgramCategory*)ptr;
		Jacobi::Vst::Core::VstMidiProgramCategory^ progCat = gcnew Jacobi::Vst::Core::VstMidiProgramCategory();
		progCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
		result = _commandStub->GetMidiProgramCategory(progCat, index);
		TypeConverter::FromMidiProgramCategory(progCat, pProgCat);
		}
		break;
	case effHasMidiProgramsChanged:
		result = _commandStub->HasMidiProgramsChanged(index) ? 1 : 0;
		break;
	case effGetMidiKeyName:
		{
		::MidiKeyName* pKeyName = (::MidiKeyName*)ptr;
		Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName = gcnew Jacobi::Vst::Core::VstMidiKeyName();
		midiKeyName->CurrentProgramIndex = pKeyName->thisProgramIndex;
		midiKeyName->CurrentKeyNumber = pKeyName->thisKeyNumber;
		result = _commandStub->GetMidiKeyName(midiKeyName, index);
		TypeConverter::StringToChar(midiKeyName->Name, pKeyName->keyName, kVstMaxNameLen);
		}
		break;
	case effBeginSetProgram:
		result = _commandStub->BeginSetProgram() ? 1 : 0;
		break;
	case effEndSetProgram:
		result = _commandStub->EndSetProgram() ? 1 : 0;
		break;
	case effGetSpeakerArrangement:
		// TODO: implement
		// mem alloc
		break;
	case effShellGetNextPlugin:
		System::String^ str;
		result = _commandStub->GetNextPlugin(str);
		TypeConverter::StringToChar(str, (char*)ptr, kVstMaxProductStrLen);
		break;
	case effStartProcess:
		result = _commandStub->StartProcess();
		break;
	case effStopProcess:
		result = _commandStub->StopProcess();
		break;
	case effSetTotalSampleToProcess:
		result = _commandStub->SetTotalSamplesToProcess(value);
		break;
	case effSetPanLaw:
		result = _commandStub->SetPanLaw(value, opt) ? 1 : 0;
		break;
	case effBeginLoadBank:
		result = _commandStub->BeginLoadBank(TypeConverter::ToPatchChunkInfo((::VstPatchChunkInfo*)ptr));
		break;
	case effBeginLoadProgram:
		result = _commandStub->BeginLoadProgram(TypeConverter::ToPatchChunkInfo((::VstPatchChunkInfo*)ptr));
		break;
	case effSetProcessPrecision:
		result = _commandStub->SetProcessPrecision((Jacobi::Vst::Core::VstProcessPrecision)value) ? 1 : 0;
		break;
	case effGetNumMidiInputChannels:
		result = _commandStub->GetNumberOfMidiInputChannels();
		break;
	case effGetNumMidiOutputChannels:
		result = _commandStub->GetNumberOfMidiOutputChannels();
		break;
	default:
		// unknown command
		break;
	}

	return result;
}