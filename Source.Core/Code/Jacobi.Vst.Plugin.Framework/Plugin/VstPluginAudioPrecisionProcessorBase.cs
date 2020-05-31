namespace Jacobi.Vst.Framework.Plugin
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstPluginAudioPrecisionProcessorBase implements the <see cref="IVstPluginAudioPrecisionProcessor"/> 
    /// interface and provides a basis for the Plugin implementation.
    /// </summary>
    public class VstPluginAudioPrecisionProcessorBase : VstPluginAudioProcessorBase, IVstPluginAudioPrecisionProcessor
    {
        /// <inheritdoc />
        protected VstPluginAudioPrecisionProcessorBase()
            : base()
        { }

        /// <inheritdoc />
        protected VstPluginAudioPrecisionProcessorBase(int inputCount, int outputCount, int tailSize)
            : base(inputCount, outputCount, tailSize)
        { }

        #region IVstPluginAudioPrecisionProcessor Members

        /// <inheritdoc />
        public virtual void Process(VstAudioPrecisionBuffer[] inChannels, VstAudioPrecisionBuffer[] outChannels)
        {
            int outCount = outChannels.Length;

            for (int n = 0; n < outCount; n++)
            {
                for (int i = 0; i < inChannels.Length && n < outCount; i++, n++)
                {
                    inChannels[i].CopyTo(outChannels[n]);
                }
            }
        }

        #endregion
    }
}
