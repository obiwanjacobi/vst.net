using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop
{
    public static class Guard
    {
        public static void ThrowIfTooLong(string parameterName, string value, int minLength, int maxLength)
        {
            if (value != null &&
                value.Length < minLength || value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(parameterName, value, 
                    String.Format("The length of the value '{0}' is not within range of {1}-{2}.", value, minLength, maxLength));
            }
        }



    }
}
