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

	// Converts a managed rect to an unmanaged Vst2Rectangle*
	static void ToUnmanagedRectangle(Vst2Rectangle* pRect, System::Drawing::Rectangle rect)
	{
		pRect->top = rect.Top;
		pRect->left = rect.Left;
		pRect->bottom = rect.Bottom;
		pRect->right = rect.Right;
	}

	// converts an unmanaged Vst2Rectangle* to a Rectangle.
	static System::Drawing::Rectangle ToManagedRectangle(Vst2Rectangle* pRect)
	{
		System::Drawing::Rectangle rect(pRect->top, pRect->left, pRect->right, pRect->bottom);

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
	static array<Jacobi::Vst::Core::VstEvent^>^ ToManagedEventArray(Vst2Events* pEvents)
	{
		array<Jacobi::Vst::Core::VstEvent^>^ eventArray = gcnew array<Jacobi::Vst::Core::VstEvent^>(pEvents->eventCount);

		for(int n = 0; n < pEvents->eventCount; n++)
		{
			::Vst2Event* pEvent = pEvents->events[n];

			switch(pEvent->kind)
			{
			case Vst2EventKind::Midi:
			{
				::Vst2MidiEvent* pMidiEvent = (::Vst2MidiEvent*)pEvent;

				array<System::Byte>^ midiData = gcnew array<System::Byte>(4);
				midiData[0] = pMidiEvent->midiData[0];
				midiData[1] = pMidiEvent->midiData[1];
				midiData[2] = pMidiEvent->midiData[2];

				Jacobi::Vst::Core::VstMidiEvent^ midiEvent = gcnew Jacobi::Vst::Core::VstMidiEvent(
					pMidiEvent->deltaFrames,
					pMidiEvent->noteLength, pMidiEvent->noteOffset, midiData, pMidiEvent->detune, pMidiEvent->noteOffVelocity,
					pMidiEvent->flags == Vst2MidiEventFlags::IsRealTime);

				eventArray[n] = midiEvent;
			}	break;
			case Vst2EventKind::SystemExclusive:
			{
				::Vst2MidiSysExEvent* pMidiEvent = (::Vst2MidiSysExEvent*)pEvent;

				// copy sysex data into managed buffer
				array<System::Byte>^ midiData = gcnew array<System::Byte>(pMidiEvent->dumpInBytes);
				for(int i = 0; i < pMidiEvent->dumpInBytes; i++)
				{
					midiData[i] = pMidiEvent->dump[i];
				}

				Jacobi::Vst::Core::VstMidiSysExEvent^ midiEvent =
					gcnew Jacobi::Vst::Core::VstMidiSysExEvent(pMidiEvent->deltaFrames, midiData);

				eventArray[n] = midiEvent;
			}	break;
			default:
			{
				// deprecated event types support
				// subtract deltaFrames and flags fields from byteSize
				int length = pEvent->sizeInBytes - (2 * sizeof(int32_t));

				array<System::Byte>^ data = gcnew array<System::Byte>(length);
				for(int i = 0; i < length; i++)
				{
					data[i] = pEvent->data[i];
				}

				Jacobi::Vst::Core::Legacy::VstGenericEvent^ genericEvent =
					gcnew Jacobi::Vst::Core::Legacy::VstGenericEvent(
						safe_cast<Jacobi::Vst::Core::VstEventTypes>(pEvent->kind), pEvent->deltaFrames, data);

				eventArray[n] = genericEvent;
			}	break;
			}
		}

		return eventArray;
	}

	// Converts a managed VstEvent array to an unmanaged VstEvent array.
	// Call DeleteVstEvents on retval.
	static ::Vst2Events* AllocUnmanagedEvents(array<Jacobi::Vst::Core::VstEvent^>^ events)
	{
		int length = events->Length;
		if(length > 2) length -= 2;

		int totalLength = sizeof(Vst2Events) + (length * sizeof(Vst2Event*));

		::Vst2Events* pEvents = (::Vst2Events*)new char[totalLength];
		ZeroMemory(pEvents, totalLength);

		pEvents->eventCount = events->Length;

		int index = 0;
		for each(Jacobi::Vst::Core::VstEvent^ evnt in events)
		{
			switch(evnt->EventType)
			{
			case Jacobi::Vst::Core::VstEventTypes::MidiEvent:
			{
				Jacobi::Vst::Core::VstMidiEvent^ midiEvent = (Jacobi::Vst::Core::VstMidiEvent^)evnt;
				::Vst2MidiEvent* pMidiEvent = new ::Vst2MidiEvent();

				pMidiEvent->sizeInBytes = sizeof(::Vst2MidiEvent);
				pMidiEvent->flags = Vst2MidiEventFlags::None;
				pMidiEvent->deltaFrames = midiEvent->DeltaFrames;
				pMidiEvent->kind = (Vst2EventKind)midiEvent->EventType;

				for(int i = 0; i < midiEvent->Data->Length && i < 4; i++)
				{
					pMidiEvent->midiData[i] = midiEvent->Data[i];
				}

				pMidiEvent->detune = (char)midiEvent->Detune;
				pMidiEvent->noteLength = midiEvent->NoteLength;
				pMidiEvent->noteOffset = midiEvent->NoteOffset;
				pMidiEvent->noteOffVelocity = midiEvent->NoteOffVelocity;
				pMidiEvent->flags = midiEvent->IsRealtime ? Vst2MidiEventFlags::IsRealTime : Vst2MidiEventFlags::None;

				pEvents->events[index] = (::Vst2Event*)pMidiEvent;
			}	break;
			case Jacobi::Vst::Core::VstEventTypes::MidiSysExEvent:
			{
				Jacobi::Vst::Core::VstMidiSysExEvent^ midiEvent = (Jacobi::Vst::Core::VstMidiSysExEvent^)evnt;
				::Vst2MidiSysExEvent* pMidiEvent = new ::Vst2MidiSysExEvent();

				pMidiEvent->sizeInBytes = sizeof(::Vst2MidiSysExEvent);
				pMidiEvent->flags = 0;
				pMidiEvent->deltaFrames = midiEvent->DeltaFrames;
				pMidiEvent->kind = (Vst2EventKind)midiEvent->EventType;

				pMidiEvent->dumpInBytes = midiEvent->Data->Length;
				pMidiEvent->dump = new char[midiEvent->Data->Length];

				for(int i = 0; i < midiEvent->Data->Length; i++)
				{
					pMidiEvent->dump[i] = (char)midiEvent->Data[i];
				}

				pEvents->events[index] = (::Vst2Event*)pMidiEvent;
			}	break;
			default:
			{
				// deprecated event types support
				Jacobi::Vst::Core::Legacy::VstGenericEvent^ genericEvent = (Jacobi::Vst::Core::Legacy::VstGenericEvent^)evnt;
				// incl. deltaFrames and flags
				int dataLength = genericEvent->Data->Length + (2 * sizeof(int32_t));
				// incl.  type and byteSize
				int structLength = dataLength + (2 * sizeof(int32_t));

				::Vst2Event* pEvent = (::Vst2Event*)new char[structLength];

				pEvent->kind = safe_cast<Vst2EventKind>(genericEvent->EventType);
				pEvent->sizeInBytes = dataLength;
				pEvent->deltaFrames = genericEvent->DeltaFrames;
				pEvent->flags = 0;

				for(int i = 0; i < genericEvent->Data->Length; i++)
				{
					pEvent->data[i] = genericEvent->Data[i];
				}

				pEvents->events[index] = pEvent;
			}	break;
			}

			index++;
		}

		return pEvents;
	}

	// Cleanup method for the return value of 'AllocUnmanagedEvents'
	static void DeleteUnmanagedEvents(::Vst2Events* pEvents)
	{
		for(int n = 0 ; n < pEvents->eventCount; n++)
		{
			if(pEvents->events[n]->kind == Vst2EventKind::SystemExclusive)
			{
				// delete the sysex buffer
				::Vst2MidiSysExEvent* pMidiEvent = (::Vst2MidiSysExEvent*)pEvents->events[n];
				delete[] pMidiEvent->dump;

				// delete the event
				delete pEvents->events[n];
			}
			else if(pEvents->events[n]->kind == Vst2EventKind::Midi)
			{
				// delete the midi event
				delete pEvents->events[n];
			}
			else // deprecated generic events
			{
				delete[] pEvents->events[n];
			}
		}

		// delete the array of events
		delete[] pEvents;
	}

	// Assigns the values of the managed pinProps to the unmanaged pProps fields.
	static void ToUnmanagedPinProperties(::Vst2PinProperties* pProps, Jacobi::Vst::Core::VstPinProperties^ pinProps)
	{
		pProps->flags = safe_cast<Vst2PinPropertiesFlags>(pinProps->Flags);
		StringToChar(pinProps->Label, pProps->label, Vst2MaxLabelLen);
		StringToChar(pinProps->ShortLabel, pProps->shortLabel, Vst2MaxShortLabelLen);
		pProps->arrangementKind = safe_cast<Vst2SpeakerArrangementKind>(pinProps->ArrangementType);
	}

	// Returns the values of the unmanaged pProps as managed VstPinProperties instance.
	static Jacobi::Vst::Core::VstPinProperties^ ToManagedPinProperties(::Vst2PinProperties* pProps)
	{
		Jacobi::Vst::Core::VstPinProperties^ pinProps = gcnew Jacobi::Vst::Core::VstPinProperties();

		pinProps->Flags = safe_cast<Jacobi::Vst::Core::VstPinPropertiesFlags>(pProps->flags);
		pinProps->Label = CharToString(pProps->label);
		pinProps->ShortLabel = CharToString(pProps->shortLabel);
		pinProps->ArrangementType = safe_cast<Jacobi::Vst::Core::VstSpeakerArrangementType>(pProps->arrangementKind);

		return pinProps;
	}

	// Converts an unmanaged speaker pArrangement to a managed VstSpeakerArrangement.
	static Jacobi::Vst::Core::VstSpeakerArrangement^ ToManagedSpeakerArrangement(::Vst2SpeakerArrangement* pArrangement)
	{
		Jacobi::Vst::Core::VstSpeakerArrangement^ spkArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();

		spkArr->Type = safe_cast<Jacobi::Vst::Core::VstSpeakerArrangementType>(pArrangement->kind);
		spkArr->Speakers = gcnew array<Jacobi::Vst::Core::VstSpeakerProperties^>(pArrangement->channelCount);

		for(int n = 0; n < pArrangement->channelCount; n++)
		{
			::Vst2SpeakerProperties propSrc = pArrangement->speakers[n];

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
	static ::Vst2SpeakerArrangement* AllocUnmanagedSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement)
	{
		::Vst2SpeakerArrangement* pArrangement = new ::Vst2SpeakerArrangement();

		ToUnmanagedSpeakerArrangement(pArrangement, arrangement);

		return pArrangement;
	}

	// copies the values from the managed arrangenment to the unmanaged pArrangement.
	static void ToUnmanagedSpeakerArrangement(::Vst2SpeakerArrangement* pArrangement, Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement)
	{
		pArrangement->channelCount = arrangement->Speakers->Length;
		pArrangement->kind = safe_cast<::Vst2SpeakerArrangementKind>(arrangement->Type);

		// a maximum of 8 audio channels is supported in the ::Vst2SpeakerArrangement struct
		for (int index = 0; index < pArrangement->channelCount && index < 8; index++)
		{
			Jacobi::Vst::Core::VstSpeakerProperties^ speakerProps = arrangement->Speakers[index];

			pArrangement->speakers[index].azimuth = speakerProps->Azimath;
			pArrangement->speakers[index].elevation = speakerProps->Elevation;
			StringToChar(speakerProps->Name, pArrangement->speakers[index].name, Vst2MaxNameLen);
			pArrangement->speakers[index].radius = speakerProps->Radius;
			pArrangement->speakers[index].type = safe_cast<Vst2SpeakerType>(speakerProps->SpeakerType);
		}
	}

	// Assigns the values from the managed paramProps to the unmanaged pProps fields.
	static void ToUnmanagedParameterProperties(::Vst2ParameterProperties* pProps, Jacobi::Vst::Core::VstParameterProperties^ paramProps)
	{
		pProps->flags = safe_cast<Vst2ParameterFlags>(paramProps->Flags);
		pProps->stepFloat = paramProps->StepFloat;
		pProps->smallStepFloat = paramProps->SmallStepFloat;
		pProps->largeStepFloat = paramProps->LargeStepFloat;

		pProps->stepInteger = paramProps->StepInteger;
		pProps->largeStepInteger = paramProps->LargeStepInteger;
		pProps->maxInteger = paramProps->MaxInteger;
		pProps->minInteger = paramProps->MinInteger;

		pProps->displayIndex = paramProps->DisplayIndex;
		StringToChar(paramProps->Label, pProps->label, Vst2MaxLabelLen);
		StringToChar(paramProps->ShortLabel, pProps->shortLabel, Vst2MaxShortLabelLen);

		pProps->category = paramProps->Category;
		StringToChar(paramProps->CategoryLabel, pProps->categoryLabel, Vst2MaxCategLabelLen);
		pProps->numParametersInCategory = paramProps->ParameterCountInCategory;
	}

	// converts an unmanaged pProps to a managed VstParameterProperties instance.
	static Jacobi::Vst::Core::VstParameterProperties^ ToManagedParameterProperties(::Vst2ParameterProperties* pProps)
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
	static void ToUnmanagedMidiProgramName(::Vst2MidiProgramName* pProgName, Jacobi::Vst::Core::VstMidiProgramName^ midiProgName)
	{
		pProgName->thisProgramIndex = midiProgName->CurrentProgramIndex;
		pProgName->flags = safe_cast<Vst2MidiProgramNameFlags>(midiProgName->Flags);
		pProgName->midiBankLsb = safe_cast<char>(midiProgName->MidiBankLSB);
		pProgName->midiBankMsb = safe_cast<char>(midiProgName->MidiBankMSB);
		pProgName->midiProgram = safe_cast<char>(midiProgName->MidiProgram);
		StringToChar(midiProgName->Name, pProgName->name, Vst2MaxNameLen);
		pProgName->parentCategoryIndex = midiProgName->ParentCategoryIndex;
	}

	// Assigns the values of the unmanaged pProgName to the managed midiProgName
	static void ToManagedMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgName, ::Vst2MidiProgramName* pProgName)
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
	static void ToUnmanagedMidiProgramCategory(::Vst2MidiProgramCategory* pProgCat, Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat)
	{
		pProgCat->thisCategoryIndex = midiProgCat->CurrentCategoryIndex;
		pProgCat->parentCategoryIndex = midiProgCat->ParentCategoryIndex;
		StringToChar(midiProgCat->Name, pProgCat->name, Vst2MaxNameLen);
	}

	// Assigns the values of the unmanaged pProgCat to the managed midiProgCat fields.
	static void ToManagedMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat, ::Vst2MidiProgramCategory* pProgCat)
	{
		midiProgCat->CurrentCategoryIndex = pProgCat->thisCategoryIndex;
		midiProgCat->ParentCategoryIndex = pProgCat->parentCategoryIndex;
		midiProgCat->Name = CharToString(pProgCat->name);
	}

	// Converts the unmanaged pChunkInfo to a managed VstPatchChunkInfo.
	static Jacobi::Vst::Core::VstPatchChunkInfo^ ToManagedPatchChunkInfo(::Vst2PatchChunkInfo* pChunkInfo)
	{
		Jacobi::Vst::Core::VstPatchChunkInfo^ patchChunkInfo =
			gcnew Jacobi::Vst::Core::VstPatchChunkInfo(
				pChunkInfo->version,
				pChunkInfo->pluginUniqueID,
				pChunkInfo->pluginVersion,
				pChunkInfo->elementCount);

		return patchChunkInfo;
	}

	// Assigns the field values of the managed chunkInfo to the unmanaged pChunkInfo fields.
	static void ToUnmanagedPatchChunkInfo(::Vst2PatchChunkInfo* pChunkInfo, Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo)
	{
		pChunkInfo->version = chunkInfo->Version;
		pChunkInfo->pluginUniqueID = chunkInfo->PluginID;
		pChunkInfo->pluginVersion = chunkInfo->PluginVersion;
		pChunkInfo->elementCount = chunkInfo->ElementCount;
	}

	// Assigns the field values of the unmanaged pTimeInfo to the managed VstTimeInfo.
	static void ToManagedTimeInfo(Jacobi::Vst::Core::VstTimeInfo^ timeInfo, ::Vst2TimeInfo* pTimeInfo)
	{
		timeInfo->BarStartPosition = pTimeInfo->barStartPosition;
		timeInfo->CycleStartPosition = pTimeInfo->cycleStartPosition;
		timeInfo->CycleEndPosition = pTimeInfo->cycleEndPosition;
		timeInfo->Flags = safe_cast<Jacobi::Vst::Core::VstTimeInfoFlags>(pTimeInfo->flags);
		timeInfo->NanoSeconds = pTimeInfo->nanoSeconds;
		timeInfo->PpqPosition = pTimeInfo->ppqPosition;
		timeInfo->SamplePosition = pTimeInfo->samplePosition;
		timeInfo->SampleRate = pTimeInfo->sampleRate;
		timeInfo->SamplesToNearestClock = pTimeInfo->sampleCountToNextClock;
		timeInfo->SmpteFrameRate = safe_cast<Jacobi::Vst::Core::VstSmpteFrameRate>(pTimeInfo->smpteFrameRate);
		timeInfo->SmpteOffset = pTimeInfo->smpteOffset;
		timeInfo->Tempo = pTimeInfo->tempo;
		timeInfo->TimeSignatureDenominator = pTimeInfo->timeSigDenominator;
		timeInfo->TimeSignatureNumerator = pTimeInfo->timeSigNumerator;
	}

	// assigns the field values of the managed timeInfo to the unmanaged pTimeInfo fields.
	static void ToUnmanagedTimeInfo(::Vst2TimeInfo* pTimeInfo, Jacobi::Vst::Core::VstTimeInfo^ timeInfo)
	{
		pTimeInfo->barStartPosition = timeInfo->BarStartPosition;
		pTimeInfo->cycleStartPosition = timeInfo->CycleStartPosition;
		pTimeInfo->cycleEndPosition = timeInfo->CycleEndPosition;
		pTimeInfo->flags = safe_cast<Vst2TimeInfoFlags>(timeInfo->Flags);
		pTimeInfo->nanoSeconds = timeInfo->NanoSeconds;
		pTimeInfo->ppqPosition = timeInfo->PpqPosition;
		pTimeInfo->samplePosition = timeInfo->SamplePosition;
		pTimeInfo->sampleRate = timeInfo->SampleRate;
		pTimeInfo->sampleCountToNextClock = timeInfo->SamplesToNearestClock;
		pTimeInfo->smpteFrameRate = safe_cast<Vst2SmpteFrameRate>(timeInfo->SmpteFrameRate);
		pTimeInfo->smpteOffset = timeInfo->SmpteOffset;
		pTimeInfo->tempo = timeInfo->Tempo;
		pTimeInfo->timeSigDenominator = timeInfo->TimeSignatureDenominator;
		pTimeInfo->timeSigNumerator = timeInfo->TimeSignatureNumerator;
	}

	// call delete on retval.
	static ::Vst2TimeInfo* AllocUnmanagedTimeInfo(Jacobi::Vst::Core::VstTimeInfo^ timeInfo)
	{
		::Vst2TimeInfo* pTimeInfo = new ::Vst2TimeInfo();

		ZeroMemory(pTimeInfo, sizeof(::Vst2TimeInfo));

		ToUnmanagedTimeInfo(pTimeInfo, timeInfo);

		return pTimeInfo;
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
	static ::Vst2FileSelect* AllocUnmanagedFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
	{
		::Vst2FileSelect* pFileSelect = new ::Vst2FileSelect();

		// clear structure
		ZeroMemory(pFileSelect, sizeof(::Vst2FileSelect));

		// keep track of unmanaged memory for call to closeFileSelector
		fileSelect->Reserved = System::IntPtr(pFileSelect);

		pFileSelect->command = safe_cast<Vst2FileSelectCommand>(fileSelect->Command);
		pFileSelect->initialPath = AllocateString(fileSelect->InitialPath);

		pFileSelect->fileTypesLength = fileSelect->FileTypes->Length;
		pFileSelect->fileTypes = new ::Vst2FileType[fileSelect->FileTypes->Length];

		// clear allocated file type structures
		ZeroMemory(pFileSelect->fileTypes, fileSelect->FileTypes->Length * sizeof(::Vst2FileType));

		// copy file type array
		for(int index = 0; index < fileSelect->FileTypes->Length; index++)
		{
			StringToChar(fileSelect->FileTypes[index]->Extension, pFileSelect->fileTypes[index].dosType, 8);
			StringToChar(fileSelect->FileTypes[index]->Name, pFileSelect->fileTypes[index].name, 128);
		}

		return pFileSelect;
	}

	// creates a managed VstFileSelect from the unmanaged pFileSelect
	static Jacobi::Vst::Core::VstFileSelect^ ToManagedFileSelect(::Vst2FileSelect* pFileSelect)
	{
		Jacobi::Vst::Core::VstFileSelect^ fileSelect = gcnew Jacobi::Vst::Core::VstFileSelect();
		// not really needed, but gives the Host a chance to work with MIME info. No harm when overwritten.
		fileSelect->Reserved = System::IntPtr(pFileSelect);

		// create a strong handle to the managed VstFileSelect instance to keep its lifetime coupled with the unmanaged one.
		System::Runtime::InteropServices::GCHandle fsHandle =
			System::Runtime::InteropServices::GCHandle::Alloc(fileSelect);

		// couple our managed instance to our unmanaged instance
		pFileSelect->reserved = (Vst2IntPtr)System::Runtime::InteropServices::GCHandle::ToIntPtr(fsHandle).ToPointer();

		fileSelect->Command = safe_cast<Jacobi::Vst::Core::VstFileSelectCommand>(pFileSelect->command);
		fileSelect->InitialPath = CharToString(pFileSelect->initialPath);
		fileSelect->FileTypes = gcnew array<Jacobi::Vst::Core::VstFileType^>(pFileSelect->fileTypesLength);

		// copy file types
		for(int index = 0; index < pFileSelect->fileTypesLength; index++)
		{
			fileSelect->FileTypes[index]->Extension = CharToString(pFileSelect->fileTypes[index].dosType);
			fileSelect->FileTypes[index]->Name = CharToString(pFileSelect->fileTypes[index].name);
		}

		return fileSelect;
	}

	// retrieves the managed VstFileSelect instance stored in the reserved field of the unmanaged pFileSelect.
	static Jacobi::Vst::Core::VstFileSelect^ GetManagedFileSelect(::Vst2FileSelect* pFileSelect)
	{
		if(pFileSelect != NULL && pFileSelect->reserved != 0)
		{
			return safe_cast<Jacobi::Vst::Core::VstFileSelect^>(
				System::Runtime::InteropServices::GCHandle::FromIntPtr(System::IntPtr(pFileSelect->reserved)).Target);
		}

		return nullptr;
	}

	// updates the unmanaged pFileSelect with output information set by the managed host (allocates unmanaged resources).
	static void AllocUpdateUnmanagedFileSelect(::Vst2FileSelect* pFileSelect, Jacobi::Vst::Core::VstFileSelect^ fileSelect)
	{
		if(pFileSelect->command == Vst2FileSelectCommand::MultipleFilesLoad)
		{
			// allocate the pointers slots
			pFileSelect->returnMultiplePaths = new char*[fileSelect->ReturnPaths->Length];
			pFileSelect->returnPathLength = fileSelect->ReturnPaths->Length;

			for(int index = 0; index < fileSelect->ReturnPaths->Length; index++)
			{
				pFileSelect->returnMultiplePaths[index] = AllocateString(fileSelect->ReturnPaths[index]);
			}
		}
		else if(fileSelect->ReturnPaths != nullptr && fileSelect->ReturnPaths->Length > 0)
		{
			pFileSelect->returnPath = AllocateString(fileSelect->ReturnPaths[0]);
			pFileSelect->sizeReturnPath = (::int32_t)strlen(pFileSelect->returnPath) + 1;
		}
	}

	// updates the managed VstFileSelect with output information set by the host.
	static void UpdateManagedFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect, ::Vst2FileSelect* pFileSelect)
	{
		if(pFileSelect->returnPath != NULL)
		{
			fileSelect->ReturnPaths = gcnew array<System::String^>(1);
			fileSelect->ReturnPaths[0] = CharToString(pFileSelect->returnPath);
		}
		else if(pFileSelect->returnMultiplePaths != NULL)
		{
			fileSelect->ReturnPaths = gcnew array<System::String^>(pFileSelect->returnPathLength);

			for(int index = 0; index < pFileSelect->returnPathLength; index++)
			{
				fileSelect->ReturnPaths[index] = CharToString(pFileSelect->returnMultiplePaths[index]);
			}
		}
	}

	// frees the unmanaged memory allocated by 'AllocUnmanagedFileSelect'
	static void DeleteUnmanagedFileSelect(::Vst2FileSelect* pFileSelect)
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
	static void DeleteUpdateUnmanagedFileSelect(::Vst2FileSelect* pFileSelect)
	{
		if(pFileSelect->returnMultiplePaths != NULL)
		{
			for(int index = 0; index < pFileSelect->returnPathLength; index++)
			{
				DeallocateString(pFileSelect->returnMultiplePaths[index]);
			}

			delete [] pFileSelect->returnMultiplePaths;
			pFileSelect->returnMultiplePaths = NULL;
			pFileSelect->returnPathLength = 0;
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