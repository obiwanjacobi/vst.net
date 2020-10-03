namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System;

    /// <summary>
    /// Provides access to the sequencing functionality of the vst host.
    /// </summary>
    internal sealed class VstHostSequencer : IVstHostSequencer
    {
        private readonly IVstHostCommands20 _commands;

        /// <summary>
        /// Constructs a new instance based on a <paramref name="commands"/> object.
        /// </summary>
        /// <param name="commands">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="commands"/> is not set to an instance of an object.</exception>
        public VstHostSequencer(IVstHostCommands20 commands)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        #region IVstHostSequencer Members

        /// <summary>
        /// Gets the current sample rate.
        /// </summary>
        public double SampleRate
        {
            get { return _commands.GetSampleRate(); }
        }

        /// <summary>
        /// Gets the current block size in bytes.
        /// </summary>
        public int BlockSize
        {
            get { return _commands.GetBlockSize(); }
        }

        /// <summary>
        /// Gets the input latency.
        /// </summary>
        public int InputLatency
        {
            get { return _commands.GetInputLatency(); }
        }

        /// <summary>
        /// Gets the output latency.
        /// </summary>
        public int OutputLatency
        {
            get { return _commands.GetOutputLatency(); }
        }

        /// <summary>
        /// Retrieves time info in a specific format.
        /// </summary>
        /// <param name="filterFlags">Indicates the preferred time information format.</param>
        /// <returns>Returns time information but not necessarilly in the format specified by <paramref name="filterFlags"/>.</returns>
        public VstTimeInfo GetTime(VstTimeInfoFlags filterFlags)
        {
            return _commands.GetTimeInfo(filterFlags);
        }

        /// <summary>
        /// Notify the host the plugin's IO has changed.
        /// </summary>
        /// <returns>Returns true if the host supports changing plugin IO at runtime.</returns>
        public bool UpdatePluginIO()
        {
            return _commands.IoChanged();
        }

        #endregion
    }
}
