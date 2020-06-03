# Implement the Plugin Command Stub

The Plugin Command Stub is a managed class that implements the <a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a> interface. This interface is called by the <a href="30e478e7-4eba-4eab-8a32-f9d9a2c4d2b3">Plugin Command Proxy</a> from the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly. The managed plugin must implement this class as a **public** type in order for the <a href="4781d47a-3b7a-41a6-b632-4a6785082bfa">Managed Plugin Factory</a> to create an instance of it that is used by the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly.


The <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> assembly contains a default implementation that dispatches these method calls to the appropriate framework interface method.



## Using the Framework

Because the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> contians a base class that implements the <a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a> interface, you simply derive your public class from the StdPluginCommandStub base class and override the CreatePluginInstance() method and return a new instance of the <a href="62feac6e-0c75-4ef8-8703-fb970f81280b">Plugin Root Class</a>.


**C#**<br />
``` C#
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace MyPluginProject
{    
    public class MyPluginCommandStub : StdPluginCommandStub, IVstPluginCommandStub
    {
        protected override IVstPlugin CreatePluginInstance()
        {
            return new MyPlugin();
        }
    }
}
```
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Note that the public class explicitly derives from <a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a> interface. When the <a href="4781d47a-3b7a-41a6-b632-4a6785082bfa">Managed Plugin Factory</a> loads the managed Plugin assembly it will search for a public type that implements that interface, but it does not search for it on base classes. By explicitly implementing the interface on your <a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a> you signal that this public class is the Command Stub to use.</td></tr></table>

## Using the Core

When interfacing at the core level with <a href="http://www.codeplex.com/vstnet">VST.NET</a> you have to imlement all the methods of the <a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a> interface yourself. Depending on the VST features that you want to use this can add up to a serious effort. Below a code template for implementing all the methods for the Plugin Command Stub.


