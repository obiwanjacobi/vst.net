using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    public static class Platform
    {
#if X86
        public const int StructurePack = 8;
#endif
#if X64
        public const int StructurePack = 16;
#endif
        public const CharSet CharacterSet = CharSet.Unicode;
        public const CallingConvention DefaultCallingConvention = CallingConvention.StdCall;
    }
}
