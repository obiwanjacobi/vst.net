using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop.Test
{
    [ComImport]
    [Guid(Interfaces.ITestSuiteW)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITestSuite
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 AddTest ([MarshalAs(UnmanagedType.LPWStr), In] string name, [MarshalAs(UnmanagedType.Interface), In] ITest test);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 AddTestSuite([MarshalAs(UnmanagedType.LPWStr), In] string name, [MarshalAs(UnmanagedType.Interface), In] ITestSuite testSuite);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetEnvironment([MarshalAs(UnmanagedType.Interface), In] ITest environment);
    }
}
