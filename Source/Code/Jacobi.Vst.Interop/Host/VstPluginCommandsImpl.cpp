#include "pch.h"
#include "UnmanagedArray.h"
#include "VstPluginCommandStub.h"
#include "..\TypeConverter.h"
#include "..\UnmanagedString.h"
#include "..\UnmanagedPointer.h"
#include "..\Properties\Resources.h"

namespace Jacobi {
namespace Vst {
namespace Host {
namespace Interop {

	VstPluginCommandsImpl::VstPluginCommandsImpl(::Vst2Plugin* plugin)
	{
		_pPlugin = plugin;
		_emptyAudio32 = new float* [0];
		_emptyAudio64 = new double* [0];

		_memoryTracker = gcnew Jacobi::Vst::Interop::MemoryTracker();
	}

	void VstPluginCommandsImpl::ClearCurrentEvents()
	{
		if (_currentEvents != NULL)
		{
			TypeConverter::DeleteUnmanagedEvents(_currentEvents);
			_currentEvents = NULL;
		}
	}

	// IVstPluginCommandsBase
	void VstPluginCommandsImpl::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs)
	{
		float** ppInputs = inputs->Length == 0 ? _emptyAudio32 : _audioInputs.GetArray(inputs->Length);
		float** ppOutputs = outputs->Length == 0 ? _emptyAudio32 : _audioOutputs.GetArray(outputs->Length);

		int32_t inputSampleCount = CopyBufferPointers(ppInputs, inputs);
		int32_t outputSampleCount = CopyBufferPointers(ppOutputs, outputs);

		CallProcess32(ppInputs, ppOutputs, max(inputSampleCount, outputSampleCount));
	}

	void VstPluginCommandsImpl::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputs)
	{
		double** ppInputs = inputs->Length == 0 ? _emptyAudio64 : _precisionInputs.GetArray(inputs->Length);
		double** ppOutputs = outputs->Length == 0 ? _emptyAudio64 : _precisionOutputs.GetArray(outputs->Length);

		int32_t inputSampleCount = CopyBufferPointers(ppInputs, inputs);
		int32_t outputSampleCount = CopyBufferPointers(ppOutputs, outputs);

		CallProcess64(ppInputs, ppOutputs, max(inputSampleCount, outputSampleCount));
	}

	void VstPluginCommandsImpl::SetParameter(System::Int32 index, System::Single value)
	{
		CallSetParameter(index, value);
	}

	System::Single VstPluginCommandsImpl::GetParameter(System::Int32 index)
	{
		return CallGetParameter(index);
	}

	// IVstPluginCommands10
	void VstPluginCommandsImpl::Open()
	{
		CallDispatch(Vst2PluginCommands::Open, 0, 0, 0, 0);
	}

	void VstPluginCommandsImpl::Close()
	{
		CallDispatch(Vst2PluginCommands::Close, 0, 0, 0, 0);

		_memoryTracker->ClearAll();
		ClearCurrentEvents();
	}

	void VstPluginCommandsImpl::SetProgram(System::Int32 programNumber)
	{
		CallDispatch(Vst2PluginCommands::ProgramSet, 0, programNumber, 0, 0);
	}

	System::Int32 VstPluginCommandsImpl::GetProgram()
	{
		return (int32_t)CallDispatch(Vst2PluginCommands::ProgramGet, 0, 0, 0, 0);
	}

	void VstPluginCommandsImpl::SetProgramName(System::String^ name)
	{
		char* progName = TypeConverter::AllocateString(name);

		try
		{
			CallDispatch(Vst2PluginCommands::ProgramSetName, 0, 0, progName, 0);
		}
		finally
		{
			TypeConverter::DeallocateString(progName);
		}
	}

	System::String^ VstPluginCommandsImpl::GetProgramName()
	{
		//UnmanagedString progName(Vst2MaxProgNameLen);
		UnmanagedString progName(129);

		CallDispatch(Vst2PluginCommands::ProgramGetName, 0, 0, progName, 0);

		return TypeConverter::CharToString(progName);
	}

