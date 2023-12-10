# VST.NET 2 for .NET 6, 7 and 8

The dotnet 6, 7 and 8 version of VST.NET.

## Issues

* NuGet Deployment is a mess
  https://github.com/dotnet/sdk/issues/6688
  https://github.com/NuGet/Home/issues/6645
  https://github.com/NuGet/Home/issues/8623
  https://github.com/dotnet/runtime/issues/18527
  Nuget package platform architecture issues:
  https://github.com/NuGet/docs.microsoft.com-nuget/issues/1083#issuecomment-597886840
  https://github.com/dotnet/roslyn/blob/master/docs/features/refout.md
  example of using nuget sdk to generate an AnyCPU definition-only assembly.
  https://github.com/NuGet/Entropy/tree/master/nuget-sdk-usage/nuget-sdk-usage

  The VST.NET CLI can perform a publish of a plugin and gather all dependencies into folder.
  NuGet still gives problems with x86/x64 managed assemblies.
  https://github.com/dotnet/sdk/issues/10875
  https://www.jerriepelser.com/blog/analyze-dotnet-project-dependencies-part-1/

* CLI does not detect dependencies of dependencies (interop)
* CLI: Host deployment: triggers plugin logic because a dotnet-core exe has/is also a dll.

* VS clean (target) should also delete the 'deploy' folder. Current task does not work.
* VS Deploy (Configuration Manager): can we switch on that for vstnet deploy?
* VS Project Template automate creation of zip.
    https://github.com/sayedihashimi/template-sample
    https://www.youtube.com/watch?v=GDNcxU0_OuE
    https://www.youtube.com/watch?v=rZFIbbxsGmc

* Resize UI window/controls when host-frame resizes.
* Plugin without Replacing crashes vsthost ?tring to call accumulating the plugin does not implement.
* Reported: Adding NAudio to a plugin fails to produce a deploy folder?

## Refactor wishes

* [Interop] look into the use tracked references (%^)
* [DevOps] automate CI build on github
    => Interop cannot find its package dependencies: https://github.com/dotnet/sdk/issues/11922
    => Interop project has hard coded paths to its managed nuget packages
    => CI Build fails for this reason on GitHub-Actions.
* [Release] have conditionals to omit debugging/trace and checks from release builds (perf).
    => need to build in dev with Debug and use Release for Deployment. Will not be transparent!
* [CLI/Interop] Add CRT (C-runtime) to output bins and let CLI publish them?
    => adds **a lot** of extra dll's, most of which are not needed...?
* Look into System.Runtime.CompilerServices.Unsafe
* Look into structs-by-ref (ref-structs?) to see if memory/performace optimizations can be made.
* Look into Span<T>, System.Memory, System.Buffers
    Span<T> in VstAudioBuffer => Done.
* Add a plugin wrapper (plugin) that can hot-load (unload/load) a plugin under development for rapid roundtrips.

---

## Decisions

* Will not multitarget the projects to support both netFx and netCore. 
Future seems to lie with netCore (.NET5/.NET6) that will be VST.NET v2.
Current v1.1 will continue to exist for current users but not be developed further (separate branch).
VST.NET1 = VST2/netFx, VST.NET2 = VST2/netCore, VST.NET3 = VST3/netCore

## References

https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#load-plugins

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.
