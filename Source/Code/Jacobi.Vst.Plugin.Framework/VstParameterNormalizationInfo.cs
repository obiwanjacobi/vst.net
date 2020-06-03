namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using System;

    /// <summary>
    /// The VstParameterNormalizationInfo class contains a factor to be 
    /// applied to a parameter value to move it into the [0, 1] range.
    /// </summary>
    public class VstParameterNormalizationInfo
    {
        /// <summary>
        /// A factor to limit the reach of the parameter value to 0-1.
        /// </summary>
        public float ScaleFactor { get; set; }
        /// <summary>
        /// An offset that moves the null (zero) value of the parameter to 0 (zero).
        /// </summary>
        public float NullOffset { get; set; }

        /// <summary>
        /// Converts the raw parameter value to a normalized value.
        /// </summary>
        /// <param name="rawValue">The raw parameter value.</param>
        /// <returns>Returns the normalized parameter value that ranges from 0.0 to 1.0.</returns>
        public float GetNormalizedValue(float rawValue)
        {
            return (rawValue + NullOffset) / ScaleFactor;
        }

        /// <summary>
        /// Converts the normalized value to a raw parameter value.
        /// </summary>
        /// <param name="normalizedValue">The normalized value ranging from 0.0 to 1.0</param>
        /// <returns>Returns the raw parameter value.</returns>
        public float GetRawValue(float normalizedValue)
        {
            return (normalizedValue * ScaleFactor) - NullOffset;
        }

        /// <summary>
        /// Attaches the normalization info to the <paramref name="paramInfo"/>.
        /// </summary>
        /// <param name="paramInfo">Must not be null.</param>
        /// <remarks>Making this call enables the Framework to transparently normalize the parameter value, 
        /// untill the host retrieves the parameter properties (info).
        /// Not making this call will always provide the host with the raw parameter value.</remarks>
        public static void AttachTo(VstParameterInfo paramInfo)
        {
            Throw.IfArgumentIsNull(paramInfo, nameof(paramInfo));
            if (paramInfo.NormalizationInfo != null)
            {
                throw new InvalidOperationException(Properties.Resources.VstParameterNormalizationInfo_AlreadyAttached);
            }
            if (!paramInfo.IsMinMaxIntegerValid)
            {
                throw new ArgumentException(Properties.Resources.VstParameterNormalizationInfo_ParameterInfoInvalid, nameof(paramInfo));
            }

            paramInfo.NormalizationInfo = new VstParameterNormalizationInfo();
            paramInfo.NormalizationInfo.ScaleFactor = paramInfo.MaxInteger - paramInfo.MinInteger;
            paramInfo.NormalizationInfo.NullOffset = -paramInfo.MinInteger;
        }
    }
}
