# Using VST.NET

VST.NET consists of 3 assemblies, one of which is optional (the framework):

* Jacobi.Vst.Interop.dll
* Jacobi.Vst.Core.dll
* Jacobi.Vst.Framework.dll

The Jacobi.Vst.Interop.dll contains the exported VSTMain function that is called by the Host application. So this dll should impersonate the actual managed plugin and should therefor be renamed to the desired plugin name. Note that there are some hosts that take the plugin name from the name of the dll. The actual managed plugin dll should have the exact same name as the renamed Jacobi.Vst.Interop.dll assembly but with a .net postfix.

If the plugin name is for example: "MySynth". You should rename the Jacobi.Vst.Interop.dll to MySynth.dll and then rename managed assembly that contains the actual plugin to MySynth.net.dll.

The Jacobi.Vst.Core and Jacobi.Vst.Framework dlls should be installed in the Global Assembly Cache (GAC). You can use the Windows Explorer to drop them into %Windows%\assembly (C:\Windows\assembly).

For each managed plugin installed into a Host application, the Jacobi.Vst.Interop.dll should be copied and renamed for that plugin.
Consult the host manual to find out how to install the plugin into the host application. Usually it is a simple matter of dropping the assemblies into a specific folder (location).

When you start to code your managed plugin you need to reference the Jacobi.Vst.Core assembly if you plan to program against the interop layer and implement the IVstPluginCommandStub interface yourself. If you plan to program against the Framework you will also need to reference the Jacobi.Vst.Framework assembly in your project. In this case you derive a class from the abstract StdPluginCommandStub class that is provided by the framework (in the Plugin namespace) and implement the CreatePluginInstance() method. Make sure your derived class is public, for it is this class that will be created by the managed plugin loader in the interop dll.

If you have existing code you want to adopt for a VST plugin you might not want to use the Framework but simply route all the plugin commands to your existing code. If you start from scratch the Framework will give you an architecture and some services for your code.

Note that the Jacobi.Vst.Interop assembly is a mixed native/managed C++ assembly. It has a dependency on the VC110.CRT package. If you have problems getting your plugins (or Host) to load/run, check if you have it installed.
See also [http://vstnet.codeplex.com/Thread/View.aspx?ThreadId=47252](http://vstnet.codeplex.com/Thread/View.aspx?ThreadId=47252)