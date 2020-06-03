# Prepare your Project

Before you can start coding you need to setup a **Visual Studio** Project. This section describes the steps you need to take to have a basis for your Plugin project.



## Create a New Project

Your Managed Plugin will be build into (at least) one assembly. The custom code you write will be compiled to a dll and loaded by the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly. This means that the **Visual Studio** Project type you choose must produce a .dll. The most obvious choice is the **Class Library** Project.


This project is going to contain (at least) the public <a href="3feb73bb-72dd-4618-816f-f9f1c46d7f73">Plugin Command Stub</a> . If you choose to separate your plugin into multiple assemblies (dlls) you need to make sure they all are installed in a single folder or in the GAC.



## Add a Post-Build Event

This step is optional. If you do not want to automatically rename and move your Plugin assembly (and the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly) just skip these instructions. You do need to follow the naming convention discussed here though. Otherwise your plugin will not load or give errors during loading by the host.
&nbsp;<ul><li>Open the project properties of your plugin .NET project.</li><li>Click on the **Build Events** tab.</li><li>Copy the statements below into the **Post-build event command line** edit box.</li></ul>&nbsp;
The following code snippet contains two statements that will rename the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly to the name of your plugin and it will rename the project output assembly to a _.net.dll_ postfix. You can also use the _.net.vstdll_ extension if you want to stop the host displaying the Managed Plugin assembly.


```
copy "$(TargetPath)" "$(SolutionDir)..\_SharedAssemblies\$(TargetName).net.dll"
copy "$(SolutionDir)..\_SharedAssemblies\Jacobi.Vst.Interop.dll" "$(SolutionDir)..\_SharedAssemblies\$(TargetName).dll"
```

The content of the post-build event is independent on the .NET language (C#/VB.NET).
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Note that these statements assume a common binary folder called `_SharedAssmblies`. Please change that to suite your project structure. But don't change the `_SharedAssemblies` in the first part of the second statement, unless you also coppied the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly to that location.</td></tr></table>&nbsp;
&nbsp;<table><tr><th>![Caution](media/AlertCaution.png) Caution</th></tr><tr><td>
Make sure the path you choose to copy the renamed assemblies to exists before building your project.</td></tr></table>

## Add the Project Dependencies

Right click in the solution browser of your Project on the References folder and select **Add Reference...**.
&nbsp;<ul><li>Add the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a>.dll</li><li>Add the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a>.dll</li></ul>&nbsp;
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
If you build a Core-Level Plugin you obviously don't need the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a>.dll dependency and you only add the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a>.dll.</td></tr></table>&nbsp;
