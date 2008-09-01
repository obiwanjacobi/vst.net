namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;

    /// <summary>
    /// The Plugin root class.
    /// </summary>
    internal class FxTestPlugin : IVstPlugin
    {
        private IVstHost _host;
        private FxPluginInterfaceManager _intfMgr;
        
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FxTestPlugin()
        {
            _intfMgr = new FxPluginInterfaceManager(this);
            ParameterFactory = new PluginParameterFactory();

            AudioProcessor audioProcessor = _intfMgr.GetInstance<AudioProcessor>();
            // add delay parameters to factory
            ParameterFactory.ParameterInfos.AddRange(audioProcessor.Delay.ParameterInfos);
        }

        /// <summary>
        /// Gets or sets the parameter factory.
        /// </summary>
        public PluginParameterFactory ParameterFactory { get; set; }


        #region IVstPlugin Members

        private VstProductInfo _productInfo;
        /// <summary>
        /// Gets the product info.
        /// </summary>
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

        /// <summary>
        /// Gets the plugin name.
        /// </summary>
        public string Name
        {
            get { return "VST.NET Delay Plugin"; }
        }

        /// <summary>
        /// Gets the plugin category
        /// </summary>
        public Jacobi.Vst.Core.VstPluginCategory Category
        {
            get { return Jacobi.Vst.Core.VstPluginCategory.RoomFx; }
        }

        /// <summary>
        /// Gets the plugin additional capabilities.
        /// </summary>
        public VstPluginCapabilities Capabilities
        {
            get { return VstPluginCapabilities.None; }
        }

        /// <summary>
        /// Gets the initial delay of the plugin.
        /// </summary>
        public int InitialDelay
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the unique plugin ID.
        /// </summary>
        /// <remarks>A four character code as integer.</remarks>
        public int PluginID
        {
            get { return 0x3A3A3A3A; }
        }

        /// <summary>
        /// Opens the plugin and passes the host root interface.
        /// </summary>
        /// <param name="host">A framework implemented instance of the host root interface.</param>
        public void Open(IVstHost host)
        {
            _host = host;
        }

        /// <summary>
        /// Suspend plugin processing.
        /// </summary>
        public void Suspend()
        {
            
        }

        /// <summary>
        /// Resume plugin processing.
        /// </summary>
        public void Resume()
        {
            
        }

        #endregion

        #region IExtensibleObject Members

        /// <summary>
        /// Indicates if a type <typeparamref name="T"/> is implemented by the plugin.
        /// </summary>
        /// <typeparam name="T">Type of an interface or class.</typeparam>
        /// <returns>Returns true if implemented, otherwise false is returned.</returns>
        public bool Supports<T>() where T : class
        {
            return _intfMgr.Supports<T>();
        }

        /// <summary>
        /// Retrieves an reference of the type <typeparamref name="T"/> or null.
        /// </summary>
        /// <typeparam name="T">Type of an interface or class.</typeparam>
        /// <returns>Returns null of the type <typeparamref name="T"/> is not implemented.</returns>
        public T GetInstance<T>() where T : class
        {
            return _intfMgr.GetInstance<T>();
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose the plugin.
        /// </summary>
        public void Dispose()
        {
            _intfMgr.Dispose();
            _intfMgr = null;
            _host = null;
        }

        #endregion
    }
}
