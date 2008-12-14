#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host
{

	public ref class VstPluginCommandStub : Jacobi::Vst::Core::Host::IVstPluginCommandStub
	{
	public:
		// IVstPluginCommandsBase
		virtual void ProcessReplacing(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, 
			array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs);
        virtual void ProcessReplacing(array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputs, 
			array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputs);
		virtual void SetParameter(System::Int32 index, System::Single value);
		virtual System::Single GetParameter(System::Int32 index);

		// IVstPluginCommands10
		virtual void Open();
        virtual void Close();
        virtual void SetProgram(System::Int32 programNumber);
        virtual System::Int32 GetProgram();
        virtual void SetProgramName(System::String^ name);
		virtual System::String^ GetProgramName();
        virtual System::String^ GetParameterLabel(System::Int32 index);
        virtual System::String^ GetParameterDisplay(System::Int32 index);
        virtual System::String^ GetParameterName(System::Int32 index);
        virtual void SetSampleRate(System::Single sampleRate);
        virtual void SetBlockSize(System::Int32 blockSize);
        virtual void MainsChanged(System::Boolean onoff);
		virtual System::Boolean EditorGetRect([System::Runtime::InteropServices::Out] System::Drawing::Rectangle% rect);
		virtual System::Boolean EditorOpen(System::IntPtr hWnd);
        virtual void EditorClose();
        virtual void EditorIdle();
        virtual array<System::Byte>^ GetChunk(System::Boolean isPreset);
        virtual System::Int32 SetChunk(array<System::Byte>^ data, System::Boolean isPreset);

		// IVstPluginCommands20
		virtual System::Boolean ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events);
        virtual System::Boolean CanParameterBeAutomated(System::Int32 index);
        virtual System::Boolean String2Parameter(System::Int32 index, System::String^ str);
        virtual System::String^ GetProgramNameIndexed(System::Int32 index);
        virtual Jacobi::Vst::Core::VstPinProperties^ GetInputProperties(System::Int32 index);
        virtual Jacobi::Vst::Core::VstPinProperties^ GetOutputProperties(System::Int32 index);
        virtual Jacobi::Vst::Core::VstPluginCategory GetCategory();
        // Offline processing not implemented
        //virtual System::Boolean OfflineNotify(array<VstAudioFile^>^ audioFiles, System::Int32 count, System::Int32 startFlag);
        //virtual System::Boolean OfflinePrepare(array<VstOfflineTask^>^ tasks, System::Int32 count);
        //virtual System::Boolean OfflineRun(array<VstOfflineTask^>^ tasks, System::Int32 count);
        //virtual System::Boolean ProcessVariableIO(VstVariableIO^ variableIO);
        virtual System::Boolean SetSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ saInput, 
			Jacobi::Vst::Core::VstSpeakerArrangement^ saOutput);
        virtual System::Boolean SetBypass(System::Boolean bypass);
        virtual System::String^ GetEffectName();
        virtual System::String^ GetVendorString();
        virtual System::String^ GetProductString();
        virtual System::Int32 GetVendorVersion();
        virtual Jacobi::Vst::Core::VstCanDoResult CanDo(System::String^ cando);
        virtual System::Int32 GetTailSize();
        virtual Jacobi::Vst::Core::VstParameterProperties^ GetParameterProperties(System::Int32 index);
        virtual System::Int32 GetVstVersion();

		// IVstPluginCommands21
		virtual System::Boolean EditorKeyDown(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, 
			Jacobi::Vst::Core::VstModifierKeys modifers);
        virtual System::Boolean EditorKeyUp(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, 
			Jacobi::Vst::Core::VstModifierKeys modifers);
        virtual System::Boolean SetEditorKnobMode(Jacobi::Vst::Core::VstKnobMode mode);
        virtual System::Int32 GetMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel);
        virtual System::Int32 GetCurrentMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel);
        virtual System::Int32 GetMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiCat, System::Int32 channel);
        virtual System::Boolean HasMidiProgramsChanged(System::Int32 channel);
        virtual System::Boolean GetMidiKeyName(Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName, System::Int32 channel);
        virtual System::Boolean BeginSetProgram();
        virtual System::Boolean EndSetProgram();

		// IVstPluginCommands23
		virtual System::Boolean GetSpeakerArrangement([System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% input, 
			[System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% output);
        // Offline processing not implemented
        //virtual System::Int32 SetTotalSamplesToProcess(System::Int32 numberOfSamples);
        // Plugin Host/Shell not implemented
        //virtual System::Int32 GetNextPlugin(([System::Runtime::InteropServices::Out] System::String^% name);
        virtual System::Int32 StartProcess();
        virtual System::Int32 StopProcess();
        virtual System::Boolean SetPanLaw(Jacobi::Vst::Core::VstPanLaw type, System::Single gain);
        virtual Jacobi::Vst::Core::VstCanDoResult BeginLoadBank(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo);
        virtual Jacobi::Vst::Core::VstCanDoResult BeginLoadProgram(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo);

		// IVstPluginCommands24
		virtual System::Boolean SetProcessPrecision(Jacobi::Vst::Core::VstProcessPrecision precision);
        virtual System::Int32 GetNumberOfMidiInputChannels();
        virtual System::Int32 GetNumberOfMidiOutputChannels();

		// IVstPluginCommandStub
		// ...

	internal:
		VstPluginCommandStub(::AEffect* pEffect);

	private:
		::AEffect* _pEffect;

		UnmanagedArray<float*> _audioInputs;
		UnmanagedArray<float*> _audioOutputs;
		
		VstInt32 CopyBufferPointers(float** ppBuffers, array<Jacobi::Vst::Core::VstAudioBuffer^>^ audioBuffers)
		{
			VstInt32 sampleCount = 0;

			for(int i = 0; i < audioBuffers->Length; i++)
			{
				Jacobi::Vst::Core::IDirectBufferAccess32^ unmanagedBuffer = 
					safe_cast<Jacobi::Vst::Core::IDirectBufferAccess32^>(audioBuffers[i]);

				ppBuffers[i] = unmanagedBuffer->Buffer;

				if(sampleCount < unmanagedBuffer->SampleCount)
				{
					sampleCount = unmanagedBuffer->SampleCount;
				}
			}

			return sampleCount;
		}

		UnmanagedArray<double*> _precisionInputs;
		UnmanagedArray<double*> _precisionOutputs;

		VstInt32 CopyBufferPointers(double** ppBuffers, array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ audioBuffers)
		{
			VstInt32 sampleCount = 0;

			for(int i = 0; i < audioBuffers->Length; i++)
			{
				Jacobi::Vst::Core::IDirectBufferAccess64^ unmanagedBuffer = 
					safe_cast<Jacobi::Vst::Core::IDirectBufferAccess64^>(audioBuffers[i]);

				ppBuffers[i] = unmanagedBuffer->Buffer;

				if(sampleCount < unmanagedBuffer->SampleCount)
				{
					sampleCount = unmanagedBuffer->SampleCount;
				}
			}

			return sampleCount;
		}

		// helper methods for calling the plugin
		::VstIntPtr CallDispatch(::VstInt32 opcode, ::VstInt32 index, ::VstIntPtr value, void* ptr, float opt)
		{ return _pEffect->dispatcher(_pEffect, opcode, index, value, ptr, opt); }
		void CallProcess32(float** inputs, float** outputs, ::VstInt32 sampleFrames)
		{ if(_pEffect->processReplacing) _pEffect->processReplacing(_pEffect, inputs, outputs, sampleFrames); }
		void CallProcess64(double** inputs, double** outputs, ::VstInt32 sampleFrames)
		{ if(_pEffect->processDoubleReplacing) _pEffect->processDoubleReplacing(_pEffect, inputs, outputs, sampleFrames); }
		void CallSetParameter(::VstInt32 index, float parameter)
		{ _pEffect->setParameter(_pEffect, index, parameter); }
		float CallGetParameter(::VstInt32 index)
		{ return _pEffect->getParameter(_pEffect, index); }

		template<typename T> void ThrowIfArgumentNotOfType(System::Object^ argument);
	};

}}}} // namespace Jacobi.Vst.Interop.Host