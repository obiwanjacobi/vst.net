# Jacobi.Vst.Interop

The <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly is responsible for posing as a native VST Plugin and relaying the communication between Host and Plugin to the managed Plugin that implements the real functionality. It makes the type conversions necessary to go from unmanaged to managed and back again.
&nbsp;<table><tr><th>![Note](media/AlertNote.png) Note</th></tr><tr><td>
Due to the fact that the Visual Studio 2008 class diagram editor for C++ does not support modeling managed types, there are no class diagrams of the internal classes of the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly.</td></tr></table>

## Implementation Details

The <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly contains an exported function `VSTPluginMain` that is called by the host and returns a `AEffect` structure that contains initial information about the plugin. In this structure there are several function pointers the host will call to call into the Plugin. The <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly intercepts these calls and forwards these to the managed Plugin through the use of a <a href="30e478e7-4eba-4eab-8a32-f9d9a2c4d2b3">Plugin Command Proxy</a> and <a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a> pair, performing type conversion from unmanaged to managed types and visa versa for the return values.


The managed Plugin is created in two steps. The first step is locating the managed Plugin assembly and loading the <a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a> and the second step is initializing it, passing it a reference to the <a href="1386a1db-aa7f-437f-94d2-a6755e375ea6">Host Command Stub</a> and retrieving the Plugin information that is copied into the `AEffect` structure.



## See Also


#### Other Resources
<a href="30e478e7-4eba-4eab-8a32-f9d9a2c4d2b3">Plugin Command Proxy</a><br /><a href="1386a1db-aa7f-437f-94d2-a6755e375ea6">Host Command Stub</a><br />