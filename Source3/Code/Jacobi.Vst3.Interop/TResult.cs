using System;

namespace Jacobi.Vst3.Interop
{
    public static class TResult
    {
        // success codes (positive)
        public const Int32 S_OK = 0;
        public const Int32 S_True = S_OK;
        public const Int32 S_False = 1;

        // error codes (negative)
        public const Int32 E_Pointer = -2147467261;
        public const Int32 E_Fail = -2147467259;
        public const Int32 E_NoInterface = -2147467262;
        public const Int32 E_ClassNotReg = -2147221164;
        public const Int32 E_InvalidArg = -2147024809;
        public const Int32 E_NotImplemented = -2147467263;
        public const Int32 E_Unexpected = -2147418113;
        public const Int32 E_OutOfMemory = -2147024882;
        public const Int32 E_Abort = -2147467260;

        public static bool Succeeded(int result)
        {
            return result >= 0;
        }

        public static bool Failed(int result)
        {
            return result < 0;
        }

        public static bool IsTrue(int result)
        {
            return result == S_True;
        }

        public static bool IsFalse(int result)
        {
            return result == S_False;
        }
    }
}
