using ComLight;
using System;
using System.Runtime.InteropServices;

namespace ComLightPlugin
{
    [Guid(Interfaces.IPluginFactory)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComInterface(Interfaces.IPluginFactory)]
    public interface IPluginFactory
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetFactoryInfo(
            [MarshalAs(UnmanagedType.Struct), In, Out] ref PFactoryInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 CountClasses();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref PClassInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CreateInstance(
            [In] ref Guid classId,
            [In] ref Guid interfaceId,
            [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref IntPtr instance);
    }
}
