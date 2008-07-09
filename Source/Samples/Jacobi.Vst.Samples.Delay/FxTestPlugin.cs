﻿namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;

    internal class FxTestPlugin : IVstPlugin
    {
        private IVstHost _host;
        private FxPluginInterfaceManager _intfMgr;
        
        public FxTestPlugin()
        {
            _intfMgr = new FxPluginInterfaceManager(this);
            ParameterFactory = new PluginParameterFactory();

            AudioProcessor audioProcessor = _intfMgr.GetInstance<AudioProcessor>();
            // add delay parameters to factory
            ParameterFactory.ParameterInfos.AddRange(audioProcessor.Delay.ParameterInfos);
        }

        public PluginParameterFactory ParameterFactory { get; set; }


        #region IVstPlugin Members

        private VstProductInfo _productInfo;
        public VstProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new VstProductInfo("VST.NET Code Samples", "Jacobi Software (c) 2008", 1000);
                }

                return _productInfo;
            }
        }

        public string Name
        {
            get { return "VST.NET Delay Plugin"; }
        }

        public Jacobi.Vst.Core.VstPluginCategory Category
        {
            get { return Jacobi.Vst.Core.VstPluginCategory.Unknown; }
        }

        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.None; }
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
