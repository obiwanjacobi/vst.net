using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop.Test
{
    [ComImport]
    [Guid(Interfaces.ITestResultW)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITestResult
    {
        [PreserveSig]
        void AddErrorMessage([MarshalAs(UnmanagedType.LPWStr), In]string msg);

        [PreserveSig]
        void AddMessage([MarshalAs(UnmanagedType.LPWStr), In]string msg);
    }
}
