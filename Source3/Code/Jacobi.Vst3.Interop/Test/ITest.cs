using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop.Test
{
    [ComImport]
    [Guid(Interfaces.ITestW)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITest
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool Setup();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool Run([MarshalAs(UnmanagedType.Interface), In] ITestResult testResult);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Bool)]
        bool Teardown();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetDescription();
    }
}
