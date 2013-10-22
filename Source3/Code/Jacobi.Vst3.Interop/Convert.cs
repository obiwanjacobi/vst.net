using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    public static class Convert
    {
        private static void ConvertGuidBuffer(byte[] buffer)
        {
            Array.Reverse(buffer, 0, 4);
            Array.Reverse(buffer, 4, 2);
            Array.Reverse(buffer, 6, 2);
        }

        public static byte[] GuidToByteArray(this Guid guid)
        {
            var buffer = guid.ToByteArray();
            ConvertGuidBuffer(buffer);

            return buffer;
        }

        public static Guid ToGuid(this byte[] buffer)
        {
            // make a copy to not screw up passed in param
            var newBuffer = new byte[Platform.GuidByteSize];
            Array.Copy(buffer, newBuffer, Platform.GuidByteSize);
            
            ConvertGuidBuffer(newBuffer);
            
            return new Guid(newBuffer);
        }

        public static bool Equals(this byte[] buffer, Guid guid)
        {
            var other = GuidToByteArray(guid);

            for (int i = 0; i < Platform.GuidByteSize; i++)
            {
                if (buffer[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
