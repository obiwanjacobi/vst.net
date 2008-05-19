namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public sealed class VstAudioChannel : IDirectBufferAccess32
    {
        private VstAudioBuffer _audioBuffer;

        public VstAudioChannel(VstAudioBuffer audioBuffer, bool canWrite)
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

        public float this[int index]
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

        #region IDirectBufferAccess32 Members

        unsafe float* IDirectBufferAccess32.Buffer
        {
            get { return _audioBuffer.Buffer; }
        }

        #endregion
    }
}
