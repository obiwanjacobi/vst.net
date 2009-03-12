#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {

class TypeConverter
{
public:
	// Converts a managed string to an unmanaged char buffer.
	static void StringToChar(System::String^ source, char* dest, size_t maxLength)
	{
		if(source)
		{
			System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(source);
			strncpy_s(dest, maxLength, (const char*)mem.ToPointer(), _TRUNCATE);
			System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
		}
	}

	// Converts an unmanaged char buffer to a managed string.
	static System::String^ CharToString(char* source)
	{
		if(source != NULL)
		{
			return gcnew System::String(source);
		}

		return nullptr;
	}

	// Call DeallocateString on retval
	static char* AllocateString(System::String^ source)
	{
		System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(source);

		return (char*)mem.ToPointer();
	}

	// frees the unmanaged memory
	static void DeallocateString(char* pBuffer)
	{
		if(pBuffer != NULL)
		{
			System::IntPtr mem(pBuffer);

			System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
		}
	}


	// Converts a managed rect to an unmanaged ppRect
	static ERect* AllocUnmanagedRectangle(System::Drawing::Rectangle rect)
	{
		ERect* pRect = new ERect();
		pRect->top = rect.Top;
		pRect->left = rect.Left;
		pRect->bottom = rect.Bottom;
		pRect->right = rect.Right;

		return pRect;
	}

	// converts an unmanaged ERect* to a Rectangle. 
	static System::Drawing::Rectangle ToManagedRectangle(ERect* pRect)
	{
		System::Drawing::Rectangle rect(pRect->top, pRect->left, pRect->bottom, pRect->right);

		return rect;
	}

	// Converts a managed byteArray to an unmanaged array.
	// delete[] retval
	static char* ByteArrayToPtr(array<System::Byte>^ byteArray)
	{
		int length = byteArray->Length;
		char* buffer = new char[length];

		for(int i = 0; i < length; i++)
		{
			buffer[i] = safe_cast<char>(byteArray[i]);
		}

		return buffer;
	}

	// Converts an unmanaged char pBuffer to a managed Byte array.
	static array<System::Byte>^ PtrToByteArray(char *pBuffer, int length)
	{
		array<System::Byte>^ byteArray = gcnew array<System::Byte>(length);

		for(int n = 0; n < length; n++)
		{
			byteArray[n] = safe_cast<System::Byte>(pBuffer[n]);
		}

		return byteArray;
	}

	// Converts an unmanaged events to a managed VstEvent array.
	// Only handles MidiEvent and MidiSysExEvent types.
	static array<Jacobi::Vst::Core::VstEvent^>^ ToManagedEventArray(VstEvents* pEvents)
	{
		array<Jacobi::Vst::Core::VstEvent^>^ eventArray = gcnew array<Jacobi::Vst::Core::VstEvent^>(pEvents->numEvents);

		for(int n = 0; n < pEvents->numEvents; n++)
		{
			::VstEvent* pEvent = pEvents->events[n];

			switch(pEvent->type)
			{
			case kVstMidiType:
				{
				::VstMidiEvent* pMidiEvent = (::VstMidiEvent*)pEvent;

				array<System::Byte>^ midiData = gcnew array<System::Byte>(4);
				midiData[0] = pMidiEvent->midiData[0];
				midiData[1] = pMidiEvent->midiData[1];
				midiData[2] = pMidiEvent->midiData[2];

				Jacobi::Vst::Core::VstMidiEvent^ midiEvent = gcnew Jacobi::Vst::Core::VstMidiEvent(
					pMidiEvent->deltaFrames, 
					pMidiEvent->noteLength, pMidiEvent->noteOffset, midiData, pMidiEvent->detune, pMidiEvent->noteOffVelocity);

				eventArray[n] = midiEvent;
				}
				break;
			case kVstSysExType:
				{
				::VstMidiSysexEvent* pMidiEvent = (::VstMidiSysexEvent*)pEvent;
				
				// copy sysex data into managed buffer
				array<System::Byte>^ midiData = gcnew array<System::Byte>(pMidiEvent->dumpBytes);
				for(int i = 0; i < pMidiEvent->dumpBytes; i++)
				{
					midiData[i] = pMidiEvent->sysexDump[i];
				}

				Jacobi::Vst::Core::VstMidiSysExEvent^ midiEvent = gcnew Jacobi::Vst::Core::VstMidiSysExEvent(
					pMidiEvent->deltaFrames, 
					midiData);

				eventArray[n] = midiEvent;
				}
				break;
			default:
				// TODO: log skipping
				break;
			}
		}

		return eventArray;
	}

