namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public class VstAudioChannel : IVstAudioChannel, IDirectBufferAccess32
    {
        private VstAudioBuffer _audioBuffer;

        public VstAudioChannel(VstAudioBuffer audioBuffer, bool canWrite)
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

        public float this[int index]
        {
            get
            {
                if(index < 0 || index > _audioBuffer.Count) throw new ArgumentOutOfRangeException("index", 
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

        #region IDirectBufferAccess32 Members

        public unsafe float* Buffer
        {
            get { return _audioBuffer.Buffer; }
        }

        #endregion
    }
}
