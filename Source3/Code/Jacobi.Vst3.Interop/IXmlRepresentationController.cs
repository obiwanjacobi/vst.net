using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IXmlRepresentationController)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IXmlRepresentationController
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetXmlRepresentationStream(
            [MarshalAs(UnmanagedType.Struct), In] ref RepresentationInfo info, 
            [MarshalAs(UnmanagedType.Interface), In] IBStream stream);
    }
}
