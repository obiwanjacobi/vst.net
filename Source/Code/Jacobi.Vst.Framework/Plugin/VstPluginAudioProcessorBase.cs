using System;
using System.Diagnostics;

using Jacobi.Vst.Core;

namespace Jacobi.Vst.Framework.Plugin
{
    public abstract class VstPluginAudioProcessorBase : IVstPluginAudioProcessor
    {
        protected VstPluginAudioProcessorBase() { }
        
        protected VstPluginAudioProcessorBase(int inputCount, int outputCount, int tailSize)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            TailSize = tailSize;
        }
        
        #region IVstPluginAudioProcessor Members

        public int InputCount { get; protected set; }

        public int OutputCount { get; protected set; }

        public int TailSize { get; protected set; }

        public virtual double SampleRate { get; set; }

        public virtual int BlockSize { get; set; }

        public virtual void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
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

        public virtual bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }

        #endregion

        protected void Copy(VstAudioBuffer source, VstAudioBuffer dest)
        {
            Debug.Assert(source.SampleCount == dest.SampleCount);
            Debug.Assert(dest.CanWrite);

            unsafe
            {
                float* inputBuffer = ((IDirectBufferAccess32)source).Buffer;
                float* outputBuffer = ((IDirectBufferAccess32)dest).Buffer;

                for (int i = 0; i < source.SampleCount; i++)
                {
                    outputBuffer[i] = inputBuffer[i];
                }
            }
        }
    }
}
