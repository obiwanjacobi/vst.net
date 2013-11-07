using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop.Plugin
{
    public static class VstExtensions
    {
        public static uint NumberOfSetBits(uint value)
        {
            // don't ask...
            value = value - ((value >> 1) & 0x55555555);
            value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
            return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
    }
}
