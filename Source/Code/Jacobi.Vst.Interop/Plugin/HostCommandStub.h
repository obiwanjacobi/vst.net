#pragma once

namespace Jacobi {
namespace Vst {
namespace Interop {
namespace Plugin {

ref class HostCommandStub : Jacobi::Vst::Core::Plugin::IVstHostCommandStub
{
public:
	~HostCommandStub();
	!HostCommandStub();

	// IVstHostCommandStub
	virtual System::Boolean IsInitialized() { return (_pluginInfo != NULL); }
	virtual System::Boolean UpdatePluginInfo(Jacobi::Vst::Core::Plugin::VstPluginInfo^ pluginInfo);
	// IVstHostCommands10
	virtual void SetParameterAutomated(System::Int32 index, System::Single value);
	virtual System::Int32 GetVersion();
    virtual System::Int32 GetCurrentPluginID();
    virtual void ProcessIdle();
	// IVstHostCommands20
	virtual Jacobi::Vst::Core::VstTimeInfo^ GetTimeInfo(Jacobi::Vst::Core::VstTimeInfoFlags filterFlags);
	virtual System::Boolean ProcessEvents(array<Jacobi::Vst::Core::VstEvent^>^ events);
	virtual System::Boolean IoChanged();
	virtual System::Boolean SizeWindow(System::Int32 width, System::Int32 height);
	virtual System::Single GetSampleRate();
	virtual System::Int32 GetBlockSize();
	virtual System::Int32 GetInputLatency();
	virtual System::Int32 GetOutputLatency();
	virtual Jacobi::Vst::Core::VstProcessLevels GetProcessLevel();
	virtual Jacobi::Vst::Core::VstAutomationStates GetAutomationState();
	//virtual System::Boolean OfflineRead(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option, System::Boolean readSource);
	//virtual System::Boolean OfflineWrite(Jacobi::Vst::Core::VstOfflineTask^ task, Jacobi::Vst::Core::VstOfflineOption option);
	//virtual System::Boolean OfflineStart(array<Jacobi::Vst::Core::VstAudioFile^>^ files, System::Int32 numberOfAudioFiles, System::Int32 numberOfNewAudioFiles);
	//virtual System::Int32 OfflineGetCurrentPass();
	//virtual System::Int32 OfflineGetCurrentMetaPass();
	virtual System::String^ GetVendorString();
	virtual System::String^ GetProductString();
	virtual System::Int32 GetVendorVersion();
	virtual Jacobi::Vst::Core::VstCanDoResult CanDo(Jacobi::Vst::Core::VstHostCanDo cando);
	virtual Jacobi::Vst::Core::VstHostLanguage GetLanguage();
	virtual System::String^ GetDirectory();
	virtual System::Boolean UpdateDisplay();
	virtual System::Boolean BeginEdit(System::Int32 index);
	virtual System::Boolean EndEdit(System::Int32 index);
	virtual System::Boolean OpenFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect);
	virtual System::Boolean CloseFileSelector(Jacobi::Vst::Core::VstFileSelect^ fileSelect);

internal:
	HostCommandStub(::audioMasterCallback hostCallback);
	void Initialize(AEffect* pluginInfo) { if(pluginInfo == NULL) { throw gcnew System::ArgumentNullException("pluginInfo"); } _pluginInfo = pluginInfo; }

private:
	AEffect* _pluginInfo;
	audioMasterCallback _hostCallback;

	void ThrowIfNotInitialized();
	VstIntPtr CallHost(VstInt32 opcode, VstInt32 index, VstIntPtr value, void* ptr, float opt) { return _hostCallback(_pluginInfo, opcode, index, value, ptr, opt); }
};

}}}} // Jacobi::Vst::Interop::Plugin