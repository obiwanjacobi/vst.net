# Implement an Audio Processor

There are three basic forms of <a href="1977452f-9b2d-4d4f-a93c-768ab2ede63e">Audio Processing</a> a Plugin can perform.
&nbsp;<ul><li>An Effect.</li><li>A Synthesizer.</li><li>A recorder (sampler).</li></ul>&nbsp;
An effect Plugin will change the audio signal to produce the desired effect. Reverb, chorus and overdrive are examples of this. A Synthesizer Plugin generates an audio signal using either oscillators or pre-recorded sounds triggered by a Midi note event. A recording Plugin or sampler records the audio for playback at a later time. Playback is usually also trigged by Midi note events.


To implement any of these three types of audio processing a Plugin needs to implement the IVstPluginAudioProcessor interface or the IVstPluginAudioPrecisionProcessor interface. The `IVstPluginAudioPrecisionProcessor` interface is only needed when you wish to perform audio processing in double in stead of single (float) precision.



## Implement the Audio Processor

The following code snippet shows a very basic implementation of the `IVstPluginAudioProcessor` interface. It implements an audio through, passing the input to the output unchanged.


**C#**<br />
``` C#
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;

class AudioProcessor : IVstPluginAudioProcessor
{
    #region IVstPluginAudioProcessor Members

    public int BlockSize { get; set; }

    public int InputCount
    {
        get { return 2; }
    }

    public int OutputCount
    {
        get { return 2; }
    }

    public double SampleRate { get; set; }

    public int TailSize
    {
        get { return 0; }
    }

    public void Process(VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
    {
        VstAudioBuffer input = inputs[0];
        VstAudioBuffer output = outputs[0];

        for (int index = 0; index < output.SampleCount; index++)
        {
            output[index] = input[index];
        }

        input = inputs[1];
        output = outputs[1];

        for (int index = 0; index < output.SampleCount; index++)
        {
            output[index] = input[index];
        }
    }

    public bool SetPanLaw(VstPanLaw type, float gain)
    {
        return false;
    }

    #endregion
}
```


## Changing the Interface Manager

Just implementing the `IVstPluginAudioProcessor` interface is not enough. The framework has no way of knowing that you have done so. The `IExtensible` interface on the Plugin root object is used to discover the interfaces a Plugin supports. Assuming you use the PluginInterfaceManagerBase base class either as a base class for your Plugin root object or as a base class for a seperate derived Interface Manager class, you have to override the CreateAudioProcessor() virtual method. This method that is called when the `IVstPluginAudioProcessor` interface is requested for the first time and returns the instance of your `IVstPluginAudioProcessor` implementation.


The following code snippet is the same as shown in the topic about the <a href="62feac6e-0c75-4ef8-8703-fb970f81280b">Plugin Root Class</a> and show a possible implementation for the `CreateAudioProcessor` method.


**C#**<br />
``` C#
using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

class MyPlugin : VstPluginWithInterfaceManagerBase
{
    public MyPlugin()
        : base("My Plugin", 
            new VstProductInfo("My Product", "My Vendor", 1000),
            VstPluginCategory.Synth, 
            VstPluginCapabilities.NoSoundInStop, 
            0, 
            0)  // enter unique plugin ID
    {}

    protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
    {
        if (instance == null) return new AudioProcessor(this);

        // base class just returns 'instance'
        return base.CreateAudioProcessor(instance);
    }

    // other methods...
}
```
&nbsp;<table><tr><th>![Caution](media/AlertCaution.png) Caution</th></tr><tr><td>
You have to synchronize access to shared variables you use in the audio processor. Generally hosts use multiple threads to call into the different parts of a plugin. Refer to <a href="T_Jacobi_Vst_Core_VstProcessLevels">VstProcessLevels</a> for more information.</td></tr></table>&nbsp;
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Note that the implementation passes a reference to the Plugin root object to the constructor of the `AudioProcessor` class. Although this was not show in the code snippet in the previous section it is in general a pattern that you will encounter; to tie the separate classes together.</td></tr></table>&nbsp;
This example is also applicable to other interfaces in the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> assembly. For each interface there is a corresponding `Create` method on the `PluginInterfaceManagerBase` base class.
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Implementing audio processing for Core Level Plugins is basically the same as shown here. There are two method overloads of ProcessReplacing() on the `IVstPluginCommandStub` interface. One is for single precision and one for double precision audio processing. You can implement SetProcessPrecision() to receive an indication of which will be used (if supported by the host).</td></tr></table>

## See Also


#### Reference
IVstPluginAudioProcessor<br />IVstPluginAudioPrecisionProcessor<br />

#### Other Resources
<a href="1977452f-9b2d-4d4f-a93c-768ab2ede63e">Audio Processing</a><br />