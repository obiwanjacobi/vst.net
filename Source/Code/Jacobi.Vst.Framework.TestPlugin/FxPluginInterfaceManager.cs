namespace Jacobi.Vst.Framework.TestPlugin
{
    class FxPluginInterfaceManager : Plugin.PluginInterfaceManagerBase
    {
        private FxTestPlugin _plugin;

        public FxPluginInterfaceManager(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        protected override IVstPluginAudioProcessor CreateAudioProcessor(bool threadSafe)
        {
            if(threadSafe == false) return new AudioProcessor(_plugin);

            return null;    // returning null for threadSafe implementations will reuse initial instance
        }

        protected override IVstPluginPrograms CreatePrograms(bool threadSafe)
        {
            if (threadSafe == false) return new PluginPrograms(_plugin);

            return null;    // returning null for threadSafe implementations will reuse initial instance
        }
    }
}