	// Converts a managed VstEvent array to an unmanaged VstEvent array.
	// Call DeleteVstEvents on retval.
	static ::VstEvents* AllocUnmanagedEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
	{
		int length = events->Length;
		if(length > 2) length -= 2;
		
		int totalLength = sizeof(VstEvent) + (length * sizeof(VstEvent*));

		::VstEvents* pEvents = (::VstEvents*)new char[totalLength];
		ZeroMemory(pEvents, totalLength);

		pEvents->numEvents = events->Length;
		
		int index = 0;
		for each(Jacobi::Vst::Core::VstEvent^ evnt in events)
		{
			switch(evnt->EventType)
			{
			case Jacobi::Vst::Core::VstEventTypes::MidiEvent:
				{
				Jacobi::Vst::Core::VstMidiEvent^ midiEvent = (Jacobi::Vst::Core::VstMidiEvent^)evnt;
				::VstMidiEvent* pMidiEvent = new ::VstMidiEvent();

				pMidiEvent->byteSize = sizeof(::VstMidiEvent) - (2 * sizeof(VstInt32));
				pMidiEvent->flags = 0;
				pMidiEvent->deltaFrames = midiEvent->DeltaFrames;
				pMidiEvent->type = (VstInt32)midiEvent->EventType;

				for(int i = 0; i < midiEvent->MidiData->Length && i < 4; i++)
				{
					pMidiEvent->midiData[i] = midiEvent->MidiData[i];
				}

				pMidiEvent->detune = (char)midiEvent->Detune;
				pMidiEvent->noteLength = midiEvent->NoteLength;
				pMidiEvent->noteOffset = midiEvent->NoteOffset;
				pMidiEvent->noteOffVelocity = midiEvent->NoteOffVelocity;

				pEvents->events[index] = (::VstEvent*)pMidiEvent;
				}
				break;
			case Jacobi::Vst::Core::VstEventTypes::MidiSysExEvent:
				{
				Jacobi::Vst::Core::VstMidiSysExEvent^ midiEvent = (Jacobi::Vst::Core::VstMidiSysExEvent^)evnt;
				::VstMidiSysexEvent* pMidiEvent = new ::VstMidiSysexEvent();

				pMidiEvent->byteSize = sizeof(::VstMidiSysexEvent) - (2 * sizeof(VstInt32));
				pMidiEvent->flags = 0;
				pMidiEvent->deltaFrames = midiEvent->DeltaFrames;
				pMidiEvent->type = (VstInt32)midiEvent->EventType;

				pMidiEvent->dumpBytes = midiEvent->SysExData->Length;
				pMidiEvent->sysexDump = new char[midiEvent->SysExData->Length];

				for(int i = 0; i < midiEvent->SysExData->Length; i++)
				{
					pMidiEvent->sysexDump[i] = (char)midiEvent->SysExData[i];
				}

				pEvents->events[index] = (::VstEvent*)pMidiEvent;
				}
				break;
			default:
				// TODO: log skipping
				break;
			}

			index++;
		}

		return pEvents;
	}

	// Cleanup method for the return value of 'AllocUnmanagedEvents'
	static void DeleteUnmanagedEvents(::VstEvents* pEvents)
	{
		for(int n = 0 ; n < pEvents->numEvents; n++)
		{
			if(pEvents->events[n]->flags == kVstSysExType)
			{
				::VstMidiSysexEvent* pMidiEvent = (::VstMidiSysexEvent*)pEvents->events[n];
				delete[] pMidiEvent->sysexDump;
			}

			delete pEvents->events[n];
		}

		delete pEvents;
	}

