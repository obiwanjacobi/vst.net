#include "StdAfx.h"
#include "UnmanagedArray.h"
#include "VstPluginCommandStub.h"
#include "..\TypeConverter.h"
#include "..\UnmanagedString.h"
#include "..\UnmanagedPointer.h"


VstPluginCommandStub::VstPluginCommandStub(::AEffect* pEffect)
{
	if(pEffect == NULL)
	{
		throw gcnew System::ArgumentNullException("pEffect");
	}

	_pEffect = pEffect;
	_memoryTracker = gcnew MemoryTracker();
}

// IVstPluginCommandsBase
void VstPluginCommandStub::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs)
{
	float** ppInputs = _audioInputs.GetArray(inputs->Length);
	float** ppOutputs = _audioOutputs.GetArray(outputs->Length);

	VstInt32 inputSampleCount = CopyBufferPointers(ppInputs, inputs);
	VstInt32 outputSampleCount = CopyBufferPointers(ppOutputs, outputs);

	if(inputSampleCount != outputSampleCount)
	{
		throw gcnew System::ArgumentException("The number of samples in the 'inputs' and the 'outputs' VstAudioBuffer array was not the same.");
	}

	CallProcess32(ppInputs, ppOutputs, inputSampleCount);
}

void VstPluginCommandStub::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputs)
{
	double** ppInputs = _precisionInputs.GetArray(inputs->Length);
	double** ppOutputs = _precisionOutputs.GetArray(outputs->Length);

	VstInt32 inputSampleCount = CopyBufferPointers(ppInputs, inputs);
	VstInt32 outputSampleCount = CopyBufferPointers(ppOutputs, outputs);

	if(inputSampleCount != outputSampleCount)
	{
		throw gcnew System::ArgumentException("The number of samples in the 'inputs' and the 'outputs' VstAudioBuffer array was not the same.");
	}

	CallProcess64(ppInputs, ppOutputs, inputSampleCount);
}

void VstPluginCommandStub::SetParameter(System::Int32 index, System::Single value)
{
	CallSetParameter(index, value);
}

System::Single VstPluginCommandStub::GetParameter(System::Int32 index)
{
	return CallGetParameter(index);
}

// IVstPluginCommands10
void VstPluginCommandStub::Open()
{
	CallDispatch(effOpen, 0, 0, 0, 0);
}

void VstPluginCommandStub::Close()
{
	_memoryTracker->ClearAll();
	CallDispatch(effClose, 0, 0, 0, 0);
}

void VstPluginCommandStub::SetProgram(System::Int32 programNumber)
{
	CallDispatch(effSetProgram, 0, programNumber, 0, 0);
}

System::Int32 VstPluginCommandStub::GetProgram()
{
	return CallDispatch(effGetProgram, 0, 0, 0, 0);
}

void VstPluginCommandStub::SetProgramName(System::String^ name)
{
	char* progName = TypeConverter::AllocateString(name);

	try
	{
		CallDispatch(effSetProgramName, 0, 0, progName, 0);
	}
	finally
	{
		TypeConverter::DeallocateString(progName);
	}
}

System::String^ VstPluginCommandStub::GetProgramName()
{
	UnmanagedString progName(kVstMaxProgNameLen);

	if(CallDispatch(effGetProgramName, 0, 0, progName, 0))
	{
		return TypeConverter::CharToString(progName);
	}

	return nullptr;
}

System::String^ VstPluginCommandStub::GetParameterLabel(System::Int32 index)
{
	UnmanagedString paramLabel(kVstMaxParamStrLen);

	if(CallDispatch(effGetParamLabel, index, 0, paramLabel, 0))
	{
		return TypeConverter::CharToString(paramLabel);
	}

	return nullptr;
}

