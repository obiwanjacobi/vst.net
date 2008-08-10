namespace Jacobi.Vst.Core
{
    using System;

    public unsafe class VstAudioPrecisionBuffer : IDirectBufferAccess64
    {
        public VstAudioPrecisionBuffer(double* buffer, int length, bool canWrite)
        {
            Buffer = buffer;
            SampleCount = length;
            CanWrite = canWrite;
        }

        public int SampleCount { get; private set; }
        public bool CanWrite { get; private set; }
        private double* Buffer { get; set; }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= SampleCount)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                        String.Format("The value should lie between '0' and '{0}'.", SampleCount));
                }

                unsafe
                {
                    return Buffer[index];
                }
            }
            set
            {
                if (!CanWrite) throw new InvalidOperationException("Cannot write to the channel.");
                if (index < 0 || index >= SampleCount)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                        String.Format("The value should lie between '0' and '{0}'.", SampleCount));
                }

                unsafe
                {
                    Buffer[index] = value;
                }
            }
        }

        #region IDirectBufferAccess64 Members

        unsafe double* IDirectBufferAccess64.Buffer
        {
            get { return Buffer; }
        }

        #endregion
    }
}