	// Assigns the values of the managed pinProps to the unmanaged pProps fields.
	static void ToUnmanagedPinProperties(::VstPinProperties* pProps, Jacobi::Vst::Core::VstPinProperties^ pinProps)
	{
		pProps->flags = safe_cast<VstInt32>(pinProps->Flags);
		StringToChar(pinProps->Label, pProps->label, kVstMaxLabelLen);
		StringToChar(pinProps->ShortLabel, pProps->shortLabel, kVstMaxShortLabelLen);
		pProps->arrangementType = safe_cast<VstInt32>(pinProps->ArrangementType);
	}

	// Returns the values of the unmanaged pProps as managed VstPinProperties instance.
	static Jacobi::Vst::Core::VstPinProperties^ ToManagedPinProperties(::VstPinProperties* pProps)
	{
		Jacobi::Vst::Core::VstPinProperties^ pinProps = gcnew Jacobi::Vst::Core::VstPinProperties();

		pinProps->Flags = safe_cast<Jacobi::Vst::Core::VstPinPropertiesFlags>(pProps->flags);
		pinProps->Label = CharToString(pProps->label);
		pinProps->ShortLabel = CharToString(pProps->shortLabel);
		pinProps->ArrangementType = safe_cast<Jacobi::Vst::Core::VstSpeakerArrangementType>(pProps->arrangementType);

		return pinProps;
	}

	// Converts an unmanaged speaker pArrangement to a managed VstSpeakerArrangement.
	static Jacobi::Vst::Core::VstSpeakerArrangement^ ToManagedSpeakerArrangement(::VstSpeakerArrangement* pArrangement)
	{
		Jacobi::Vst::Core::VstSpeakerArrangement^ spkArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();

		spkArr->Type = safe_cast<Jacobi::Vst::Core::VstSpeakerArrangementType>(pArrangement->type);
		spkArr->Speakers = gcnew array<Jacobi::Vst::Core::VstSpeakerProperties^>(pArrangement->numChannels);

		for(int n = 0; n < pArrangement->numChannels; n++)
		{
			::VstSpeakerProperties propSrc = pArrangement->speakers[n];

			Jacobi::Vst::Core::VstSpeakerProperties^ spkProp = gcnew Jacobi::Vst::Core::VstSpeakerProperties();
			spkProp->Azimath = propSrc.azimuth;
			spkProp->Elevation = propSrc.elevation;
			spkProp->Radius = propSrc.radius;
			spkProp->SpeakerType = safe_cast<Jacobi::Vst::Core::VstSpeakerTypes>(propSrc.type);
			spkProp->Name = CharToString(propSrc.name);

			spkArr->Speakers[n] = spkProp;
		}

		return spkArr;
	}

