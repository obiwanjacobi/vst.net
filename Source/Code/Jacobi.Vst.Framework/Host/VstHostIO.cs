namespace Jacobi.Vst.Framework.Host
{
    internal class VstHostIO : IVstHostIO
    {
        private VstHost _host;

        public VstHostIO(VstHost host)
        {
            _host = host;
        }

        #region IVstHostIO Members

        public bool UpdatePluginIO()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
