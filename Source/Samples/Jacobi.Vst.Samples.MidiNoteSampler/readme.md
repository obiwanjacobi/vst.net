# Midi Note Sampler Sample Plugin

The Midi Note Sample sample plugin demonstrates the combination of an Audio and Midi plugin - in this case a crude sampler.

The sampler stores incoming stereo audio when a Midi note is received for the first time.
The next time that same Midi note is received it plays back what was recorder previously.

## `PluginCommandStub` class

The `Plugin.Interop` will create an instance of this class and route all calls 
from the host to the plugin to methods this class implements. 
The plugin framework will translate those calls to calls on the various framework interfaces the plugin implements.

The `PluginCommandStub` class creates the `Plugin` root class.

## `Plugin` class

This is the plugin root class. 
It manages handing out instances to framework interface implementations for the various functions.

In this case we opted for using Dependency Injection to manage creating instances and managing dependencies. 
In the `ConfigureServices` method, the plugin registers its implemented framework interfaces and other objects
with the service collection.

## `AudioProcessor` class

The `AudioProcessor` class manages recording and playing back the audio. 
Most of that logic is in the `SampleManager` class.

## `MidiProcessor` class

The `MidiProcessor` class listens for Midi note events and signals the `SampleManager` class of these events.

## `SampleManager` class

The `SampleManager` class contains the stereo buffers for the recorded audio for each Midi note.
