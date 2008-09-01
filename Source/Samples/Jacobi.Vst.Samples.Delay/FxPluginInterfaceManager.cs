namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Common;
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// This class manages the interface references used by the plugin.
    /// </summary>
    class FxPluginInterfaceManager : PluginInterfaceManagerBase
    {
        private FxTestPlugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public FxPluginInterfaceManager(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new AudioProcessor(_plugin);

            return instance;    // reuse initial instance
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            if (instance == null) return new PluginPrograms(_plugin);

            return instance;    // reuse initial instance
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginPersistence CreatePersistence(IVstPluginPersistence instance)
        {
            if (instance == null) return new PluginPersistence(_plugin);

            return instance;
        }
    }
}
