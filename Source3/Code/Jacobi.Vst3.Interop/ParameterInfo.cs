using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = Platform.StructurePack)]
    public struct ParameterInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 ParamId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public string Title;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public string ShortTitle;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.Fixed128)]
        public string Units;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 StepCount;

        [MarshalAs(UnmanagedType.R8)]
        public Double DefaultNormalizedValue;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 UnitId;

        [MarshalAs(UnmanagedType.I4)]
        ParameterFlags Flags;

        [Flags]
        public enum ParameterFlags
        {
            None = 0,
            CanAutomate = 1 << 0,
            IsReadONly = 1 << 1,
            IsWrapAround = 1 << 2,
            IsList = 1 << 3,

            IsProgramChange = 1 << 15,
            IsBypass = 1 << 16
        }
    }
}
