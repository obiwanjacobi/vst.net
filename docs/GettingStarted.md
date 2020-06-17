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

## Host

Add the `VST.NET2-Host` (PreRelease!) NuGet package to your project.

This gives you two references:

- Jacobi.Vst.Core
- Jacobi.Vst.Host.Interop

A good starting point for learning the VST.NET host API is the Host [Sample](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples).

## Deployment

For both the plugin Interop as well as the host Interop, you need to install the `C++ 2019 Redistributables` on the client machine. You need to match the Processor Architecture, either [32-bits](https://aka.ms/vs/16/release/VC_redist.x86.exe) or [64-bits](https://aka.ms/vs/16/release/VC_redist.x64.exe). If you have Visual Studio installed, these files are already present. But if you distribute your project made with VST.NET you need to install this on the client machine.

As of `2.0.0-RC1` the nuget packages for plugin and host both contain a build file to create a deployment after each successful build. The `deploy` folder is at the same location as the project binaries.
For instance: `MyProject\bin\Debug\netcoreapp3.1\deploy`.

If that doesn't work for you, you could use the [VST.NET Command Line Interface](cli) and use the `publish` command that can help you with deployment. The `vstnet.exe` is located in the nuget package cache folder. For instance for the plugin nuget package:
`C:\Users\[me]\.nuget\packages\vst.net2-plugin\2.0.0[-rc1]\tools\netcoreapp3.1`

---

> Back to [Index](index.md)
