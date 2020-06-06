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

The [VST.NET Command Line Interface](cli) has a `publish` command that can help you with deployment. This is for both plugin projects as well as host projects.

For both the plugin Interop as well as the host Interop, you need to install the `C++ 2019 Redistributables` on the client machine. You need to match the Processor Architecture, either [32-bits](https://aka.ms/vs/16/release/VC_redist.x86.exe) or [64-bits](https://aka.ms/vs/16/release/VC_redist.x64.exe).

---

> Back to [Index](index)
