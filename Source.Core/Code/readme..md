# VST.NET netcore

The dotnet-core version of VST.NET.

## Issues

* Replace Configuration/AppSettings (with what? see ConfigurationBuilder)
* Will AppDomain (now AssemblyLoadContext?) and dynamic assembly loading still work?
* Remove WinForms dependency from Interop


## Refactor wishes

* [Interop] remove config for assembly probing. Can be replaced with netcore .deps.json file
* [All] rename all Depricated to Legacy (less confusing)
* [Framework] remove thread management from interface manager (simplifies the class)
* [Interop] split interop for plugin and host into separate assemblies.
* [Interop/Core] Use new/different managed vst.net dll extensions (remove: .net.dll   .net.vstdll is ok    add: .net.vst2 or .netcore.vst2?)
* [DevOps] automate CI build on github
* [Core/Framework] is there a better wording for stub and proxy? (CommandStub / CommandProxy)

## References

Dotnet core hosting
https://docs.microsoft.com/en-us/dotnet/core/tutorials/netcore-hosting
https://github.com/dotnet/core-setup/blob/master/Documentation/design-docs/native-hosting.md
https://github.com/dotnet/samples/tree/master/core/hosting
https://github.com/dotnet/runtime/blob/master/src/coreclr/src/hosts/inc/coreclrhost.h
https://github.com/mjrousos/SampleCoreCLRHost
https://www.youtube.com/watch?v=GTq03YpWUoY
https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://github.com/dotnet/cli/blob/v2.0.0/Documentation/specs/corehost.md

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.

COM (VST3?)
https://github.com/dotnet/runtime/blob/master/docs/design/features/COM-activation.md#NET-Framework-Class-COM-Activation
