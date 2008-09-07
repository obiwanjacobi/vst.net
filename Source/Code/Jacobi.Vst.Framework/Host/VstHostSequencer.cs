﻿namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// Provides access to the sequencing functionality of the vst host.
    /// </summary>
    internal class VstHostSequencer : IVstHostSequencer
    {
        /// <summary>Reference to the root host object.</summary>
        private VstHost _host;

        /// <summary>
        /// Constructs a new instance based on a root <paramref name="host"/> object.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostSequencer(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstHostSequencer Members

        /// <summary>
        /// Gets the current sample rate.
        /// </summary>
        public double SampleRate
        {
            get { return _host.HostCommandStub.GetSampleRate(); }
        }

        /// <summary>
        /// Gets the current block size in bytes.
        /// </summary>
        public int BlockSize
        {
            get { return _host.HostCommandStub.GetBlockSize(); }
        }

        /// <summary>
        /// Gets the input latency.
        /// </summary>
        public int InputLatency
        {
            get { return _host.HostCommandStub.GetInputLatency(); }
        }

        /// <summary>
        /// Gets the output latency.
        /// </summary>
        public int OutputLatency
        {
            get { return _host.HostCommandStub.GetOutputLatency(); }
        }

        /// <summary>
        /// Retrieves time info in a specific format.
        /// </summary>
        /// <param name="filterFlags">Indicates the preferred time information format.</param>
        /// <returns>Returns time information but not necessarilly in the format specified by <paramref name="filterFlags"/>.</returns>
        public VstTimeInfo GetTime(VstTimeInfoFlags filterFlags)
        {
            return _host.HostCommandStub.GetTimeInfo(filterFlags);
        }

        /// <summary>
        /// Notify the host the plugin's IO has changed.
        /// </summary>
        /// <returns>Returns true if the host supports changing plugin IO at runtime.</returns>
        public bool UpdatePluginIO()
        {
            return _host.HostCommandStub.IoChanged();
        }

        #endregion
    }
}
