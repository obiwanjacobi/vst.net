The core interfaces are seperated into version specific interfaces definitions that inherit from the previous version. The Stub interfaces used for the plugin derive from the latest commands interface and implement specific methods for interfacing between host and plugin.
![](VST.NET Core_Jacobi.Vst.Core.Interfaces.png)

These core types are used to communicate information across the unmanaged/managed boundary.
![](VST.NET Core_Jacobi.Vst.Core.Types.png)

These core enumerations define valid values within the VST interface.
![](VST.NET Core_Jacobi.Vst.Core.Enums.png)