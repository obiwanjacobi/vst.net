#pragma once

class TypeConverter
{
public:
	// Converts a managed string to an unmanaged char buffer.
	static void StringToChar(System::String^ source, char* dest, size_t maxLength)
	{
		if(source)
		{
			System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(source);
			strcpy_s(dest, maxLength, (const char*)mem.ToPointer());
			System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
		}
	}

	// Converts an unmanaged char buffer to a managed string.
	static System::String^ CharToString(char* source)
	{
		System::String^ str;

		if(source)
		{
			str = gcnew System::String(source);
		}

		return str;
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
	static void RectangleToERect(System::Drawing::Rectangle rect, ERect** ppRect)
	{
		*ppRect = new ERect();
		(*ppRect)->top = rect.Top;
		(*ppRect)->left = rect.Left;
		(*ppRect)->bottom = rect.Bottom;
		(*ppRect)->right = rect.Right;
	}

	// converts an unmanaged ERect* to a Rectangle. 
	static System::Drawing::Rectangle ERectToRectangle(ERect* pRect)
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
	static array<Jacobi::Vst::Core::VstEvent^>^ ToEventArray(VstEvents* pEvents)
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
	static ::VstEvents* FromEventsArray(array<Jacobi::Vst::Core::VstEvent^>^ events)
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

	// Cleanup method for the return value of 'FromEventsArray'
	static void DeleteVstEvents(::VstEvents* pEvents)
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
	static void FromPinProperties(Jacobi::Vst::Core::VstPinProperties^ pinProps, ::VstPinProperties* pProps)
	{
		pProps->flags = safe_cast<VstInt32>(pinProps->Flags);
		StringToChar(pinProps->Label, pProps->label, kVstMaxLabelLen);
		StringToChar(pinProps->ShortLabel, pProps->shortLabel, kVstMaxShortLabelLen);
		pProps->arrangementType = safe_cast<VstInt32>(pinProps->ArrangementType);
	}

	static Jacobi::Vst::Core::VstPinProperties^ ToPinProperties(::VstPinProperties* pProps)
	{
		Jacobi::Vst::Core::VstPinProperties^ pinProps = gcnew Jacobi::Vst::Core::VstPinProperties();

		pinProps->Flags = safe_cast<Jacobi::Vst::Core::VstPinPropertiesFlags>(pProps->flags);
		pinProps->Label = CharToString(pProps->label);
		pinProps->ShortLabel = CharToString(pProps->shortLabel);
		pinProps->ArrangementType = safe_cast<Jacobi::Vst::Core::VstSpeakerArrangementType>(pProps->arrangementType);

		return pinProps;
	}

	// Converts an unmanaged speaker pArrangement to a managed VstSpeakerArrangement.
	static Jacobi::Vst::Core::VstSpeakerArrangement^ ToSpeakerArrangement(::VstSpeakerArrangement* pArrangement)
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
	static ::VstSpeakerArrangement* FromSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ arrangement)
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
	static void FromParameterProperties(Jacobi::Vst::Core::VstParameterProperties^ paramProps, ::VstParameterProperties* pProps)
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

	// Assigns the values of the managed midiProgName to the unmanaged pProgName fields.
	static void FromMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgName, ::MidiProgramName* pProgName)
	{
		pProgName->thisProgramIndex = midiProgName->CurrentProgramIndex;
		pProgName->flags = (VstInt32)midiProgName->Flags;
		pProgName->midiBankLsb = safe_cast<char>(midiProgName->MidiBankLSB);
		pProgName->midiBankMsb = safe_cast<char>(midiProgName->MidiBankMSB);
		pProgName->midiProgram = safe_cast<char>(midiProgName->MidiProgram);
		StringToChar(midiProgName->Name, pProgName->name, kVstMaxNameLen);
		pProgName->parentCategoryIndex = midiProgName->ParentCategoryIndex;
	}

	// Assigns the values of the managed midiProgCat to the unmanaged pProgCat fields.
	static void FromMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat, ::MidiProgramCategory* pProgCat)
	{
		pProgCat->thisCategoryIndex = midiProgCat->CurrentCategoryIndex;
		pProgCat->parentCategoryIndex = midiProgCat->ParentCategoryIndex;
		StringToChar(midiProgCat->Name, pProgCat->name, kVstMaxNameLen);
	}

	// Converts the unmanaged pChunkInfo to a managed VstPatchChunkInfo.
	static Jacobi::Vst::Core::VstPatchChunkInfo^ ToPatchChunkInfo(::VstPatchChunkInfo* pChunkInfo)
	{
		Jacobi::Vst::Core::VstPatchChunkInfo^ patchChunkInfo = 
			gcnew Jacobi::Vst::Core::VstPatchChunkInfo(
				pChunkInfo->version,
				pChunkInfo->pluginUniqueID,
				pChunkInfo->pluginVersion,
				pChunkInfo->numElements);

		return patchChunkInfo;
	}

	// Converts the unmanaged pTimeInfo to the managed VstTimeInfo.
	static Jacobi::Vst::Core::VstTimeInfo^ ToTimeInfo(::VstTimeInfo* pTimeInfo)
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

	// Converts the unmanaged sample buffer to a managed VstAudioBuffer array.
	static array<Jacobi::Vst::Core::VstAudioBuffer^>^ ToAudioBufferArray(float** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

	// Converts the unmanaged sample buffer to a managed VstAudioPrecisionBuffer array.
	static array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ ToAudioBufferArray(double** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioPrecisionBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

	// Call DeleteFileSelect on retval
	static ::VstFileSelect* FromFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect)
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

	// updates the managed VstFileSelect with output information set by the host.
	static void ToFileSelect(Jacobi::Vst::Core::VstFileSelect^ fileSelect, ::VstFileSelect* pFileSelect)
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

	// frees the unmanaged memory
	static void DeleteFileSelect(::VstFileSelect* pFileSelect)
	{
		DeallocateString(pFileSelect->initialPath);

		delete[] pFileSelect->fileTypes;
		delete pFileSelect;
	}

private:
	TypeConverter(){}
};
