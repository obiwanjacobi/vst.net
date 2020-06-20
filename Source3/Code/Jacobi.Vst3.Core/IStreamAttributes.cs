using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IStreamAttributes)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStreamAttributes
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetFileName(
            [MarshalAs(UnmanagedType.LPStr), In, Out] String name);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Interface)]
        IAttributeList GetAttributes();
    }
}
