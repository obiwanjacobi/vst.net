# Build Configurations

Because the Jacobi.Vst.Interop assembly (renamed to the name of your plugin) is the first to load, it determines what CLR is loaded into the (unmanaged) host process. Although there is a way to alter the default behavior by specifying a .exe.config file
 for the host, that situation is not desirable.

The solution and project files of the VST.NET Code folder –containing the main assemblies- define the following build configurations. The projects that target the Clr v4 are postfixed with .Clr4 and are compatible with Visual Studio 2010. Note that the Jacobi.Vst.Interop
 Clr4 project does not have this extension (to keep the name of the generated assembly consistent). Instead it is identified by the new file extension for C&#43;&#43; projects in VS2010: “.vcxproj”.

(The managed assemblies are Jacobi.Vst.Core and Jacobi.Vst.Framework -and Jacobi.Vst.UnitTest.)


- Any CPU (RTM):

    This configuration is used during the automated build to produce the ‘Any CPU’ managed assemblies (except the UnitTest assembly).

- Any CPU (x86)

    This configuration will give you the ‘Any CPU’ managed assemblies and the ‘Win32’ (x86) configuration of the Jacobi.Vst.Interop assembly.

- Any CPU (x64)

    This configuration will give you the ‘Any CPU’ managed assemblies and the x64 configuration of the Jacobi.Vst.Interop assembly.

- x86

    This configuration will build all assemblies for x86 (Win32 for Jacobi.Vst.Interop).

- x64

    This configuration will build all assemblies for x64.

# Build Automation

The **Build** folder contains some scripts that use the
*MsBuild.exe* to build the solution in various configurations. This was built to provide me with an easy way to build all the different configurations with “one click of a button”. The file
**BuildAndPackageRTM.cmd** is the entry point for a completely automated build for all configurations (both Clr2 and Clr4, both Debug and Release).

**Note** that the script depends on a command line version of 7-zip to create a .zip package. The executable is checked-in in the Build folder. The complete archive (and license) can be found in the *Support\External* folder.

**Note** that these scripts are not designed to work with the Visual Studio Express editions (were projects of different languages cannot be mixed into one solution).

A more finer grained control can be achieved by using the individual build scripts located in the*Build\Code* folder. I hope the names of the scripts are self explanatory.

# What Configuration to Choose?

So with all these options, which one should you use? Well it depends.  :wink:

- What type of dependencies do you use (besides the usual System assemblies)?
- Are any of those specifically x86 or x64? Then you should mark all your assemblies as such for it will hint to that fact.
- For managed assemblies it really doesn’t matter that much. The assembly is tagged with the configuration but may as well run fine in other environments.

The important thing is the Jacobi.Vst.Interop assembly. That is the assembly that is loaded first (renamed to your plugin name) and is a native assembly.&#160; So trying to load a x86 Jacobi.Vst.Interop into a 64-bit process will fail or the other way around.

For more information on build targets in Visual Studio see also: [http://visualstudiohacks.com/articles/visual-studio-net-platform-target-explained/](http://visualstudiohacks.com/articles/visual-studio-net-platform-target-explained/)
