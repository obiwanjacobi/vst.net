# Managed Plugin Factory

One of the first objects that is instantiated during the loading sequence of a VST Plugin is the Managed Plugin Factory only preceded by the `AssemblyLoader`. Together these objects determine how managed Plugin assemblies are loaded.


The following sequence describes the startup sequence in more detail.
&nbsp;<ul><li>The host calls the VSTPluginMain exported function in the renamed <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly. This assembly is renamed to resembly the managed Plugin's name and the host thinks it is the actual plugin.</li><li>The <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly instantiates the `AssemblyLoader` class that resolves assemblies in the local plugin folder. This is the folder that also contains the renamed <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly. This is done so the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a> and <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> assemblies can also be located in the local plugin folder.</li><li>Next the `ManagedPluginFactory` is created. This class is located in the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a> assembly. It loads the managed Plugin assembly and creates the <a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a>. Then the rest of the loading sequence is executed (such as retrieving Plugin capability information).</li></ul>

## Loading the Managed Plugin Assembly

The Managed Plugin Factory has a set of rules that determine where managed Plugin assemblies can be located and how they can be named.


When the `ManagedPluginFactory` is created it is passed the file path (and name) of the renamed <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly and from that it comes up with to new file names: the name of the renamed <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly without its _.dll_ extension but with:
&nbsp;<ul><li>_.net.dll_</li><li>_.net.vstdll_</li></ul>&nbsp;
extensions.


So if the name of the renamed <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly is _MyVendor.MyProduct.MyPlugin.dll_ the name of the assembly that contains the managed Plugin must be in the same folder and be named _MyVendor.MyProduct.MyPlugin.net.dll_ or _MyVendor.MyProduct.MyPlugin.net.vstdll_. The _.net.vstdll_ extension was introduced to stop the hosts from loading the secondary assembly that was no real VST plugin to host.


When the managed Plugin assembly is loaded all types (classes) are iterated in search of a public class that implements the <a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a> interface. That type is then instantiated and a reference is returned from the factory. Once the VST Plugin loading sequence is done and the Plugin is loaded, the `ManagedPluginFactory` is not used anymore.



## See Also


#### Reference
<a href="T_Jacobi_Vst_Core_Plugin_IVstPluginCommandStub">IVstPluginCommandStub</a><br />

#### Other Resources
<a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a><br /><a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a><br />