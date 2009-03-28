using System;
using System.Diagnostics;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.Framework.Plugin
{
    /// <summary>
    /// The VstPluginAudioPrecisionProcessorBase implements the <see cref="IVstPluginAudioPrecisionProcessor"/> 
    /// interface and provides a basis for the Plugin implementation.
    /// </summary>
    public class VstPluginAudioPrecisionProcessorBase : VstPluginAudioProcessorBase, IVstPluginAudioPrecisionProcessor
    {
        /// <inheritdoc />
        protected VstPluginAudioPrecisionProcessorBase()
            : base ()
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
                    Copy(inChannels[i], outChannels[n]);
                }
            }
        }

        #endregion

        /// <summary>
        /// Copies the samples from the <paramref name="source"/> to the <paramref name="dest"/>ination.
        /// </summary>
        /// <param name="source">The source audio buffer. Must not be null.</param>
        /// <param name="dest">The destination audio buffer. Must be writable. Must not be null.</param>
        protected void Copy(VstAudioPrecisionBuffer source, VstAudioPrecisionBuffer dest)
        {
            Debug.Assert(source.SampleCount == dest.SampleCount);
            Debug.Assert(dest.CanWrite);

            unsafe
            {
                double* inputBuffer = ((IDirectBufferAccess64)source).Buffer;
                double* outputBuffer = ((IDirectBufferAccess64)dest).Buffer;

                for (int i = 0; i < source.SampleCount; i++)
                {
                    outputBuffer[i] = inputBuffer[i];
                }
            }
        }
    }
}
