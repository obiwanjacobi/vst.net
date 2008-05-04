namespace Jacobi.Vst.Framework.TestPlugin
{
    internal class FxTestPlugin : IVstPlugin
    {
        private IVstHost _host;
        private FxPluginInterfaceManager _intfMgr;

        public FxTestPlugin()
        {
            _intfMgr = new FxPluginInterfaceManager(this);
        }

        #region IVstPlugin Members

        private ProductInfo _productInfo;
        public ProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new ProductInfo("VST.NET Framework TestPlugin", "Jacobi Software", 1000);
                }

                return _productInfo;
            }
        }

        public string BaseDirectory
        {
            get { return null; }
        }

        public string Name
        {
            get { return "VST.NET Fx Test Plugin"; }
        }

        public Jacobi.Vst.Core.VstPluginCategory Category
        {
            get { return Jacobi.Vst.Core.VstPluginCategory.Unknown; }
        }

        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.NoSoundInStop; }
        }

        public int InitialDelay
        {
            get { return 0; }
        }

        public int PluginID
        {
            get { return 0x3A3A3A3A; }
        }

        public void Open(IVstHost host)
        {
            _host = host;
        }

        public void Suspend()
        {
            
        }

        public void Resume()
        {
            
        }

        #endregion

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return _intfMgr.Supports<T>();
        }

        public T GetInstance<T>() where T : class
        {
            return _intfMgr.GetInstance<T>();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _intfMgr.Dispose();
            _intfMgr = null;
            _host = null;
        }

        #endregion
    }
}
