using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    public static class Platform
    {
        public const int StructurePack = 1;
        public const CharSet CharacterSet = CharSet.Unicode;
        public const CallingConvention DefaultCallingConvention = CallingConvention.StdCall;
    }
}
