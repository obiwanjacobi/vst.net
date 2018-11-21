# VST.NET
I have recently started an open source project that aims to bring Virtual Studio Technology (VST) - an audio and midi processing Plugin interface standard owned by Steinberg - to the .NET world.

The native VST interface consists of a C++ SDK on top of a C procedural interface. The actual interchange of information between Host (for instance a sequencer application like Cubase) and the Plugin takes place through function pointers using opcodes. An opcode is basically a message identifier that states the action to be taken, either by the Host or the Plugin.

A Plugin is allowed to process digital audio (samples) and midi information and also has the option to parameterize its operation and to provide a custom UI. Most of these features are optional. Plugin capabilities can be queried by the Host as can host capabilities be queried by the Plugin.

On top of this plain C interface definition of function pointers and opcodes an (somewhat) object oriented C++ layer is provided in the Steinberg VST SDK. But this SDK does hardly anything to soften (or structure) the overwhelming number of methods.

VST.NET does nothing with the C++ SDK classes that Steinberg provides. It interfaces at the lowest level with the C function pointers and opcodes and translates those to and from managed code (C# in this case). But besides just providing the marshaling between managed and unmanaged worlds, VST.NET also adds a Framework in an attempt to structure and clarify the posibilities of the VST interface. I have written about this idea in a [previous post](http://obiwanjacobi.blogspot.com/2007/09/redesigning-steinberg-vst-sdk.html).

## Interop
So how do you interop bewteen C and managed code? Well, Microsoft was kind enough to put managed code features in C++ as well. This means that you can write fully managed code in C++ or mix between native unmanaged C++ and managed C++ in one assembly. It is this ability that lies at the heart of the Interop solution.

The Interop assembly in VST.NET is a C++ mixed assembly. On one side it interfaces with the native C interface using function pointers and opcodes while on the other side it forwards those calls to a managed object after it has converted the native structs to managed structs.

There are two objects involved in marshaling a call from the Host to a Plugin. The PluginCommandProxy is managed a C++ class that has a reference to the PluginCommandStub, a managed implementation of the .NET interface: IVstPluginCommandStub. It is the proxy that takes the calls from the Host and converts the parameters and calls the managed Stub. The IVstPluginCommandStub interface just lists all methods the Plugin could implement (in a versioned hierarchy).

There are also two objects involved in marshaling a call from the Plugin to the Host. The HostCommandStub implements the managed .NET interface IVstHostCommandStub that lists all the methods a Host can implement (also in a versioned hierarchy). The implementation of this interface is passed to the managed Plugin in (at a very early stage) in order to be able to call the Host. All conversion (marshaling) of method parameters takes place in the stub. Wether or not the Plugin implements a proxy is up to the plugin developer.

## The Core
Basically all C datatypes (mostly structs and enums) defined in the VST interface have a managed counterpart living in the Core assembly. This assembly is a C# .NET assembly and that is its sole purpose. The Interop assembly references the Core assembly to be able to marshal to (and sometime from) the managed types. The Core assembly also contains the IVstPluginCommandStub and IVstHostCommandStub interface definitions.

It is posible to create a managed Plugin interfacing at the Core level of VST.NET. The plugin would have to implement the IVstPluginCommandStub interface in a public class that will get called by the Interop assembly. But this interface does nothing to structure, group or partition the method available to the Plugin.

When trying to adopt an existing managed code audio or midi processing logic to a VST Plugin, interfacing at the Core level might be the best thing to do. This would produce the least overhead and you would only have to connect the IVstPluginCommandStub methods you need (like I said: a lot of Plugin features are optional) to your existing code.

## The Framework
Basically the Framework provides the managed plugin developer with a set of clearly defined interfaces that represent the total set of features available to the Plugin. All plugins must implement the IVstPlugin interface for it contains the capability 'mechanism' to communicate to the Host what features are implemented (more on this later) along with some basic product information.

So if a Plugin wants to process digital audio samples it implements the IVstPluginAudioProcessor interface. If it provides its own GUI it implements IVstPluginEditor. If it supports parameters and programs it implements the IVstPluginParameters and IVstPluginPrograms interfaces (etc.). The Framework defines an interface for each group of functionality the Plugin can implement.

The Plugin root object - that implements IVstPlugin - also implements the IExtensibleObject interface. This interface allows you to do a 'query interface' on the object. Through this mechanism the Plugin can publish what interfaces it supports. So IExtensibleObject.GetInstance&lt;IVstPluginAudioProcessor&gt;() queries for the audio processor interface. If the Plugin returns null it doesn't support audio processing. The reason to introduce this mechanism lies in the desire to be able to dynamically determine the capabilities of a plugin (there is a class of Plugin that requires that feature) but also to communicate these capabilities in coherent interface definitions.

## Loading the Managed Plugin
There is one part missing from this story. How does the managed Plugin get loaded and how does the Interop assembly acquire a reference to the implementation of the IVstPluginCommandStub interface.

The Interop assembly exports a 'main' method. This is part of the VST interface definition. The exported main method is called by the Host after the dll has been loaded. The host passes its own function pointer to the main and expects a structure that defines the plugin it is dealing with (this structure holds several function pointers to the Plugin). So the Host thinks the our Interop assembly contains the actual Plugin. But our managed Plugin is located in another asssembly. The Interop main function uses a Loader (located in Core) to find and load the managed plugin assembly and create an instance of the public class that implements the IVstPluginCommandStub interface. There is a naming convention that requires the managed plugin assembly to have a .net postfix to the assembly name in order for the Interop assembly - also renamed - to find it.

Both the Core and Framework assemblies should either be located in the same loaction as the renamed Interop and managed plugin assembly or they can be installed into the GAC which makes sense if you have a number of managed plugins installed on one machine.

## Wrapping up
Well, currently I'm still developing VST.NET so some details mentioned here might change. I hope you have some idea how VST.NET works and how you can use it. You can always leave questions at the VST.NET site at codeplex or email me at obiwanjacobi at hotmail dot com.