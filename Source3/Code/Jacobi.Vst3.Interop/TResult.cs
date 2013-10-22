using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Jacobi.Vst3.Interop
{
    public static class TResult
    {
        

        // success codes (positive)
        public const Int32 S_OK = 0;
        public const Int32 S_False = 1;

        // error codes (negative)
        public const Int32 E_Pointer = -2147467262;    // TODO: E_FAIL code
        public const Int32 E_Fail = -2147467262;    // TODO: E_FAIL code
        public const Int32 E_NoInterface = -2147467262;
        public const Int32 E_ClassNotReg = -2147467262;    // TODO: E_FAIL code
    }
}