	System::String^ VstPluginCommandsImpl::GetParameterLabel(System::Int32 index)
	{
		//UnmanagedString paramLabel(Vst2MaxParamStrLen);
		// Some plugin don't have 8 character param labels
		UnmanagedString paramLabel(65);

		CallDispatch(Vst2PluginCommands::ParameterGetLabel, index, 0, paramLabel, 0);

		return TypeConverter::CharToString(paramLabel);
	}

	System::String^ VstPluginCommandsImpl::GetParameterDisplay(System::Int32 index)
	{
		//UnmanagedString paramLabel(Vst2MaxParamStrLen);
		// Some plugin don't have 8 character param display values
		UnmanagedString paramLabel(65);

		CallDispatch(Vst2PluginCommands::ParameterGetDisplay, index, 0, paramLabel, 0);

		return TypeConverter::CharToString(paramLabel);
	}

	System::String^ VstPluginCommandsImpl::GetParameterName(System::Int32 index)
	{
		//UnmanagedString paramName(Vst2MaxParamStrLen);
		// Some plugin don't have 8 character param names
		UnmanagedString paramName(65);

		CallDispatch(Vst2PluginCommands::ParameterGetName, index, 0, paramName, 0);

		return TypeConverter::CharToString(paramName);
	}

	void VstPluginCommandsImpl::SetSampleRate(System::Single sampleRate)
	{
		CallDispatch(Vst2PluginCommands::SampleRateSet, 0, 0, 0, sampleRate);
	}

	void VstPluginCommandsImpl::SetBlockSize(System::Int32 blockSize)
	{
		CallDispatch(Vst2PluginCommands::BlockSizeSet, 0, blockSize, 0, 0);
	}

	void VstPluginCommandsImpl::MainsChanged(System::Boolean onoff)
	{
		CallDispatch(Vst2PluginCommands::OnOff, 0, onoff ? 1 : 0, 0, 0);
	}

	System::Boolean VstPluginCommandsImpl::EditorGetRect([System::Runtime::InteropServices::Out] System::Drawing::Rectangle% rect)
	{
		UnmanagedPointer<Vst2Rectangle> unmanagedRect(NULL);

		// some plugins return zero even when succesful.
		CallDispatch(Vst2PluginCommands::EditorGetRectangle, 0, 0, &unmanagedRect, 0);

		if (unmanagedRect->bottom != 0 || unmanagedRect->right != 0)
		{
			rect = TypeConverter::ToManagedRectangle(unmanagedRect);
			return true;
		}

		// dummy rect
		rect = System::Drawing::Rectangle();
		return false;
	}

	System::Boolean VstPluginCommandsImpl::EditorOpen(System::IntPtr hWnd)
	{
		return (CallDispatch(Vst2PluginCommands::EditorOpen, 0, 0, hWnd.ToPointer(), 0) != 0);
	}

	void VstPluginCommandsImpl::EditorClose()
	{
		CallDispatch(Vst2PluginCommands::EditorClose, 0, 0, 0, 0);
	}

	void VstPluginCommandsImpl::EditorIdle()
	{
		CallDispatch(Vst2PluginCommands::EditorIdle, 0, 0, 0, 0);
	}

	array<System::Byte>^ VstPluginCommandsImpl::GetChunk(System::Boolean isPreset)
	{
		// we don't own the memory passed to us
		char* pBuffer = NULL;

		int32_t length = (int32_t)CallDispatch(Vst2PluginCommands::ChunkGet, isPreset ? 1 : 0, 0, &pBuffer, 0);

		if (length > 0)
		{
			return TypeConverter::PtrToByteArray(pBuffer, length);
		}

		return nullptr;
	}

