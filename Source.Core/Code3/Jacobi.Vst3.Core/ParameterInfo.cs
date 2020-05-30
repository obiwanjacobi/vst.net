using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = Platform.StructurePack)]
    public struct ParameterInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ParamId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public String Title;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public String ShortTitle;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public String Units;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 StepCount;

        [MarshalAs(UnmanagedType.R8)]
        public Double DefaultNormalizedValue;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 UnitId;

        [MarshalAs(UnmanagedType.I4)]
        public ParameterFlags Flags;

        [Flags]
        public enum ParameterFlags
        {
            None = 0,
            CanAutomate = 1 << 0,
            IsReadOnly = 1 << 1,
            IsWrapAround = 1 << 2,
            IsList = 1 << 3,

            IsProgramChange = 1 << 15,
            IsBypass = 1 << 16
        }
    }
}
