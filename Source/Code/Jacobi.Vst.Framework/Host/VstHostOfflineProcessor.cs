namespace Jacobi.Vst.Framework.Host
{
    internal class VstHostOfflineProcessor : IVstHostOfflineProcessor
    {
        private VstHost _host;

        public VstHostOfflineProcessor(VstHost host)
        {
            _host = host;
        }

        #region IVstHostOfflineProcessor Members

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Read()
        {
            throw new System.NotImplementedException();
        }

        public void Write()
        {
            throw new System.NotImplementedException();
        }

        public void GetCurrentPass()
        {
            throw new System.NotImplementedException();
        }

        public void GetCurrentMetaPass()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
