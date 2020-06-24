# VST.NET 2 for .NET core 3.1

The dotnet-core 3.1 version of VST.NET.

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

* CLI does not detect dependencies of dependencies (interop)
* CLI: Host deployment: triggers plugin logic because a dotnet-core exe has/is also a dll.
    => Nuget deployment is wrong. Ijwhost is missing from build-bin folder (of Samples.Host).

* Some Host DAWs need to have the ijwhost.dll next to their .exe in order to load the plugin
    (Also the case for VST3).

* VS clean (target) should also delete the 'deploy' folder.

## Refactor wishes

* [Interop] look into the use tracked references (%^)
* [DevOps] automate CI build on github
    => Interop cannot find its package dependencies: https://github.com/dotnet/sdk/issues/11922
    => Interop project has hard coded paths to its managed nuget packages
    => CI Build fails for this reason on GitHub-Actions.
* [Release] have conditionals to ommit debugging/trace and checks from release builds (perf).
    => need to build in dev with Debug and use Release for Deployment. Will not be transparent!
* [Interop/Core] Hookup tracing again. Use ILogger<> API: https://stackify.com/net-core-loggerfactory-use-correctly/
    => Plugin: will read local plugin.appsetting.json and creates a logger factory.
    => Host: will read own appsettings.json and create logger factory.
* [CLI/Interop] Add CRT to output bins and let CLI publish them?
    => adds **a lot** of extra dll's, most of which are not needed...?

---

## Decisions

* Will not multitarget the projects to support both netFx and netCore. 
Future seems to lie with netCore (.NET5) that will be VST.NET v2.
Current v1.1 will continue to exist for current users but not be developed further (separate branch).
VST.NET1 = VST2/netFx, VST.NET2 = VST2/netCore, VST.NET3 = VST3/netCore

## References

https://github.com/dotnet/runtime/blob/master/docs/design/features/IJW-activation.md
https://natemcmaster.com/blog/2017/12/21/netcore-primitives/
https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#load-plugins

You can collect the native hosting trace - run the process with environment: COREHOST_TRACE=1 and COREHOST_TRACEFILE=t.txt.
It should produce t.txt in the current directory with lot of info about the native hosting side.
