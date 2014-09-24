using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Jacobi.Vst3.TestPlugin
{

    internal static class NativeMethods
    {
        [Flags]
        public enum CoInitFlags
        {
            MultiThreaded = 0x0,
            ApartmentThreaded = 0x2,
            DisableOLE1DDE = 0x4,
            SpeedOverMemory = 0x8
        }    

        [DllImport("Ole32.dll", ExactSpelling = true, EntryPoint = "CoInitializeEx", CallingConvention = CallingConvention.StdCall, SetLastError = false, PreserveSig = false)]
        public static extern void CoInitializeEx(IntPtr pvReserved, CoInitFlags coInitFlags);
    }
}
