namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Common;
    using Jacobi.Vst.Framework.Plugin;

    class FxPluginInterfaceManager : PluginInterfaceManagerBase
    {
        private FxTestPlugin _plugin;

        public FxPluginInterfaceManager(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new AudioProcessor(_plugin);

            return instance;    // reuse initial instance
        }

        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            if (instance == null) return new PluginPrograms(_plugin);

            return instance;    // reuse initial instance
        }

        protected override IVstPluginPersistence CreatePersistence(IVstPluginPersistence instance)
        {
            if (instance == null) return new PluginPersistence(_plugin);

            return instance;
        }
    }
}
