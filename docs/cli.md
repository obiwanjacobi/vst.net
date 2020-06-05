# Command Line Interface

In the spririt of .NET core (`dotnet`) and Command Line Interface (cli) has been created to help with common tasks when working with VST.NET.

The following commands are available:

- `publish` - This command is similar to the `dotnet` `publish`. It gathers all dependencies of either a plugin (.dll) or a host (.exe) into one folder ready for deployment.

More commands may be added later as the need arises.

> Use "double quotes" around paths with spaces!

## Publish

`vstnet publish <path-to-bin> [-o <output-path>]`

- `<path-to-bin>` is a path to the binary output of your VST.NET project. For a plugin that would be a .dll file. For a host project, that would be a .exe file.
- `-o` allows you to specify an output directory that will receive all the dependencies of the specified '`<path-to-bin>`'.

The `publish` command will gather all the dependencies (from the NuGet cache folder on your local drive) and perform the necessary renaming of plugin .dll's in order for them to load correctly by the Interop.
It also copies over any `settings.json` files that may be next to your primary project binary.

---

> Back to [Index](index)
