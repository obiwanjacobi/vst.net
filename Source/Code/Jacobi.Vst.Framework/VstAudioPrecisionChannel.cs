namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public sealed class VstAudioPrecisionChannel : IDirectBufferAccess64
    {
        private VstAudioPrecisionBuffer _audioBuffer;

        public VstAudioPrecisionChannel(VstAudioPrecisionBuffer audioBuffer, bool canWrite)
        {
            _audioBuffer = audioBuffer;
            _canWrite = canWrite;
        }

        #region IVstAudioChannel Members

        private bool _canWrite;
        public bool CanWrite
        {
            get { return _canWrite; }
        }

        public int SampleCount
        {
            get { return _audioBuffer.Count; }
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index > _audioBuffer.Count) throw new ArgumentOutOfRangeException("index",
                     "The index must lie between 0 and the SampleCount property value.");

                unsafe
                {
                    return _audioBuffer.Buffer[index];
                }
            }
            set
            {
                if (!CanWrite) throw new InvalidOperationException("Cannot write to the channel.");
                if (index < 0 || index > _audioBuffer.Count) throw new ArgumentOutOfRangeException("index",
                     "The index must lie between 0 and the SampleCount property value.");

                unsafe
                {
                    _audioBuffer.Buffer[index] = value;
                }
            }
        }

        #endregion

        #region IDirectBufferAccess64 Members

        unsafe double* IDirectBufferAccess64.Buffer
        {
            get { return _audioBuffer.Buffer; }
        }

        #endregion
    }
}
