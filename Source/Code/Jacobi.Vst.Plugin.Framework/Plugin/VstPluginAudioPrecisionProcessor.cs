namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstPluginAudioPrecisionProcessorBase implements the <see cref="IVstPluginAudioPrecisionProcessor"/> 
    /// interface and provides a basis for the Plugin implementation.
    /// </summary>
    public class VstPluginAudioPrecisionProcessor : VstPluginAudioProcessor, IVstPluginAudioPrecisionProcessor
    {
        /// <inheritdoc />
        protected VstPluginAudioPrecisionProcessor()
            : base()
        { }

        /// <inheritdoc />
        protected VstPluginAudioPrecisionProcessor(int inputCount, int outputCount, int tailSize, bool noSoundOnStop)
            : base(inputCount, outputCount, tailSize, noSoundOnStop)
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
