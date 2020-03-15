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

* [Fixed] External dependencies (Microsoft.Extension.Configuration) are not found during load of Interop.
    => Forgot Recursive dependencies. Will popup msgbox with missing dll.

## Refactor wishes

* [Interop] Remove WinForms dependency from Interop? Still require 'windoswdesktop'...
* [Interop] remove config for assembly probing. Can be replaced with netcore .deps.json file
* [All] rename all Deprecated to Legacy (less confusing)
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
