#include "StdAfx.h"
#include "PluginCommandProxy.h"
#include "..\TypeConverter.h"
#include "..\Utils.h"
#include<vcclr.h>

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

// constructs a new instance based on a reference to the plugin command stub
PluginCommandProxy::PluginCommandProxy(Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ cmdStub)
{
	if(cmdStub == nullptr)
	{
		throw gcnew System::ArgumentNullException("cmdStub");
	}

	_commandStub = cmdStub;
	_deprecatedCmdStub = dynamic_cast<Jacobi::Vst::Core::Deprecated::IVstPluginCommandsDeprecated20^>(cmdStub);

	_memTracker = gcnew MemoryTracker();
	_pEditorRect = new ERect();

	// construct a trace source for this command stub specific to the plugin its attached to.
	_traceCtx = gcnew Jacobi::Vst::Core::Diagnostics::TraceContext(Utils::GetPluginName() + ".Plugin.PluginCommandProxy", Jacobi::Vst::Core::Plugin::IVstPluginCommandStub::typeid);
}

PluginCommandProxy::~PluginCommandProxy()
{
	this->!PluginCommandProxy();
}

PluginCommandProxy::!PluginCommandProxy()
{
	Cleanup();
	delete _pEditorRect;
}

// Dispatches an opcode to the plugin command stub.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
VstIntPtr PluginCommandProxy::Dispatch(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	VstIntPtr result = 0;

	_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

	try
	{
		switch(opcode)
		{
		case effOpen:
			_commandStub->Open();
			break;
		case effClose:
			_commandStub->Close();
			// call Dispose() on this instance
			delete this;
			break;
		case effSetProgram:
			_commandStub->SetProgram(value);
			result = 1;
			break;
		case effGetProgram:
			result = _commandStub->GetProgram();
			break;
		case effSetProgramName:
			_commandStub->SetProgramName(TypeConverter::CharToString((char*)ptr));
			result = 1;
			break;
		case effGetProgramName:
			TypeConverter::StringToChar(_commandStub->GetProgramName(), (char*)ptr, kVstMaxProgNameLen);
			result = 1;
			break;
		case effGetParamLabel:
			TypeConverter::StringToChar(_commandStub->GetParameterLabel(index), (char*)ptr, kVstMaxParamStrLen);
			result = 1;
			break;
		case effGetParamDisplay:
			TypeConverter::StringToChar(_commandStub->GetParameterDisplay(index), (char*)ptr, kVstMaxParamStrLen);
			result = 1;
			break;
		case effGetParamName:
			TypeConverter::StringToChar(_commandStub->GetParameterName(index), (char*)ptr, kVstMaxParamStrLen);
			result = 1;
			break;
		case effSetSampleRate:
			_commandStub->SetSampleRate(opt);
			result = 1;
			break;
		case effSetBlockSize:
			_commandStub->SetBlockSize(value);
			result = 1;
			break;
		case effMainsChanged:
			_memTracker->ClearAll(); // safe to delete allocated memory during suspend/resume
			_commandStub->MainsChanged(value != 0);
			result = 1;
			break;
		case effEditGetRect:
		{
			System::Drawing::Rectangle rect;
			if (_commandStub->EditorGetRect(rect))
			{
				TypeConverter::ToUnmanagedRectangle(_pEditorRect, rect);
				*((ERect**)ptr) = _pEditorRect;
				result = 1;
			}
		}	break;
		case effEditOpen:
			result = _commandStub->EditorOpen(System::IntPtr(ptr));
			break;
		case effEditClose:
			_commandStub->EditorClose();
			result = 1;
			break;
		case effEditIdle:
			_commandStub->EditorIdle();
			result = 1;
			break;
		case effGetChunk:
		{
			array<System::Byte>^ buffer = _commandStub->GetChunk(index != 0);
			if(buffer != nullptr)
			{
				*(void**)ptr = TypeConverter::ByteArrayToPtr(buffer);
			
				_memTracker->RegisterArray(*(void**)ptr);

				result = buffer->Length;
			}
		}	break;
		case effSetChunk:
		{
			array<System::Byte>^ buffer = TypeConverter::PtrToByteArray((char*)ptr, value);
			result = _commandStub->SetChunk(buffer, index != 0) ? 1 : 0;
		}	break;
		case effProcessEvents:
			result = _commandStub->ProcessEvents(TypeConverter::ToManagedEventArray((VstEvents*)ptr)) ? 1 : 0;
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
		}	break;
		case effGetInputProperties:
		{
			Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetInputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::ToUnmanagedPinProperties((::VstPinProperties*)ptr, pinProps);
				result = 1;
			}
		}	break;
		case effGetOutputProperties:
		{
			Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetOutputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::ToUnmanagedPinProperties((::VstPinProperties*)ptr, pinProps);
				result = 1;
			}
		}	break;
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
			result = _commandStub->SetSpeakerArrangement(TypeConverter::ToManagedSpeakerArrangement((::VstSpeakerArrangement*)value),
				TypeConverter::ToManagedSpeakerArrangement((::VstSpeakerArrangement*)ptr));
			break;
		case effSetBypass:
			result = _commandStub->SetBypass(value != 0) ? 1 : 0;
			break;
		case effGetEffectName:
		{
			System::String^ name = _commandStub->GetEffectName();
			if(name != nullptr)
			{
				TypeConverter::StringToChar(name, (char*)ptr, kVstMaxEffectNameLen);
				result = 1;
			}
		}	break;
		case effGetVendorString:
		{
			System::String^ str = _commandStub->GetVendorString();
			if(str != nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, kVstMaxVendorStrLen);
				result = 1;
			}
		}	break;
		case effGetProductString:
		{
			System::String^ str = _commandStub->GetProductString();
			if(str != nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, kVstMaxProductStrLen);
				result = 1;
			}
		}	break;
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
				TypeConverter::ToUnmanagedParameterProperties((::VstParameterProperties*)ptr, paramProps);
				result = 1;
			}
		}	break;
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
			TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
		}	break;
		case effGetCurrentMidiProgram:
		{
			::MidiProgramName* pProgName = (::MidiProgramName*)ptr;
			Jacobi::Vst::Core::VstMidiProgramName^ progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
			result = _commandStub->GetCurrentMidiProgramName(progName, index);
			TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
		}	break;
		case effGetMidiProgramCategory:
		{
			::MidiProgramCategory* pProgCat = (::MidiProgramCategory*)ptr;
			Jacobi::Vst::Core::VstMidiProgramCategory^ progCat = gcnew Jacobi::Vst::Core::VstMidiProgramCategory();
			progCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
			result = _commandStub->GetMidiProgramCategory(progCat, index);
			TypeConverter::ToUnmanagedMidiProgramCategory(pProgCat, progCat);
		}	break;
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
		}	break;
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
				*ppInput = TypeConverter::AllocUnmanagedSpeakerArrangement(inputArr);
				_memTracker->RegisterObject(*ppInput);

				*ppOutput = TypeConverter::AllocUnmanagedSpeakerArrangement(outputArr);
				_memTracker->RegisterObject(*ppOutput);
			}
		}	break;
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
		//case effSetTotalSampleToProcess:
			//result = _commandStub->SetTotalSamplesToProcess(value);
			//break;
		case effSetPanLaw:
			result = _commandStub->SetPanLaw(safe_cast<Jacobi::Vst::Core::VstPanLaw>(value), opt) ? 1 : 0;
			break;
		case effBeginLoadBank:
			result = safe_cast<VstInt32>(_commandStub->BeginLoadBank(TypeConverter::ToManagedPatchChunkInfo((::VstPatchChunkInfo*)ptr)));
			break;
		case effBeginLoadProgram:
			result = safe_cast<VstInt32>(_commandStub->BeginLoadProgram(TypeConverter::ToManagedPatchChunkInfo((::VstPatchChunkInfo*)ptr)));
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

