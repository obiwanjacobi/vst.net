#pragma once

ref class HostCommandStub : Jacobi::Vst::Core::IVstHostCommandStub
{
public:
	// IVstHostCommandStubBase
	virtual System::Boolean IsInitialized() { return (_pluginInfo != NULL); }
	// IVstHostCommandStub10
	virtual void SetParameterAutomated(System::Int32 index, System::Single value);
	virtual System::Int32 GetVersion();
    virtual System::Int32 GetCurrentPluginID();
    virtual void ProcessIdle();
	// IVstHostCommandStub
	virtual Jacobi::Vst::Core::VstTimeInfo^ GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags);
	virtual System::Boolean ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events);
	virtual System::Boolean IoChanged();
	virtual System::Boolean SizeWindow(System::Int32 width, System::Int32 height);
	virtual System::Double GetSampleRate();
	virtual System::Int32 GetBlockSize();
	virtual System::Int32 GetInputLatency();
	virtual System::Int32 GetOutputLatency();
	virtual Jacobi::Vst::Core::VstProcessLevels GetProcessLevel();
	virtual Jacobi::Vst::Core::VstAutomationStates GetAutomationState();
	virtual System::Boolean OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource);
	virtual System::Boolean OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option);
	virtual System::Boolean OfflineStart(Jacobi::Vst::Core::VstAudioFile^ file, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles);
	virtual System::Int32 OfflineGetCurrentPass();
	virtual System::Int32 OfflineGetCurrentMetaPass();
	virtual System::String^ GetVendorString();
	virtual System::String^ GetProductString();
	virtual System::Int32 GetVendorVersion();
	virtual System::Boolean CanDo(System::String^ cando);
	virtual Jacobi::Vst::Core::VstHostLanguage GetLanguage();
	virtual System::String^ GetDirectory();
	virtual System::Boolean UpdateDisplay();
	virtual System::Boolean BeginEdit();
	virtual System::Boolean EndEdit();
	virtual System::Boolean OpenFileSelector(/*VstFileSelect*/);
	virtual System::Boolean CloseFileSelector(/*VstFileSelect*/);
internal:
	HostCommandStub(::audioMasterCallback hostCallback);
	void Initialize(AEffect* pluginInfo) { _pluginInfo = pluginInfo; }

private:
	AEffect* _pluginInfo;
	audioMasterCallback _hostCallback;

	void ThrowIfNotInitialized();
};
