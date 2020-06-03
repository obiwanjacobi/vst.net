# Host Command Stub

The Host Command Stub receives methods calls from the Plugin and relays these to the host using a function pointer passed into the _VSTPluginMain_ during the Plugin load sequence. The Host Command Stub converts all data types from managed to unmanaged and visa versa (for return values).



## Implementation Details

The Host Command Stub implements the <a href="T_Jacobi_Vst_Core_Plugin_IVstHostCommandStub">IVstHostCommandStub</a> interface. A reference of this interface implementation is passed to the Plugin during the load sequence of the Plugin. The <a href="a6802bfe-1ae8-444e-abd5-dbe1a348f193">Host Command Proxy</a> implementation in the <a href="bf34ecc4-5cd1-4770-86fe-2cda55f05823">Jacobi.Vst.Framework</a> assembly calls this reference of the `IVstHostCommandStub` interface to dispatch its calls to the Host. The Host Command Stub class is passed the unmanaged function pointer to the Host's dispatch function when it was constructed in the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly. So incoming calls from the managed Plugin ( <a href="a6802bfe-1ae8-444e-abd5-dbe1a348f193">Host Command Proxy</a> ) are forwarded to the host using the callback function pointer. Managed types are converted to unmanaged types before passed to the Host and visa versa for return types.



## See Also


#### Other Resources
<a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a><br />