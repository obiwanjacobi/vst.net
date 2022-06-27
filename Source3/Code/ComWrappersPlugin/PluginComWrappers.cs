using ComWrappersPlugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static System.Runtime.InteropServices.ComWrappers;

namespace ComWrappersPlugin
{
    [SupportedOSPlatform("windows")]
    unsafe internal class PluginComWrappers : ComWrappers
    {
        private static Dictionary<Type, (IntPtr, int)> _objectVtableMap = new();

#pragma warning disable S3963 // "static" fields should be initialized inline
        static PluginComWrappers()
        {
            GetIUnknownImpl(
                out IntPtr queryInterfacePtr,
                out IntPtr addRefPtr,
                out IntPtr releasePtr);

            // IPluginFactory interface vtable
            var vtable = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(
                typeof(PluginComWrappers), IntPtr.Size * 7);

            var idx = 0;
            vtable[idx++] = queryInterfacePtr;
            vtable[idx++] = addRefPtr;
            vtable[idx++] = releasePtr;
            vtable[idx++] = (IntPtr)(delegate* unmanaged<IntPtr, IntPtr, Int32>)&ABI.IPluginFactoryManagedWrapper.GetFactoryInfo;
            vtable[idx++] = (IntPtr)(delegate* unmanaged<IntPtr, Int32>)&ABI.IPluginFactoryManagedWrapper.CountClasses;
            vtable[idx++] = (IntPtr)(delegate* unmanaged<IntPtr, Int32, IntPtr, Int32>)&ABI.IPluginFactoryManagedWrapper.GetClassInfo;
            vtable[idx++] = (IntPtr)(delegate* unmanaged<IntPtr, Guid, Guid, IntPtr, Int32>)&ABI.IPluginFactoryManagedWrapper.CreateInstance;

            // PluginFactory interface-vtable map
            var entries = (ComInterfaceEntry*)RuntimeHelpers.AllocateTypeAssociatedMemory(
                typeof(PluginComWrappers), sizeof(ComInterfaceEntry) * 1);

            idx = 0;
            entries[idx].IID = Guid.Parse(Interfaces.IPluginFactory);
            entries[idx++].Vtable = (IntPtr)vtable;

            _objectVtableMap.Add(typeof(PluginFactory), ((IntPtr)entries, 1));
        }
#pragma warning restore S3963 // "static" fields should be initialized inline

        public PluginComWrappers()
        {
            ComWrappers.RegisterForMarshalling(this);
        }

        protected override unsafe ComInterfaceEntry* ComputeVtables(object obj, CreateComInterfaceFlags flags, out int count)
        {
            if (flags != CreateComInterfaceFlags.None)
                throw new NotSupportedException("ComputeVtables does not support any flags (only None).");

            if (_objectVtableMap.TryGetValue(obj.GetType(), out var entry))
            {
                count = entry.Item2;
                return (ComInterfaceEntry*)entry.Item1;
            }

            count = 0;
            return null;
        }

        protected override object CreateObject(IntPtr externalComObject, CreateObjectFlags flags)
        {
            if (!flags.HasFlag(CreateObjectFlags.UniqueInstance))
            {
                throw new NotSupportedException("CreateObject only supports 'UniqueInstance' flags.");
            }

            return null;
        }

        protected override void ReleaseObjects(IEnumerable objects)
            => throw new NotSupportedException();
    }
}

namespace ABI
{
    [SupportedOSPlatform("windows")]
    unsafe internal static class IPluginFactoryManagedWrapper
    {
        [UnmanagedCallersOnly]
        internal static Int32 GetFactoryInfo(IntPtr self, IntPtr infoPtr)
        {
            var instance = ComInterfaceDispatch.GetInstance<IPluginFactory>((ComInterfaceDispatch*)self);

            var factoryInfo = new PFactoryInfo();
            var result = instance.GetFactoryInfo(ref factoryInfo);
            if (TResult.Succeeded(result))
            {
                IntPtr urlPtr = Interop.CopyAnsi(factoryInfo.Vendor, infoPtr, 64);
                IntPtr emailPtr = Interop.CopyAnsi(factoryInfo.Url, urlPtr, 256);
                IntPtr flagsPtr = Interop.CopyAnsi(factoryInfo.Email, emailPtr, 128);
                Interop.WriteInt32((int)factoryInfo.Flags, flagsPtr);
            }
            return result;
        }

        [UnmanagedCallersOnly]
        internal static Int32 CountClasses(IntPtr self)
        {
            return 0;
        }

        [UnmanagedCallersOnly]
        internal static Int32 GetClassInfo(IntPtr self, Int32 index, IntPtr info)
        {
            return TResult.S_OK;
        }

        [UnmanagedCallersOnly]
        internal static Int32 CreateInstance(IntPtr self, Guid classId, Guid interfaceId, IntPtr instance)
        {
            return TResult.S_OK;
        }
    }
}
