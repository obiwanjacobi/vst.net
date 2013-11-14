using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IMessage)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMessage
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        String GetMessageID();

        [PreserveSig]
        void SetMessageID(
            [MarshalAs(UnmanagedType.LPWStr), In] String id);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Interface)]
        IAttributeList GetAttributes();
    }
}