	// Converts a managed speaker arrangement to an unmanaged VstSpeakerArrangement.
	// delete retval
	static ::VstSpeakerArrangement* AllocUnmanagedSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement)
	{
		::VstSpeakerArrangement* pArrangement = new ::VstSpeakerArrangement();

		pArrangement->numChannels = arrangement->Speakers->Length;
		pArrangement->type = safe_cast<::VstSpeakerArrangementType>(arrangement->Type);

		// a maximum of 8 audio channels is supported in the ::VstSpeakerArrangement struct
		for (int index = 0; index < pArrangement->numChannels && index < 8; index++)
		{
			Jacobi::Vst::Core::VstSpeakerProperties^ speakerProps = arrangement->Speakers[index];

			pArrangement->speakers[index].azimuth = speakerProps->Azimath;
			pArrangement->speakers[index].elevation = speakerProps->Elevation;
			StringToChar(speakerProps->Name, pArrangement->speakers[index].name, kVstMaxNameLen);
			pArrangement->speakers[index].radius = speakerProps->Radius;
			pArrangement->speakers[index].type = safe_cast<VstInt32>(speakerProps->SpeakerType);
		}

		return pArrangement;
	}

	// Assigns the values from the managed paramProps to the unmanaged pProps fields.
	static void ToUnmanagedParameterProperties(::VstParameterProperties* pProps, Jacobi::Vst::Core::VstParameterProperties^ paramProps)
	{
		pProps->flags = safe_cast<VstInt32>(paramProps->Flags);
		pProps->stepFloat = paramProps->StepFloat;
		pProps->smallStepFloat = paramProps->SmallStepFloat;
		pProps->largeStepFloat = paramProps->LargeStepFloat;

		pProps->stepInteger = paramProps->StepInteger;
		pProps->largeStepInteger = paramProps->LargeStepInteger;
		pProps->maxInteger = paramProps->MaxInteger;
		pProps->minInteger = paramProps->MinInteger;

		pProps->displayIndex = paramProps->DisplayIndex;
		StringToChar(paramProps->Label, pProps->label, kVstMaxLabelLen);
		StringToChar(paramProps->ShortLabel, pProps->shortLabel, kVstMaxShortLabelLen);

		pProps->category = paramProps->Category;
		StringToChar(paramProps->CategoryLabel, pProps->categoryLabel, kVstMaxCategLabelLen);
		pProps->numParametersInCategory = paramProps->ParameterCountInCategory;
	}

	// converts an unmanaged pProps to a managed VstParameterProperties instance.
	static Jacobi::Vst::Core::VstParameterProperties^ ToManagedParameterProperties(::VstParameterProperties* pProps)
	{
		Jacobi::Vst::Core::VstParameterProperties^ paramProps = gcnew Jacobi::Vst::Core::VstParameterProperties();

		paramProps->Flags = safe_cast<Jacobi::Vst::Core::VstParameterPropertiesFlags>(pProps->flags);
		paramProps->StepFloat = pProps->stepFloat;
		paramProps->SmallStepFloat = pProps->smallStepFloat;
		paramProps->LargeStepFloat = pProps->largeStepFloat;

		paramProps->StepInteger = pProps->stepInteger;
		paramProps->LargeStepInteger = pProps->largeStepInteger;
		paramProps->MaxInteger = pProps->maxInteger;
		paramProps->MinInteger = pProps->minInteger;

		paramProps->DisplayIndex = pProps->displayIndex;
		paramProps->Label = CharToString(pProps->label);
		paramProps->ShortLabel = CharToString(pProps->shortLabel);

		paramProps->Category = pProps->category;
		paramProps->CategoryLabel = CharToString(pProps->categoryLabel);
		paramProps->ParameterCountInCategory = pProps->numParametersInCategory;

		return paramProps;
	}

	// Assigns the values of the managed midiProgName to the unmanaged pProgName fields.
	static void ToUnmanagedMidiProgramName(::MidiProgramName* pProgName, Jacobi::Vst::Core::VstMidiProgramName^ midiProgName)
	{
		pProgName->thisProgramIndex = midiProgName->CurrentProgramIndex;
		pProgName->flags = safe_cast<VstInt32>(midiProgName->Flags);
		pProgName->midiBankLsb = safe_cast<char>(midiProgName->MidiBankLSB);
		pProgName->midiBankMsb = safe_cast<char>(midiProgName->MidiBankMSB);
		pProgName->midiProgram = safe_cast<char>(midiProgName->MidiProgram);
		StringToChar(midiProgName->Name, pProgName->name, kVstMaxNameLen);
		pProgName->parentCategoryIndex = midiProgName->ParentCategoryIndex;
	}

	// Assigns the values of the unmanaged pProgName to the managed midiProgName
	static void ToManagedMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgName, ::MidiProgramName* pProgName)
	{
		midiProgName->CurrentProgramIndex = pProgName->thisProgramIndex;
		midiProgName->Flags = safe_cast<Jacobi::Vst::Core::VstMidiProgramNameFlags>(pProgName->flags);
		midiProgName->MidiBankLSB = pProgName->midiBankLsb;
		midiProgName->MidiBankMSB = pProgName->midiBankMsb;
		midiProgName->MidiProgram = pProgName->midiProgram;
		midiProgName->Name = CharToString(pProgName->name);
		midiProgName->ParentCategoryIndex = pProgName->parentCategoryIndex;
	}

	// Assigns the values of the managed midiProgCat to the unmanaged pProgCat fields.
	static void ToUnmanagedMidiProgramCategory(::MidiProgramCategory* pProgCat, Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat)
	{
		pProgCat->thisCategoryIndex = midiProgCat->CurrentCategoryIndex;
		pProgCat->parentCategoryIndex = midiProgCat->ParentCategoryIndex;
		StringToChar(midiProgCat->Name, pProgCat->name, kVstMaxNameLen);
	}

	// Assigns the values of the unmanaged pProgCat to the managed midiProgCat fields.
	static void ToManagedMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat, ::MidiProgramCategory* pProgCat)
	{
		midiProgCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
		midiProgCat->ParentCategoryIndex = pProgCat->parentCategoryIndex;
		midiProgCat->Name = CharToString(pProgCat->name);
	}

	// Converts the unmanaged pChunkInfo to a managed VstPatchChunkInfo.
	static Jacobi::Vst::Core::VstPatchChunkInfo^ ToManagedPatchChunkInfo(::VstPatchChunkInfo* pChunkInfo)
	{
		Jacobi::Vst::Core::VstPatchChunkInfo^ patchChunkInfo = 
			gcnew Jacobi::Vst::Core::VstPatchChunkInfo(
				pChunkInfo->version,
				pChunkInfo->pluginUniqueID,
				pChunkInfo->pluginVersion,
				pChunkInfo->numElements);

		return patchChunkInfo;
	}

	// Assigns the field values of the managed chunkInfo to the unmanaged pChunkInfo fields.
	static void ToUnmanagedPatchChunkInfo(::VstPatchChunkInfo* pChunkInfo, Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		pChunkInfo->version = chunkInfo->Version;
		pChunkInfo->pluginUniqueID = chunkInfo->PluginID;
		pChunkInfo->pluginVersion = chunkInfo->PluginVersion;
		pChunkInfo->numElements = chunkInfo->ElementCount;
	}

	// Converts the unmanaged pTimeInfo to the managed VstTimeInfo.
	static Jacobi::Vst::Core::VstTimeInfo^ ToManagedTimeInfo(::VstTimeInfo* pTimeInfo)
	{
		Jacobi::Vst::Core::VstTimeInfo^ timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();

		timeInfo->BarStartPosition = pTimeInfo->barStartPos;
		timeInfo->CycleStartPosition = pTimeInfo->cycleStartPos;
		timeInfo->CycleEndPosition = pTimeInfo->cycleEndPos;
		timeInfo->Flags = safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(pTimeInfo->flags);
		timeInfo->NanoSeconds = pTimeInfo->nanoSeconds;
		timeInfo->PpqPosition = pTimeInfo->ppqPos;
		timeInfo->SamplePosition = pTimeInfo->samplePos;
		timeInfo->SampleRate = pTimeInfo->sampleRate;
		timeInfo->SamplesToNearestClock = pTimeInfo->samplesToNextClock;
		timeInfo->SmpteFrameRate = safe_cast<Jacobi::Vst::Core::VstSmpteFrameRate>(pTimeInfo->smpteFrameRate);
		timeInfo->SmpteOffset = pTimeInfo->smpteOffset;
		timeInfo->Tempo = pTimeInfo->tempo;
		timeInfo->TimeSignatureDenominator = pTimeInfo->timeSigDenominator;
		timeInfo->TimeSignatureNumerator = pTimeInfo->timeSigNumerator;

		return timeInfo;
	}

	// assigns the field values of the managed timeInfo to the unmanaged pTimeInfo fields.
	static void ToUnmanagedTimeInfo(::VstTimeInfo* pTimeInfo, Jacobi::Vst::Core::VstTimeInfo^ timeInfo)
	{
		pTimeInfo->barStartPos = timeInfo->BarStartPosition;
		pTimeInfo->cycleStartPos = timeInfo->CycleStartPosition;
		pTimeInfo->cycleEndPos = timeInfo->CycleEndPosition;
		pTimeInfo->flags = safe_cast<VstInt32>(timeInfo->Flags);
		pTimeInfo->nanoSeconds = timeInfo->NanoSeconds;
		pTimeInfo->ppqPos = timeInfo->PpqPosition;
		pTimeInfo->samplePos = timeInfo->SamplePosition;
		pTimeInfo->sampleRate = timeInfo->SampleRate;
		pTimeInfo->samplesToNextClock = timeInfo->SamplesToNearestClock;
		pTimeInfo->smpteFrameRate = safe_cast<VstInt32>(timeInfo->SmpteFrameRate);
		pTimeInfo->smpteOffset = timeInfo->SmpteOffset;
		pTimeInfo->tempo = timeInfo->Tempo;
		pTimeInfo->timeSigDenominator = timeInfo->TimeSignatureDenominator;
		pTimeInfo->timeSigNumerator = timeInfo->TimeSignatureNumerator;
	}

	// Converts the unmanaged sample buffer to a managed VstAudioBuffer array.
	static array<Jacobi::Vst::Core::VstAudioBuffer^>^ ToManagedAudioBufferArray(float** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

	// Converts the unmanaged sample buffer to a managed VstAudioPrecisionBuffer array.
	static array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ ToManagedAudioBufferArray(double** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioPrecisionBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

	// Call DeleteFileSelect on retval
	static ::VstFileSelect* AllocUnmanagedFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
	{
		::VstFileSelect* pFileSelect = new ::VstFileSelect();
		
		// clear structure
		ZeroMemory(pFileSelect, sizeof(::VstFileSelect));

		// keep track of unmanaged memory for call to closeFileSelector
		fileSelect->Reserved = System::IntPtr(pFileSelect);

		pFileSelect->command = safe_cast<VstInt32>(fileSelect->Command);
		pFileSelect->initialPath = AllocateString(fileSelect->InitialPath);
		
		pFileSelect->nbFileTypes = fileSelect->FileTypes->Length;
		pFileSelect->fileTypes = new ::VstFileType[fileSelect->FileTypes->Length];
		
		// clear allocated file type structures
		ZeroMemory(pFileSelect->fileTypes, fileSelect->FileTypes->Length * sizeof(::VstFileType));

		// copy file type array
		for(int index = 0; index < fileSelect->FileTypes->Length; index++)
		{
			StringToChar(fileSelect->FileTypes[index]->Extension, pFileSelect->fileTypes[index].dosType, 8);
			StringToChar(fileSelect->FileTypes[index]->Name, pFileSelect->fileTypes[index].name, 128);
		}

		return pFileSelect;
	}

	// creates a managed VstFileSelect from the unmanaged pFileSelect
	static Jacobi::Vst::Core::VstFileSelect^ ToManagedFileSelect(::VstFileSelect* pFileSelect)
	{
		Jacobi::Vst::Core::VstFileSelect^ fileSelect = gcnew Jacobi::Vst::Core::VstFileSelect();
		// not really needed, but gives the Host a chance to work with MIME info. No harm when overwritten.
		fileSelect->Reserved = System::IntPtr(pFileSelect);
		
		// create a strong handle to the managed VstFileSelect instance to keep its lifetime coupled with the unmanaged one.
		System::Runtime::InteropServices::GCHandle fsHandle = 
			System::Runtime::InteropServices::GCHandle::Alloc(fileSelect);
		
		// couple our managed instance to our unmanaged instance
		pFileSelect->reserved = (VstIntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(fsHandle).ToPointer();

		fileSelect->Command = safe_cast<Jacobi::Vst::Core::VstFileSelectCommand>(pFileSelect->command);
		fileSelect->InitialPath = CharToString(pFileSelect->initialPath);
		fileSelect->FileTypes = gcnew array<Jacobi::Vst::Core::VstFileType^>(pFileSelect->nbFileTypes);

		// copy file types
		for(int index = 0; index < pFileSelect->nbFileTypes; index++)
		{
			fileSelect->FileTypes[index]->Extension = CharToString(pFileSelect->fileTypes[index].dosType);
			fileSelect->FileTypes[index]->Name = CharToString(pFileSelect->fileTypes[index].name);
		}

		return fileSelect;
	}

	// retrieves the managed VstFileSelect instance stored in the reserved field of the unmanaged pFileSelect.
	static Jacobi::Vst::Core::VstFileSelect^ GetManagedFileSelect(::VstFileSelect* pFileSelect)
	{
		if(pFileSelect != NULL && pFileSelect->reserved != 0)
		{
			return safe_cast<Jacobi::Vst::Core::VstFileSelect^>(
				System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pFileSelect->reserved)).Target);
		}

		return nullptr;
	}

	// updates the unmanaged pFileSelect with output information set by the managed host (allocates unmanaged resources).
	static void AllocUpdateUnmanagedFileSelect(::VstFileSelect* pFileSelect, Jacobi::Vst::Core::VstFileSelect^ fileSelect)
	{
		if(pFileSelect->command == ::kVstMultipleFilesLoad)
		{
			// allocate the pointers slots
			pFileSelect->returnMultiplePaths = new char*[fileSelect->ReturnPaths->Length];
			pFileSelect->nbReturnPath = fileSelect->ReturnPaths->Length;

			for(int index = 0; index < fileSelect->ReturnPaths->Length; index++)
			{
				pFileSelect->returnMultiplePaths[index] = AllocateString(fileSelect->ReturnPaths[index]);
			}
		}
		else if(fileSelect->ReturnPaths != nullptr && fileSelect->ReturnPaths->Length > 0)
		{
			pFileSelect->returnPath = AllocateString(fileSelect->ReturnPaths[0]);
			pFileSelect->sizeReturnPath = strlen(pFileSelect->returnPath) + 1;
		}
	}

	// updates the managed VstFileSelect with output information set by the host.
	static void UpdateManagedFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect, ::VstFileSelect* pFileSelect)
	{
		if(pFileSelect->returnPath != NULL)
		{
			fileSelect->ReturnPaths = gcnew array<System::String^>(1);
			fileSelect->ReturnPaths[0] = CharToString(pFileSelect->returnPath);
		}
		else if(pFileSelect->returnMultiplePaths != NULL)
		{
			fileSelect->ReturnPaths = gcnew array<System::String^>(pFileSelect->nbReturnPath);

			for(int index = 0; index < pFileSelect->nbReturnPath; index++)
			{
				fileSelect->ReturnPaths[index] = CharToString(pFileSelect->returnMultiplePaths[index]);
			}
		}
	}

	// frees the unmanaged memory allocated by 'AllocUnmanagedFileSelect'
	static void DeleteUnmanagedFileSelect(::VstFileSelect* pFileSelect)
	{
		if(pFileSelect->initialPath != NULL)
		{
			DeallocateString(pFileSelect->initialPath);
			pFileSelect->initialPath = NULL;
		}

		if(pFileSelect->fileTypes != NULL)
		{
			delete[] pFileSelect->fileTypes;
			pFileSelect->fileTypes = NULL;
		}

		delete pFileSelect;
	}

	// frees the unmanaged resources allocated by 'AllocUpdateUnmanagedFileSelect'. Also releases the managed instance.
	static void DeleteUpdateUnmanagedFileSelect(::VstFileSelect* pFileSelect)
	{
		if(pFileSelect->returnMultiplePaths != NULL)
		{
			for(int index = 0; index < pFileSelect->nbReturnPath; index++)
			{
				DeallocateString(pFileSelect->returnMultiplePaths[index]);
			}

			delete [] pFileSelect->returnMultiplePaths;
			pFileSelect->returnMultiplePaths = NULL;
			pFileSelect->nbReturnPath = 0;
		}

		if(pFileSelect->returnPath != NULL)
		{
			DeallocateString(pFileSelect->returnPath);
			pFileSelect->returnPath = NULL;
			pFileSelect->sizeReturnPath = 0;
		}

		if(pFileSelect->reserved != 0)
		{
			// release the managed instance
			System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pFileSelect->reserved)).Free();
			pFileSelect->reserved = 0;
		}
	}

private:
	TypeConverter(){}
};

}}} // Jacobi::Vst::Interop