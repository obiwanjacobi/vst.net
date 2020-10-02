#include "pch.h"
#include "PluginCommandProxy.h"
#include "..\TypeConverter.h"
#include "..\Utils.h"
#include<vcclr.h>

namespace Jacobi {
namespace Vst {
namespace Plugin {
namespace Interop {

// constructs a new instance based on a reference to the plugin command stub
PluginCommandProxy::PluginCommandProxy(Jacobi::Vst::Core::Plugin::IVstPluginCommandStub^ cmdStub)
{
	if(cmdStub == nullptr)
	{
		throw gcnew System::ArgumentNullException("cmdStub");
	}

	_commandStub = cmdStub;
	_commands = cmdStub->Commands;
	_legacyCommands = dynamic_cast<Jacobi::Vst::Core::Legacy::IVstPluginCommandsLegacy20^>(cmdStub);

	_memTracker = gcnew Jacobi::Vst::Interop::MemoryTracker();
	_pEditorRect = new Vst2Rectangle();
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
Vst2IntPtr PluginCommandProxy::Dispatch(int32_t opcode, int32_t index, ::Vst2IntPtr value, void* ptr, float opt)
{
	if (_commands == nullptr)
	{
		return 0;
	}

	::Vst2IntPtr result = 0;

	try
	{
		auto command = safe_cast<Vst2PluginCommands>(opcode);

		switch(command)
		{
		case Vst2PluginCommands::Open:
			_commands->Open();
			break;
		case Vst2PluginCommands::Close:
			_commands->Close();
			// call Dispose() on this instance
			delete this;
			break;
		case Vst2PluginCommands::ProgramSet:
			_commands->SetProgram(safe_cast<System::Int32>(value));
			result = 1;
			break;
		case Vst2PluginCommands::ProgramGet:
			result = _commands->GetProgram();
			break;
		case Vst2PluginCommands::ProgramSetName:
			_commands->SetProgramName(TypeConverter::CharToString((char*)ptr));
			result = 1;
			break;
		case Vst2PluginCommands::ProgramGetName:
			TypeConverter::StringToChar(_commands->GetProgramName(), (char*)ptr, Vst2MaxProgNameLen);
			result = 1;
			break;
		case Vst2PluginCommands::ParameterGetLabel:
			TypeConverter::StringToChar(_commands->GetParameterLabel(index), (char*)ptr, Vst2MaxParamStrLen);
			result = 1;
			break;
		case Vst2PluginCommands::ParameterGetDisplay:
			TypeConverter::StringToChar(_commands->GetParameterDisplay(index), (char*)ptr, Vst2MaxParamStrLen);
			result = 1;
			break;
		case Vst2PluginCommands::ParameterGetName:
			TypeConverter::StringToChar(_commands->GetParameterName(index), (char*)ptr, Vst2MaxParamStrLen);
			result = 1;
			break;
		case Vst2PluginCommands::SampleRateSet:
			_commands->SetSampleRate(opt);
			result = 1;
			break;
		case Vst2PluginCommands::BlockSizeSet:
			_commands->SetBlockSize(safe_cast<System::Int32>(value));
			result = 1;
			break;
		case Vst2PluginCommands::OnOff:
			_memTracker->ClearAll(); // safe to delete allocated memory during suspend/resume
			_commands->MainsChanged(value != 0);
			result = 1;
			break;
		case Vst2PluginCommands::EditorGetRectangle:
		{
			System::Drawing::Rectangle rect;
			if (_commands->EditorGetRect(rect))
			{
				TypeConverter::ToUnmanagedRectangle(_pEditorRect, rect);
				*((Vst2Rectangle**)ptr) = _pEditorRect;
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::EditorOpen:
			result = _commands->EditorOpen(System::IntPtr(ptr));
			break;
		case Vst2PluginCommands::EditorClose:
			_commands->EditorClose();
			result = 1;
			break;
		case Vst2PluginCommands::EditorIdle:
			_commands->EditorIdle();
			result = 1;
			break;
		case Vst2PluginCommands::ChunkGet:
		{
			array<System::Byte>^ buffer = _commands->GetChunk(index != 0);
			if(buffer != nullptr)
			{
				*(void**)ptr = TypeConverter::ByteArrayToPtr(buffer);
				
				_memTracker->RegisterArray(*(void**)ptr);

				result = buffer->Length;
			}
		}	break;
		case Vst2PluginCommands::ChunkSet:
		{
			auto buffer = TypeConverter::PtrToByteArray((char*)ptr, safe_cast<System::Int32>(value));
			result = _commands->SetChunk(buffer, index != 0) ? 1 : 0;
		}	break;
		case Vst2PluginCommands::ProcessEvents:
			result = _commands->ProcessEvents(TypeConverter::ToManagedEventArray((Vst2Events*)ptr)) ? 1 : 0;
			break;
		case Vst2PluginCommands::ParameterCanBeAutomated:
			result = _commands->CanParameterBeAutomated(index) ? 1 : 0;
			break;
		case Vst2PluginCommands::ParameterFromString:
			result = _commands->String2Parameter(index, TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
			break;
		case Vst2PluginCommands::ProgramGetNameByIndex:
		{
			auto name = _commands->GetProgramNameIndexed(index);
			if(name != nullptr)
			{
				TypeConverter::StringToChar(name, (char*)ptr, Vst2MaxProgNameLen);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::GetInputProperties:
		{
			auto pinProps = _commands->GetInputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::ToUnmanagedPinProperties((::Vst2PinProperties*)ptr, pinProps);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::GetOutputProperties:
		{
			auto pinProps = _commands->GetOutputProperties(index);
			if(pinProps != nullptr)
			{
				TypeConverter::ToUnmanagedPinProperties((::Vst2PinProperties*)ptr, pinProps);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::PluginGetCategory:
			result = safe_cast<int32_t>(_commands->GetCategory());
			break;
		case Vst2PluginCommands::SetSpeakerArrangement:
			result = _commands->SetSpeakerArrangement(TypeConverter::ToManagedSpeakerArrangement((::Vst2SpeakerArrangement*)value),
				TypeConverter::ToManagedSpeakerArrangement((::Vst2SpeakerArrangement*)ptr));
			break;
		case Vst2PluginCommands::SetBypass:
			result = _commands->SetBypass(value != 0) ? 1 : 0;
			break;
		case Vst2PluginCommands::PluginGetName:
		{
			auto name = _commands->GetEffectName();
			if(name != nullptr)
			{
				TypeConverter::StringToChar(name, (char*)ptr, Vst2MaxEffectNameLen);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::VendorGetString:
		{
			auto str = _commands->GetVendorString();
			if(str != nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxVendorStrLen);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::ProductGetString:
		{
			auto str = _commands->GetProductString();
			if(str != nullptr)
			{
				TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxProductStrLen);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::VendorGetVersion:
			result = _commands->GetVendorVersion();
			break;
		case Vst2PluginCommands::CanDo:
			result = safe_cast<int32_t>(_commands->CanDo(TypeConverter::CharToString((char*)ptr)));
			break;
		case Vst2PluginCommands::GetTailSizeInSamples:
			result = _commands->GetTailSize();
			break;
		case Vst2PluginCommands::ParameterGetProperties:
		{
			auto paramProps = _commands->GetParameterProperties(index);
			if(paramProps != nullptr)
			{
				TypeConverter::ToUnmanagedParameterProperties((::Vst2ParameterProperties*)ptr, paramProps);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::GetVstVersion:
			result = _commands->GetVstVersion();
			break;
		case Vst2PluginCommands::EditorKeyDown:
			result = _commands->EditorKeyDown(safe_cast<System::Byte>(index), 
				safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
				safe_cast<Jacobi::Vst::Core::VstModifierKeys>((int32_t)opt)) ? 1 : 0;
			break;
		case Vst2PluginCommands::EditorKeyUp:
			result = _commands->EditorKeyUp(safe_cast<System::Byte>(index), 
				safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
				safe_cast<Jacobi::Vst::Core::VstModifierKeys>((int32_t)opt)) ? 1 : 0;
			break;
		case Vst2PluginCommands::SetKnobMode:
			result = _commands->SetEditorKnobMode(safe_cast<Jacobi::Vst::Core::VstKnobMode>(value)) ? 1 : 0;
			break;
		case Vst2PluginCommands::MidiProgramGetName:
		{
			auto pProgName = (::Vst2MidiProgramName*)ptr;
			auto progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
			progName->CurrentProgramIndex = pProgName->thisProgramIndex;
			result = _commands->GetMidiProgramName(progName, index);
			TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
		}	break;
		case Vst2PluginCommands::MidiProgramGetCurrent:
		{
			auto pProgName = (::Vst2MidiProgramName*)ptr;
			auto progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
			result = _commands->GetCurrentMidiProgramName(progName, index);
			TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
		}	break;
		case Vst2PluginCommands::MidiProgramGetCategory:
		{
			auto pProgCat = (::Vst2MidiProgramCategory*)ptr;
			auto progCat = gcnew Jacobi::Vst::Core::VstMidiProgramCategory();
			progCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
			result = _commands->GetMidiProgramCategory(progCat, index);
			TypeConverter::ToUnmanagedMidiProgramCategory(pProgCat, progCat);
		}	break;
		case Vst2PluginCommands::MidiProgramsChanged:
			result = _commands->HasMidiProgramsChanged(index) ? 1 : 0;
			break;
		case Vst2PluginCommands::MidiKeyGetName:
		{
			auto pKeyName = (::Vst2MidiKeyName*)ptr;
			auto midiKeyName = gcnew Jacobi::Vst::Core::VstMidiKeyName();
			midiKeyName->CurrentProgramIndex = pKeyName->thisProgramIndex;
			midiKeyName->CurrentKeyNumber = pKeyName->thisKeyNumber;
			result = _commands->GetMidiKeyName(midiKeyName, index);
			TypeConverter::StringToChar(midiKeyName->Name, pKeyName->keyName, Vst2MaxNameLen);
		}	break;
		case Vst2PluginCommands::BeginSetProgram:
			result = _commands->BeginSetProgram() ? 1 : 0;
			break;
		case Vst2PluginCommands::EndSetProgram:
			result = _commands->EndSetProgram() ? 1 : 0;
			break;
		case Vst2PluginCommands::GetSpeakerArrangement:
		{
			auto ppInput = (::Vst2SpeakerArrangement**)value;
			auto ppOutput = (::Vst2SpeakerArrangement**)ptr;
			*ppInput = NULL;
			*ppOutput = NULL;

			auto inputArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();
			auto outputArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();
			// let plugin fill the input and output speaker arrangements
			result = _commands->GetSpeakerArrangement(inputArr, outputArr) ? 1 : 0;
			if(result)
			{
				// NOTE: register retvals with the memory tracker to be deleted later.
				*ppInput = TypeConverter::AllocUnmanagedSpeakerArrangement(inputArr);
				_memTracker->RegisterObject(*ppInput);

				*ppOutput = TypeConverter::AllocUnmanagedSpeakerArrangement(outputArr);
				_memTracker->RegisterObject(*ppOutput);
			}
		}	break;
		//case Vst2PluginCommands::ShellGetNextPlugin:
			/*{
			System::String^ str;
			result = _commands->GetNextPlugin(str);
			TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxProductStrLen);
			}*/
			//break;
		case Vst2PluginCommands::ProcessStart:
			result = _commands->StartProcess();
			break;
		case Vst2PluginCommands::ProcessStop:
			result = _commands->StopProcess();
			break;
		//case Vst2PluginCommands::SetTotalSampleToProcess:
			//result = _commands->SetTotalSamplesToProcess(value);
			//break;
		case Vst2PluginCommands::SetPanLaw:
			result = _commands->SetPanLaw(safe_cast<Jacobi::Vst::Core::VstPanLaw>(value), opt) ? 1 : 0;
			break;
		case Vst2PluginCommands::BeginLoadBank:
			result = safe_cast<int32_t>(_commands->BeginLoadBank(TypeConverter::ToManagedPatchChunkInfo((::Vst2PatchChunkInfo*)ptr)));
			break;
		case Vst2PluginCommands::BeginLoadProgram:
			result = safe_cast<int32_t>(_commands->BeginLoadProgram(TypeConverter::ToManagedPatchChunkInfo((::Vst2PatchChunkInfo*)ptr)));
			break;
		case Vst2PluginCommands::SetProcessPrecision:
			result = _commands->SetProcessPrecision(safe_cast<Jacobi::Vst::Core::VstProcessPrecision>(value)) ? 1 : 0;
			break;
		case Vst2PluginCommands::MidiGetInputChannelCount:
			result = _commands->GetNumberOfMidiInputChannels();
			break;
		case Vst2PluginCommands::MidiGetOutputChannelCount:
			result = _commands->GetNumberOfMidiOutputChannels();
			break;
		default:
			result = DispatchLegacy(command, index, value, ptr, opt);
			break;
		}
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}

	return result;
}

// continuation of Dispatch()
// Dispatches an opcode to the plugin legacy command stub.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
Vst2IntPtr PluginCommandProxy::DispatchLegacy(Vst2PluginCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	Vst2IntPtr result = 0;

	if(_legacyCommands != nullptr)
	{
		switch(command)
		{
		// VST 1.0 legacy
		case Vst2PluginCommands::VuGet:
			result = safe_cast<Vst2IntPtr>(_legacyCommands->GetVu());
			break;
		//case Vst2PluginCommands::EditDraw:
		//	break;
		//case Vst2PluginCommands::EditMouse:
		//	break;
		case Vst2PluginCommands::EditorKey:
			result = _legacyCommands->EditorKey(safe_cast<System::Int32>(value)) ? 1 : 0;
			break;
		case Vst2PluginCommands::EditorTop:
			result = _legacyCommands->EditorTop() ? 1 : 0;
			break;
		case Vst2PluginCommands::EditorSleep:
			result = _legacyCommands->EditorSleep() ? 1 : 0;
			break;
		case Vst2PluginCommands::Identify:
			result = _legacyCommands->Identify();
			break;

		// VST 2.0 legacy
		case Vst2PluginCommands::ProgramGetCategoriesCount:
			result = _legacyCommands->GetProgramCategoriesCount();
			break;
		case Vst2PluginCommands::ProgramCopy:
			result = _legacyCommands->CopyCurrentProgramTo(index);
			break;
		case Vst2PluginCommands::ConnectInput:
			result = _legacyCommands->ConnectInput(index, value != 0) ? 1 : 0;
			break;
		case Vst2PluginCommands::ConnectOutput:
			result = _legacyCommands->ConnectOutput(index, value != 0) ? 1 : 0;
			break;
		case Vst2PluginCommands::GetCurrentPosition:
			result = _legacyCommands->GetCurrentPosition();
			break;
		case Vst2PluginCommands::GetDestinationBuffer:
		{
			auto audioBuffer = dynamic_cast<Jacobi::Vst::Core::IDirectBufferAccess32^>(_legacyCommands->GetDestinationBuffer());
			if(audioBuffer != nullptr)
			{
				result = (Vst2IntPtr)audioBuffer->Buffer;
			}
		}	break;
		case Vst2PluginCommands::SetBlockSizeAndSampleRate:
			result = _legacyCommands->SetBlockSizeAndSampleRate(safe_cast<System::Int32>(value), opt) ? 1 : 0;
			break;
		case Vst2PluginCommands::GetErrorText:
		{
			auto txt = _legacyCommands->GetErrorText();
			if(txt != nullptr)
			{
				TypeConverter::StringToChar(txt, (char*)ptr, 256);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::Idle:
			result = _legacyCommands->Idle() ? 1 : 0;
			break;
		case Vst2PluginCommands::GetIcon:
		{
			System::IntPtr^ icon = _legacyCommands->GetIcon();
			if(icon != nullptr)
			{
				// TODO:
				// void* in <ptr>, not yet defined
				//result = 1;
			}
		}	break;
		case Vst2PluginCommands::SetViewPosition:
			result = _legacyCommands->SetViewPosition(System::Drawing::Point(index, safe_cast<System::Int32>(value))) ? 1 : 0;
			break;
		case Vst2PluginCommands::KeysRequired:
			// NOTE: 0=Required, 1=dont need.
			result = _legacyCommands->KeysRequired() ? 0 : 1;
			break;
		default:
			// unknown command
			break;
		}
	}

	return result;
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
{
	try
	{
		auto inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		auto outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commands->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(double** inputs, double** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
{
	try
	{
		auto inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		auto outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_commands->ProcessReplacing(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to assign the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::SetParameter(int32_t index, float value)
{
	try
	{
		_commands->SetParameter(index, value);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}
}

// Calls the plugin command stub to retrieve the parameter.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
float PluginCommandProxy::GetParameter(int32_t index)
{
	try
	{
		return _commands->GetParameter(index);
	}
	catch(System::Exception^ e)
	{
		Utils::ShowError(e);
	}

	return 0.0f;
}

// Calls the plugin command stub to process audio (legacy).
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::ProcessAcc(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
{
	if(_legacyCommands == nullptr) return;

	try
	{
		auto inputBuffers = TypeConverter::ToManagedAudioBufferArray(inputs, sampleFrames, numInputs, false);
		auto outputBuffers = TypeConverter::ToManagedAudioBufferArray(outputs, sampleFrames, numOutputs, true);

		_legacyCommands->ProcessAcc(inputBuffers, outputBuffers);
	}
	catch(System::Exception^ e)
	{
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

}}}} // Jacobi::Vst::Plugin::Interop