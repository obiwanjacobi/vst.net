namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// Forwards the <see cref="IVstMidiProcessor"/> calls to the host stub.
    /// </summary>
    internal class VstHostMidiProcessor : IVstMidiProcessor
    {
        private VstHost _host;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        public VstHostMidiProcessor(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstMidiProcessor Members

        /// <summary>
        /// Always returns 16.
        /// </summary>
        public int ChannelCount
        {
            // default number of channels for host
            get { return 16; }
        }

        /// <summary>
        /// Passes the <paramref name="events"/> onto the host.
        /// </summary>
        /// <param name="events">Must not be null.</param>
        public void Process(VstEventCollection events)
        {
            Throw.IfArgumentIsNull(events, "events");

            _host.HostCommandStub.ProcessEvents(events.ToArray());
        }

        #endregion
    }
}
