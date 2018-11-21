# VST.NET Plugin Persistence
VST.NET provides a simple way for a VST Plugin to persist its internal state. This post discusses how to implement persistence in a VST.NET Framework Plugin.

Before you start to implement persistence into your plugin make sure you need it. The Host will in general save all parameter data instead when no persistence support is detected in the Plugin. So when all you need are your Plugin Parameters you don't have to do anything.

But as soon as you support Programs; the ability to have multiple (editable) presets with Parameter settings then you have to implement persistence. When your plugin contains custom configuration data that is not expressed in Parameters (such as mapping items in a Midi Note Mapper), implementing persistence will be your only way to allow the user to save and load those settings inside the Host.

Another reason to implement persistence (even if you don't support Programs) is when you like to be backwards compatible with an earlier version of your Plugin. When the new version of your Plugin should be able to read in data of older versions, you will need to handle that manually.

Persistence is implemented through implementing an interface, like any other feature in the VST.NET Framework. The `IVstPluginPersistence` interface contains methods for reading and writing chunks of data and determining the version of the plugin that wrote the data (called during reading).

How and where you wish to implement the `IVstPluginPersistence` interface in your Plugin code design is up to you. Some options are discussed in this post: [VST.NET Plugin Structure](VST.NET%20Plugin%20Structure%20post.md).

An important concept of VST persistence is that it has two modes: Persist a single (current) Program or all Programs. But you'll not see any method parameters to distiguish between the two modes in VST.NET. You'll only have to worry about implementing one set of persistence methods.

The `ReadPrograms` method reads multiple Programs from a Stream and stores them in the collection passed into the method. The `WritePrograms` method also takes a collection of Programs you have to serialize into the Stream. It doesn't really matter where those Programs came from (current or all). Its good practice to serialize a Program count into the stream so you know -when reading it back in- how many Programs to expect.

The `CanLoadChunk` method returns a boolean value to indicate if the data can be read. The method accepts a structure that contains details on the Plugin that wrote the data and its version.

Its important that you write out data in the exact same sequence as you expect to read it back in. One way to ensure this, is to make Reader and Writer classes in your plugin. These classes will be constructed on the Stream and have public methods to de-/serialize an object, say a Program or a Parameter.

Having these classes in your plugin architecture will also help moving to a new version while still supporting backwards compatibility.

So thats it. Implement three methods, expose the `IVstPluginPersistence` interface through the `PluginInterfaceManager(Base)` and you have persistence in your plugin.

Have any questions or suggestions? Leave them at VST.NET at the Discussion list.