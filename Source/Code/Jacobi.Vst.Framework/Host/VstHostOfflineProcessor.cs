//
// This source file is not compiled!
// It is part of offline processing which is not implemented
//
namespace Jacobi.Vst.Framework.Host
{
    /// <summary>
    /// Not implemented.
    /// </summary>
    internal class VstHostOfflineProcessor : IVstHostOfflineProcessor
    {
        private VstHost _host;
        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        public VstHostOfflineProcessor(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

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
