namespace Jacobi.Vst.Framework.Host
{
    internal class VstHostMidiProcessor : IVstMidiProcessor
    {
        private VstHost _host;

        public VstHostMidiProcessor(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstMidiProcessor Members

        public int ChannelCount
        {
            // default number of channels for host
            get { return 16; }
        }

        public void Process(VstEventCollection events)
        {
            Throw.IfArgumentIsNull(events, "events");

            _host.HostCommandStub.ProcessEvents(events.ToArray());
        }

        #endregion
    }
}