// continueation of Dispatch()
// Dispatches an opcode to the plugin deprecated command stub.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
VstIntPtr PluginCommandProxy::DispatchDeprecated(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt)
{
	VstIntPtr result = 0;

	if(_deprecatedCmdStub != nullptr)
	{
		switch(opcode)
		{
		// VST 1.0 deprecated
		case DECLARE_VST_DEPRECATED (effGetVu):
			// TODO: conversion from 'float' to 'VstIntPtr', possible loss of data
			result = _deprecatedCmdStub->GetVu();
			break;
		//case DECLARE_VST_DEPRECATED (effEditDraw):
		//	break;
		//case DECLARE_VST_DEPRECATED (effEditMouse):
		//	break;
		case DECLARE_VST_DEPRECATED (effEditKey):
			result = _deprecatedCmdStub->EditorKey(safe_cast<System::Int32>(value)) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effEditTop):
			result = _deprecatedCmdStub->EditorTop() ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effEditSleep):
			result = _deprecatedCmdStub->EditorSleep() ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effIdentify):
			result = _deprecatedCmdStub->Identify();
			break;

		// VST 2.0 deprecated
		case DECLARE_VST_DEPRECATED (effGetNumProgramCategories):
			result = _deprecatedCmdStub->GetProgramCategoriesCount();
			break;
		case DECLARE_VST_DEPRECATED (effCopyProgram):
			result = _deprecatedCmdStub->CopyCurrentProgramTo(index);
			break;
		case DECLARE_VST_DEPRECATED (effConnectInput):
			result = _deprecatedCmdStub->ConnectInput(index, value != 0) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effConnectOutput):
			result = _deprecatedCmdStub->ConnectOutput(index, value != 0) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effGetCurrentPosition):
			result = _deprecatedCmdStub->GetCurrentPosition();
			break;
		case DECLARE_VST_DEPRECATED (effGetDestinationBuffer):
		{
			Jacobi::Vst::Core::IDirectBufferAccess32^ audioBuffer = 
				dynamic_cast<Jacobi::Vst::Core::IDirectBufferAccess32^>(_deprecatedCmdStub->GetDestinationBuffer());
			if(audioBuffer != nullptr)
			{
				result = (VstIntPtr)audioBuffer->Buffer;
			}
		}	break;
		case DECLARE_VST_DEPRECATED (effSetBlockSizeAndSampleRate):
			result = _deprecatedCmdStub->SetBlockSizeAndSampleRate(safe_cast<System::Int32>(value), opt) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effGetErrorText):
		{
			System::String^ txt = _deprecatedCmdStub->GetErrorText();
			if(txt != nullptr)
			{
				TypeConverter::StringToChar(txt, (char*)ptr, 256);
				result = 1;
			}
		}	break;
		case DECLARE_VST_DEPRECATED (effIdle):
			result = _deprecatedCmdStub->Idle() ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effGetIcon):
		{
			System::Drawing::Icon^ icon = _deprecatedCmdStub->GetIcon();
			if(icon != nullptr)
			{
				// TODO:
				// void* in <ptr>, not yet defined
				//result = 1;
			}
		}	break;
		case DECLARE_VST_DEPRECATED (effSetViewPosition):
			result = _deprecatedCmdStub->SetViewPosition(System::Drawing::Point(index, safe_cast<System::Int32>(value))) ? 1 : 0;
			break;
		case DECLARE_VST_DEPRECATED (effKeysRequired):
			// NOTE: 0=Required, 1=dont need.
			result = _deprecatedCmdStub->KeysRequired() ? 0 : 1;
			break;

		default:
			// unknown command
			System::Diagnostics::Debug::WriteLine("Plugin.PluginCommandProxy: Unhandled dispatcher opcode:" + opcode, "VST.NET");
			break;
		}
	}

	return result;
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(float** inputs, float** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs)
{
	_traceCtx->WriteProcess(numInputs, numOutputs, sampleFrames, sampleFrames);

	try
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commandStub->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(double** inputs, double** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs)
{
	_traceCtx->WriteProcess(numInputs, numOutputs, sampleFrames, sampleFrames);

	try
	{
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commandStub->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to assign the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::SetParameter(VstInt32 index, float value)
{
	_traceCtx->WriteSetParameter(index, value);

	try
	{
		_commandStub->SetParameter(index, value);
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to retrieve the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
float PluginCommandProxy::GetParameter(VstInt32 index)
{
	_traceCtx->WriteGetParameterBegin(index);

	try
	{
		float value = _commandStub->GetParameter(index);

		_traceCtx->WriteGetParameterEnd(value);

		return value;
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}

	return 0.0f;
}

// Calls the plugin command stub to process audio (deprecated).
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::ProcessAcc(float** inputs, float** outputs, VstInt32 sampleFrames, VstInt32 numInputs, VstInt32 numOutputs)
{
	if(_deprecatedCmdStub == nullptr) return;

	_traceCtx->WriteProcess(numInputs, numOutputs, sampleFrames, sampleFrames);

	try
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_deprecatedCmdStub->ProcessAcc(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		_traceCtx->WriteError(e);

		Utils::ShowError(e);
	}
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

}}}} // Jacobi::Vst::Interop::Plugin