namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Provides unmanged buffer access to an audio buffer
    /// </summary>
    [System.CLSCompliant(false)]
    public unsafe interface IDirectBufferAccess32
    {
        /// <summary>
        /// Gets the number of samples (floats) in the buffer
        /// </summary>
        int SampleCount { get; }
        /// <summary>
        /// Gets the pointer to the audio buffer
        /// </summary>
        float* Buffer { get; }
    }

    /// <summary>
    /// Provides unmanged buffer access to an audio buffer
    /// </summary>
    [System.CLSCompliant(false)]
    public unsafe interface IDirectBufferAccess64
    {
        /// <summary>
        /// Gets the number of samples (doubles) in the buffer
        /// </summary>
        int SampleCount { get; }
        /// <summary>
        /// Gets the pointer to the audio buffer
        /// </summary>
        double* Buffer { get; }
    }
}
