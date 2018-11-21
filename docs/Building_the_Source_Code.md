Here are some tips in order for you to build the source code.

I use Visual Studio 2012 Professional edition to write the code. But I think it should work with VS2012 Express edition as well (not tested). All projects target .NET Framework 2.0 (CLR2) and 4.0 (CLR4). I use the post build event in project files to copy the assembly to the _SharedAssemblies folder. I use the post build event in the Samples projects to copy and rename the project assembly to the root debug directory (also used as target output in the Interop project). 

All projects (except the Samples projects) use a private key file for signing the assemblies. Jacobi.snk is that key file and is **NOT** checked into source control. It is private to me and a way for users to identify an official release (important for the LGPL license). Also the projects use AssemblyInfo.General.cs/cpp which is also private to me.
These files are 6 levels up the folder hierarchy (relative to the project file) in my build. You can safely remove these files from all the projects.

**Visual Studio Express editions**
I have written down the steps you need to take to adapt the source code to the Visual Studio Express Editions (VS2008).

* Download the Steinberg VST SDK and copy the aeffect.h and aeffectx.h files into Jacobi.Vst.Interop/\_vst folder. These files are also in the VST3 SDK.
* Create a 'Debug' and 'Release' directory at 'Code' level (where the .sln file is).

* Load solution in C# Express
* Permanently remove source control bindings
* Remove AssemblyInfo.General.cs from 'Properties' folders (all projects)
* Remove the Jacobi.snk file from Core and Framework
* Uncheck "Sign the assembly" checkbox in project properties (Core & Framework)
* REM out the gacutil call in post build events (Core & Framework)
* Add a copy to the post build event: **copy "$(TargetPath)" "$(SolutionDir)$(ConfigurationName)\$(TargetFileName)"** (Core & Framework)
* Build Core & Framework

* Load solution in C++ Express
* Permanently remove source control bindings
* Remove AssemblyInfo.General.cpp from 'Source Files' folders
* Open project properties, navigate to Common properties and:
* Delete the missing project reference.
* Add a reference to the Jacobi.Vst.Core.dll in the Debug folder.
* Navigate to the advanced linker options and clear the value of 'Key File'
* Build Interop

* Build the C# Jacobi.Vst.Samples projects

**Assembly dependencies**
The dependencies between the projects and the order in which to build them is as follows:
* Jacobi.Vst.Core (no dependcies)
* Jacobi.Vst.Framework -> Jacobi.Vst.Core
* Jacobi.Vst.Interop -> Jacobi.Vst.Core
* Jacobi.Vst.Samples.CorePlugin -> Jacobi.Vst.Core
* Jacobi.Vst.Samples.* -> Jacobi.Vst.Core, Jacobi.Vst.Framework

The Jacobi.Vst.Samples projects should be build after the Interop has been build, because they contain post build events that rename the Interop dll.


