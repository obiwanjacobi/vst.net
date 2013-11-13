using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IComponent)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IComponent
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Initialize(
            [MarshalAs(UnmanagedType.IUnknown), In] Object context);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Terminate();

        //---------------------------------------------------------------------

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetControllerClassId(
            [MarshalAs(UnmanagedType.Struct), In, Out] ref Guid controllerClassId);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetIoMode(
            [MarshalAs(UnmanagedType.I4), In] IoModes mode);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetBusCount(
            [MarshalAs(UnmanagedType.I4), In] MediaTypes type, 
            [MarshalAs(UnmanagedType.I4), In] BusDirections dir);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetBusInfo (
            [MarshalAs(UnmanagedType.I4), In] MediaTypes type, 
            [MarshalAs(UnmanagedType.I4), In] BusDirections dir, Int32 index, 
            [MarshalAs(UnmanagedType.Struct), In, Out] ref BusInfo bus);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetRoutingInfo(
            [MarshalAs(UnmanagedType.Struct), In] ref RoutingInfo inInfo, 
            [MarshalAs(UnmanagedType.Struct), In, Out] ref RoutingInfo outInfo);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 ActivateBus (
            [MarshalAs(UnmanagedType.I4), In] MediaTypes type, 
            [MarshalAs(UnmanagedType.I4), In] BusDirections dir, 
            [MarshalAs(UnmanagedType.I4), In] Int32 index, 
            [MarshalAs(UnmanagedType.U1), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetActive(
            [MarshalAs(UnmanagedType.U1), In] Boolean state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetState(
            [MarshalAs(UnmanagedType.Interface), In] IBStream state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetState(
            [MarshalAs(UnmanagedType.Interface), In] IBStream state);
    }
}