System::String^ VstPluginCommandStub::GetParameterDisplay(System::Int32 index)
{
	UnmanagedString paramLabel(kVstMaxParamStrLen);

	if(CallDispatch(effGetParamDisplay, index, 0, paramLabel, 0))
	{
		return TypeConverter::CharToString(paramLabel);
	}

	return nullptr;
}

System::String^ VstPluginCommandStub::GetParameterName(System::Int32 index)
{
	UnmanagedString paramName(kVstMaxParamStrLen);

	if(CallDispatch(effGetParamName, index, 0, paramName, 0))
	{
		return TypeConverter::CharToString(paramName);
	}

	return nullptr;
}

void VstPluginCommandStub::SetSampleRate(System::Single sampleRate)
{
	CallDispatch(effSetSampleRate, 0, 0, 0, sampleRate);
}

void VstPluginCommandStub::SetBlockSize(System::Int32 blockSize)
{
	CallDispatch(effSetBlockSize, 0, blockSize, 0, 0);
}

void VstPluginCommandStub::MainsChanged(System::Boolean onoff)
{
	CallDispatch(effMainsChanged, 0, onoff ? 1 : 0, 0, 0);
}

System::Boolean VstPluginCommandStub::EditorGetRect([System::Runtime::InteropServices::Out] System::Drawing::Rectangle% rect)
{
	UnmanagedPointer<ERect> unmanagedRect(false);

	if(CallDispatch(effEditGetRect, 0, 0, &unmanagedRect, 0))
	{
		rect = TypeConverter::ToManagedRectangle(unmanagedRect);
		return true;
	}

	// dummy rect
	rect = System::Drawing::Rectangle();
	return false;
}

System::Boolean VstPluginCommandStub::EditorOpen(System::IntPtr hWnd)
{
	return (CallDispatch(effEditOpen, 0, 0, hWnd.ToPointer(), 0) != 0);
}

void VstPluginCommandStub::EditorClose()
{
	CallDispatch(effEditClose, 0, 0, 0, 0);
}

void VstPluginCommandStub::EditorIdle()
{
	CallDispatch(effEditIdle, 0, 0, 0, 0);
}

array<System::Byte>^ VstPluginCommandStub::GetChunk(System::Boolean isPreset)
{
	// we don't own the memory passed to us
	char* pBuffer = NULL;
	
	VstInt32 length = CallDispatch(effGetChunk, isPreset ? 1 : 0, 0, &pBuffer, 0);
	
	if(length > 0)
	{
		return TypeConverter::PtrToByteArray(pBuffer, length);
	}

	return nullptr;
}

System::Int32 VstPluginCommandStub::SetChunk(array<System::Byte>^ data, System::Boolean isPreset)
{
	char* dataArr = TypeConverter::ByteArrayToPtr(data);

	// we need to hold on to the unmanaged memory until suspend/resume is called.
	_memoryTracker->RegisterArray(dataArr);

	return safe_cast<System::Int32>(CallDispatch(effSetChunk, isPreset ? 1 : 0, data->Length, dataArr, 0));
}

// IVstPluginCommands20
System::Boolean VstPluginCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
{
	::VstEvents* pEvents = TypeConverter::AllocUnmanagedEvents(events);

	try
	{
		return (CallDispatch(effProcessEvents, 0, 0, pEvents, 0) != 0);
	}
	finally
	{
		TypeConverter::DeleteUnmanagedEvents(pEvents);
	}
}

System::Boolean VstPluginCommandStub::CanParameterBeAutomated(System::Int32 index)
{
	return (CallDispatch(effCanBeAutomated, index, 0, 0, 0) != 0);
}

System::Boolean VstPluginCommandStub::String2Parameter(System::Int32 index, System::String^ str)
{
	char* pStr = TypeConverter::AllocateString(str);

	try
	{
		return (CallDispatch(effString2Parameter, index, 0, pStr, 0) != 0);
	}
	finally
	{
		TypeConverter::DeallocateString(pStr);
	}
}

