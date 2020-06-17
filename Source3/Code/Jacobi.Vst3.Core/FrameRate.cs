using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct FrameRate
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 framesPerSecond;		// frame rate

        [MarshalAs(UnmanagedType.U4)]
        public FrameRateFlags flags;				// flags #FrameRateFlags

        public enum FrameRateFlags
        {
            kPullDownRate = 1 << 0, // for ex. HDTV: 23.976 fps with 24 as frame rate
            kDropRate = 1 << 1	// for ex. 29.97 fps drop with 30 as frame rate
        }
    }
}
