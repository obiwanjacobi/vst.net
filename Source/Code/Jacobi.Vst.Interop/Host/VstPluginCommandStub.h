#pragma once

#include "UnmanagedArray.h"
#include "..\MemoryTracker.h"

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Host {

/// <summary>
/// The VstPluginCommandStub class implements the <see cref="Jacobi::Vst::Core::Host::IVstPluginCommandStub"/>
/// interface that is called by the host to access the Plugin.
/// </summary>
/// <remarks>
/// The class also implements the <see cref="Jacobi::Vst::Core::Deprecated::IVstPluginCommandsDeprecated20"/> 
/// interface for deprecated method support.
/// </remarks>
ref class VstPluginCommandStub : Jacobi::Vst::Core::Host::IVstPluginCommandStub, 
	Jacobi::Vst::Core::Deprecated::IVstPluginCommandsDeprecated20, System::IDisposable
{
public:
	~VstPluginCommandStub()
	{
		this->!VstPluginCommandStub();
	}
	!VstPluginCommandStub()
	{
		_memoryTracker->ClearAll();
		ClearCurrentEvents();
		delete[] _emptyAudio32;
		delete[] _emptyAudio64;
	}

	// IVstPluginCommandsBase
	/// <summary>
    /// Called by the host once every cycle to process incoming audio as well as output audio.
    /// </summary>
    /// <param name="inputs">An array with audio input buffers.</param>
    /// <param name="outputs">An array with audio output buffers.</param>
	virtual void ProcessReplacing(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, 
		array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs);
    /// <summary>
    /// Called by the host once every cycle to process incoming audio as well as output audio.
    /// </summary>
    /// <param name="inputs">An array with audio input buffers.</param>
    /// <param name="outputs">An array with audio output buffers.</param>
	virtual void ProcessReplacing(array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ inputs, 
		array<Jacobi::Vst::Core::VstAudioPrecisionBuffer^>^ outputs);
	/// <summary>
    /// Called by the host to assign a new <paramref name="value"/> to the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-base index into the parameters collection.</param>
    /// <param name="value">The new value for the parameter.</param>
	virtual void SetParameter(System::Int32 index, System::Single value);
	/// <summary>
    /// Called by the host to retrieve the current value of the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-base index into the parameters collection.</param>
    /// <returns>Returns 0.0 the value for the parameter at <paramref name="index"/>.</returns>
	virtual System::Single GetParameter(System::Int32 index);

	// IVstPluginCommands10
	/// <summary>
    /// This is the first method called by the host right after the assembly is loaded.
    /// </summary>
	virtual void Open();
    /// <summary>
    /// This is the last method the host calls. Dispose your resources.
    /// </summary>
	virtual void Close();
    /// <summary>
    /// The plugin should activate the Program at <paramref name="programNumber"/>.
    /// </summary>
    /// <param name="programNumber">A zero-based program number (index).</param>
	virtual void SetProgram(System::Int32 programNumber);
    /// <summary>
    /// Retrieve the current program index.
    /// </summary>
    /// <returns>Returns the current program index or 0 as a default.</returns>
	virtual System::Int32 GetProgram();
    /// <summary>
    /// Assign a new name to the current/active program.
    /// </summary>
    /// <param name="name">The new program name.</param>
	virtual void SetProgramName(System::String^ name);
	/// <summary>
    /// Retrieves the name of the current/active program.
    /// </summary>
    /// <returns>Can return null or an empty string.</returns>
	virtual System::String^ GetProgramName();
    /// <summary>
    /// Retrieves the label for the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the parameter collection.</param>
    /// <returns>Can return null or an empty string.</returns>
	virtual System::String^ GetParameterLabel(System::Int32 index);
    /// <summary>
    /// Retrieves the display value for the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the parameter collection.</param>
    /// <returns>Can return null or an empty string.</returns>
	virtual System::String^ GetParameterDisplay(System::Int32 index);
    /// <summary>
    /// Retrieves the name for the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the parameter collection.</param>
    /// <returns>Can return null or an empty string.</returns>
	virtual System::String^ GetParameterName(System::Int32 index);
    /// <summary>
    /// Assigns the <paramref name="sampleRate"/> to the plugin.
    /// </summary>
    /// <param name="sampleRate">The number of audio samples per second.</param>
	virtual void SetSampleRate(System::Single sampleRate);
    /// <summary>
    /// Assigns the <paramref name="blockSize"/> to the plugin.
    /// </summary>
    /// <param name="blockSize">The number samples per cycle.</param>
	virtual void SetBlockSize(System::Int32 blockSize);
    /// <summary>
    /// Called by the host when the users has turned the plugin on or off.
    /// </summary>
    /// <param name="onoff">True when on, false when off.</param>
	virtual void MainsChanged(System::Boolean onoff);
	/// <summary>
    /// Called by the host to retrieve the bounding rectangle of the editor.
    /// </summary>
    /// <param name="rect">The rectangle receiving the bounds.</param>
    /// <returns>Returns true when the <paramref name="rect"/> was set.</returns>
	virtual System::Boolean EditorGetRect([System::Runtime::InteropServices::Out] System::Drawing::Rectangle% rect);
	/// <summary>
    /// Called by the host to open the plugin custom editor.
    /// </summary>
    /// <param name="hWnd">The handle to the parent window.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean EditorOpen(System::IntPtr hWnd);
    /// <summary>
    /// Called by the host to close (and destroy) the plugin custom editor.
    /// </summary>
	virtual void EditorClose();
    /// <summary>
    /// Called by the host when the editor is idle.
    /// </summary>
    /// <remarks>Keep your processing short.</remarks>
	virtual void EditorIdle();
    /// <summary>
    /// Called by the host to retrieve a buffer with Program (and Parameter) content.
    /// </summary>
    /// <param name="isPreset">True if only the current/active program should be serialized, 
    /// otherwise (false) the complete program bank should be serialized.</param>
    /// <returns>Returns null when not implemented.</returns>
	virtual array<System::Byte>^ GetChunk(System::Boolean isPreset);
    /// <summary>
    /// Called by the host to load in a previously serialized program buffer.
    /// </summary>
    /// <param name="data">The buffer provided by the host that contains the program data.</param>
    /// <param name="isPreset">True if only the current/active program should be deserialized, 
    /// otherwise (false) the complete program bank should be deserialized.</param>
    /// <returns>Returns the number of bytes read from the <paramref name="data"/> buffer or 
    /// zero not implemented.</returns>
	virtual System::Int32 SetChunk(array<System::Byte>^ data, System::Boolean isPreset);

	// IVstPluginCommands20
	/// <summary>
	/// Called by the host when the plugin has specified the <see cref="Jacobi::Vst::Core::VstPluginCanDo"/><b>.ReceiveVstMidiEvent</b> flag.
    /// </summary>
    /// <param name="events">The (Midi) events for the current 'block'.</param>
    /// <returns>Returns false if not implemented.</returns>
	virtual System::Boolean ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events);
    /// <summary>
    /// Called by the host to query the plugin whether the parameter at <paramref name="index"/> can be automated.
    /// </summary>
    /// <param name="index">The zero-based index into the parameters.</param>
    /// <returns>Returns true if the parameter can be automated or false if not implemented.</returns>
	virtual System::Boolean CanParameterBeAutomated(System::Int32 index);
    /// <summary>
    /// Parses the <paramref name="str"/> value to assign to the parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The zero-base parameter index.</param>
    /// <param name="str">The value for the parameter.</param>
    /// <returns>Returns true when the parameter was successfully parsed or
    /// false if not implemented.</returns>
	virtual System::Boolean String2Parameter(System::Int32 index, System::String^ str);
    /// <summary>
    /// Retrieves the name of the program at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The zero-base index into the plugin Programs.</param>
    /// <returns>Returns null if not implemented.</returns>
	virtual System::String^ GetProgramNameIndexed(System::Int32 index);
    /// <summary>
    /// Retrieves the pin properties for the input at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the plugin inputs.</param>
    /// <returns>Returns null if not implemented.</returns>
	virtual Jacobi::Vst::Core::VstPinProperties^ GetInputProperties(System::Int32 index);
    /// <summary>
    /// Retrieves the pin properties for the output at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the plugin outputs.</param>
    /// <returns>Returns null if not implemented.</returns>
	virtual Jacobi::Vst::Core::VstPinProperties^ GetOutputProperties(System::Int32 index);
    /// <summary>
    /// Retrieves a categorization value for the plugin.
    /// </summary>
    /// <returns>Returns the plugin category.</returns>
	virtual Jacobi::Vst::Core::VstPluginCategory GetCategory();
    // Offline processing not implemented
    //virtual System::Boolean OfflineNotify(array<VstAudioFile^>^ audioFiles, System::Int32 count, System::Int32 startFlag);
    //virtual System::Boolean OfflinePrepare(array<VstOfflineTask^>^ tasks, System::Int32 count);
    //virtual System::Boolean OfflineRun(array<VstOfflineTask^>^ tasks, System::Int32 count);
    //virtual System::Boolean ProcessVariableIO(VstVariableIO^ variableIO);
    /// <summary>
    /// Under Construction
    /// </summary>
    /// <param name="saInput">Must not be null.</param>
    /// <param name="saOutput">Must not be null.</param>
    /// <returns>Returns false if not implemented.</returns>
	virtual System::Boolean SetSpeakerArrangement(Jacobi::Vst::Core::VstSpeakerArrangement^ saInput, 
		Jacobi::Vst::Core::VstSpeakerArrangement^ saOutput);
    /// <summary>
    /// Called by the host to bypass plugin processing.
    /// </summary>
    /// <param name="bypass">True to bypass, false to process.</param>
    /// <returns>Returns false if not implemented.</returns>
	virtual System::Boolean SetBypass(System::Boolean bypass);
    /// <summary>
    /// Called by the host to retrieve the name of plugin.
    /// </summary>
    /// <returns>Returns the name. Must not be null.</returns>
    /// <remarks>The plugin name should not exceed 31 characters.</remarks>
	virtual System::String^ GetEffectName();
    /// <summary>
    /// Called to retrieve the plugin vendor information.
    /// </summary>
    /// <returns>Returns the Vendor name.</returns>
	virtual System::String^ GetVendorString();
    /// <summary>
    /// Called to retrieve the plugin product information.
    /// </summary>
    /// <returns>Returns the Product name.</returns>
	virtual System::String^ GetProductString();
    /// <summary>
    /// Called to retrieve the plugin version information.
    /// </summary>
    /// <returns>Returns the Version number.</returns>
	virtual System::Int32 GetVendorVersion();
    /// <summary>
    /// Called by the host to query the plugin if a certain behavior or aspect is supported.
    /// </summary>
    /// <param name="cando">The string containing the can-do string, which can be host specific.</param>
    /// <returns>Returns an indication if the capability is supported.</returns>
	virtual Jacobi::Vst::Core::VstCanDoResult CanDo(System::String^ cando);
    /// <summary>
    /// Called by the host to retrieve the number of samples that the plugin outputs after the input has gone silent.
    /// </summary>
    /// <returns>Returns zero if not implemented.</returns>
	virtual System::Int32 GetTailSize();
    /// <summary>
    /// Called by the host to retrieve information about a plugin parameter at <paramref name="index"/>.
    /// </summary>
    /// <param name="index">A zero-based index into the plugin parameters.</param>
    /// <returns>Returns null if not implemented.</returns>
	virtual Jacobi::Vst::Core::VstParameterProperties^ GetParameterProperties(System::Int32 index);
    /// <summary>
    /// Called by the host to query the plugin what VST version it supports.
    /// </summary>
    /// <returns>Returns 2400 for VST 2.4.</returns>
	virtual System::Int32 GetVstVersion();

	// IVstPluginCommands21
	/// <summary>
    /// Called by the host when the user presses a key.
    /// </summary>
    /// <param name="ascii">The identification of the key.</param>
    /// <param name="virtualKey">Virtual key information.</param>
    /// <param name="modifers">Additional keys pressed.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean EditorKeyDown(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, 
		Jacobi::Vst::Core::VstModifierKeys modifers);
    /// <summary>
    /// Called by the host when the user releases a key.
    /// </summary>
    /// <param name="ascii">The identification of the key.</param>
    /// <param name="virtualKey">Virtual key information.</param>
    /// <param name="modifers">Additional keys pressed.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean EditorKeyUp(System::Byte ascii, Jacobi::Vst::Core::VstVirtualKey virtualKey, 
		Jacobi::Vst::Core::VstModifierKeys modifers);
    /// <summary>
    /// Called by the host to set the mode for turning knobs.
    /// </summary>
    /// <param name="mode">The mode to use for turning knobs.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean SetEditorKnobMode(Jacobi::Vst::Core::VstKnobMode mode);
    /// <summary>
    /// Retrieves information about a midi program for a specific Midi <paramref name="channel"/>.
    /// </summary>
    /// <param name="midiProgram">Must not be null.</param>
    /// <param name="channel">The zero-based Midi channel.</param>
    /// <returns>Returns the number of implemented Midi programs or 0 if not implemented.</returns>
	virtual System::Int32 GetMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel);
    /// <summary>
    /// Retrieves information about the current midi program for a specific Midi <paramref name="channel"/>.
    /// </summary>
    /// <param name="midiProgram">Must not be null.</param>
    /// <param name="channel">The zero-based Midi channel.</param>
    /// <returns>Returns the number of implemented Midi programs or 0 if not implemented.</returns>
	virtual System::Int32 GetCurrentMidiProgramName(Jacobi::Vst::Core::VstMidiProgramName^ midiProgram, System::Int32 channel);
    /// <summary>
    /// Retrieves information about a Midi Program Category.
    /// </summary>
    /// <param name="midiCat">Must not be null.</param>
    /// <param name="channel">The zero-based Midi channel.</param>
    /// <returns>Returns the total number of Midi program categories or 0 if not implemented.</returns>
	virtual System::Int32 GetMidiProgramCategory(Jacobi::Vst::Core::VstMidiProgramCategory^ midiCat, System::Int32 channel);
    /// <summary>
    /// Indicates if the program for the specified Midi <paramref name="channel"/> has changed.
    /// </summary>
    /// <param name="channel">The zero-base Midi channel.</param>
    /// <returns>Returns true if the Midi Program has changed, otherwise false is returned.</returns>
	virtual System::Boolean HasMidiProgramsChanged(System::Int32 channel);
    /// <summary>
    /// Retrieves information about a Midi Key (or note).
    /// </summary>
    /// <param name="midiKeyName">Must not be null.</param>
    /// <param name="channel">The zero-base Midi channel.</param>
    /// <returns>Returns true when the <paramref name="midiKeyName"/>.Name was filled.</returns>
	virtual System::Boolean GetMidiKeyName(Jacobi::Vst::Core::VstMidiKeyName^ midiKeyName, System::Int32 channel);
    /// <summary>
    /// Called by the host just before a new Program is set.
    /// </summary>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean BeginSetProgram();
    /// <summary>
    /// Called by the host just after a new Program is set.
    /// </summary>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean EndSetProgram();

	// IVstPluginCommands23
	/// <summary>
    /// Returns the speaker arrangements for the input and output of the plugin.
    /// </summary>
    /// <param name="input">Filled with the speaker arrangement for the plugin inputs.</param>
    /// <param name="output">Filled with the speaker arrangement for the plugin outputs.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean GetSpeakerArrangement([System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% input, 
		[System::Runtime::InteropServices::Out] Jacobi::Vst::Core::VstSpeakerArrangement^% output);
    // Offline processing not implemented
    //virtual System::Int32 SetTotalSamplesToProcess(System::Int32 numberOfSamples);
    // Plugin Host/Shell not implemented
    //virtual System::Int32 GetNextPlugin(([System::Runtime::InteropServices::Out] System::String^% name);
    /// <summary>
    /// Called just before the first call to Process is made.
    /// </summary>
    /// <returns>It is unclear what this return value represents.</returns>
	virtual System::Int32 StartProcess();
    /// <summary>
    /// Called just after the last call to Process is made.
    /// </summary>
    /// <returns>It is unclear what this return value represents.</returns>
	virtual System::Int32 StopProcess();
    /// <summary>
    /// Informs the plugin of the pan algorithm to use.
    /// </summary>
    /// <param name="type">The pan algorithm type.</param>
    /// <param name="gain">A gain factor.</param>
    /// <returns>Returns false when not implemented.</returns>
	virtual System::Boolean SetPanLaw(Jacobi::Vst::Core::VstPanLaw type, System::Single gain);
    /// <summary>
    /// Called by the host to query the plugin that supports persistence if the chunk can be read.
    /// </summary>
    /// <param name="chunkInfo">Must not be null.</param>
	/// <returns>Returns <see cref="Jacobi::Vst::Core::VstCanDoResult"/><b>.Yes</b> if the plugin can read the data.</returns>
	virtual Jacobi::Vst::Core::VstCanDoResult BeginLoadBank(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo);
    /// <summary>
    /// Called by the host to query the plugin that supports persistence if the chunk can be read.
    /// </summary>
    /// <param name="chunkInfo">Must not be null.</param>
	/// <returns>Returns <see cref="Jacobi::Vst::Core::VstCanDoResult"/><b>.Yes</b> if the plugin can read the data.</returns>
	virtual Jacobi::Vst::Core::VstCanDoResult BeginLoadProgram(Jacobi::Vst::Core::VstPatchChunkInfo^ chunkInfo);

	// IVstPluginCommands24
	/// <summary>
    /// Called by the host query inform the plugin on the precision of audio processing it supports.
    /// </summary>
    /// <param name="precision">An indication of either 32 bit or 64 bit samples.</param>
    /// <returns>Returns true when the requested <paramref name="precision"/> is supported.</returns>
	virtual System::Boolean SetProcessPrecision(Jacobi::Vst::Core::VstProcessPrecision precision);
    /// <summary>
    /// Called by the host to retrieve the number of Midi In channels the plugin supports.
    /// </summary>
    /// <returns>Returns the number of Midi In channels, or 0 (zero) if not supported.</returns>
	virtual System::Int32 GetNumberOfMidiInputChannels();
    /// <summary>
    /// Called by the host to retrieve the number of Midi Out channels the plugin supports.
    /// </summary>
    /// <returns>Returns the number of Midi Out channels, or 0 (zero) if not supported.</returns>
	virtual System::Int32 GetNumberOfMidiOutputChannels();

	// IVstPluginCommandStub
	/// <summary>
	/// Gets or sets the Plugin Context for this implementation.
	/// </summary>
	virtual property Jacobi::Vst::Core::Host::IVstPluginContext^ PluginContext;

	//
	// Deprecated support
	//

	// IVstPluginCommandsDeprecatedBase
	/// <summary>
    /// Processes audio in an accumulating fashion.
    /// </summary>
    /// <param name="inputs">Audio input buffers. Must not be null.</param>
    /// <param name="outputs">Audio output buffers. Must not be null.</param>
	virtual void ProcessAcc(array<Jacobi::Vst::Core::VstAudioBuffer^>^ inputs, array<Jacobi::Vst::Core::VstAudioBuffer^>^ outputs);

	// IVstPluginCommandsDeprecated10
	/// <summary>
    /// Called if the VstPluginDeprecatedInfo.DeprecatedFlags has the "HasClip" or "HasVu" flags set.
    /// </summary>
    /// <returns>Returns the current Vu value.</returns>
    virtual System::Single GetVu();
    /// <summary>
    /// Called when a key stroke occurs in the editor.
    /// </summary>
    /// <param name="keycode">The key code value.</param>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean EditorKey(System::Int32 keycode);
    /// <summary>
    /// The window that hosts the plugin editor is put on top of other windows.
    /// </summary>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean EditorTop();
    /// <summary>
    /// The window that hosts the plugin editor is put in the background.
    /// </summary>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean EditorSleep();
    /// <summary>
    /// Returns an identifaction code.
    /// </summary>
    /// <returns>Returns 'NvEf' as an integer.</returns>
    virtual System::Int32 Identify();

	// IVstPluginCommandsDeprecated20
	/// <summary>
    /// Retrieves the number of program categories.
    /// </summary>
    /// <returns>Returns the number of program categories.</returns>
    virtual System::Int32 GetProgramCategoriesCount();
    /// <summary>
    /// Copy the current program to the program at <paramref name="programIndex"/>.
    /// </summary>
    /// <param name="programIndex">A zero-based index into the program collection.</param>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean CopyCurrentProgramTo(System::Int32 programIndex);
    /// <summary>
    /// Notifies the plugin of the fact that an input pin was dis/connected.
    /// </summary>
    /// <param name="inputIndex">A zero-based index into the input connection collection.</param>
    /// <param name="connected">Indicates if the pin was connected (True) or disconnected (False).</param>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean ConnectInput(System::Int32 inputIndex, System::Boolean connected);
    /// <summary>
    /// Notifies the plugin of the fact that an output pin was dis/connected.
    /// </summary>
    /// <param name="outputIndex">A zero-based index into the output connection collection.</param>
    /// <param name="connected">Indicates if the pin was connected (True) or disconnected (False).</param>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean ConnectOutput(System::Int32 outputIndex, System::Boolean connected);
    /// <summary>
    /// For external DSP.
    /// </summary>
    /// <returns>Returns the current position.</returns>
	/// <remarks>The <see cref="Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedFlags"/>.ExtIsAsync 
    /// must be set in order for this method to be called.</remarks>
    virtual System::Int32 GetCurrentPosition();
    /// <summary>
    /// For external DSP.
    /// </summary>
    /// <returns>Returns the destination audio buffer.</returns>
	/// <remarks>The <see cref="Jacobi::Vst::Core::Deprecated::VstPluginDeprecatedFlags"/>.ExtHasBuffer 
    /// must be set in order for this method to be called.</remarks>
    virtual Jacobi::Vst::Core::VstAudioBuffer^ GetDestinationBuffer();
    /// <summary>
    /// Assigns a new block size and sample rate value to the plugin.
    /// </summary>
    /// <param name="blockSize">The number of samples per frame (cycle).</param>
    /// <param name="sampleRate">The new sample rate.</param>
    /// <returns>Returns true if the call was successful.</returns>
    virtual System::Boolean SetBlockSizeAndSampleRate(System::Int32 blockSize, System::Single sampleRate);
    /// <summary>
    /// Retrieves an error text from the plugin.
    /// </summary>
    /// <returns>Returns the error text.</returns>
    /// <remarks>The length of the text must not exceed 256 characters.</remarks>
    virtual System::String^ GetErrorText(); // max 256 chars
    /// <summary>
    /// Called by the host to allow some light idle processing by the plugin.
    /// </summary>
    /// <returns>Returns True when subsequent Idle calls should follow. 
    /// False is returned when no further Idle processing is required.</returns>
    virtual System::Boolean Idle();
    /// <summary>
    /// Retrieves an iconic representation of the plugin.
    /// </summary>
    /// <returns>Returns null when not supported.</returns>
    /// <remarks>The VST specs are not final for this method. Not supported.</remarks>
	virtual System::Drawing::Icon^ GetIcon();
    /// <summary>
    /// Moves the view to a new position inside the window.
    /// </summary>
    /// <param name="position">The x and y coordinates.</param>
    /// <returns>Returns true if the call was successful.</returns>
	virtual System::Boolean SetViewPosition(System::Drawing::Point% position);
    /// <summary>
    /// Indicates if keys are required by the plugin.
    /// </summary>
    /// <returns>Returns true if keys are required.</returns>
    virtual System::Boolean KeysRequired();

