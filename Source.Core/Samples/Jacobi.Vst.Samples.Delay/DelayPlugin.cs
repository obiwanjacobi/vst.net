namespace Jacobi.Vst.Samples.Delay
{
    /// <summary>
    /// The Plugin root class.
    /// </summary>
    internal class DelayPlugin : VstPluginBase
    {
        private DelayPluginInterfaceManager _intfMgr;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public DelayPlugin()
            : base("VST.NET Delay Plugin",
                new VstProductInfo("VST.NET Code Samples", "Jacobi Software (c) 2011", 1000),
                VstPluginCategory.RoomFx, VstPluginCapabilities.None, 0, 0x3A3A3A3A)
        {
            _intfMgr = new DelayPluginInterfaceManager(this);
            ParameterFactory = new PluginParameterFactory();

            AudioProcessor audioProcessor = _intfMgr.GetInstance<AudioProcessor>();
            // add delay parameters to factory
            ParameterFactory.ParameterInfos.AddRange(audioProcessor.Delay.ParameterInfos);
        }

        /// <summary>
        /// Gets the parameter factory.
        /// </summary>
        public PluginParameterFactory ParameterFactory { get; private set; }

        #region IExtensibleObject Members

        /// <summary>
        /// Indicates if a type <typeparamref name="T"/> is implemented by the plugin.
        /// </summary>
        /// <typeparam name="T">Type of an interface or class.</typeparam>
        /// <returns>Returns true if implemented, otherwise false is returned.</returns>
        public override bool Supports<T>()
        {
            return _intfMgr.Supports<T>();
        }

        /// <summary>
        /// Retrieves an reference of the type <typeparamref name="T"/> or null.
        /// </summary>
        /// <typeparam name="T">Type of an interface or class.</typeparam>
        /// <returns>Returns null of the type <typeparamref name="T"/> is not implemented.</returns>
        public override T GetInstance<T>()
        {
            return _intfMgr.GetInstance<T>();
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Dispose the plugin.
        /// </summary>
        public override void Dispose()
        {
            _intfMgr.Dispose();
            _intfMgr = null;

            base.Dispose();
        }

        #endregion
    }
}
