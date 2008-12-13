#include "StdAfx.h"
#include "VstPluginCommandStub.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	VstPluginCommandStub::VstPluginCommandStub(::AEffect* pEffect)
	{
		if(pEffect == NULL)
		{
			throw gcnew System::ArgumentNullException("pEffect");
		}

		_pEffect = pEffect;
	}

	// IVstPluginCommandsBase
	void VstPluginCommandStub::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs)
	{
	}

    void VstPluginCommandStub::ProcessReplacing(array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputs)
	{
	}

	void VstPluginCommandStub::SetParameter(System::Int32 index, System::Single value)
	{
	}

	System::Single VstPluginCommandStub::GetParameter(System::Int32 index)
	{
		return 0;
	}

	// IVstPluginCommands10
	void VstPluginCommandStub::Open()
	{
	}

    void VstPluginCommandStub::Close()
	{
	}

    void VstPluginCommandStub::SetProgram(System::Int32 programNumber)
	{
	}

    System::Int32 VstPluginCommandStub::GetProgram()
	{
		return 0;
	}

    void VstPluginCommandStub::SetProgramName(System::String^ name)
	{
	}

	System::String^ VstPluginCommandStub::GetProgramName()
	{
		return nullptr;
	}

    System::String^ VstPluginCommandStub::GetParameterLabel(System::Int32 index)
	{
		return nullptr;
	}

    System::String^ VstPluginCommandStub::GetParameterDisplay(System::Int32 index)
	{
		return nullptr;
	}

    System::String^ VstPluginCommandStub::GetParameterName(System::Int32 index)
	{
		return nullptr;
	}

    void VstPluginCommandStub::SetSampleRate(System::Single sampleRate)
	{
	}

    void VstPluginCommandStub::SetBlockSize(System::Int32 blockSize)
	{
	}

    void VstPluginCommandStub::MainsChanged(System::Boolean onoff)
	{
	}

	System::Boolean VstPluginCommandStub::EditorGetRect([System::Runtime::InteropServices::Out] System::Drawing::Rectangle% rect)
	{
		return false;
	}

	System::Boolean VstPluginCommandStub::EditorOpen(System::IntPtr hWnd)
	{
		return false;
	}

    void VstPluginCommandStub::EditorClose()
	{
	}

    void VstPluginCommandStub::EditorIdle()
	{
	}

    array<System::Byte>^ VstPluginCommandStub::GetChunk(System::Boolean isPreset)
	{
		return nullptr;
	}

    System::Int32 VstPluginCommandStub::SetChunk(array<System::Byte>^ data, System::Boolean isPreset)
	{
		return 0;
	}

	// IVstPluginCommands20
	System::Boolean VstPluginCommandStub::ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::CanParameterBeAutomated(System::Int32 index)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::String2Parameter(System::Int32 index, System::String^ str)
	{
		return false;
	}

    System::String^ VstPluginCommandStub::GetProgramNameIndexed(System::Int32 index)
	{
		return nullptr;
	}

    Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandStub::GetInputProperties(System::Int32 index)
	{
		return nullptr;
	}

    Jacobi::Vst::Core::VstPinProperties^ VstPluginCommandStub::GetOutputProperties(System::Int32 index)
	{
		return nullptr;
	}

    Jacobi::Vst::Core::VstPluginCategory VstPluginCommandStub::GetCategory()
	{
		return Jacobi::Vst::Core::VstPluginCategory::Unknown;
	}

    // Offline processing not implemented
    //System::Boolean OfflineNotify(array<VstAudioFile^>^ audioFiles, System::Int32 count, System::Int32 startFlag);
    //System::Boolean OfflinePrepare(array<VstOfflineTask^>^ tasks, System::Int32 count);
    //System::Boolean OfflineRun(array<VstOfflineTask^>^ tasks, System::Int32 count);
    //System::Boolean ProcessVariableIO(VstVariableIO^ variableIO);

    System::Boolean VstPluginCommandStub::SetSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ saInput, Jacobi::Vst::Core::VstSpeakerArrangement^ saOutput)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::SetBypass(System::Boolean bypass)
	{
		return false;
	}

    System::String^ VstPluginCommandStub::GetEffectName()
	{
		return nullptr;
	}

    System::String^ VstPluginCommandStub::GetVendorString()
	{
		return nullptr;
	}

    System::String^ VstPluginCommandStub::GetProductString()
	{
		return nullptr;
	}

    System::Int32 VstPluginCommandStub::GetVendorVersion()
	{
		return 0;
	}

    Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::CanDo(System::String^ cando)
	{
		return Jacobi::Vst::Core::VstCanDoResult::Unknown;
	}

    System::Int32 VstPluginCommandStub::GetTailSize()
	{
		return 0;
	}

    Jacobi::Vst::Core::VstParameterProperties^ VstPluginCommandStub::GetParameterProperties(System::Int32 index)
	{
		return nullptr;
	}

    System::Int32 VstPluginCommandStub::GetVstVersion()
	{
		return 0;
	}

	// IVstPluginCommands21
	System::Boolean VstPluginCommandStub::EditorKeyDown(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::EditorKeyUp(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, Jacobi::Vst::Core::VstModifierKeys modifers)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::SetEditorKnobMode(Jacobi::Vst::Core::VstKnobMode mode)
	{
		return false;
	}

    System::Int32 VstPluginCommandStub::GetMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
	{
		return 0;
	}

    System::Int32 VstPluginCommandStub::GetCurrentMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel)
	{
		return 0;
	}

    System::Int32 VstPluginCommandStub::GetMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiCat, System::Int32 channel)
	{
		return 0;
	}

    System::Boolean VstPluginCommandStub::HasMidiProgramsChanged(System::Int32 channel)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::GetMidiKeyName(Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName, System::Int32 channel)
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::BeginSetProgram()
	{
		return false;
	}

    System::Boolean VstPluginCommandStub::EndSetProgram()
	{
		return false;
	}

	// IVstPluginCommands23
	System::Boolean VstPluginCommandStub::GetSpeakerArrangement([System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% input, [System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% output)
	{
		return false;
	}

    // Offline processing not implemented
    //System::Int32 SetTotalSamplesToProcess(System::Int32 numberOfSamples);
    // Plugin Host/Shell not implemented
    //System::Int32 GetNextPlugin([System::Runtime::InteropServices::Out] System::String^% name);

    System::Int32 VstPluginCommandStub::StartProcess()
	{
		return 0;
	}

    System::Int32 VstPluginCommandStub::StopProcess()
	{
		return 0;
	}

    System::Boolean VstPluginCommandStub::SetPanLaw(Jacobi::Vst::Core::VstPanLaw type, System::Single gain)
	{
		return false;
	}

    Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::BeginLoadBank(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		return Jacobi::Vst::Core::VstCanDoResult::Unknown;
	}

    Jacobi::Vst::Core::VstCanDoResult VstPluginCommandStub::BeginLoadProgram(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		return Jacobi::Vst::Core::VstCanDoResult::Unknown;
	}

	// IVstPluginCommands24
	System::Boolean VstPluginCommandStub::SetProcessPrecision(Jacobi::Vst::Core::VstProcessPrecision precision)
	{
		return false;
	}

    System::Int32 VstPluginCommandStub::GetNumberOfMidiInputChannels()
	{
		return 0;
	}

    System::Int32 VstPluginCommandStub::GetNumberOfMidiOutputChannels()
	{
		return 0;
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

}}}} // namespace Jacobi.Vst.Interop.Host