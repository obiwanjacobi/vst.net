using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IParameterChanges)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IParameterChanges
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetParameterCount();

    	IParamValueQueue GetParameterData(
            [MarshalAs(UnmanagedType.I4), In] Int32 index);

	    IParamValueQueue AddParameterData(
            [MarshalAs(UnmanagedType.U4), In] UInt32 id, 
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 index);
    }
}
