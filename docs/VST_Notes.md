# Programs and Parameters

A Program is a preset of parameter values that allows a user to quickly initialize the plugin with those settings. A Parameter is one setting that describes what kind of behavior the setting exhibits (is it a switch or a fader) and the way the value is manipulated (See also VstParameterProperties).

A plugin can have multiple Programs and also have multiple Parameters. Each Program should therefor have the same list of all plugin Parameters (with different values for these Parameters per Program).

This means that the plugin should implement IVstPluginPrograms if it wishes to support Programs for the plugin. If it does, the requested IVstPluginParameters can be serviced from the current active VstProgram (that also implements this interface). If the plugin does not implement IVstPluginPrograms it can still provide an implementation for IVstPluginParameters. It just means that the plugin does support paramaters (you can tweak it) but not a mechanism to store multiple presets (Programs) for the Parameters.

Q: How are Programs (and their Parameters) persisted?
A: I think that is where GetChunk/SetChunk come in.

Besides implementing the IVstPluginPrograms and IVstPluginParameters interfaces you'd might also want to override the VstProgram(Collection) and the VstParameter(Collection) classes. These types serve as base classes the Framework works with but you are free to override them. Custom value formatting and parsing back values as strings are typical scenarios for overriding these classes.


# Midi Programs
Midi Programs are presets of settings used by a Midi Processor plugin (a Synth for example) to make it easy to for the user to switch 'midi behavior' of the plugin for a specific midi channel (Perhaps Midi Patch would be a better term?). A Midi Program can be linked to a Category and Categories can be hierarchical. A Midi Program can be set through MIDI using a Bank Select and a Program Change.
When a plugin implements the IVstMidiPrograms interface, it communicates its 'patches' and the categories they fall under to the Host.

Q: What is the relation of Midi Programs to plugin Programs and Parameters?

# Shell Plugins
A shell plugin is a plugin that hosts other plugins. The IVstPluginHost interface must be implemented to trigger this functionality (provided, the host supports it). The exported main function is called for each 'sub-plugin' that has to be activated. The Host will set the currentUniqueID to the ID of the plugin it wants to load. So the VSTMain logic should check the current unique ID and if non-zero it should forward the call to the IVstPluginHost interface. Plugins that implement this interface also have the option to override/intercept the calls the plugin makes to the Host, by providing their own implementation of IVstHost and related interfaces.

Note: We need to refactor the interop if we want to support this scenario. Currently the AEffect struct passed to the plugin callback procedures are ignored. They identify the actual (sub)plugin.


* Is it a good idea to provide a base implementation for IVstPluginAudioProcessor that by default implements an audio pass-thru (bypass)?
* Where do we put SetPanLaw?