internal:
	/// <summary>Constructs a new instance based on an <b>AEffect</b> structure.</summary>
	VstPluginCommandStub(::AEffect* pEffect);

private:
	::AEffect* _pEffect;	// the unmanaged Effect structure

	// unmanaged events passed in during ProcessEvents. Will be deleted after the processing call.
	::VstEvents* _currentEvents;
	void ClearCurrentEvents();

	// an empty audio buffer array
	float** _emptyAudio32;	

	// unmanaged audio buffers
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

	// an empty precision audio buffer array
	double** _emptyAudio64;

	// unmanaged precision audio buffers
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
	{
		if(_pEffect && _pEffect->dispatcher)
		{
			_traceCtx->WriteDispatchBegin(opcode, index, System::IntPtr(value), System::IntPtr(ptr), opt);

			::VstIntPtr result = _pEffect->dispatcher(_pEffect, opcode, index, value, ptr, opt);

			_traceCtx->WriteDispatchEnd(System::IntPtr(result));
		}

		return 0;
	}
	void CallProcess32(float** inputs, float** outputs, ::VstInt32 sampleFrames)
	{
		if(_pEffect && _pEffect->processReplacing)
		{
			_traceCtx->WriteProcess(_pEffect->numInputs, _pEffect->numOutputs, sampleFrames, sampleFrames);

			_pEffect->processReplacing(_pEffect, inputs, outputs, sampleFrames);
		}
	}
	void CallProcess64(double** inputs, double** outputs, ::VstInt32 sampleFrames)
	{
		if(_pEffect && _pEffect->processDoubleReplacing) 
		{
			_traceCtx->WriteProcess(_pEffect->numInputs, _pEffect->numOutputs, sampleFrames, sampleFrames);

			_pEffect->processDoubleReplacing(_pEffect, inputs, outputs, sampleFrames);
		}
	}
	void CallSetParameter(::VstInt32 index, float parameter)
	{
		if(_pEffect && _pEffect->setParameter)
		{
			_traceCtx->WriteSetParameter(index, parameter);

			_pEffect->setParameter(_pEffect, index, parameter);
		}
	}
	float CallGetParameter(::VstInt32 index)
	{
		if(_pEffect && _pEffect->getParameter)
		{
			_traceCtx->WriteGetParameterBegin(index);

			float result = _pEffect->getParameter(_pEffect, index);

			_traceCtx->WriteGetParameterEnd(result);

			return result;
		}

		return 0.0f;
	}

	// deprecated support
	void CallProcess32Acc(float** inputs, float** outputs, ::VstInt32 sampleFrames)
	{
		if(_pEffect && _pEffect->DECLARE_VST_DEPRECATED (process))
		{
			_traceCtx->WriteProcess(_pEffect->numInputs, _pEffect->numOutputs, sampleFrames, sampleFrames);

			_pEffect->DECLARE_VST_DEPRECATED (process)(_pEffect, inputs, outputs, sampleFrames);
		}
	}

	MemoryTracker^ _memoryTracker;
	Jacobi::Vst::Core::Diagnostics::TraceContext^ _traceCtx;
};

}}}} // Jacobi::Vst::Interop::Host