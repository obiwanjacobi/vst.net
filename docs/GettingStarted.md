# Getting Started

VST.NET 2 is split into a plugin NuGet package and a separate package for writing a host application.

For both Plugin and Host scenario's no `AnyCpu` configuration is supported. Because `VST.NET` interoperates with native (unmanaged) code, the configuration has to be explicit: either x64 or x86 (32-bit).
That means that in your project file there should be this line, or similar:

```xml
<Platforms>x64;x86</Platforms>
```

But you still want to setup proper platform definitions in the Configuration Manager of Visual Studio.

> You do **NOT** have to build the source code to get started!

## Project

To start of your first project, a couple of Visual Studio project templates are available for C#.

These project templates demonstrate a possible way to structure your plugin.

- VstNetAudioPlugin: Gives you a working audio effect plugin of a stereo delay.
- VstNetMidiPlugin: Gives you a working Midi 'effect' plugin for Transposing notes and adjusting their Gain (velocity).

> Eventually these project templates should be easily installable, but until I figure out how to do that the following procedure can be followed to get them on your machine.

Download the [VstNetAudioPlugin.zip](./media/VstNetAudioPlugin.zip) and/or the [VstNetMidiPlugin.zip](./media/VstNetMidiPlugin.zip) files into your local folder on your machine at: '`C:\Users\[me]\Documents\Visual Studio 2019\Templates\ProjectTemplates\Visual C#`'.

An alternative is to open [the template solution](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Templates/CSharp) in Visual Studio and choose `Export Template...` from the `Project` menu for each of the projects.

After you've restarted Visual Studio the new `VST.NET` project templates should now be available in the 'Create New Project' dialog. Search for 'vst' to find them quickly.

## Plugin

Add the `VST.NET2-Plugin` NuGet package to your project.

This gives you three references:

- Jacobi.Vst.Core
- Jacobi.Vst.Plugin.Interop
- Jacobi.Vst.Plugin.Framework

There are several plugin [Samples](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples) that can be a good starting point to learn the VST.NET plugin API.

### Loading a Plugin

After you have compiled a sample -or your own- plugin it is time to load it into a host application.
If the build was successful you should have a deploy folder at `[MyProject]\bin\[x64|x86]\[Debug|Release]\net6.0\deploy`.

> The **Deploy** folder contains everything needed to load the plugin into a host application.

You can copy the entire folder to a new location where your DAW scans for plugins, or point the DAW towards the deploy folder.
This last option is preferable if you are developing.

Refer to the [Trouble Shooting](TroubleShooting.md) section for more info on problems with loading your plugin into a host application.

## Host

Add the `VST.NET2-Host` NuGet package to your project.

This gives you these references:

- Jacobi.Vst.Core
- Jacobi.Vst.Host.Interop
- A file link to Ijwhost.dll

The file-link is required to be able to run your host.exe from its build location (bin folder).

A good starting point for learning the VST.NET host API is the Host [Sample](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples).

## Deployment

For both the plugin Interop as well as the host Interop,
you need to install the `C++ 2022 Redistributables` on the client machine.
You need to match the Processor Architecture, either [32-bits](https://aka.ms/vs/17/release/VC_redist.x86.exe) or [64-bits](https://aka.ms/vs/17/release/VC_redist.x64.exe).
If you have Visual Studio installed, these files are already present.
But if you distribute your project made with VST.NET you need to install this on the client machine where your software will be running.

As of `2.0.0-RC1` the nuget packages for plugin and host both contain a build file to create a deployment after each successful build.
The `deploy` folder is at the same location as the project binaries: `[MyProject]\bin\[x64|x86]\[Debug|Release]\net6.0\deploy`.

---

> Back to [Index](index.md)
