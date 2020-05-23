# VST.NET Command Line Interface

The Command Line Interface (CLI) for VST.NET provides some utility functions for developing VST.NET Plugins or Hosts.

All commands are spelled in lower case. The Following commands are available:

- Help
- Publish

## Help

`vstnet help`

Displays usage details on the commands and their arguments.

## Publish

`vstnet publish <file> [-o <output>]`

- `file` The full path to the plugin (.dll) or host (.exe) file, or to its `.deps.json` file.
- `-o` - Optionally specify the output folder where all files are gathered. Default is `.\\deploy`.

This command helps to create a deployment for publishing the VST.NET plugin or host. It will:

- Copy the dependent assemblies into the output folder.
- Do renaming of the target plugin assembly and Jacobi.Vst.Interop
- Create a `.runtimeconfig.json` file to allow the plugin to load into the host.

Note this command uses the `.nuget` file cache to retrieve the dependent assemblies.

### Example

`vstnet C:\myproject\bin\netcore3.1\MyProject.MyPlugin.dll -o C:\deploy`

This will result in `C:\deploy` containing the following files:

- ijwhost.dll
- Jacobi.Vst.Core.dll
- Jacobi.Vst.Framework.dll
- Microsoft.Extensions.Configuration.dll
- < other dependent assemblies >
- MyProject.MyPlugin.dll  (renamed from Jacobi.Vst.Interop.dll)
- MyProject.MyPlugin.net.vstdll (renamed from the original MyProject.MyPlugin.dll)
- MyProject.MyPlugin.runtimeconfig.json
