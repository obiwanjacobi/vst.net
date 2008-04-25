namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Core;

    internal class VstHost : IExtensibleObject, IVstHost, IDisposable
    {
        private IVstHostCommandStub _hostCmdStub;

        public VstHost(IVstHostCommandStub hostCmdStub)
        {
            _hostCmdStub = hostCmdStub;
        }

        #region IVstHost Members

        private ProductInfo _productInfo;
        public ProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new ProductInfo(
                        _hostCmdStub.GetProductString(),
                        _hostCmdStub.GetVendorString(),
                        _hostCmdStub.GetVendorVersion());
                }

                return _productInfo;
            }
        }

        #endregion

        #region IExtensibleObject Members

        public bool Supports<T>(bool threadSafe) where T : class
        {
            throw new System.NotImplementedException();
        }

        public T GetInstance<T>(bool threadSafe) where T : class
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _hostCmdStub = null;
        }

        #endregion
    }
}