**C#**<br />
``` C#
using Jacobi.Vst.Core.Plugin;

public class MyPluginCommandStub : IVstPluginCommandStub
{
    private VstPluginInfo _pluginInfo;
    private IVstHostCommandStub _hostStub;

    #region IVstPluginCommandStub Members

    public VstPluginInfo GetPluginInfo(IVstHostCommandStub hostCmdStub)
    {
        _hostStub = hostCmdStub;
        _pluginInfo = new VstPluginInfo();

        //_pluginInfo.AudioInputCount = ?;
        //_pluginInfo.AudioOutputCount = ?;
        //_pluginInfo.ProgramCount = ?;
        //_pluginInfo.Flags = ?;
        //_pluginInfo.PluginID = ?;
        //_pluginInfo.PluginVersion = ?;

        return _pluginInfo;
    }

    #endregion

    #region IVstPluginCommands24 Members

    public bool SetProcessPrecision(VstProcessPrecision precision)
    {
        return false;
    }

    public int GetNumberOfMidiInputChannels()
    {
        return 0;
    }

    public int GetNumberOfMidiOutputChannels()
    {
        return 0;
    }

    #endregion

    #region IVstPluginCommands23 Members

    public bool GetSpeakerArrangement(out VstSpeakerArrangement input, out VstSpeakerArrangement output)
    {
        input = null;
        output = null;

        return false;
    }

    public int SetTotalSamplesToProcess(int numberOfSamples)
    {
        return 0;
    }

    public int GetNextPlugin(out string name)
    {
        name = null;
        return 0;
    }

    public int StartProcess()
    {
        return 0;
    }

    public int StopProcess()
    {
        return 0;
    }

    public bool SetPanLaw(VstPanLaw type, float value)
    {
        return false;
    }

    public VstCanDoResult BeginLoadBank(VstPatchChunkInfo chunkInfo)
    {
        return VstCanDoResult.Unknown;
    }

    public VstCanDoResult BeginLoadProgram(VstPatchChunkInfo chunkInfo)
    {
        return VstCanDoResult.Unknown;
    }

    #endregion

    #region IVstPluginCommands21 Members

    public bool EditorKeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
    {
        return false;
    }

    public bool EditorKeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
    {
        return false;
    }

    public bool SetEditorKnobMode(VstKnobMode mode)
    {
        return false;
    }

    public int GetMidiProgramName(VstMidiProgramName midiProgram, int channel)
    {
        return 0;
    }

    public int GetCurrentMidiProgramName(VstMidiProgramName midiProgram, int channel)
    {
        return 0;
    }

    public int GetMidiProgramCategory(VstMidiProgramCategory midiCat, int channel)
    {
        return 0;
    }

    public bool HasMidiProgramsChanged(int channel)
    {
        return false;
    }

    public bool GetMidiKeyName(VstMidiKeyName midiKeyName, int channel)
    {
        return false;
    }

    public bool BeginSetProgram()
    {
        return false;
    }

    public bool EndSetProgram()
    {
        return false;
    }

    #endregion

    #region IVstPluginCommands20 Members

    public bool ProcessEvents(VstEvent[] events)
    {
        return false;
    }

    public bool CanParameterBeAutomated(int index)
    {
        return false;
    }

    public bool String2Parameter(int index, string str)
    {
        return false;
    }

    public string GetProgramNameIndexed(int index)
    {
        return null;
    }

    public VstPinProperties GetInputProperties(int index)
    {
        return null;
    }

    public VstPinProperties GetOutputProperties(int index)
    {
        return null;
    }

    public VstPluginCategory GetCategory()
    {
        return VstPluginCategory.Unknown;
    }

    public bool SetSpeakerArrangement(VstSpeakerArrangement saInput, VstSpeakerArrangement saOutput)
    {
        return false;
    }

    public bool SetBypass(bool bypass)
    {
        return false;
    }

    public string GetEffectName()
    {
        return null;
    }

    public string GetVendorString()
    {
        return null;
    }

    public string GetProductString()
    {
        return null;
    }

    public int GetVendorVersion()
    {
    	// version 1.0.0.0
        return 1000;
    }

    public VstCanDoResult CanDo(string cando)
    {
        return VstCanDoResult.No;
    }

    public int GetTailSize()
    {
        return 0;
    }

    public VstParameterProperties GetParameterProperties(int index)
    {
        return null;
    }

    public int GetVstVersion()
    {
    	// VST 2.4
        return 2400;
    }

    #endregion

    #region IVstPluginCommands10 Members

    public void Open()
    {
        // first call to plugin after it has been loaded.
    }

    public void Close()
    {
        // Last call to plugin. Release all (unmanaged) resources.
    }

    public void SetProgram(int programNumber)
    {
    }

    public int GetProgram()
    {
        return 0;
    }

    public void SetProgramName(string name)
    {
    }

    public string GetProgramName()
    {
        return null;
    }

    public string GetParameterLabel(int index)
    {
        return null;
    }

    public string GetParameterDisplay(int index)
    {
        return null;
    }

    public string GetParameterName(int index)
    {
        return null;
    }

    public void SetSampleRate(float sampleRate)
    {
    }

    public void SetBlockSize(int blockSize)
    {
    }

    public void MainsChanged(bool onoff)
    {
    }

    public bool EditorGetRect(out System.Drawing.Rectangle rect)
    {
        return false;
    }

    public bool EditorOpen(System.IntPtr hWnd)
    {
        return false;
    }

    public void EditorClose()
    {
    }

    public void EditorIdle()
    {
    }

    public byte[] GetChunk(bool isPreset)
    {
        return null;
    }

    public int SetChunk(byte[] data, bool isPreset)
    {
        return 0;
    }

    #endregion

    #region IVstPluginCommandsBase Members

    public void ProcessReplacing(VstAudioBuffer[] input, VstAudioBuffer[] outputs)
    {
    }

    public void ProcessReplacing(VstAudioPrecisionBuffer[] input, VstAudioPrecisionBuffer[] outputs)
    {
    }

    public void SetParameter(int index, float value)
    {
    }

    public float GetParameter(int index)
    {
        return 0.0f;
    }

    #endregion

}
```


## See Also


#### Other Resources
<a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a><br />