System::String^ VstPluginCommandStub::GetProgramNameIndexed(System::Int32 index)
{
	UnmanagedString progName(kVstMaxProgNameLen);

	if(CallDispatch(effGetProgramNameIndexed, index, 0, progName, 0))
	{
		return TypeConverter::CharToString(progName);
	}

	return nullptr;
}

Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandStub::GetInputProperties(System::Int32 index)
{
	UnmanagedPointer<VstPinProperties> pinProps;

	if(CallDispatch(effGetInputProperties, index, 0, pinProps, 0) != 0)
	{
		return TypeConverter::ToManagedPinProperties(pinProps);
	}
	
	return nullptr;
}

Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandStub::GetOutputProperties(System::Int32 index)
{
	UnmanagedPointer<VstPinProperties> pinProps;

	if(CallDispatch(effGetOutputProperties, index, 0, pinProps, 0) != 0)
	{
		return TypeConverter::ToManagedPinProperties(pinProps);
	}

	return nullptr;
}

Jacobi::Vst::Core::VstPluginCategory VstPluginCommandStub::GetCategory()
{
	return safe_cast<Jacobi::Vst::Core::VstPluginCategory>(CallDispatch(effGetPlugCategory, 0, 0, 0, 0));
}

// Offline processing not implemented
//System::Boolean OfflineNotify(array<VstAudioFile^>^ audioFiles, System::Int32 count, System::Int32 startFlag);
//System::Boolean OfflinePrepare(array<VstOfflineTask^>^ tasks, System::Int32 count);
//System::Boolean OfflineRun(array<VstOfflineTask^>^ tasks, System::Int32 count);
//System::Boolean ProcessVariableIO(VstVariableIO^ variableIO);

System::Boolean VstPluginCommandStub::SetSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ saInput, 
	Jacobi::Vst::Core::VstSpeakerArrangement^ saOutput)
{
	UnmanagedPointer<::VstSpeakerArrangement> pInput(TypeConverter::AllocUnmanagedSpeakerArrangement(saInput));
	UnmanagedPointer<::VstSpeakerArrangement> pOutput(TypeConverter::AllocUnmanagedSpeakerArrangement(saOutput));

	return (CallDispatch(effSetSpeakerArrangement, 0, (VstIntPtr)(::VstSpeakerArrangement*)pInput, pOutput, 0) != 0);
}

System::Boolean VstPluginCommandStub::SetBypass(System::Boolean bypass)
{
	return (CallDispatch(effSetBypass, 0, bypass ? 1 : 0, 0, 0) != 0);
}

System::String^ VstPluginCommandStub::GetEffectName()
{
	UnmanagedString effectName(kVstMaxEffectNameLen);

	if(CallDispatch(effGetEffectName, 0, 0, effectName, 0) != 0)
	{
		return TypeConverter::CharToString(effectName);
	}

	return nullptr;
}

System::String^ VstPluginCommandStub::GetVendorString()
{
	UnmanagedString vendor(kVstMaxEffectNameLen);

	if(CallDispatch(effGetVendorString, 0, 0, vendor, 0) != 0)
	{
		return TypeConverter::CharToString(vendor);
	}

	return nullptr;
}

System::String^ VstPluginCommandStub::GetProductString()
{
	UnmanagedString product(kVstMaxEffectNameLen);

	if(CallDispatch(effGetProductString, 0, 0, product, 0) != 0)
	{
		return TypeConverter::CharToString(product);
	}

	return nullptr;
}

System::Int32 VstPluginCommandStub::GetVendorVersion()
{
	return safe_cast<System::Int32>(CallDispatch(effGetVendorVersion, 0, 0, 0, 0));
}

Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::CanDo(System::String^ cando)
{
	char* pCanDo = TypeConverter::AllocateString(cando);

	try
	{
		return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(CallDispatch(effCanDo, 0, 0, pCanDo, 0));
	}
	finally
	{
		TypeConverter::DeallocateString(pCanDo);
	}
}

