#pragma once

class TypeConverter
{
public:
	static void StringToChar(System::String^ source, char* dest, size_t maxLength)
	{
		if(source)
		{
			System::IntPtr mem = System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(source);
			strcpy_s(dest, maxLength, (const char*)mem.ToPointer());
			System::Runtime::InteropServices::Marshal::FreeHGlobal(mem);
		}
	}

	static System::String^ CharToString(char* source)
	{
		System::String^ str;

		if(source)
		{
			str = gcnew System::String(source);
		}

		return str;
	}

	static void RectangleToERect(System::Drawing::Rectangle rect, ERect** ppRect)
	{
		*ppRect = new ERect();
		(*ppRect)->top = rect.Top;
		(*ppRect)->left = rect.Left;
		(*ppRect)->bottom = rect.Bottom;
		(*ppRect)->right = rect.Right;
	}

	// delete[] retval
	static void* ByteArrayToPtr(array<System::Byte>^ byteArray)
	{
		int length = byteArray->Length;
		char* buffer = new char[length];

		for(int i = 0; i < length; i++)
		{
			buffer[i] = safe_cast<char>(byteArray[i]);
		}

		return buffer;
	}

	static array<System::Byte>^ PtrToByteArray(char *pBuffer, int length)
	{
		array<System::Byte>^ byteArray = gcnew array<System::Byte>(length);

		for(int n = 0; n < length; n++)
		{
			byteArray[n] = pBuffer[n];
		}

		return byteArray;
	}

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
				// log skipping
				break;
			}
		}

		return eventArray;
	}

	// delete retval
	static ::VstEvents* FromEventsArray(array<Jacobi::Vst::Core::VstEvent^>^ events)
	{
		int length = events->Length;
		if(length > 2) length -= 2;

		::VstEvents* pEvents = (::VstEvents*)new char[sizeof(VstEvent) + (length * sizeof(VstEvent*))];

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
			}

			index++;
		}

		return pEvents;
	}

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

	/*static Jacobi::Vst::Core::VstPinProperties^ ToPinProperties(::VstPinProperties* pProps)
	{
		Jacobi::Vst::Core::VstPinProperties^ pinProperties = gcnew Jacobi::Vst::Core::VstPinProperties();
		
		pinProperties->Flags = (Jacobi::Vst::Core::VstPinPropertiesFlags)pProps->flags;
		pinProperties->Label = CharToString(pProps->label);
		pinProperties->ShortLabel = CharToString(pProps->shortLabel);
		pinProperties->ArrangementType = (Jacobi::Vst::Core::VstSpeakerArrangementType)pProps->arrangementType;

		return pinProperties;
	}*/

	static void FromPinProperties(Jacobi::Vst::Core::VstPinProperties^ pinProps, ::VstPinProperties* pProps)
	{
		pProps->flags = safe_cast<VstInt32>(pinProps->Flags);
		StringToChar(pinProps->Label, pProps->label, kVstMaxLabelLen);
		StringToChar(pinProps->ShortLabel, pProps->shortLabel, kVstMaxShortLabelLen);
		pProps->arrangementType = safe_cast<VstInt32>(pinProps->ArrangementType);
	}

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

	/*static Jacobi::Vst::Core::VstParameterProperties^ ToParameterProperties(::VstParameterProperties* pProps)
	{
		Jacobi::Vst::Core::VstParameterProperties^ paramProps = gcnew Jacobi::Vst::Core::VstParameterProperties();

		paramProps->Flags = (Jacobi::Vst::Core::VstParameterPropertiesFlags)pProps->flags;
		paramProps->StepFloat = pProps->stepFloat;
		paramProps->LargeStepFloat = pProps->largeStepFloat;
		paramProps->SmallStepFloat = pProps->smallStepFloat;

		paramProps->StepInteger = pProps->stepInteger;
		paramProps->LargeStepInteger = pProps->largeStepInteger;
		paramProps->MaxInteger = pProps->maxInteger;
		paramProps->MinInteger = pProps->minInteger;
		
		paramProps->DisplayIndex = pProps->displayIndex;

		paramProps->Label = CharToString(pProps->label);
		paramProps->ShortLabel = CharToString(pProps->shortLabel);

		paramProps->Category = pProps->category;
		paramProps->NumParametersInCategory = pProps->numParametersInCategory;
		paramProps->CategoryLabel = CharToString(pProps->categoryLabel);

		return paramProps;
	}*/

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
		pProps->numParametersInCategory = paramProps->NumParametersInCategory;
	}

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

	static void FromMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiProgCat, ::MidiProgramCategory* pProgCat)
	{
		pProgCat->thisCategoryIndex = midiProgCat->CurrentCategoryIndex;
		pProgCat->parentCategoryIndex = midiProgCat->ParentCategoryIndex;
		StringToChar(midiProgCat->Name, pProgCat->name, kVstMaxNameLen);
	}

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

	static Jacobi::Vst::Core::VstTimeInfo^ ToTimeInfo(::VstTimeInfo* pTimeInfo)
	{
		Jacobi::Vst::Core::VstTimeInfo^ timeInfo = gcnew Jacobi::Vst::Core::VstTimeInfo();

		timeInfo->BarStartPosition = pTimeInfo->barStartPos;
		timeInfo->CycleStartPosition = pTimeInfo->cycleEndPos;
		timeInfo->CysleEndPosition = pTimeInfo->cycleStartPos;
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

	static array<Jacobi::Vst::Core::VstAudioBuffer^>^ ToAudioBufferArray(float** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

	static array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ ToAudioBufferArray(double** buffer, int sampleFrames, int numberOfBuffers, bool canWrite)
	{
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ bufferArray = gcnew array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>(numberOfBuffers);

		for(int n = 0; n < numberOfBuffers; n++)
		{
			bufferArray[n] = gcnew Jacobi::Vst::Core::VstAudioPrecisionBuffer(buffer[n], sampleFrames, canWrite);
		}

		return bufferArray;
	}

private:
	TypeConverter(){}
};
