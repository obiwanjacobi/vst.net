using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Core
{
    [ComImport]
    [Guid(Interfaces.IEditController)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEditController
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Initialize(
            [MarshalAs(UnmanagedType.IUnknown), In] Object context);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 Terminate();

        //---------------------------------------------------------------------

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetComponentState(
            [MarshalAs(UnmanagedType.Interface), In] IBStream state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetState(
            [MarshalAs(UnmanagedType.Interface), In] IBStream state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetState(
            [MarshalAs(UnmanagedType.Interface), In] IBStream state);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetParameterCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetParameterInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 paramIndex,
            [MarshalAs(UnmanagedType.Struct), In, Out] ref ParameterInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetParamStringByValue(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramId,
            [MarshalAs(UnmanagedType.R8), In] Double valueNormalized,
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), Out] StringBuilder @string);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetParamValueByString(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramId,
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), In] String @string,
            [MarshalAs(UnmanagedType.R8), In, Out] ref Double valueNormalized);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.R8)]
        Double NormalizedParamToPlain(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramId,
            [MarshalAs(UnmanagedType.R8), In] Double valueNormalized);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.R8)]
        Double PlainParamToNormalized(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramId,
            [MarshalAs(UnmanagedType.R8), In] Double plainValue);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.R8)]
        Double GetParamNormalized(
            [MarshalAs(UnmanagedType.U4), In] UInt32 paramId);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetParamNormalized(
            [MarshalAs(UnmanagedType.I4), In] UInt32 paramId,
            [MarshalAs(UnmanagedType.R8), In] Double value);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 SetComponentHandler(
            [MarshalAs(UnmanagedType.Interface), In] IComponentHandler handler);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Interface)]
        IPlugView CreateView(
            [MarshalAs(UnmanagedType.LPStr), In] String name);
    }
}
