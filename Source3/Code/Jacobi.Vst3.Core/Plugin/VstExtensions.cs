using Jacobi.Vst3.Common;
using Jacobi.Vst3.Core;
using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Plugin
{
    public static class VstExtensions
    {
        public static IMessage CreateMessage(this IHostApplication host)
        {
            Guard.ThrowIfNull("host", host);

            var msgType = typeof(IMessage);
            var iid = msgType.GUID;
            IntPtr ptr = IntPtr.Zero;

            var result = host.CreateInstance(ref iid, ref iid, ref ptr);

            if (TResult.Succeeded(result))
            {
                return (IMessage)Marshal.GetTypedObjectForIUnknown(ptr, msgType);
            }

            return null;
        }

        public static uint NumberOfSetBits(uint value)
        {
            // don't ask...
            value = value - ((value >> 1) & 0x55555555);
            value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
            return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
    }
}
