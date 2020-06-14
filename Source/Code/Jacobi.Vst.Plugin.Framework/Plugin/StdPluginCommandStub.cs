namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The StdPluginCommandStub class provides a default implementation for adapting the <see cref="IVstPluginCommandStub"/> 
    /// interface calls to the framework.
    /// </summary>
    /// <remarks>Each plugin must implement a public class the implements the <see cref="IVstPluginCommandStub"/> interface.
    /// Plugins that use the framework can just derive from this class and override the <see cref="CreatePluginInstance"/> method
    /// to create their plugin root object.</remarks>
    public abstract class StdPluginCommandStub : IVstPluginCommandStub
    {
        private VstPluginContext? _pluginCtx;

        /// <summary>
        /// Provides derived classes accces to the root object of the Plugin.
        /// </summary>
        protected IVstPlugin? Plugin
        {
            get { return _pluginCtx?.Plugin; }
        }

        #region IVstPluginCommandStub Members
        /// <summary>
        /// Called by the Interop loader to retrieve the plugin information.
        /// </summary>
        /// <param name="hostCmdProxy">Must not be null.</param>
        /// <returns>Returns a fully populated <see cref="VstPluginInfo"/> instance. Never returns null.</returns>
        /// <remarks>Override <see cref="CreatePluginInfo"/> to change the default behavior of how the plugin info is built.</remarks>
        public VstPluginInfo? GetPluginInfo(IVstHostCommandProxy hostCmdProxy)
        {
            IVstPlugin plugin = CreatePluginInstance();

            if (plugin != null)
            {
                if (plugin is IConfigurable config)
                {
                    config.Configuration = this.PluginConfiguration;
                }

                _pluginCtx = new VstPluginContext(
                    plugin, new Host.VstHost(hostCmdProxy, plugin), CreatePluginInfo(plugin));

                Commands = CreatePluginCommands(_pluginCtx);

                return _pluginCtx.PluginInfo;
            }

            return null;
        }

        /// <summary>
        /// Gets or sets the custom plugin specific configuration object.
        /// </summary>
        /// <remarks>Can be null if the plugin has not deployed a config file.</remarks>
        public IConfiguration? PluginConfiguration { get; set; }

        /// <inheritdoc />
        public IVstPluginCommands24? Commands { get; private set; }

        #endregion

        /// <summary>
        /// Derived class must override and create the plugin instance.
        /// </summary>
        /// <returns>Returning null will abort loading the plugin.</returns>
        protected abstract IVstPlugin CreatePluginInstance();

        /// <summary>
        /// Override to create a custom Command implementation.
        /// </summary>
        /// <param name="pluginCtx">Plugin and Host info. Is never null.</param>
        /// <returns>Never returns null.</returns>
        protected virtual IVstPluginCommands24 CreatePluginCommands(VstPluginContext pluginCtx)
        {
            return new VstPluginCommands(pluginCtx);
        }

        /// <summary>
        /// Creates summary info based on the <paramref name="plugin"/>.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        /// <returns>Never returns null.</returns>
        /// <remarks>Override to add or change behavior.</remarks>
        protected virtual VstPluginInfo CreatePluginInfo(IVstPlugin plugin)
        {
            VstPluginInfo pluginInfo = new VstPluginInfo();

            var audioProcessor = plugin.GetInstance<IVstPluginAudioProcessor>();

            // determine flags
            if (plugin.Supports<IVstPluginEditor>())
                pluginInfo.Flags |= VstPluginFlags.HasEditor;
            if (audioProcessor != null)
                pluginInfo.Flags |= VstPluginFlags.CanReplacing;
            if (plugin.Supports<IVstPluginAudioPrecisionProcessor>())
                pluginInfo.Flags |= VstPluginFlags.CanDoubleReplacing;
            if (plugin.Supports<IVstPluginPersistence>())
                pluginInfo.Flags |= VstPluginFlags.ProgramChunks;
            if (audioProcessor != null && plugin.Supports<IVstMidiProcessor>())
                pluginInfo.Flags |= VstPluginFlags.IsSynth;
            if (audioProcessor != null && audioProcessor.NoSoundInStop)
                pluginInfo.Flags |= VstPluginFlags.NoSoundInStop;

            // basic plugin info
            pluginInfo.InitialDelay = plugin.InitialDelay;
            pluginInfo.PluginID = plugin.PluginID;
            pluginInfo.PluginVersion = plugin.ProductInfo.Version;

            // audio processing info
            if (audioProcessor != null)
            {
                pluginInfo.AudioInputCount = audioProcessor.InputCount;
                pluginInfo.AudioOutputCount = audioProcessor.OutputCount;
            }

            // parameter info
            IVstPluginParameters? pluginParameters = plugin.GetInstance<IVstPluginParameters>();
            if (pluginParameters != null)
            {
                pluginInfo.ParameterCount = pluginParameters.Parameters.Count;
            }

            // program info
            IVstPluginPrograms? pluginPrograms = plugin.GetInstance<IVstPluginPrograms>();
            if (pluginPrograms != null)
            {
                pluginInfo.ProgramCount = pluginPrograms.Programs.Count;
            }

            return pluginInfo;
        }
    }
}
