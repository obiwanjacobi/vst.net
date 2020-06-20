using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IInfoListener)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInfoListener
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetChannelContextInfos(
            [MarshalAs(UnmanagedType.Interface), In] IAttributeList list);
    }
}
