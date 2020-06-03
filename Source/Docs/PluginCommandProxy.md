# Plugin Command Proxy

The `PluginCommandProxy` class is the front end of the journey a Host-to-Plugin call makes. As a result of the VST Plugin loading sequence the Host receives several function pointers it uses to perform certain operations. These calls all come in at the `PluginCommandProxy` where the parameters are transformed into .NET compatible types and forwarded to the `PluginCommandStub`.



## The role of the Plugin Command Proxy

The role of the `PluginCommandProxy` is to receive calls from the Host made on those function pointers it got when the Plugin was loaded and forward them to the `PluginCommandStub`. The problem this objects addresses is that the Host is C++ and the `PluginCommandStub` is managed code (C#). So in forwarding the call it also converts the method parameters into .NET (compatible) types.


Not all calls from the Host are treated in the same fashion. <a href="http://www.codeplex.com/vstnet">VST.NET</a> devides the calls into two categories:
&nbsp;<ul><li>Time-critical. (`ProcesReplacing`)</li><li>Non-time-critical. (all others)</li></ul>&nbsp;
The `ProcessReplacing` calls handle audio processing (32 bit and 64 bit) and are seen as time-critical. During execution of the calls the Garbage Collector of .NET is set to a low-latency mode, causing as few interuptions as possible. During this time you should **not** allocate large structures.



## Making the Jump to .NET

Without going into too much details on how the type conversion in the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assemlby works exactly this section attempts to shed some light on it. The <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly uses the Managed Extensions of the Microsoft C++ compiler to work its magic. The Managed Extensions allow you to build a C++ dll that contains both native C++ code as well as managed .NET code. This means that the <a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a> assembly can implement a native C++ (VST) interface but still call out to a managed object on the other side. The VST Standard uses a lot of structures (the managed versions of these structures are located in the <a href="4f3d4350-e61e-4909-a294-c281511a336a">Jacobi.Vst.Core</a> assembly) that have to be converted by hand. The `TypeConverter` has all the conversion routines needed for all the data types used in the VST interface. Luckily for us there is not much difference in the C++ basic data types (integer, float, etc) and its managed counter parts. These types are known as blittable types and can be assigned freely.


Some VST method calls require an unmanaged memory allocation. There is a `MemoryTracker` object that keeps track on all allocated memory (just a list, really). During the suspend/resume call, when the Plugin is turned on or off, the unmanaged memory is freed.



## More on Proxy - Stub

The terminology of Proxy and Stub is taken from COM (Component Object Model) a binary component standard in Windows. Whenever a call had to be marshalled from a remote place to the actual object a proxy-stub pair was used to make that happen. The proxy implemented the exact same interface as the real object so the client didn't have to change any code. The proxy took the call, serialized it into a blob and sent it to the stub where the blob was deserialized and the actual object was called with the parameters that were provided by the client. The return value went in reverse.


So a proxy in <a href="http://www.codeplex.com/vstnet">VST.NET</a> is an object that accepts calls from a 'client' and transforms them in such a way that they make it over to the other side, where the stub is waiting to dispatch that call to the actual object.



## See Also


#### Other Resources
<a href="bf904c4c-fdf7-4e94-8590-13d0b3d9baf6">Plugin Command Stub</a><br /><a href="e5d53d11-e4bb-43b9-abe9-04b0507465dc">Jacobi.Vst.Interop</a><br />