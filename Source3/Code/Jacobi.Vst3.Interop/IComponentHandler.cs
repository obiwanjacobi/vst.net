using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.FUnknown)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IComponentHandler
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 BeginEdit([MarshalAs(UnmanagedType.U4), In] UInt32 paramId);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 PerformEdit([MarshalAs(UnmanagedType.U4), In] UInt32 paramId, [MarshalAs(UnmanagedType.R8), In] Double valueNormalized);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 EndEdit([MarshalAs(UnmanagedType.U4), In] UInt32 paramId);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 RestartComponent([MarshalAs(UnmanagedType.I4), In] Int32 flags);
    }
}
