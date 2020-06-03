using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IKeyswitchController)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IKeyswitchController
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetKeyswitchCount(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel);


        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetKeyswitchInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
            [MarshalAs(UnmanagedType.I2), In] Int16 channel,
            [MarshalAs(UnmanagedType.I4), In] Int32 keySwitchIndex,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref KeyswitchInfo info);
    }
}
