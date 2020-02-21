namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// Provides access to the sequencing functionality of the host.
    /// </summary>
    public interface IVstHostSequencer
    {
        /// <summary>
        /// Gets the current sample rate.
        /// </summary>
        double SampleRate { get;}
        /// <summary>
        /// Gets the current block size in bytes.
        /// </summary>
        int BlockSize { get;}
        /// <summary>
        /// Gets the input latency.
        /// </summary>
        int InputLatency { get;}
        /// <summary>
        /// Gets the output latency.
        /// </summary>
        int OutputLatency { get;}
        
        /// <summary>
        /// Retrieves the current time information.
        /// </summary>
        /// <param name="filterFlags">Indicates the preferred time information format.</param>
        /// <returns>Returns time information but not necessarilly in the format specified by <paramref name="filterFlags"/>.</returns>
        VstTimeInfo GetTime(VstTimeInfoFlags filterFlags);

        /// <summary>
        /// Notify the host the plugin's IO has changed.
        /// </summary>
        /// <returns>Returns true if the host supports changing plugin IO at runtime.</returns>
        bool UpdatePluginIO();
    }
}
