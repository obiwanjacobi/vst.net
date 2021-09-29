using System;
using System.Text;

namespace ComWrappersPlugin
{
    unsafe internal static class Interop
    {
        public static IntPtr CopyAnsi(string source, IntPtr destination, int maxLength)
        {
            var ansiBytes = Encoding.ASCII.GetBytes(source);
            byte* target = (byte*)destination;
            for (int i = 0; i < Math.Min(ansiBytes.Length, maxLength); i++)
            {
                target[i] = ansiBytes[i];
            }

            return destination + maxLength;
        }

        public static IntPtr WriteInt32(Int32 source, IntPtr destination)
        {
            var intPtr = (int*)destination;
            *intPtr = source;
            return destination + sizeof(Int32);
        }
    }
}
