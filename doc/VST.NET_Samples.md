TIP: If you're a VB.NET programmer you can use Reflector ([http://www.red-gate.com/products/reflector/](http://www.red-gate.com/products/reflector/)) to display the source code for the compiled Sample assemblies. There are even add-ins for Reflector that allow you to save them as a Visual Studio Solution.

# Jacobi.Vst.Samples.CorePlugin
The CorePlugin demonstrates how to interface with VST.NET at 'core level'. The plugin implements the IVstPluginCommandStub interface in a public class. This class will be created by the loader in the Interop assembly. The actual implementation is very simplistic in the sample; each method on the PluginCommandStub just logs a text line with some additional information to the plugin editor. The plugin editor is a WinForms user control that is attached the the main window that is provided by the host.

When you have existing managed code that is a synth or some effect, it might be simplest to route the methods of the PluginCommandStub to those classes, instead of building on top of the Framework, which introduces an extra layer with fixed interface definitions that might not suite your design. Note you don't have to route all the methods of the PluginCommandStub, just those that you need.

# Jacobi.Vst.Samples.Delay
The delay sample is build on the Framework and is a sample of an effect plugin. The sample is implemented using seperate classes for each VST feature and does not use any of the Plugin base classes (only the PluginInterfaceManagerBase). The plugin root class, FxTestPlugin, is created by the TestPluginCommandStub; the (only) public class that is derived from the StdPluginCommandStub class. 

The FxTestPlugin root class contains a reference to an instance of the Delay class, which implements the actual delay audio processing (single sample). It also references a FxPluginInterfaceManager instance that is derived from the PluginInterfaceManagerBase and implements the creation of the other sub components of the delay plugin. 

The AudioProcessor class implements the IVstPluginAudioProcessor interface and calls the Delay class during processing. 
The PluginPersistence class implements the IVstPluginPersistence interface and (de)serializes the Program and Parameter information (from/) into a Stream.
The PluginPrograms class implements the IVstPluginPrograms interface and manages the (3) programs of the Delay plugin. Each program contains all (4) Parameters. During initialization of the Programs the PluginParameterFactory class is used to fill each Program's Parameter collection.
The PluginParameterFactory class is initialized (in the plugin root class constructor) with the Parameters that the Delay class exposes.

Note that this sample used to be called Framework.TestPlugin. Some of the class names still hint on that legacy.
Also note that this sample is also available in VB.NET: Jacobi.Vst.Samples.Delay.VB.

# Jacobi.Vst.Samples.MidiNoteMapper
The Midi Note Mapper is build on the Framework and demonstrates how to do Midi In / Out. The plugin allows the user to map incoming Midi notes to other notes. The plugin implements a custom Editor and shows you how to implement that.

The Plugin root class derives in this sample from the PluginInterfaceManageBase and implements the IVstPlugin and IVstPluginMidiSource interfaces. So you'll see all the CreateXxxx overrides of the interface manager on this Plugin root class. This demonstrates that you do not need to have a seperate class for the interface manager but also have the option to build it into one class. Note that the CreateMidiSource always returns 'this' for that interface is also implemented on the Plugin root class. The Plugin root class also maintains the map with note items.

The MidiProcessor class implements IVstMidiProcessor and filters the incoming Midi Events and stores only the Note on and Note off messages. The midi note events are stored to be put out to the host during an (audio) process cycle later on. Some hosts might require this but its easily changed. The AudioProcess implements IVstPluginAudioProcessor and reports zero input and output channels but does send the stored midi events to the Host.

The PluginEditor implements the IVstPluginEditor and will show the WinForms UI (MidiNoteMapperUI). The editor uses the WindowsFormsWrapper to wrap the WinForms User Control and attach it to the parent window of the host when Open is called. Before the UI is shown, the note map (from the Plugin root class) is assigned to the User Control so it can manipulate its contents.

The MidiNoteMapper UI User Control displays the list of note mappings and buttons to add, edit and delete a map item. The MapNoteDetails Form shows the user the details of one map item: the trigger note number, the name of the item, and the output note number.

# Jacobi.Vst.Samples.MidiNoteSampler
The Midi Note Sampler demonstrates a very simplistic sampler that will allow the user to sample the current audio stream using the key on his (virtual) keyboard. When a Midi note is first received, the audio is recorded until the key is released. When that same note is received again, the sample is played back (not mixed with the current audio stream).

The Plugin root class is derived from the new VstPluginWithInterfaceManagerBase base class provided by the Framework. This demonstrates that using this base class cuts down on the code the plugin developer has to write. The Plugin root class also contains an instance of the SamplerManager (discussed later).

The MidiProcessor implements the IVstMidiProcessor and filters incoming Midi events for note on and note off messages and calls the SampleManager Note on and Note off methods. The AudioProcessor implements the IVstPluginAudioProcessor and use the SampleManager to pass on the audio sample in case a note is recording or playback a sample in case a note is playing. If there is no sample playing, the incoming audio is passed to the output. The AudioProcessor reports 2 input and 2 output channels. All samples recorded and played back are in stereo.

The SampleManager class manages a list of sample buffers (the recorded samples) and the process of recording and playing back the audio samples. When the ProcessNoteOnEvent method is called by the MidiProcessor, the SampleManager checks the buffer list to see if it should record or playback a sample. If the note number is not in the list the SampleManager will instantiate a private SampleRecorder class and will let the AudioProcessor provide the actual audio samples through a call to RecordAudio. When the note is in the buffer list it instantiates the private SamplePlayer class and passes it the stored audio buffer that holds the recorded audio samples. Then it will wait until the AudioManager calls the PlayAudio to play back the sample.

Note that the sample does not look at any timing information in the Midi Note events. All events are processed as if they all ocurred at the begining of the current cycle. Same goes for the sampling of audio. The complete audio buffer is always used in either recording or playing back the samples.


