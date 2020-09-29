# Midi Note Mapper Sample Plugin

The Midi Note Mapper sample plugin demonstrates a Midi plugin that modifies incoming Midi note events.

The sample plugin has a custom UI (WinForms) and implements persistence, 
in order to be able to save the note mapping to file -usually a feature provided by the host.

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

The `AudioProcessor` class in this case is only used to time the trigger when the incoming Midi note events
are transformed. It is a typical scenario to use when implementing a Midi plugin.

## `MidiProcessor` class

Here the Midi events are received and put in a list to be output. 
When a match with the note mapping is detected, and altered event is added to the output list.

## `PluginPersistence` class

The `PluginPersistence` class implements serialization and deserialization of the Midi note map contents
when the host initiates a load or save of the song or effect chain.

The data is written into or read from a binary stream.

## `PluginEditor` class

The `PluginEditor` class is the entry point for showing custom UI.
The view shows the contents of the Midi note map and allows the user to change it.

### `MidiNoteMapperView` class

The `MidiNoteMapperView` class implements the UI using a Windows Forms Control.
It shows a list of mapped note events the user can manipulate.
