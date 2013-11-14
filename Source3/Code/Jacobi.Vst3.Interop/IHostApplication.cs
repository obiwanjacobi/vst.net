using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IHostApplication)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IHostApplication
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetName(
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), In] StringBuilder name);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 CreateInstance(
            [In] ref Guid classId,
            [In] ref Guid interfaceId,
            [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref IntPtr instance);
    }
}
