# COM Wrappers Generator

https://docs.microsoft.com/en-us/dotnet/standard/native-interop/tutorial-comwrappers
https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.comwrappers?view=net-5.0
https://devblogs.microsoft.com/dotnet/improvements-in-native-code-interop-in-net-5-0/#comwrappers
https://github.com/dotnet/samples/blob/main/core/interop/comwrappers/Tutorial/Program.cs

---

Attempt to auto generate all the code for implementing COM wrappers for a set of annotated managed COM interface representations.

When the code works, we'll make a Roslyn Source Generator.

There are several parts that make up a COM wrappers implementation.

---

## COM Wrappers

An application specific class derived from the `ComWrappers` class that implements 3 methods (1 is optional)

- ComputeVtables
- CreateObject
- ReleaseObjects (optional)

---

### Constructing Vtables

For each exposed COM object it's corresponding vtable is constructed for all the public implemented COM interface methods.
The first three entries of any COM vtable are alway the `IUnknown` methods which can be easily obtained.
Implementing `IUnknown` can also be done manually.
The size of a vtable is the number of methods (including 3 IUnknown) times the size of a pointer (4 or 8 bytes).
That memory has to be allocated with the `RuntimeHelpers.AllocateTypeAssociatedMemory` API.

A pointer to the method is acquired by casting the address of the `static` ABI method with:

```csharp
var pFn = (IntPtr)(delegate* unmanaged<IntPtr, --params--, int>)&ABI.MyMethod;
```

The template parameters for the `unmanaged` `delegate` are dependent on the method in question (`--params--`),
but always start with an `IntPtr` for the COM object's `this` pointer and end with an `int` for the COM
Method's `HResult` return value.

---

### Application Binary Interface

This is a set of functions the native code can call into and represent the total functional area exposed to COM.
The entries in the vtable point to these functions.

All these functions are `public static` and marked with a `[UnmanagedCallersOnly]` attribute and
marshal data between unmanaged COM and managed .NET (or visa versa).

> .NET5 does not support marshaling functions for other platforms than windows. 
In .NET6 this support is said to be released.

---

### Managed Wrappers

The managed wrappers make it possible for native code to call into managed code.

All COM needs is the vtable that represents the interface and a `this` pointer.

- vtable 'Interface'
    - QueryInterface
    - AddRef
    - Release
    - ABI.Method1
    - ABI.Method2

The managed wrapper can implement `IDispose` which will be called when the GC deletes the wrapper object.

---

### Native (COM) Wrappers

The native wrappers allow managed code to call into unmanaged (COM) code.

The interface wrappers marshal all methods of a COM interface to a unmanaged object instance and
marshal back its result.

The object wrappers represent a concrete COM object and contain the interface wrappers for
the interfaces this object implements.

When the interfaces a COM object implements are not known up front, or are dynamic,
a special mechanism is used: `DynamicInterfaceCastable`.

---

## Generating Code

- Scan public interfaces for a custom code attribute (something like ComInterface(IID)).
- scan public classes that implement one or more of these interfaces.
- generate vtable construction code for each interface
- generate an ABI for each interface (marshaling)
- generate wrapper code for each class
