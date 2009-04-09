namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Represents an audio buffer (mono) passed to plugin by the host.
    /// </summary>
    /// <remarks>The VstAudioBuffer implements <see cref="IDirectBufferAccess32"/> for direct, unmanaged access to the buffer.</remarks>
    public unsafe class VstAudioBuffer : IDirectBufferAccess32
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="buffer">The buffer as specified by the host. Must not be null.</param>
        /// <param name="length">The length of the <paramref name="buffer"/>.</param>
        /// <param name="canWrite">An indaction if the buffer content can be changed by plugin.</param>
        public VstAudioBuffer(float* buffer, int length, bool canWrite)
        {
            Buffer = buffer;
            SampleCount = length;
            CanWrite = canWrite;
        }

        /// <summary>
        /// Gets the number of samples in the buffer.
        /// </summary>
        public int SampleCount { get; private set; }
        /// <summary>
        /// Gets an indication if the buffer is writable.
        /// </summary>
        /// <remarks>Writing to a read-only buffer will generate an exception.</remarks>
        public bool CanWrite { get; private set; }
        /// <summary>
        /// The raw buffer.
        /// </summary>
        private float* Buffer { get; set; }

        /// <summary>
        /// Gets or sets (see remarks) the sample value at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">A zer-based index into the buffer.</param>
        /// <returns>Returns the sample value.</returns>
        /// <remarks>The setter will cause an exception when <see cref="CanWrite"/> is false.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified <paramref name="index"/> was not greater 
        /// or equal to zero or it was greater or equal to <see cref="SampleCount"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the setter was used on a read-only buffer (<see cref="CanWrite"/> is false).</exception>
        public float this[int index]
        {
            get
            {
                Throw.IfArgumentNotInRange(index, 0, SampleCount - 1, "index");

                unsafe
                {
                    return Buffer[index];
                }
            }
            set
            {
                if (!CanWrite) throw new InvalidOperationException(Properties.Resources.VstAudioBuffer_BufferNotWritable);
                Throw.IfArgumentNotInRange(index, 0, SampleCount - 1, "index");

                unsafe
                {
                    Buffer[index] = value;
                }
            }
        }

        #region IDirectBufferAccess32 Members
        /// <summary>
        /// Unsafe access to the raw sample buffer.
        /// </summary>
        unsafe float* IDirectBufferAccess32.Buffer
        {
            get { return Buffer; }
        }

        #endregion

        /// <summary>
        /// Copies the samples from this instance to the <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The destination audio buffer. Must be writable. Must not be null.</param>
        public void CopyTo(VstAudioBuffer destination)
        {
            if (this.SampleCount <= destination.SampleCount)
            {
                throw new ArgumentException(Properties.Resources.VstAudioBuffer_BufferTooSmall, "destination");
            }
            if (destination.CanWrite)
            {
                throw new ArgumentException(Properties.Resources.VstAudioBuffer_BufferNotWritable, "destination");
            }

            unsafe
            {
                float* inputBuffer = this.Buffer;
                float* outputBuffer = ((IDirectBufferAccess32)destination).Buffer;

                for (int i = 0; i < this.SampleCount; i++)
                {
                    outputBuffer[i] = inputBuffer[i];
                }
            }
        }
    }
}
