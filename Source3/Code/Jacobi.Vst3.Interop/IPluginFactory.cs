using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    /// <summary>
    /// Class factory that any Plug-in defines for creating class instances.
    /// - [plug imp]
    ///
    /// From the host's point of view a Plug-in module is a factory which can create 
    /// a certain kind of object(s). The interface IPluginFactory provides methods 
    /// to get information about the classes exported by the Plug-in and a 
    /// mechanism to create instances of these classes (that usually define the IPluginBase interface).
    /// </summary>
    [ComImport]
    [Guid(Interfaces.IPluginFactory)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPluginFactory
    {
        /// <summary>
        /// Fill a PFactoryInfo structure with information about the Plug-in vendor.
        /// </summary>
        /// <param name="info">A reference to the <see cref="PFactoryInfo"/> structure.</param>
        /// <returns>Returns a standard result code.</returns>
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

    [ComImport]
    [Guid(Interfaces.IPluginFactory2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPluginFactory2
    {

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetFactoryInfo(
            [MarshalAs(UnmanagedType.Struct)] ref PFactoryInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 CountClasses();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct)] ref PClassInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CreateInstance(
            [In] ref Guid classId,
            [In] ref Guid interfaceId,
            [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref IntPtr instance);


        //---------------------------------------------------------------------


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfo2(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct)] ref PClassInfo2 info);

    }

    [ComImport]
    [Guid(Interfaces.IPluginFactory3)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPluginFactory3
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetFactoryInfo(
            [MarshalAs(UnmanagedType.Struct)] ref PFactoryInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 CountClasses();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct)] ref PClassInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CreateInstance(
            [In] ref Guid classId,
            [In] ref Guid interfaceId,
            [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref IntPtr instance);


        //---------------------------------------------------------------------


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfo2(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct)] ref PClassInfo2 info);


        //---------------------------------------------------------------------


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetClassInfoUnicode(
            [MarshalAs(UnmanagedType.I4), In] Int32 index,
            [MarshalAs(UnmanagedType.Struct)] ref PClassInfoW info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetHostContext(
            [MarshalAs(UnmanagedType.IUnknown), In] Object context);
    }
}
