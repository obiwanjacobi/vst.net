using System;
using System.Diagnostics;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.Framework.Plugin
{
    public class VstPluginAudioPrecisionProcessorBase : VstPluginAudioProcessorBase, IVstPluginAudioPrecisionProcessor
    {
        protected VstPluginAudioPrecisionProcessorBase()
            : base ()
        { }

        protected VstPluginAudioPrecisionProcessorBase(int inputCount, int outputCount, int tailSize)
            : base(inputCount, outputCount, tailSize)
        { }

        #region IVstPluginAudioPrecisionProcessor Members

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
