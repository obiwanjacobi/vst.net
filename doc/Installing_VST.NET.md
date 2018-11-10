**Installing VST.NET**

VST.NET consists of 3 assemblies, one of which is optional (the framework):

* Jacobi.Vst.Interop.dll
* Jacobi.Vst.Core.dll
* Jacobi.Vst.Framework.dll

The Jacobi.Vst.Interop.dll contains the exported VSTMain function that is called by the Host application. So this dll should impersonate the actual managed plugin and should therefor be renamed to the desired snap in name. Note that there are some hosts that take the snap in name from the name of the dll. The actual managed plugin dll should have the exact same name as the renamed Jacobi.Vst.Interop.dll assembly but with a .net postfix.

If the plugin name is for example: "MySynth". You should rename the Jacobi.Vst.Interop.dll to MySynth.dll and the rename managed assembly that contains the actual plugin to MySynth.net.dll.

The Jacobi.Vst.Core and Jacobi.Vst.Framework dlls should be installed in the Global Assembly Cache (GAC). You can use the Windows Explorer to drop them into %Windows%\assembly (C:\Windows\assembly).

For each managed plugin installed into a Host application, the Jacobi.Vst.Interop.dll should be copied and renamed for that plugin.
Consult the host manual to find out how to install the plugin into the host application. Usually it is a simple matter of dropping the assemblies into a specific folder (location).

