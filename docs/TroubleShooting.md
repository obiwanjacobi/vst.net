# Trouble Shooting

Hopefully some helpful tips when things don't work right away.

## Not all dependencies were found after successful build

As a workaround to another problem with using NuGet packages in a mixed assembly project,
the dependencies for the interop library are hard-coded in the vstnet cli.

Because of that, the final step after a successful build may display warnings
that certain dependencies were not found at the NuGet package cache location: `C:\Users\[me]\.nuget\packages\`.

Currently the simplest way around this is to add the missing NuGet dependencies to your project - even if you don't use them. Make sure you match the exact version of the package.

## Plugin won't load in host (DAW)

Depending on the host, a plugin may not load into the DAW. First things you should try:

- Clear plugin caches the host may have.
- Let the host rescan for plugins.

### Match processor architecture

Make sure that the plugin matches the processor architecture of the host.
That means if the host is 64-bits, your plugin needs to be build for `x64` and the same goes for 32-bits (`x86`) of course.
Most application these days are 64 bits but not all!

### Verify your plugin is correct

In order to eliminate problem areas, you should check if you plugin is correct and has all the required files in its folder.

During development of VST.NET itself the [vsthost](https://www.hermannseib.com/english/vsthost.htm) is used as a test host.
If your plugin does not load in this host you know that you are missing files.
You just need to test if the plugin loads (File->New Plugin...).

### Copy `Ijwhost.dll` into host .exe's folder

For some hosts the loading of .NET core does not work at all. It is unclear what the problem is,
but copying over the `Ijwhost.dll` file into the same folder the host's .exe is located usually solves the issue.

> This problem should have been fixed in RC2 - but I leave it here if anybody may need it. Please report an issue if you do.

### Debugging

If all else fails, you may have to attach the debugger, explained [here](Debugging.md).

### System.BadImageFormatException

This almost always means that you are mixing 64-bit (x64) code with 32-bit (x86) code. It could be that not all your dependencies are for the same system architecture (x86/x64) or that you are trying to load 32-bit plugins into a 64-bit host application (or visa versa).

---

> Back to [Index](index.md)