System::Int32 VstPluginCommandStub::GetTailSize()
{
	return safe_cast<System::Int32>(CallDispatch(effGetTailSize, 0, 0, 0, 0));
}

Jacobi::Vst::Core::VstParameterProperties^ VstPluginCommandStub::GetParameterProperties(System::Int32 index)
{
	UnmanagedPointer<::VstParameterProperties> pProps;

	if(CallDispatch(effGetParameterProperties, index, 0, pProps, 0))
	{
		return TypeConverter::ToManagedParameterProperties(pProps);
	}

	return nullptr;
}

System::Int32 VstPluginCommandStub::GetVstVersion()
{
	return safe_cast<System::Int32>(CallDispatch(effGetVstVersion, 0, 0, 0, 0));
}

// IVstPluginCommands21
System::Boolean VstPluginCommandStub::EditorKeyDown(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
{
	return (CallDispatch(effEditKeyDown, ascii, safe_cast<VstIntPtr>(virtualKey), 0, safe_cast<float>(modifers)) != 0);
}

System::Boolean VstPluginCommandStub::EditorKeyUp(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
{
	return (CallDispatch(effEditKeyUp, ascii, safe_cast<VstIntPtr>(virtualKey), 0, safe_cast<float>(modifers)) != 0);
}

System::Boolean VstPluginCommandStub::SetEditorKnobMode(Jacobi::Vst::Core::VstKnobMode mode)
{
	return (CallDispatch(effSetEditKnobMode, 0, safe_cast<VstIntPtr>(mode), 0, 0) != 0);
}

System::Int32 VstPluginCommandStub::GetMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
{
	UnmanagedPointer<::MidiProgramName> pProgName;

	pProgName->thisProgramIndex = midiProgram->CurrentProgramIndex;

	System::Int32 result = safe_cast<System::Int32>(CallDispatch(effGetMidiProgramName, channel, 0, pProgName, 0));

	if(result != 0)
	{
		TypeConverter::ToManagedMidiProgramName(midiProgram, pProgName);
	}

	return result;
}

System::Int32 VstPluginCommandStub::GetCurrentMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
{
	UnmanagedPointer<::MidiProgramName> pProgName;

	System::Int32 result = safe_cast<System::Int32>(CallDispatch(effGetCurrentMidiProgram, channel, 0, pProgName, 0));

	if(result != 0)
	{
		TypeConverter::ToManagedMidiProgramName(midiProgram, pProgName);
	}

	return result;
}

System::Int32 VstPluginCommandStub::GetMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiCat, System::Int32 channel)
{
	UnmanagedPointer<::MidiProgramCategory> pProgCat;

	System::Int32 result = safe_cast<System::Int32>(CallDispatch(effGetMidiProgramCategory, channel, 0, pProgCat, 0));

	if(result != 0)
	{
		TypeConverter::ToManagedMidiProgramCategory(midiCat, pProgCat);
	}

	return result;
}

System::Boolean VstPluginCommandStub::HasMidiProgramsChanged(System::Int32 channel)
{
	return (CallDispatch(effHasMidiProgramsChanged, channel, 0, 0, 0) != 0);
}

System::Boolean VstPluginCommandStub::GetMidiKeyName(Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName, System::Int32 channel)
{
	UnmanagedPointer<::MidiKeyName> pKeyName;

	pKeyName->thisProgramIndex = midiKeyName->CurrentProgramIndex;
	pKeyName->thisKeyNumber = midiKeyName->CurrentKeyNumber;

	if(CallDispatch(effGetMidiProgramCategory, channel, 0, pKeyName, 0) != 0)
	{
		midiKeyName->Name = TypeConverter::CharToString(pKeyName->keyName);
		return true;
	}

	return false;
}

System::Boolean VstPluginCommandStub::BeginSetProgram()
{
	return (CallDispatch(effBeginSetProgram, 0, 0, 0, 0) != 0);
}

System::Boolean VstPluginCommandStub::EndSetProgram()
{
	return (CallDispatch(effEndSetProgram, 0, 0, 0, 0) != 0);
}

// IVstPluginCommands23
System::Boolean VstPluginCommandStub::GetSpeakerArrangement([System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% input, [System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% output)
{
	UnmanagedPointer<::VstSpeakerArrangement> pInput;
	UnmanagedPointer<::VstSpeakerArrangement> pOutput;

	if(CallDispatch(effGetSpeakerArrangement, 0, (VstIntPtr)(::VstSpeakerArrangement*)pInput, pOutput, 0))
	{
		input = TypeConverter::ToManagedSpeakerArrangement(pInput);
		output = TypeConverter::ToManagedSpeakerArrangement(pOutput);
		return true;
	}

	input = nullptr;
	output = nullptr;
	return false;
}

// Offline processing not implemented
//System::Int32 SetTotalSamplesToProcess(System::Int32 numberOfSamples);
// Plugin Host/Shell not implemented
//System::Int32 GetNextPlugin([System::Runtime::InteropServices::Out] System::String^% name);

System::Int32 VstPluginCommandStub::StartProcess()
{
	return safe_cast<System::Int32>(CallDispatch(effStartProcess, 0, 0, 0, 0));
}

System::Int32 VstPluginCommandStub::StopProcess()
{
	return safe_cast<System::Int32>(CallDispatch(effStopProcess, 0, 0, 0, 0));
}

System::Boolean VstPluginCommandStub::SetPanLaw(Jacobi::Vst::Core::VstPanLaw type, System::Single gain)
{
	return (CallDispatch(effSetPanLaw, 0, safe_cast<VstIntPtr>(type), 0, gain) != 0);
}

Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::BeginLoadBank(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
{
	UnmanagedPointer<::VstPatchChunkInfo> pChunkInfo;

	VstInt32 result = CallDispatch(effBeginLoadBank, 0, 0, pChunkInfo, 0);

	if(result != 0)
	{
		TypeConverter::ToUnmanagedPatchChunkInfo(pChunkInfo, chunkInfo);
	}

	return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(result);
}

Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::BeginLoadProgram(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
{
	UnmanagedPointer<::VstPatchChunkInfo> pChunkInfo;

	VstInt32 result = CallDispatch(effBeginLoadProgram, 0, 0, pChunkInfo, 0);

	if(result != 0)
	{
		TypeConverter::ToUnmanagedPatchChunkInfo(pChunkInfo, chunkInfo);
	}

	return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(result);
}

// IVstPluginCommands24
System::Boolean VstPluginCommandStub::SetProcessPrecision(Jacobi::Vst::Core::VstProcessPrecision precision)
{
	return (CallDispatch(effSetProcessPrecision, 0, safe_cast<VstIntPtr>(precision), 0, 0) != 0);
}

System::Int32 VstPluginCommandStub::GetNumberOfMidiInputChannels()
{
	return safe_cast<System::Int32>(CallDispatch(effGetNumMidiInputChannels, 0, 0, 0, 0));
}

System::Int32 VstPluginCommandStub::GetNumberOfMidiOutputChannels()
{
	return safe_cast<System::Int32>(CallDispatch(effGetNumMidiOutputChannels, 0, 0, 0, 0));
}

// validates if the correct type was passed in as an argument
template<typename T> void VstPluginCommandStub::ThrowIfArgumentNotOfType(System::Object^ argument)
{
	if(argument != nullptr)
	{
		System::Type^ actualType = argument->GetType();
		System::Type^ expectedType = T::typeid;

		if(!expectedType->IsAssignableFrom(actualType))
		{
			throw gcnew ArgumentException(
				System::String::Format("The actual argument type is '{0}' while '{1}' was expected.", actualType->FullName, expectedType->FullName));
		}
	}
}
