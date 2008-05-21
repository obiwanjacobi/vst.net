namespace Jacobi.Vst.Framework.TestPlugin
{
    class FxPluginInterfaceManager : Plugin.PluginInterfaceManagerBase
    {
        private FxTestPlugin _plugin;

        public FxPluginInterfaceManager(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if(instance == null) return new AudioProcessor(_plugin);

            return instance;    // reuse initial instance
        }

        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            if (instance == null) return new PluginPrograms(_plugin);

            return instance;    // reuse initial instance
        }
    }
}
