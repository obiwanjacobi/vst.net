namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public sealed class VstAudioPrecisionChannel : IDirectBufferAccess64
    {
        private VstAudioPrecisionBuffer _audioBuffer;

        public VstAudioPrecisionChannel(VstAudioPrecisionBuffer audioBuffer, bool canWrite)
        {
            Throw.IfArgumentIsNull(audioBuffer, "audioBuffer");

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
                Throw.IfArgumentNotInRange(index, 0, _audioBuffer.Count, "index");

                unsafe
                {
                    return _audioBuffer.Buffer[index];
                }
            }
            set
            {
                if (!CanWrite) throw new InvalidOperationException("Cannot write to the channel.");
                Throw.IfArgumentNotInRange(index, 0, _audioBuffer.Count, "index");

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
