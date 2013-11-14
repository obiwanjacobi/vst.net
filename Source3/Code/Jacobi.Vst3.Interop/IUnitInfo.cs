using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    [ComImport]
    [Guid(Interfaces.IUnitInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IUnitInfo
    {
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetUnitCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
    	Int32 GetUnitInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 unitIndex, 
            [MarshalAs(UnmanagedType.Struct), In, Out] ref UnitInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
	    Int32 GetProgramListCount();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetProgramListInfo(
            [MarshalAs(UnmanagedType.I4), In] Int32 listIndex, 
            [MarshalAs(UnmanagedType.I4), In, Out] ref ProgramListInfo info);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 GetProgramName (
            [MarshalAs(UnmanagedType.I4), In] Int32 listId, 
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex, 
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), In, Out] StringBuilder name);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 GetProgramInfo (
            [MarshalAs(UnmanagedType.I4), In] Int32 listId, 
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex, 
		    [MarshalAs(UnmanagedType.LPWStr), In] String attributeId, 
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), In, Out] StringBuilder attributeValue);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        Int32 HasProgramPitchNames (
            [MarshalAs(UnmanagedType.I4), In] Int32 listId, 
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex);

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 GetProgramPitchName (
            [MarshalAs(UnmanagedType.I4), In] Int32 listId, 
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex,
		    [MarshalAs(UnmanagedType.I4), In] Int16 midiPitch, 
            [MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.Fixed128), In, Out] StringBuilder name);

	    [PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        Int32 GetSelectedUnit();

        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 SelectUnit (
            [MarshalAs(UnmanagedType.I4), In] Int32 unitId);

	    [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 GetUnitByBus (
            [MarshalAs(UnmanagedType.I4), In] MediaTypes type, 
            [MarshalAs(UnmanagedType.I4), In] BusDirections dir, 
            [MarshalAs(UnmanagedType.I4), In] Int32 busIndex,
		    [MarshalAs(UnmanagedType.I4), In] Int32 channel, 
            [MarshalAs(UnmanagedType.I4), In, Out] ref Int32 unitId);

	    [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
	    Int32 SetUnitProgramData (
            [MarshalAs(UnmanagedType.I4), In] Int32 listOrUnitId, 
            [MarshalAs(UnmanagedType.I4), In] Int32 programIndex, 
            [MarshalAs(UnmanagedType.I4), In] IBStream data);
    }
}
