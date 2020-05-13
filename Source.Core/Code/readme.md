# VST.NET netcore

The dotnet-core version of VST.NET.

## Issues

* Deployment is a mess
  https://github.com/dotnet/sdk/issues/6688
  https://github.com/NuGet/Home/issues/6645
  https://github.com/NuGet/Home/issues/8623
  https://github.com/dotnet/runtime/issues/18527
  https://github.com/natemcmaster/DotNetCorePlugins
  Goal is to 
  1. make a Jacobi.Vst package that can be included in plugin and host projects...
  Interop project is not seen/included. Looking for workaround/manual options...
  2. allow plugin to publish all dependencies to folder - incl. interop rename etc.
  https://github.com/dotnet/sdk/blob/master/src/Tasks/Microsoft.NET.Build.Tasks/ResolvePackageDependencies.cs

* [Fixed] External dependencies (Microsoft.Extension.Configuration) are not found during load of Interop.
    => Forgot Recursive dependencies. Will popup msgbox with missing dll.

## Refactor wishes

* [Interop] Remove WinForms dependency from Interop? Still require 'windoswdesktop'...
* [Interop] remove config for assembly probing. Can be replaced with netcore .deps.json file
* [All] rename all Deprecated to Legacy (less confusing)
* [All] remove all [Obsolete] API.
* [Framework] remove thread management from interface manager (simplifies the class)
* [Interop] split interop for plugin and host into separate assemblies (duplicate commonalities?).
* [Core/Framework] merge all managed code into one assembly?
* [Interop/Core] Use new/different managed vst.net dll extensions (remove: .net.dll   .net.vstdll is ok    add: .net.vst2 or .netcore.vst2?)
* [DevOps] automate CI build on github
    => https://www.continuousimprover.com/2020/03/reasons-for-adopting-nuke.html
* [Core/Framework] is there a better wording for stub and proxy? (CommandStub / CommandProxy)
* [Core] Remove Offline types (VstOfflineTask)

## References

https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#load-plugins

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.

## Research

COM (VST3?)
https://github.com/dotnet/runtime/blob/master/docs/design/features/COM-activation.md#NET-Framework-Class-COM-Activation


---

## VST.NET Plugin

Creating a VST.NET plugin with the new DotNet Core libraries.

### Development

Install VST.NET dotnet core nuget package into your plugin project.

### Deployment

You either deploy 32-bits (x86) or 64-bits (x64). There is no `AnyCPU`.

> You can run `dotnet publish -o deployment` to get a start on finding the dependencies.

* `ijwhost.dll` needs to be present in the same folder your managed plugin dll is installed to. 
There is a x86 and a x64 version - match it with your plugin.
* Rename your plugin to end with either the `.net.dll` or `.net.vstdll` extension.
* Jacobi.Vst.Interop.dll needs to be renamed to the same (orignal) name of your plugin.
* Rename the Jacobi.Vst.Interop.runtimeconfig.json to match your plugin: `[myplugin].runtimeconfig.json`

You should now be able to load your plugin in the host. 
VST.NET will show bootstrap errors with messages telling you which assemblies were not found (FileNotFoundException).
These assemblies have to be added to your 'deployment' folder.
Once you can load you plugin in the host without it showing errors 
-click through your plugin, assemblies may be lazy loaded- 
you have the set of files you need to deploy in order for your plugin to work on another machine.

An alternative way to find all assembly dependencies is to follow all [assembly].deps.json files.

## VST.NET Host

Creating a VST.NET host with the new DotNet Core libraries.

### Development

Install VST.NET dotnet core nuget package into your host project.

### Deployment

You either deploy 32-bits (x86) or 64-bits (x64). There is no `AnyCPU`.

* `ijwhost.dll` needs to be present in the same folder your managed host exe is installed to. 
There is a x86 and a x64 version - match it with your host.
