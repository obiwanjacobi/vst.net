namespace Jacobi.Vst.Plugin.Framework
{
    /// <summary>
    /// Extension methods for VstParamaterInfo
    /// </summary>
    public static class VstParameterInfoExtensions
    {
        /// <summary>
        /// Attaches a VstParameterNormalizationInfo instance to the parameter info.
        /// </summary>
        /// <param name="paramInfo">Must not be null.</param>
        /// <returns>Returns <paramref name="paramInfo"/> for fluent syntax.</returns>
        public static VstParameterInfo Normalize(this VstParameterInfo paramInfo)
        {
            VstParameterNormalizationInfo.AttachTo(paramInfo);
            return paramInfo;
        }

        /// <summary>
        /// Creates a <see cref="VstParameterManager"/> instance for the specified <paramref name="paramInfo"/> (this).
        /// </summary>
        /// <param name="paramInfo">Must not be null</param>
        /// <returns>Never returns null.</returns>
        public static VstParameterManager ToManager(this VstParameterInfo paramInfo)
        {
            return new VstParameterManager(paramInfo);
        }
    }
}
