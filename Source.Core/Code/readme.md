# VST.NET netcore

The dotnet-core version of VST.NET.

## TODOs

* Merge core branch into master


## Issues

* NuGet Deployment is a mess
  https://github.com/dotnet/sdk/issues/6688
  https://github.com/NuGet/Home/issues/6645
  https://github.com/NuGet/Home/issues/8623
  https://github.com/dotnet/runtime/issues/18527
  Nuget package platform architecture issues:
  https://github.com/NuGet/docs.microsoft.com-nuget/issues/1083#issuecomment-597886840
  https://github.com/dotnet/roslyn/blob/master/docs/features/refout.md
  The VST.NET CLI can perform a publish of a plugin and gather all dependencies into folder.
  NuGet still gives problems with x86/x64 managed assemblies.

* Add vstnet cli as build tool and add a publish command to the project.
Then on a successful build a deployment will automatically be generated.
https://natemcmaster.com/blog/2017/11/11/build-tools-in-nuget/

* CLI does not detect dependencies of dependencies (interop)
* CLSComplient has no great support in netCore3.1. I think I will remove it too. (revisit RegisterServices)

## Refactor wishes

* [All] rename all Deprecated to Legacy (less confusing)
* [All] remove all [Obsolete] API.
* [Framework] remove thread management from interface manager (simplifies the class)
* [Framework] .NET Core DI instead of interface manager?
* [Interop] look into the use tracked references (%^)
* [DevOps] automate CI build on github
    => https://www.continuousimprover.com/2020/03/reasons-for-adopting-nuke.html
* Update docs: convert the Sandcastle .aml file to .md. Deploy source code docs (.xml) with nuget. Exit Sandcastle tool.
https://github.com/EWSoftware/SHFB
https://github.com/maxtoroq/sandcastle-md
* [Core/Framework] Double check to see if Core and Framework need to be x86/x64 or could be AnyCPU?
* Turn on nullable reference types.
* Update code to modern constructs (linter suggestions)
* [UnitTest] Use FluentAssertions, remove TestContext prop.
* [Core] Try to get rid of the Adapters that pass essentially the same interface between Host and Plugin.
* [Release] have conditionals to ommit debugging/trace and checks from release builds.
* ObservableCollection is in System.Collections.DataModel

## Decisions

* Will not multitarget the projects to support both netFx and netCore. 
Future seems to lie with netCore (.NET5) that will be VST.NET v2.
Current v1.1 will continue to exist for current users but not be developed further (separate branch?).
VST.NET1 = VST2/netFx, VST.NET2 = VST2/netCore, VST.NET3 = VST3/netCore (if we get it to work)


## References

https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#load-plugins

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.

## Research

COM (VST3?)
https://github.com/dotnet/runtime/blob/master/docs/design/features/COM-activation.md#NET-Framework-Class-COM-Activation

- `ISSUE`: ijwhost/clr loading does not work on a mixed assembly! 
That means the plugin wont load in the unmanage host.


---

## VST.NET Plugin

Creating a VST.NET plugin with the new DotNet Core libraries.

### Development

Install VST.NET dotnet core nuget package into your plugin project.

### Deployment

You either deploy 32-bits (x86) or 64-bits (x64). There is no `AnyCPU`.

> You can run `vstnet publish <targetpath> -o <out folder>` to get all the dependencies into one folder.

You should now be able to load your plugin in the host.

## VST.NET Host

Creating a VST.NET host with the new DotNet Core libraries.

### Development

Install VST.NET dotnet core nuget package into your host project.

### Deployment

You either deploy 32-bits (x86) or 64-bits (x64). There is no `AnyCPU`.

* `ijwhost.dll` needs to be present in the same folder your managed host exe is installed to.
There is a x86 and a x64 version - match it with your host.
