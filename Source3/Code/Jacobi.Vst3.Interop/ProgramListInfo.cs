using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct ProgramListInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        Int32 Id;				///< program list identifier

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        String Name;					///< name of program list

        [MarshalAs(UnmanagedType.I4)]
        Int32 ProgramCount;				///< number of programs in this list
    }
}
