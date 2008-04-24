#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

class TypeConverter
{
public:
	static void StringToChar(String^ source, char* dest, size_t maxLength)
	{
		if(source)
		{
			const char* str = (const char*)(Marshal::StringToHGlobalAnsi(source)).ToPointer();
			strcpy_s(dest, maxLength, str);
			Marshal::FreeHGlobal(IntPtr((void*)str));
		}
	}

	static String^ CharToString(char* source)
	{
		String^ str;

		if(source)
		{
			str = gcnew String(source);
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

	static void ByteArrayToPtr(array<System::Byte>^ byteArray, void** ppBuffer)
	{
		// TODO: implement
	}

	static array<System::Byte>^ PtrToByteArray(void *pBuffer, int length)
	{
		array<System::Byte>^ byteArray = gcnew array<System::Byte>(length);

		// TODO: implement

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
					Jacobi::Vst::Core::VstEventTypes::MidiEvent, pMidiEvent->deltaFrames, pMidiEvent->flags, 
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
					Jacobi::Vst::Core::VstEventTypes::MidiEvent, pMidiEvent->deltaFrames, pMidiEvent->flags, 
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

	static Jacobi::Vst::Core::VstPinProperties^ ToPinProperties(::VstPinProperties* pProps)
	{
		Jacobi::Vst::Core::VstPinProperties^ pinProperties = gcnew Jacobi::Vst::Core::VstPinProperties();
		
		pinProperties->Flags = (Jacobi::Vst::Core::VstPinPropertiesFlags)pProps->flags;
		pinProperties->Label = CharToString(pProps->label);
		pinProperties->ShortLabel = CharToString(pProps->shortLabel);
		pinProperties->ArrangementType = (Jacobi::Vst::Core::VstSpeakerArrangementType)pProps->arrangementType;

		return pinProperties;
	}

	static Jacobi::Vst::Core::VstSpeakerArrangement^ ToSpeakerArrangement(::VstSpeakerArrangement* pArrangement)
	{
		Jacobi::Vst::Core::VstSpeakerArrangement^ spkArr = gcnew Jacobi::Vst::Core::VstSpeakerArrangement();

		spkArr->Type = (Jacobi::Vst::Core::VstSpeakerArrangementType)pArrangement->type;
		spkArr->Speakers = gcnew array<Jacobi::Vst::Core::VstSpeakerProperties^>(pArrangement->numChannels);

		for(int n = 0; n < pArrangement->numChannels; n++)
		{
			::VstSpeakerProperties propSrc = pArrangement->speakers[n];

			Jacobi::Vst::Core::VstSpeakerProperties^ spkProp = gcnew Jacobi::Vst::Core::VstSpeakerProperties();
			spkProp->Azimath = propSrc.azimuth;
			spkProp->Elevation = propSrc.elevation;
			spkProp->Radius = propSrc.radius;
			spkProp->SpeakerType = (Jacobi::Vst::Core::VstSpeakerTypes)propSrc.type;
			spkProp->Name = CharToString(propSrc.name);

			spkArr->Speakers[n] = spkProp;
		}

		return spkArr;
	}

	static Jacobi::Vst::Core::VstParameterProperties^ ToParameterProperties(::VstParameterProperties* pProps)
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
	}

	static void FromMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgName, ::MidiProgramName* pProgName)
	{
		pProgName->thisProgramIndex = midiProgName->CurrentProgramIndex;
		pProgName->flags = (VstInt32)midiProgName->Flags;
		pProgName->midiBankLsb = (char)midiProgName->MidiBankLSB;
		pProgName->midiBankMsb = (char)midiProgName->MidiBankMSB;
		pProgName->midiProgram = (char)midiProgName->MidiProgram;
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
		Jacobi::Vst::Core::VstPatchChunkInfo^ patchChunkInfo = gcnew Jacobi::Vst::Core::VstPatchChunkInfo();

		patchChunkInfo->NumberOfElements = pChunkInfo->numElements;
		patchChunkInfo->PluginID = pChunkInfo->pluginUniqueID;
		patchChunkInfo->PluginVersion = pChunkInfo->pluginVersion;
		patchChunkInfo->Version = pChunkInfo->version;

		return patchChunkInfo;
	}

private:
	TypeConverter(){}
};
