# Audio Processing

There are three basic types of audio processing that a Plugin can perform.
&nbsp;<ul><li>An Effect.</li><li>A Synthesizer.</li><li>A recorder (sampler).</li></ul>&nbsp;
An effect Plugin will change the audio signal to produce the desired effect. Reverb, chorus and overdrive are examples of this. A Synthesizer Plugin generates an audio signal using either oscillators or pre-recorded sounds triggered by a Midi note event. A recording Plugin or sampler records the audio for playback at a later time. Playback is usually also trigged by Midi note events.


To implement any of these three types of audio processing a Plugin needs to implement the IVstPluginAudioProcessor interface or the IVstPluginAudioPrecisionProcessor interface. The `IVstPluginAudioPrecisionProcessor` interface is only needed when you wish to perform audio processing in double in stead of single (float) precision.



## The IVstPluginAudio(Precision)Processor interface

The IVstPluginAudioProcessor interface indicates to the Framework (and the Host indriectly) that the Plugin processes audio in some form or another. The `Process` method receives the input audio buffers - the audio the Plugin can analyse and perform its algorithms on - and the output audio buffers - the place where the Plugin stores the result of its processing. The number of audio buffers that you can expect for input and output is specified in the <a href="62feac6e-0c75-4ef8-8703-fb970f81280b">Plugin Root Class</a>. The `Process` method on the Audio Processer is called repeatedly in rapid succession. This means that you need to be quick with you processing in order to keep the latency down as much as possible. The audio buffers are the **not** copied at all during the marshalling of the `Process` call.
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Because of the Interop marshalling that is done to get from unmanaged to managed code and visa versa, the performance of a VST.NET Plugin can never be the same as a Plugin written in C++ and using hand-optimized assembly code (SSE).</td></tr></table>&nbsp;
The principles and interactions are the same for both normal (single or float) precision as wel as for double precision audio processing. With single audio processing each audio sample is stored in a 32-bit float datatype. With double audio processing, the audio sample is stored in a 64-bit double datatype.


The IVstPluginAudioPrecisionProcessor interface derives from the IVstPluginAudioProcessor interface. Which means that when you want to support the double precision audio processing you must also support the normal precision audio processing. This design decision was made because there are not many hosts out there that support double precision audio processing.



## The VstAudio(Precision)Buffer

The <a href="T_Jacobi_Vst_Core_VstAudioBuffer">VstAudioBuffer</a> class wraps the unmanaged sample array (float*) and exposes this as a managed indexable array of `float`s. The Plugin can use the `SampleCount` property to determine how many audio samples are in the buffer.


An instance of the <a href="T_Jacobi_Vst_Core_VstAudioBuffer">VstAudioBuffer</a> can be cast to the <a href="T_Jacobi_Vst_Core_IDirectBufferAccess32">IDirectBufferAccess32</a> interface. This interface allows you access to the unmanaged (and unsafe) underlying array directly. This gives the developer the opportunity to work with the unmanaged buffer directly.


For double precision audio processing the <a href="T_Jacobi_Vst_Core_VstAudioPrecisionBuffer">VstAudioPrecisionBuffer</a> represents the audio buffer. It can be cast to <a href="T_Jacobi_Vst_Core_IDirectBufferAccess64">IDirectBufferAccess64</a> to gain access to the underlying unmanaged array of `double`s.



## The VstPluginAudio(Presision)ProcessorBase base class

Because processing audio is something most Plugins do, VST.NET provides a base class to eas implementing an audio processor class. The VstPluginAudioProcessorBase abstract base class provides implementation for all interface members. The implementation of the `Process` method is a pass-through of the audio input to the output. You typically overide the `Process` method and implement your custom audio processing in that overridden method. When you want to bypass your Plugin audio processing you can call the base implementation of the `Process` method.


The VstPluginAudioPrecisionProcessorBase abstract base class provides the same eas for double precision audio processing. The class is derived from the VstPluginAudioProcessorBase base class in-line with the inetrface inheritence. Here also, the base implementation of the `Process` methods provides an audio-through.



## The IVstPluginBypass interface

Although it is defined as a seperate interface, the IVstPluginBypass interface is typically implemented on the audio processor class. The audio processor should bypass its processing algorithms when the Bypass property is set to true.
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
When you implement multiple interfaces on the same class, remember to pass the correct instance from the virtual `CreateXxxxx` methods of the <a href="0aca5a96-16d9-4f8e-a830-202d8bad418a">Plugin Interface Manager</a>. In the example above you would implement the `CreateBypass` method as follows: 
**C#**<br />
``` C#
protected override IVstPluginBypass CreateBypass(IVstPluginBypass instance)
{
    if(instance == null)
    {
        return GetInstance<IVstPluginAudioProcessor>() as IVstPluginBypass;
    }

    // assume the default implementation is also thread-safe.
    return instance;
}
```</td></tr></table>

## See Also


#### Reference
IVstPluginAudioProcessor<br />IVstPluginAudioPrecisionProcessor<br /><a href="T_Jacobi_Vst_Core_VstAudioBuffer">VstAudioBuffer</a><br /><a href="T_Jacobi_Vst_Core_VstAudioPrecisionBuffer">VstAudioPrecisionBuffer</a><br />IVstPluginBypass<br />VstPluginAudioProcessorBase<br />VstPluginAudioPrecisionProcessorBase<br />

#### Other Resources
<a href="62feac6e-0c75-4ef8-8703-fb970f81280b">Plugin Root Class</a><br /><a href="0aca5a96-16d9-4f8e-a830-202d8bad418a">Plugin Interface Manager</a><br /><a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a><br />