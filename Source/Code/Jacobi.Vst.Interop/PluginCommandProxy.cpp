#include "StdAfx.h"
#include "PluginCommandProxy.h"
#include "TypeConverter.h"
#include "Utils.h"
#include<vcclr.h>

// constructs a new instance based on a reference to the plugin command stub
PluginCommandProxy::PluginCommandProxy(Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ cmdStub)
{
	if(cmdStub == nullptr)
	{
		throw gcnew System::ArgumentNullException("cmdStub");
	}

	_commandStub = cmdStub;
	_memTracker = gcnew MemoryTracker();
}

// Dispatches an opcode to the plugin command stub.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
VstIntPtr PluginCommandProxy::Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	VstIntPtr result = 0;

	try
	{
		switch(opcode)
		{
		// Cubase 2.X SX asks for this and wont load otherwise
		case DECLARE_VST_DEPRECATED (effIdentify):
			result = CCONST ('N', 'v', 'E', 'f');
			break;

		case effOpen:
			_commandStub->Open();
			break;
		case effClose:
			_commandStub->Close();
			Cleanup();
			break;
		case effSetProgram:
			_commandStub->SetProgram(value);
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
			_commandStub->SetBlockSize(value);
			break;
		case effMainsChanged:
			_memTracker->ClearAll(); // safe to delete allocated memory during suspend/resume
			_commandStub->MainsChanged(value != 0);
			break;
		case effEditGetRect:
			{
			System::Drawing::Rectangle rect;
			result = (_commandStub->EditorGetRect(rect) ? 1 : 0);
			TypeConverter::RectangleToERect(rect, (ERect**)ptr);
			}
			break;
		case effEditOpen:
			result = _commandStub->EditorOpen(System::IntPtr(ptr));
			break;
		case effEditClose:
			_commandStub->EditorClose();
			break;
		case effEditIdle:
			_commandStub->EditorIdle();
			break;
		case effGetChunk:
			{
			array<System::Byte>^ buffer = _commandStub->GetChunk(index != 0);
			*(void**)ptr = TypeConverter::ByteArrayToPtr(buffer);
			
			_memTracker->RegisterArray(*(void**)ptr);

			result = buffer->Length;
			}
			break;
		case effSetChunk:
			{
			array<System::Byte>^ buffer = TypeConverter::PtrToByteArray((char*)ptr, value);
			result = _commandStub->SetChunk(buffer, index != 0) ? 1 : 0;
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
			System::String^ name = _commandStub->GetProgramNameIndexed(index);
			if(name != nullptr)
			{
				TypeConverter::StringToChar(name, (char*)ptr, kVstMaxProgNameLen);
				result = 1;
			}
			}
			break;
		case effGetInputProperties:
			{
			Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetInputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::FromPinProperties(pinProps, (::VstPinProperties*)ptr);
				result = 1;
			}
			}
			break;
		case effGetOutputProperties:
			{
			Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetOutputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::FromPinProperties(pinProps, (::VstPinProperties*)ptr);
				result = 1;
			}
			}
			break;
		case effGetPlugCategory:
			result = (VstInt32)_commandStub->GetCategory();
			break;
		//case effOfflineNotify:
			// ptr -> VstAudioFile*
			//result = _commandStub->OfflineNotify(value, index != 0);
			//break;
		//case effOfflinePrepare:
			// ptr -> VstOfflineTask*
			//result = _commandStub->OfflinePrepare(value);
			//break;
		//case effOfflineRun:
			// ptr -> VstOfflineTask*
			//result = _commandStub->OfflineRun(value);
			//break;
		//case effProcessVarIo:
			// ptr -> VstVariableIo*
			//result = _commandStub->ProcessVariableIO();
			//break;
		case effSetSpeakerArrangement:
			result = _commandStub->SetSpeakerArrangement(TypeConverter::ToSpeakerArrangement((::VstSpeakerArrangement*)value),
				TypeConverter::ToSpeakerArrangement((::VstSpeakerArrangement*)ptr));
			break;
		case effSetBypass:
			result = _commandStub->SetBypass(value != 0 ? true : false) ? 1 : 0;
			break;
		case effGetEffectName:
			{
			System::String^ name = _commandStub->GetEffectName();
			if(name != nullptr)
			{
				TypeConverter::StringToChar(name, (char*)ptr, kVstMaxEffectNameLen);
				result = 1;
			}
			}
			break;
		case effGetVendorString:
			{
			System::String^ str = _commandStub->GetVendorString();
			if(str != nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, kVstMaxEffectNameLen);
				result = 1;
			}
			}
			break;
		case effGetProductString:
			{
			System::String^ str = _commandStub->GetProductString();
			if(str == nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, kVstMaxEffectNameLen);
				result = 1;
			}
			}
			break;
		case effGetVendorVersion:
			result = _commandStub->GetVendorVersion();
			break;
		case effCanDo:
			result = safe_cast<VstInt32>(_commandStub->CanDo(TypeConverter::CharToString((char*)ptr)));
			break;
		case effGetTailSize:
			result = _commandStub->GetTailSize();
			break;
		case effGetParameterProperties:
			{
			Jacobi::Vst::Core::VstParameterProperties^ paramProps = _commandStub->GetParameterProperties(index);
			if(paramProps != nullptr)
			{
				TypeConverter::FromParameterProperties(paramProps,(::VstParameterProperties*)ptr);
				result = 1;
			}
			}
			break;
		case effGetVstVersion:
			result = _commandStub->GetVstVersion();
			break;
		case effEditKeyDown:
			result = _commandStub->EditorKeyDown(safe_cast<System::Byte>(index), 
				safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
				safe_cast<Jacobi::Vst::Core::VstModifierKeys>((VstInt32)opt)) ? 1 : 0;
			break;
		case effEditKeyUp:
			result = _commandStub->EditorKeyUp(safe_cast<System::Byte>(index), 
				safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
				safe_cast<Jacobi::Vst::Core::VstModifierKeys>((VstInt32)opt)) ? 1 : 0;
			break;
		case effSetEditKnobMode:
			result = _commandStub->SetEditorKnobMode(safe_cast<Jacobi::Vst::Core::VstKnobMode>(value)) ? 1 : 0;
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
			{
			::VstSpeakerArrangement** ppInput = (::VstSpeakerArrangement**)value;
			::VstSpeakerArrangement** ppOutput = (::VstSpeakerArrangement**)ptr;

			*ppInput = NULL;
			*ppOutput = NULL;

			Jacobi::Vst::Core::VstSpeakerArrangement^ inputArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();
			Jacobi::Vst::Core::VstSpeakerArrangement^ outputArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();
			// let plugin fill the input and output speaker arrangements
			result = _commandStub->GetSpeakerArrangement(inputArr, outputArr) ? 1 : 0;
			if(result)
			{
				// NOTE: register retvals with the memory tracker to be deleted later.
				*ppInput = TypeConverter::FromSpeakerArrangement(inputArr);
				_memTracker->RegisterObject(*ppInput);

				*ppOutput = TypeConverter::FromSpeakerArrangement(outputArr);
				_memTracker->RegisterObject(*ppOutput);
			}
			}
			break;
		//case effShellGetNextPlugin:
			/*{
			System::String^ str;
			result = _commandStub->GetNextPlugin(str);
			TypeConverter::StringToChar(str, (char*)ptr, kVstMaxProductStrLen);
			}*/
			//break;
		case effStartProcess:
			result = _commandStub->StartProcess();
			break;
		case effStopProcess:
			result = _commandStub->StopProcess();
			break;
		case effSetTotalSampleToProcess:
			//result = _commandStub->SetTotalSamplesToProcess(value);
			break;
		case effSetPanLaw:
			result = _commandStub->SetPanLaw(safe_cast<Jacobi::Vst::Core::VstPanLaw>(value), opt) ? 1 : 0;
			break;
		case effBeginLoadBank:
			// TODO: how do we deal with VstPatchChunkInfo in PluginHost scenario?
			result = safe_cast<VstInt32>(_commandStub->BeginLoadBank(TypeConverter::ToPatchChunkInfo((::VstPatchChunkInfo*)ptr)));
			break;
		case effBeginLoadProgram:
			// TODO: how do we deal with VstPatchChunkInfo in PluginHost scenario?
			result = safe_cast<VstInt32>(_commandStub->BeginLoadProgram(TypeConverter::ToPatchChunkInfo((::VstPatchChunkInfo*)ptr)));
			break;
		case effSetProcessPrecision:
			result = _commandStub->SetProcessPrecision(safe_cast<Jacobi::Vst::Core::VstProcessPrecision>(value)) ? 1 : 0;
			break;
		case effGetNumMidiInputChannels:
			result = _commandStub->GetNumberOfMidiInputChannels();
			break;
		case effGetNumMidiOutputChannels:
			result = _commandStub->GetNumberOfMidiOutputChannels();
			break;
		default:
			// unknown command
			System::Diagnostics::Debug::WriteLine("Unhandled dispatcher opcode:" + opcode, "VST.NET");
			break;
		}
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}

	return result;
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(float** inputs, float** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs)
{
	try
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputBuffers = TypeConverter::ToAudioBufferArray(inputs, sampleFrames, numInputs, false);
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputBuffers = TypeConverter::ToAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commandStub->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(double** inputs, double** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs)
{
	try
	{
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputBuffers = TypeConverter::ToAudioBufferArray(inputs, sampleFrames, numInputs, false);
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputBuffers = TypeConverter::ToAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commandStub->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to assign the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::SetParameter(VstInt32 index, float value)
{
	try
	{
		_commandStub->SetParameter(index, value);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to retrieve the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
float PluginCommandProxy::GetParameter(VstInt32 index)
{
	try
	{
		return _commandStub->GetParameter(index);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}

	return 0.0f;
}

// Cleans up any delayed memory deletes.
void PluginCommandProxy::Cleanup()
{
	if(_memTracker != nullptr)
	{
		_memTracker->ClearAll();
		_memTracker = nullptr;
	}

	_commandStub = nullptr;
}