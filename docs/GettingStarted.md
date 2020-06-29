# Getting Started

VST.NET 2 is split into a plugin NuGet package and a separate package for writing a host application.

> You do **NOT** have to build the source code to get started!

## Plugin

Add the `VST.NET2-Plugin` (PreRelease!) NuGet package to your project.

This gives you three references:

- Jacobi.Vst.Core
- Jacobi.Vst.Plugin.Interop
- Jacobi.Vst.Plugin.Framework

There are several plugin [Samples](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples) that can be a good starting point to learn the VST.NET plugin API.

### Loading a Plugin

After you have compiled a sample -or your own- plugin it is time to load it into a host application.
If the build was successful you should have a deploy folder at `[MyProject]bin\[x64|x86]\[Debug|Release]\netcoreapp3.1\deploy`.

> The **Deploy** folder contains everything needed to load the plugin into a host application.

You can copy the entire folder to a new location where your DAW scans for plugins, or point the DAW towards the deploy folder. 
This last option is preferable if you are developing.

Refer to the [Trouble Shooting](TroubleShooting.md) section for more info on problems with loading your plugin into a host application.

## Host

Add the `VST.NET2-Host` (PreRelease!) NuGet package to your project.

This gives you two references:

- Jacobi.Vst.Core
- Jacobi.Vst.Host.Interop
- A file link to Ijwhost.dll

The file-link is required to be able to run your host.exe from its build location (bin folder).

A good starting point for learning the VST.NET host API is the Host [Sample](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples).

## Deployment

For both the plugin Interop as well as the host Interop, 
you need to install the `C++ 2019 Redistributables` on the client machine. 
You need to match the Processor Architecture, either [32-bits](https://aka.ms/vs/16/release/VC_redist.x86.exe) or [64-bits](https://aka.ms/vs/16/release/VC_redist.x64.exe). 
If you have Visual Studio installed, these files are already present. 
But if you distribute your project made with VST.NET you need to install this on the client machine.

As of `2.0.0-RC1` the nuget packages for plugin and host both contain a build file to create a deployment after each successful build. 
The `deploy` folder is at the same location as the project binaries.
For instance: `MyProject\bin\Debug\netcoreapp3.1\deploy`.

---

> Back to [Index](index.md)
