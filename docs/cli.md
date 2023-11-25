# Command Line Interface

> Note that the `VST.NET` NuGet packages inject calling the CLI into the standard build process. You should not have to call this manually in most scenarios.

In the spirit of .NET core (`dotnet`) a Command Line Interface (cli) has been created to help with common tasks when working with VST.NET.

The following commands are available:

- `publish` - This command is similar to the `dotnet` `publish` command. It gathers all dependencies of either a plugin (.dll) or a host (.exe) into one folder ready for deployment.

More commands may be added later as the need arises.

> Use "double quotes" around paths with spaces!

## Publish

`vstnet publish <path-to-bin> [-o <output-path>] -p <platform>`

- `<path-to-bin>` is a path to the binary output of your VST.NET project. For a plugin that would be a .dll file. For a host project, that would be a .exe file.
- `-o` allows you to specify an output directory that will receive all the dependencies of the specified '`<path-to-bin>`'. If not specified, the output folder will default to '.\deploy'.
- `<platform>` must contain `x64` for 64-bits or `x86` for 32-bits.

The `publish` command will gather all the dependencies (from the NuGet cache folder on your local drive) and perform the necessary renaming of plugin .dll's in order for them to load correctly by the Interop.
It also copies over any `settings.json` files that may be next to your primary project binary.

---

> Back to [Index](index.md)
