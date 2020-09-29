# Delay Sample Plugin

> This plugin was generated from the Visual Studio VST.NET Audio Project Template.

The Delay sample plugin provides a stereo delay and demonstrates using Parameters, Programs and UI.

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

The `AudioProcessor` class implements the logic for changing the incoming 
audio into something that resembles a delayed signal on the output.
It does this through the Delay Dsp component - 
to demonstrate splitting up processing in separate components - 
useful for when complexity increases.

Note that the AudioProcessor uses two instances of the same `Delay` 
Dsp component to separately process the Left and Right audio channels.

### `Delay` class

The `Delay` class maintains a buffer that stores delayed audio which is added to the output signal at a later time.
Several parameters are used to determine delay time and feedback.

### `DelayParameters` class

The parameters of the Delay logic are separated out into a class of its own. 
This solves a potential cyclic dependency during DI container construction and ensures that all plugin parameters
can be registered even when the AudioProcessor was not created yet.

The `PluginParameters` class uses this class to build a complete list of plugin parameters.

## `PluginParameters` class

The `PluginParameters` class is a central place where parameters (infos) 
are registered and a complete picture of all plugin parameters is maintained.

The `PluginPrograms` class uses it when it creates programs.

## `PluginPrograms` class

This class is in charge of creating programs -also known as presets.
The Delay sample plugin creates 3 programs with each a full set of parameters.

## `PluginEditor` class

The `PluginEditor` class is the entry point for showing custom UI.
It passes on the plugin's parameters to the the actual view.

### `PluginEditorView` class

The `PluginEditorView` class implements the UI using a Windows Forms Control.
For each parameter it displays a track bar control to manipulate its value.
