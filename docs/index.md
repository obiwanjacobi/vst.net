# VST.NET 2

> **`Currently in Beta.`**

Welcome to VST.NET version 2 built on .NET core.

This library support building Steinberg's VST2 plugins and host applications.

## Getting Started

VST.NET 2 is split into a plugin NuGet package and a separate package for writing a host application.

### Plugin

Add the `VST.NET2-Plugin` NuGet package to your project.

This gives you three references:

- Jacobi.Vst.Core
- Jacobi.Vst.Plugin.Interop
- Jacobi.Vst.Plugin.Framework

There are several plugin [Samples](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples) that can be a good starting point to learn the VST.NET plugin API.

### Host

Add the `VST.NET2-Host` NuGet package to your project.

This gives you two references:

- Jacobi.Vst.Core
- Jacobi.Vst.Host.Interop

> At this time there is no host framework.

A good starting place for learning the VST.NET host API is the Host [Sample](https://github.com/obiwanjacobi/vst.net/tree/master/Source/Samples).
