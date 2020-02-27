#include "pch.h"
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
	_pEditorRect = new Vst2Rectangle();

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
Vst2IntPtr PluginCommandProxy::Dispatch(int32_t opcode, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	Vst2IntPtr result = 0;

	_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

	if(_commandStub != nullptr)
	{
		try
		{
			Vst2PluginCommands command = safe_cast<Vst2PluginCommands>(opcode);

			switch(command)
			{
			case Vst2PluginCommands::Open:
				_commandStub->Open();
				break;
			case Vst2PluginCommands::Close:
				_commandStub->Close();
				// call Dispose() on this instance
				delete this;
				break;
			case Vst2PluginCommands::ProgramSet:
				_commandStub->SetProgram(safe_cast<System::Int32>(value));
				result = 1;
				break;
			case Vst2PluginCommands::ProgramGet:
				result = _commandStub->GetProgram();
				break;
			case Vst2PluginCommands::ProgramSetName:
				_commandStub->SetProgramName(TypeConverter::CharToString((char*)ptr));
				result = 1;
				break;
			case Vst2PluginCommands::ProgramGetName:
				TypeConverter::StringToChar(_commandStub->GetProgramName(), (char*)ptr, Vst2MaxProgNameLen);
				result = 1;
				break;
			case Vst2PluginCommands::ParameterGetLabel:
				TypeConverter::StringToChar(_commandStub->GetParameterLabel(index), (char*)ptr, Vst2MaxParamStrLen);
				result = 1;
				break;
			case Vst2PluginCommands::ParameterGetDisplay:
				TypeConverter::StringToChar(_commandStub->GetParameterDisplay(index), (char*)ptr, Vst2MaxParamStrLen);
				result = 1;
				break;
			case Vst2PluginCommands::ParameterGetName:
				TypeConverter::StringToChar(_commandStub->GetParameterName(index), (char*)ptr, Vst2MaxParamStrLen);
				result = 1;
				break;
			case Vst2PluginCommands::SampleRateSet:
				_commandStub->SetSampleRate(opt);
				result = 1;
				break;
			case Vst2PluginCommands::BlockSizeSet:
				_commandStub->SetBlockSize(safe_cast<System::Int32>(value));
				result = 1;
				break;
			case Vst2PluginCommands::OnOff:
				_memTracker->ClearAll(); // safe to delete allocated memory during suspend/resume
				_commandStub->MainsChanged(value != 0);
				result = 1;
				break;
			case Vst2PluginCommands::EditorGetRectangle:
			{
				System::Drawing::Rectangle rect;
				if (_commandStub->EditorGetRect(rect))
				{
					TypeConverter::ToUnmanagedRectangle(_pEditorRect, rect);
					*((Vst2Rectangle**)ptr) = _pEditorRect;
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::EditorOpen:
				result = _commandStub->EditorOpen(System::IntPtr(ptr));
				break;
			case Vst2PluginCommands::EditorClose:
				_commandStub->EditorClose();
				result = 1;
				break;
			case Vst2PluginCommands::EditorIdle:
				_commandStub->EditorIdle();
				result = 1;
				break;
			case Vst2PluginCommands::ChunkGet:
			{
				array<System::Byte>^ buffer = _commandStub->GetChunk(index != 0);
				if(buffer != nullptr)
				{
					*(void**)ptr = TypeConverter::ByteArrayToPtr(buffer);
				
					_memTracker->RegisterArray(*(void**)ptr);

					result = buffer->Length;
				}
			}	break;
			case Vst2PluginCommands::ChunkSet:
			{
				array<System::Byte>^ buffer = TypeConverter::PtrToByteArray((char*)ptr, safe_cast<System::Int32>(value));
				result = _commandStub->SetChunk(buffer, index != 0) ? 1 : 0;
			}	break;
			case Vst2PluginCommands::ProcessEvents:
				result = _commandStub->ProcessEvents(TypeConverter::ToManagedEventArray((Vst2Events*)ptr)) ? 1 : 0;
				break;
			case Vst2PluginCommands::ParameterCanBeAutomated:
				result = _commandStub->CanParameterBeAutomated(index) ? 1 : 0;
				break;
			case Vst2PluginCommands::ParameterFromString:
				result = _commandStub->String2Parameter(index, TypeConverter::CharToString((char*)ptr)) ? 1 : 0;
				break;
			case Vst2PluginCommands::ProgramGetNameByIndex:
			{
				System::String^ name = _commandStub->GetProgramNameIndexed(index);
				if(name != nullptr)
				{
					TypeConverter::StringToChar(name, (char*)ptr, Vst2MaxProgNameLen);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::GetInputProperties:
			{
				Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetInputProperties(index);
				if(pinProps != nullptr)
				{
					TypeConverter::ToUnmanagedPinProperties((::Vst2PinProperties*)ptr, pinProps);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::GetOutputProperties:
			{
				Jacobi::Vst::Core::VstPinProperties^ pinProps = _commandStub->GetOutputProperties(index);
				if(pinProps != nullptr)
				{
					TypeConverter::ToUnmanagedPinProperties((::Vst2PinProperties*)ptr, pinProps);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::PluginGetCategory:
				result = safe_cast<int32_t>(_commandStub->GetCategory());
				break;
			case Vst2PluginCommands::SetSpeakerArrangement:
				result = _commandStub->SetSpeakerArrangement(TypeConverter::ToManagedSpeakerArrangement((::Vst2SpeakerArrangement*)value),
					TypeConverter::ToManagedSpeakerArrangement((::Vst2SpeakerArrangement*)ptr));
				break;
			case Vst2PluginCommands::SetBypass:
				result = _commandStub->SetBypass(value != 0) ? 1 : 0;
				break;
			case Vst2PluginCommands::PluginGetName:
			{
				System::String^ name = _commandStub->GetEffectName();
				if(name != nullptr)
				{
					TypeConverter::StringToChar(name, (char*)ptr, Vst2MaxEffectNameLen);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::VendorGetString:
			{
				System::String^ str = _commandStub->GetVendorString();
				if(str != nullptr)
				{
					TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxVendorStrLen);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::ProductGetString:
			{
				System::String^ str = _commandStub->GetProductString();
				if(str != nullptr)
				{
					TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxProductStrLen);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::VendorGetVersion:
				result = _commandStub->GetVendorVersion();
				break;
			case Vst2PluginCommands::CanDo:
				result = safe_cast<int32_t>(_commandStub->CanDo(TypeConverter::CharToString((char*)ptr)));
				break;
			case Vst2PluginCommands::GetTailSizeInSamples:
				result = _commandStub->GetTailSize();
				break;
			case Vst2PluginCommands::ParameterGetProperties:
			{
				Jacobi::Vst::Core::VstParameterProperties^ paramProps = _commandStub->GetParameterProperties(index);
				if(paramProps != nullptr)
				{
					TypeConverter::ToUnmanagedParameterProperties((::Vst2ParameterProperties*)ptr, paramProps);
					result = 1;
				}
			}	break;
			case Vst2PluginCommands::GetVstVersion:
				result = _commandStub->GetVstVersion();
				break;
			case Vst2PluginCommands::EditorKeyDown:
				result = _commandStub->EditorKeyDown(safe_cast<System::Byte>(index), 
					safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
					safe_cast<Jacobi::Vst::Core::VstModifierKeys>((int32_t)opt)) ? 1 : 0;
				break;
			case Vst2PluginCommands::EditorKeyUp:
				result = _commandStub->EditorKeyUp(safe_cast<System::Byte>(index), 
					safe_cast<Jacobi::Vst::Core::VstVirtualKey>(value), 
					safe_cast<Jacobi::Vst::Core::VstModifierKeys>((int32_t)opt)) ? 1 : 0;
				break;
			case Vst2PluginCommands::SetKnobMode:
				result = _commandStub->SetEditorKnobMode(safe_cast<Jacobi::Vst::Core::VstKnobMode>(value)) ? 1 : 0;
				break;
			case Vst2PluginCommands::MidiProgramGetName:
			{
				::Vst2MidiProgramName* pProgName = (::Vst2MidiProgramName*)ptr;
				Jacobi::Vst::Core::VstMidiProgramName^ progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
				progName->CurrentProgramIndex = pProgName->thisProgramIndex;
				result = _commandStub->GetMidiProgramName(progName, index);
				TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
			}	break;
			case Vst2PluginCommands::MidiProgramGetCurrent:
			{
				::Vst2MidiProgramName* pProgName = (::Vst2MidiProgramName*)ptr;
				Jacobi::Vst::Core::VstMidiProgramName^ progName = gcnew Jacobi::Vst::Core::VstMidiProgramName();
				result = _commandStub->GetCurrentMidiProgramName(progName, index);
				TypeConverter::ToUnmanagedMidiProgramName(pProgName, progName);
			}	break;
			case Vst2PluginCommands::MidiProgramGetCategory:
			{
				::Vst2MidiProgramCategory* pProgCat = (::Vst2MidiProgramCategory*)ptr;
				Jacobi::Vst::Core::VstMidiProgramCategory^ progCat = gcnew Jacobi::Vst::Core::VstMidiProgramCategory();
				progCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
				result = _commandStub->GetMidiProgramCategory(progCat, index);
				TypeConverter::ToUnmanagedMidiProgramCategory(pProgCat, progCat);
			}	break;
			case Vst2PluginCommands::MidiProgramsChanged:
				result = _commandStub->HasMidiProgramsChanged(index) ? 1 : 0;
				break;
			case Vst2PluginCommands::MidiKeyGetName:
			{
				::Vst2MidiKeyName* pKeyName = (::Vst2MidiKeyName*)ptr;
				Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName = gcnew Jacobi::Vst::Core::VstMidiKeyName();
				midiKeyName->CurrentProgramIndex = pKeyName->thisProgramIndex;
				midiKeyName->CurrentKeyNumber = pKeyName->thisKeyNumber;
				result = _commandStub->GetMidiKeyName(midiKeyName, index);
				TypeConverter::StringToChar(midiKeyName->Name, pKeyName->keyName, Vst2MaxNameLen);
			}	break;
			case Vst2PluginCommands::BeginSetProgram:
				result = _commandStub->BeginSetProgram() ? 1 : 0;
				break;
			case Vst2PluginCommands::EndSetProgram:
				result = _commandStub->EndSetProgram() ? 1 : 0;
				break;
			case Vst2PluginCommands::GetSpeakerArrangement:
			{
				::Vst2SpeakerArrangement** ppInput = (::Vst2SpeakerArrangement**)value;
				::Vst2SpeakerArrangement** ppOutput = (::Vst2SpeakerArrangement**)ptr;
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
			//case Vst2PluginCommands::ShellGetNextPlugin:
				/*{
				System::String^ str;
				result = _commandStub->GetNextPlugin(str);
				TypeConverter::StringToChar(str, (char*)ptr, Vst2MaxProductStrLen);
				}*/
				//break;
			case Vst2PluginCommands::ProcessStart:
				result = _commandStub->StartProcess();
				break;
			case Vst2PluginCommands::ProcessStop:
				result = _commandStub->StopProcess();
				break;
			//case Vst2PluginCommands::SetTotalSampleToProcess:
				//result = _commandStub->SetTotalSamplesToProcess(value);
				//break;
			case Vst2PluginCommands::SetPanLaw:
				result = _commandStub->SetPanLaw(safe_cast<Jacobi::Vst::Core::VstPanLaw>(value), opt) ? 1 : 0;
				break;
			case Vst2PluginCommands::BeginLoadBank:
				result = safe_cast<int32_t>(_commandStub->BeginLoadBank(TypeConverter::ToManagedPatchChunkInfo((::Vst2PatchChunkInfo*)ptr)));
				break;
			case Vst2PluginCommands::BeginLoadProgram:
				result = safe_cast<int32_t>(_commandStub->BeginLoadProgram(TypeConverter::ToManagedPatchChunkInfo((::Vst2PatchChunkInfo*)ptr)));
				break;
			case Vst2PluginCommands::SetProcessPrecision:
				result = _commandStub->SetProcessPrecision(safe_cast<Jacobi::Vst::Core::VstProcessPrecision>(value)) ? 1 : 0;
				break;
			case Vst2PluginCommands::MidiGetInputChannelCount:
				result = _commandStub->GetNumberOfMidiInputChannels();
				break;
			case Vst2PluginCommands::MidiGetOutputChannelCount:
				result = _commandStub->GetNumberOfMidiOutputChannels();
				break;
			default:
				result = DispatchDeprecated(command, index, value, ptr, opt);
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
		_traceCtx->WriteEvent(System::Diagnostics::TraceEventType::Warning, "Plugin Command Stub was not set.");
	}

	_traceCtx->WriteDispatchEnd(System::IntPtr(result));

	return result;
}

// continuation of Dispatch()
// Dispatches an opcode to the plugin deprecated command stub.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
Vst2IntPtr PluginCommandProxy::DispatchDeprecated(Vst2PluginCommands command, int32_t index, Vst2IntPtr value, void* ptr, float opt)
{
	Vst2IntPtr result = 0;

	if(_deprecatedCmdStub != nullptr)
	{
		switch(command)
		{
		// VST 1.0 deprecated
		case Vst2PluginCommands::VuGet:
			result = safe_cast<Vst2IntPtr>(_deprecatedCmdStub->GetVu());
			break;
		//case Vst2PluginCommands::EditDraw:
		//	break;
		//case Vst2PluginCommands::EditMouse:
		//	break;
		case Vst2PluginCommands::EditorKey:
			result = _deprecatedCmdStub->EditorKey(safe_cast<System::Int32>(value)) ? 1 : 0;
			break;
		case Vst2PluginCommands::EditorTop:
			result = _deprecatedCmdStub->EditorTop() ? 1 : 0;
			break;
		case Vst2PluginCommands::EditorSleep:
			result = _deprecatedCmdStub->EditorSleep() ? 1 : 0;
			break;
		case Vst2PluginCommands::Identify:
			result = _deprecatedCmdStub->Identify();
			break;

		// VST 2.0 deprecated
		case Vst2PluginCommands::ProgramGetCategoriesCount:
			result = _deprecatedCmdStub->GetProgramCategoriesCount();
			break;
		case Vst2PluginCommands::ProgramCopy:
			result = _deprecatedCmdStub->CopyCurrentProgramTo(index);
			break;
		case Vst2PluginCommands::ConnectInput:
			result = _deprecatedCmdStub->ConnectInput(index, value != 0) ? 1 : 0;
			break;
		case Vst2PluginCommands::ConnectOutput:
			result = _deprecatedCmdStub->ConnectOutput(index, value != 0) ? 1 : 0;
			break;
		case Vst2PluginCommands::GetCurrentPosition:
			result = _deprecatedCmdStub->GetCurrentPosition();
			break;
		case Vst2PluginCommands::GetDestinationBuffer:
		{
			Jacobi::Vst::Core::IDirectBufferAccess32^ audioBuffer = 
				dynamic_cast<Jacobi::Vst::Core::IDirectBufferAccess32^>(_deprecatedCmdStub->GetDestinationBuffer());
			if(audioBuffer != nullptr)
			{
				result = (Vst2IntPtr)audioBuffer->Buffer;
			}
		}	break;
		case Vst2PluginCommands::SetBlockSizeAndSampleRate:
			result = _deprecatedCmdStub->SetBlockSizeAndSampleRate(safe_cast<System::Int32>(value), opt) ? 1 : 0;
			break;
		case Vst2PluginCommands::GetErrorText:
		{
			System::String^ txt = _deprecatedCmdStub->GetErrorText();
			if(txt != nullptr)
			{
				TypeConverter::StringToChar(txt, (char*)ptr, 256);
				result = 1;
			}
		}	break;
		case Vst2PluginCommands::Idle:
			result = _deprecatedCmdStub->Idle() ? 1 : 0;
			break;
		case Vst2PluginCommands::GetIcon:
		{
			System::IntPtr^ icon = _deprecatedCmdStub->GetIcon();
			if(icon != nullptr)
			{
				// TODO:
				// void* in <ptr>, not yet defined
				//result = 1;
			}
		}	break;
		case Vst2PluginCommands::SetViewPosition:
			result = _deprecatedCmdStub->SetViewPosition(System::Drawing::Point(index, safe_cast<System::Int32>(value))) ? 1 : 0;
			break;
		case Vst2PluginCommands::KeysRequired:
			// NOTE: 0=Required, 1=dont need.
			result = _deprecatedCmdStub->KeysRequired() ? 0 : 1;
			break;
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

// Calls the plugin command stub to process audio.
// Takes care of marshaling from C++ to Managed .NET and visa versa.
void PluginCommandProxy::Process(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
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
void PluginCommandProxy::Process(double** inputs, double** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
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
void PluginCommandProxy::SetParameter(int32_t index, float value)
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
float PluginCommandProxy::GetParameter(int32_t index)
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
void PluginCommandProxy::ProcessAcc(float** inputs, float** outputs, int32_t sampleFrames, int32_t numInputs, int32_t numOutputs)
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