using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core.Test
{
    [ComImport]
    [Guid(Interfaces.ITestW)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITest
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U1)]
        Boolean Setup();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U1)]
        Boolean Run(
            [MarshalAs(UnmanagedType.Interface), In] ITestResult testResult);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.U1)]
        Boolean Teardown();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        String GetDescription();
    }
}
