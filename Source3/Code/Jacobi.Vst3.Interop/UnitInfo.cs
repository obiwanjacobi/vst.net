﻿using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct UnitInfo
    {
        /** Special UnitIDs for UnitInfo */
        public const Int32 RootUnitId = 0;        ///< identifier for the top level unit (root)
        public const Int32 NoParentUnitId = -1;	///< used for the root unit which doesn't have a parent.
        /** Special ProgramListIDs for UnitInfo */
        public const Int32 NoProgramListId = -1;	///< no programs are used in the unit.

        [MarshalAs(UnmanagedType.I4)]
        Int32 Id;						///< unit identifier

        [MarshalAs(UnmanagedType.I4)]
        Int32 ParentUnitId;			///< identifier of parent unit (kNoParentUnitId: does not apply, this unit is the root)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        String Name;					///< name, optional for the root component, required otherwise

        [MarshalAs(UnmanagedType.I4)]
        Int32 ProgramListId;	///< id of program list used in unit (kNoProgramListId = no programs used in this unit)
    }
}
