﻿using System;

namespace Jacobi.Vst.Core
{
    /// <summary>
    /// A helper class to parse strings into a <see cref="VstPluginCanDo"/> or <see cref="VstHostCanDo"/> enumeration value
    /// and to convert those to strings that start with a lower case character.
    /// </summary>
    public static class VstCanDoHelper
    {
        /// <summary>
        /// Attempts to parse the <paramref name="cando"/> string.
        /// </summary>
        /// <param name="cando">Must not be null or empty.</param>
        /// <returns>Returns <see cref="VstPluginCanDo.Unknown"/> when string did not match an enum value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="cando"/> is not set to an instance of an object.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="cando"/> is an empty string.</exception>
        public static VstPluginCanDo ParsePluginCanDo(string cando)
        {
            Throw.IfArgumentIsNullOrEmpty(cando, "cando");

            VstPluginCanDo result = VstPluginCanDo.Unknown;
            Type enumType = typeof(VstPluginCanDo);

            foreach (string name in Enum.GetNames(enumType))
            {
                if (name.Equals(cando, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = (VstPluginCanDo)Enum.Parse(enumType, cando, true);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Attempts to parse the <paramref name="cando"/> string.
        /// </summary>
        /// <param name="cando">Must not be null or empty.</param>
        /// <returns>Returns <see cref="VstHostCanDo.None"/> when string did not match an enum value.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="cando"/> is not set to an instance of an object.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="cando"/> is an empty string.</exception>
        public static VstHostCanDo ParseHostCanDo(string cando)
        {
            Throw.IfArgumentIsNullOrEmpty(cando, "cando");

            VstHostCanDo result = VstHostCanDo.None;
            Type enumType = typeof(VstHostCanDo);

            foreach (string name in Enum.GetNames(enumType))
            {
                if (name.Equals(cando, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = (VstHostCanDo)Enum.Parse(enumType, cando, true);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts a <paramref name="cando"/> value to a string compatible with VST.
        /// </summary>
        /// <param name="cando">The value to convert to string.</param>
        /// <returns>Returns the string value for the <paramref name="cando"/>.</returns>
        public static string ToString(VstHostCanDo cando)
        {
            string result = cando.ToString();

            if (result[0] == 'x')
            {
                // strip of leading 'x'
                result = result.Substring(1);
            }
            else
            {
                // lower case on first character
                result = char.ToLowerInvariant(result[0]) + result.Substring(1);
            }

            return result;
        }

        /// <summary>
        /// Converts a <paramref name="cando"/> value to a string compatible with VST.
        /// </summary>
        /// <param name="cando">The value to convert to string.</param>
        /// <returns>Returns the string value for the <paramref name="cando"/>.</returns>
        public static string ToString(VstPluginCanDo cando)
        {
            string result = cando.ToString();

            // lower case on first character
            result = char.ToLowerInvariant(result[0]) + result.Substring(1);

            return result;
        }
    }
}
