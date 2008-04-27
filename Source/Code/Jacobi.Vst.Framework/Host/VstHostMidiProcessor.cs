namespace Jacobi.Vst.Framework.Host
{
    internal class VstHostMidiProcessor : IVstMidiProcessor
    {
        private VstHost _host;

        public VstHostMidiProcessor(VstHost host)
        {
            _host = host;
        }

        #region IVstMidiProcessor Members

        public int ChannelCount
        {
            get { return 16; }
        }

        public void Process(VstEventCollection events)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
