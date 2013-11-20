using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop.Test
{
    [ComImport]
    [Guid(Interfaces.ITestFactoryW)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITestFactory
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CreateTests(
            [MarshalAs(UnmanagedType.IUnknown), In] Object context, 
            [MarshalAs(UnmanagedType.Interface), In] ITestSuite parentSuite);
    }
}
