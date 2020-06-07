# VST.NET netcore

The dotnet-core version of VST.NET.

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
  https://github.com/dotnet/sdk/issues/10875

* Add vstnet cli as build tool and add a publish command to the project.
Then on a successful build a deployment will automatically be generated.
https://natemcmaster.com/blog/2017/11/11/build-tools-in-nuget/
* CLI does not detect dependencies of dependencies (interop)

## Refactor wishes

* [Interop] look into the use tracked references (%^)
* [DevOps] automate CI build on github
    => Interop cannot find its package dependencies: https://github.com/dotnet/sdk/issues/11922
* [UnitTest] Use FluentAssertions.
* [Core] Try to get rid of the Adapters that pass essentially the same interface between Host and Plugin.
* [Release] have conditionals to ommit debugging/trace and checks from release builds (perf).
* [Interop] Start vstsettings.json with the plugin (host?) name.
* [Interop/Core] Hookup tracing again. Use ILogger<> API.
* [CLI/Interop] Add CRT to output bins and let CLI publish them?
    => adds **a lot** of extra dll's, most of which are not needed...?

---

## Decisions

* Will not multitarget the projects to support both netFx and netCore. 
Future seems to lie with netCore (.NET5) that will be VST.NET v2.
Current v1.1 will continue to exist for current users but not be developed further (separate branch?).
VST.NET1 = VST2/netFx, VST.NET2 = VST2/netCore, VST.NET3 = VST3/netCore


## References

https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#load-plugins

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.

## Research

COM (VST3)
https://github.com/dotnet/runtime/blob/master/docs/design/features/COM-activation.md#NET-Framework-Class-COM-Activation

- `ISSUE`: ijwhost/clr loading does not work on a mixed assembly!?
That means the plugin wont load in the unmanage host.
https://github.com/dotnet/runtime/issues/37194