	System::Int32 VstPluginCommandsImpl::SetChunk(array<System::Byte>^ data, System::Boolean isPreset)
	{
		char* dataArr = TypeConverter::ByteArrayToPtr(data);

		// we need to hold on to the unmanaged memory until suspend/resume is called.
		_memoryTracker->RegisterArray(dataArr);

		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::ChunkSet, isPreset ? 1 : 0, data->Length, dataArr, 0));
	}

	// IVstPluginCommands20
	System::Boolean VstPluginCommandsImpl::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
	{
		ClearCurrentEvents();

		_currentEvents = TypeConverter::AllocUnmanagedEvents(events);

		return (CallDispatch(Vst2PluginCommands::ProcessEvents, 0, 0, _currentEvents, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::CanParameterBeAutomated(System::Int32 index)
	{
		return (CallDispatch(Vst2PluginCommands::ParameterCanBeAutomated, index, 0, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::String2Parameter(System::Int32 index, System::String^ str)
	{
		char* pStr = TypeConverter::AllocateString(str);

		try
		{
			return (CallDispatch(Vst2PluginCommands::ParameterFromString, index, 0, pStr, 0) != 0);
		}
		finally
		{
			TypeConverter::DeallocateString(pStr);
		}
	}

	System::String^ VstPluginCommandsImpl::GetProgramNameIndexed(System::Int32 index)
	{
		//UnmanagedString progName(Vst2MaxProgNameLen);
		UnmanagedString progName(129);

		CallDispatch(Vst2PluginCommands::ProgramGetNameByIndex, index, 0, progName, 0);

		return TypeConverter::CharToString(progName);
	}

	Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandsImpl::GetInputProperties(System::Int32 index)
	{
		UnmanagedPointer<Vst2PinProperties> pinProps;

		if (CallDispatch(Vst2PluginCommands::GetInputProperties, index, 0, pinProps, 0) != 0)
		{
			return TypeConverter::ToManagedPinProperties(pinProps);
		}

		return nullptr;
	}

	Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandsImpl::GetOutputProperties(System::Int32 index)
	{
		UnmanagedPointer<Vst2PinProperties> pinProps;

		if (CallDispatch(Vst2PluginCommands::GetOutputProperties, index, 0, pinProps, 0) != 0)
		{
			return TypeConverter::ToManagedPinProperties(pinProps);
		}

		return nullptr;
	}

	Jacobi::Vst::Core::VstPluginCategory VstPluginCommandsImpl::GetCategory()
	{
		return safe_cast<Jacobi::Vst::Core::VstPluginCategory>(CallDispatch(Vst2PluginCommands::PluginGetCategory, 0, 0, 0, 0));
	}

	System::Boolean VstPluginCommandsImpl::SetSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ saInput,
		Jacobi::Vst::Core::VstSpeakerArrangement^ saOutput)
	{
		UnmanagedPointer<::Vst2SpeakerArrangement> pInput(TypeConverter::AllocUnmanagedSpeakerArrangement(saInput));
		UnmanagedPointer<::Vst2SpeakerArrangement> pOutput(TypeConverter::AllocUnmanagedSpeakerArrangement(saOutput));

		return (CallDispatch(Vst2PluginCommands::SetSpeakerArrangement, 0, (Vst2IntPtr)(::Vst2SpeakerArrangement*)pInput, pOutput, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::SetBypass(System::Boolean bypass)
	{
		return (CallDispatch(Vst2PluginCommands::SetBypass, 0, bypass ? 1 : 0, 0, 0) != 0);
	}

	System::String^ VstPluginCommandsImpl::GetEffectName()
	{
		//UnmanagedString name(Vst2MaxEffectNameLen);
		UnmanagedString name(128);

		CallDispatch(Vst2PluginCommands::PluginGetName, 0, 0, name, 0);

		return TypeConverter::CharToString(name);
	}

	System::String^ VstPluginCommandsImpl::GetVendorString()
	{
		//UnmanagedString vendor(Vst2MaxVendorStrLen);
		UnmanagedString vendor(129);

		CallDispatch(Vst2PluginCommands::VendorGetString, 0, 0, vendor, 0);

		return TypeConverter::CharToString(vendor);
	}

	System::String^ VstPluginCommandsImpl::GetProductString()
	{
		//UnmanagedString product(Vst2MaxProductStrLen);
		UnmanagedString product(129);

		if (CallDispatch(Vst2PluginCommands::ProductGetString, 0, 0, product, 0) != 0)
		{
			return TypeConverter::CharToString(product);
		}

		return nullptr;
	}

	System::Int32 VstPluginCommandsImpl::GetVendorVersion()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::VendorGetVersion, 0, 0, 0, 0));
	}

	Jacobi::Vst::Core::VstCanDoResult VstPluginCommandsImpl::CanDo(System::String^ cando)
	{
		char* pCanDo = TypeConverter::AllocateString(cando);

		try
		{
			return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(CallDispatch(Vst2PluginCommands::CanDo, 0, 0, pCanDo, 0));
		}
		finally
		{
			TypeConverter::DeallocateString(pCanDo);
		}
	}

	System::Int32 VstPluginCommandsImpl::GetTailSize()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::GetTailSizeInSamples, 0, 0, 0, 0));
	}

	Jacobi::Vst::Core::VstParameterProperties^ VstPluginCommandsImpl::GetParameterProperties(System::Int32 index)
	{
		UnmanagedPointer<::Vst2ParameterProperties> pProps;

		if (CallDispatch(Vst2PluginCommands::ParameterGetProperties, index, 0, pProps, 0))
		{
			return TypeConverter::ToManagedParameterProperties(pProps);
		}

		return nullptr;
	}

	System::Int32 VstPluginCommandsImpl::GetVstVersion()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::GetVstVersion, 0, 0, 0, 0));
	}

	// IVstPluginCommands21
	System::Boolean VstPluginCommandsImpl::EditorKeyDown(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
	{
		return (CallDispatch(Vst2PluginCommands::EditorKeyDown, ascii, safe_cast<Vst2IntPtr>(virtualKey), 0, safe_cast<float>(modifers)) != 0);
	}

	System::Boolean VstPluginCommandsImpl::EditorKeyUp(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
	{
		return (CallDispatch(Vst2PluginCommands::EditorKeyUp, ascii, safe_cast<Vst2IntPtr>(virtualKey), 0, safe_cast<float>(modifers)) != 0);
	}

	System::Boolean VstPluginCommandsImpl::SetEditorKnobMode(Jacobi::Vst::Core::VstKnobMode mode)
	{
		return (CallDispatch(Vst2PluginCommands::SetKnobMode, 0, safe_cast<Vst2IntPtr>(mode), 0, 0) != 0);
	}

	System::Int32 VstPluginCommandsImpl::GetMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
	{
		UnmanagedPointer<::Vst2MidiProgramName> pProgName;

		pProgName->thisProgramIndex = midiProgram->CurrentProgramIndex;

		System::Int32 result = safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::MidiProgramGetName, channel, 0, pProgName, 0));

		if (result != 0)
		{
			TypeConverter::ToManagedMidiProgramName(midiProgram, pProgName);
		}

		return result;
	}

	System::Int32 VstPluginCommandsImpl::GetCurrentMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
	{
		UnmanagedPointer<::Vst2MidiProgramName> pProgName;

		System::Int32 result = safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::MidiProgramGetCurrent, channel, 0, pProgName, 0));

		if (result != 0)
		{
			TypeConverter::ToManagedMidiProgramName(midiProgram, pProgName);
		}

		return result;
	}

	System::Int32 VstPluginCommandsImpl::GetMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiCat, System::Int32 channel)
	{
		UnmanagedPointer<::Vst2MidiProgramCategory> pProgCat;

		System::Int32 result = safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::MidiProgramGetCategory, channel, 0, pProgCat, 0));

		if (result != 0)
		{
			TypeConverter::ToManagedMidiProgramCategory(midiCat, pProgCat);
		}

		return result;
	}

	System::Boolean VstPluginCommandsImpl::HasMidiProgramsChanged(System::Int32 channel)
	{
		return (CallDispatch(Vst2PluginCommands::MidiProgramsChanged, channel, 0, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::GetMidiKeyName(Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName, System::Int32 channel)
	{
		UnmanagedPointer<::Vst2MidiKeyName> pKeyName;

		pKeyName->thisProgramIndex = midiKeyName->CurrentProgramIndex;
		pKeyName->thisKeyNumber = midiKeyName->CurrentKeyNumber;

		if (CallDispatch(Vst2PluginCommands::MidiProgramGetCategory, channel, 0, pKeyName, 0) != 0)
		{
			midiKeyName->Name = TypeConverter::CharToString(pKeyName->keyName);
			return true;
		}

		return false;
	}

	System::Boolean VstPluginCommandsImpl::BeginSetProgram()
	{
		return (CallDispatch(Vst2PluginCommands::BeginSetProgram, 0, 0, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::EndSetProgram()
	{
		return (CallDispatch(Vst2PluginCommands::EndSetProgram, 0, 0, 0, 0) != 0);
	}

	// IVstPluginCommands23
	System::Boolean VstPluginCommandsImpl::GetSpeakerArrangement([System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% input, [System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% output)
	{
		UnmanagedPointer<::Vst2SpeakerArrangement> pInput;
		UnmanagedPointer<::Vst2SpeakerArrangement> pOutput;

		if (CallDispatch(Vst2PluginCommands::GetSpeakerArrangement, 0, (Vst2IntPtr)(::Vst2SpeakerArrangement*)pInput, pOutput, 0))
		{
			input = TypeConverter::ToManagedSpeakerArrangement(pInput);
			output = TypeConverter::ToManagedSpeakerArrangement(pOutput);
			return true;
		}

		input = nullptr;
		output = nullptr;
		return false;
	}

	System::Int32 VstPluginCommandsImpl::GetNextPlugin([System::Runtime::InteropServices::Out] System::String^% name)
	{
		//UnmanagedString pName(Vst2MaxProductStrLen);
		UnmanagedString pName(129);

		System::Int32 pluginId = safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::GetNextPlugin, 0, 0, &pName, 0.0));

		name = TypeConverter::CharToString(pName);

		return pluginId;
	}

	System::Int32 VstPluginCommandsImpl::StartProcess()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::ProcessStart, 0, 0, 0, 0));
	}

	System::Int32 VstPluginCommandsImpl::StopProcess()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::ProcessStop, 0, 0, 0, 0));
	}

	System::Boolean VstPluginCommandsImpl::SetPanLaw(Jacobi::Vst::Core::VstPanLaw type, System::Single gain)
	{
		return (CallDispatch(Vst2PluginCommands::SetPanLaw, 0, safe_cast<Vst2IntPtr>(type), 0, gain) != 0);
	}

	Jacobi::Vst::Core::VstCanDoResult VstPluginCommandsImpl::BeginLoadBank(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		UnmanagedPointer<::Vst2PatchChunkInfo> pChunkInfo;

		int32_t result = (::int32_t)CallDispatch(Vst2PluginCommands::BeginLoadBank, 0, 0, pChunkInfo, 0);

		if (result != 0)
		{
			TypeConverter::ToUnmanagedPatchChunkInfo(pChunkInfo, chunkInfo);
		}

		return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(result);
	}

	Jacobi::Vst::Core::VstCanDoResult VstPluginCommandsImpl::BeginLoadProgram(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		UnmanagedPointer<::Vst2PatchChunkInfo> pChunkInfo;

		int32_t result = (::int32_t)CallDispatch(Vst2PluginCommands::BeginLoadProgram, 0, 0, pChunkInfo, 0);

		if (result != 0)
		{
			TypeConverter::ToUnmanagedPatchChunkInfo(pChunkInfo, chunkInfo);
		}

		return safe_cast<Jacobi::Vst::Core::VstCanDoResult>(result);
	}

	// IVstPluginCommands24
	System::Boolean VstPluginCommandsImpl::SetProcessPrecision(Jacobi::Vst::Core::VstProcessPrecision precision)
	{
		return (CallDispatch(Vst2PluginCommands::SetProcessPrecision, 0, safe_cast<Vst2IntPtr>(precision), 0, 0) != 0);
	}

	System::Int32 VstPluginCommandsImpl::GetNumberOfMidiInputChannels()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::MidiGetInputChannelCount, 0, 0, 0, 0));
	}

	System::Int32 VstPluginCommandsImpl::GetNumberOfMidiOutputChannels()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::MidiGetOutputChannelCount, 0, 0, 0, 0));
	}

	//
	// Legacy support
	//

	// IVstPluginCommandsLegacyBase
	void VstPluginCommandsImpl::ProcessAcc(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs)
	{
		float** ppInputs = inputs->Length == 0 ? _emptyAudio32 : _audioInputs.GetArray(inputs->Length);
		float** ppOutputs = outputs->Length == 0 ? _emptyAudio32 : _audioOutputs.GetArray(outputs->Length);

		int32_t inputSampleCount = CopyBufferPointers(ppInputs, inputs);
		int32_t outputSampleCount = CopyBufferPointers(ppOutputs, outputs);

		CallProcess32Acc(ppInputs, ppOutputs, inputSampleCount);
	}

	// IVstPluginCommandsLegacy10
	System::Single VstPluginCommandsImpl::GetVu()
	{
		return safe_cast<System::Single>(CallDispatch(Vst2PluginCommands::VuGet, 0, 0, 0, 0));
	}

	System::Boolean VstPluginCommandsImpl::EditorKey(System::Int32 keycode)
	{
		return (CallDispatch(Vst2PluginCommands::EditorKey, 0, keycode, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::EditorTop()
	{
		return (CallDispatch(Vst2PluginCommands::EditorTop, 0, 0, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::EditorSleep()
	{
		return (CallDispatch(Vst2PluginCommands::EditorSleep, 0, 0, 0, 0) != 0);
	}

	System::Int32 VstPluginCommandsImpl::Identify()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::Identify, 0, 0, 0, 0));
	}

	// IVstPluginCommandsLegacy20
	System::Int32 VstPluginCommandsImpl::GetProgramCategoriesCount()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::ProgramGetCategoriesCount, 0, 0, 0, 0));
	}

	System::Boolean VstPluginCommandsImpl::CopyCurrentProgramTo(System::Int32 programIndex)
	{
		return (CallDispatch(Vst2PluginCommands::ProgramCopy, programIndex, 0, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::ConnectInput(System::Int32 inputIndex, System::Boolean connected)
	{
		return (CallDispatch(Vst2PluginCommands::ConnectInput, inputIndex, connected, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::ConnectOutput(System::Int32 outputIndex, System::Boolean connected)
	{
		return (CallDispatch(Vst2PluginCommands::ConnectOutput, outputIndex, connected, 0, 0) != 0);
	}

	System::Int32 VstPluginCommandsImpl::GetCurrentPosition()
	{
		return safe_cast<System::Int32>(CallDispatch(Vst2PluginCommands::GetCurrentPosition, 0, 0, 0, 0));
	}

	Jacobi::Vst::Core::VstAudioBuffer^ VstPluginCommandsImpl::GetDestinationBuffer()
	{
		int length = 0; // TODO: retrieve block size?

		float* pBuffer = (float*)CallDispatch(Vst2PluginCommands::GetDestinationBuffer, 0, 0, 0, 0);

		return gcnew Jacobi::Vst::Core::VstAudioBuffer(pBuffer, length, true);
	}

	System::Boolean VstPluginCommandsImpl::SetBlockSizeAndSampleRate(System::Int32 blockSize, System::Single sampleRate)
	{
		return (CallDispatch(Vst2PluginCommands::SetBlockSizeAndSampleRate, 0, blockSize, 0, sampleRate) != 0);
	}

	System::String^ VstPluginCommandsImpl::GetErrorText()
	{
		UnmanagedString pText(257);

		if (CallDispatch(Vst2PluginCommands::GetErrorText, 0, 0, 0, 0) != 0)
		{
			return TypeConverter::CharToString(pText);
		}

		return nullptr;
	}

	System::Boolean VstPluginCommandsImpl::Idle()
	{
		return (CallDispatch(Vst2PluginCommands::Idle, 0, 0, 0, 0) != 0);
	}

	System::IntPtr VstPluginCommandsImpl::GetIcon()
	{
		// TODO: implement
		return System::IntPtr::Zero;
	}

	System::Boolean VstPluginCommandsImpl::SetViewPosition(System::Drawing::Point% position)
	{
		return (CallDispatch(Vst2PluginCommands::SetViewPosition, position.X, position.Y, 0, 0) != 0);
	}

	System::Boolean VstPluginCommandsImpl::KeysRequired()
	{
		// NOTE: 0=Require keys, 1=dont need.
		return (CallDispatch(Vst2PluginCommands::KeysRequired, 0, 0, 0, 0) == 0);
	}

}}}} // Jacobi::Vst::Host::Interop
