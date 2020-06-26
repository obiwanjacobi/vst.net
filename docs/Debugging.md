# Debugging

You can use Visual Studio as a Debugger to debug your VST.NET code. Assumed is that you have the project / solution you want to debug open in Visual Studio.

For plugins it is a little different than for hosts.

## Plugin

If you experience loading problems of your plugin in a specific DAW, or just want see what is happening at runtime, debugging your plugin can be a good option.

Because a plugin is not an executable, you have to attach the Visual Studio debugger to the host application you are (trying to) load(ing) the plugin into.

In Visual Studio, the Debug menu has an `Attach to Process...` option that brings up a dialog that shows running processes. If you cannot find the host process, you can try the `Show processes from all users` checkbox at the bottom.

Make sure `Managed (CoreCLR) code` is selected.

After you've selected the host application process you can hit the `Attach` button and Visual Studio will be in debug mode.

Now you place breakpoints in your plugin code.

You may also have to perform actions in the host application to get it to call the plugin in the specific area of interest.

For investigating plugin loading problems, it may be beneficial to break on all Exceptions. That can be found on the `Debug => Windows => Exception Settings...` menu. Look for `Common Language Runtime Exceptions`.

## Host

Because you are building a managed host, debugging it is a simple matter of making a debug build and hitting F5.

If you have their source code you should even be able to step into plugin as well.

## VST.NET

- Get a debug build of VST.NET.
- Overwrite the release assemblies from the NuGet package.
- Select Managed and Native debugging.
