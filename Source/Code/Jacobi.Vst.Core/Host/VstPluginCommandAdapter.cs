namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The VstPluginCommandAdapter class implements the Plugin <see cref="Jacobi.Vst.Core.Host.IVstPluginCommandStub"/>
    /// interface and forwards those calls to a <see cref="Jacobi.Vst.Core.Plugin.IVstPluginCommandStub"/> implementation
    /// provided when the adapter class is constructed.
    /// </summary>
    public class VstPluginCommandAdapter : IVstPluginCommandStub
    {
        private readonly Plugin.IVstPluginCommandStub _pluginCmdStub;

        /// <summary>
        /// Constructs a new instance based on the <paramref name="pluginCmdStub"/>
        /// </summary>
        /// <param name="pluginCmdStub">Will be used to forward calls to. Must not be null.</param>
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public VstPluginCommandAdapter(Plugin.IVstPluginCommandStub pluginCmdStub)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            Throw.IfArgumentIsNull(pluginCmdStub, nameof(pluginCmdStub));

            _pluginCmdStub = pluginCmdStub;
        }

        #region IVstPluginCommandStub Members

        /// <inheritdoc />
        public IVstPluginContext PluginContext { get; set; }

        public IVstPluginCommands24 Commands
        {
            get { return _pluginCmdStub.Commands; }
        }

        #endregion

        /// <summary>
        /// A factory method to create the correct <see cref="VstPluginCommandAdapter"/> class type.
        /// </summary>
        /// <param name="pluginCmdStub">A reference to the plugin command stub. Must not be null.</param>
        /// <returns>Returns an instance of <see cref="Legacy.VstPluginCommandLegacyAdapter"/> when the <paramref name="pluginCmdStub"/> supports legacy methods.</returns>
        public static VstPluginCommandAdapter Create(Plugin.IVstPluginCommandStub pluginCmdStub)
        {
            if (pluginCmdStub is Legacy.IVstPluginCommandsLegacy20)
            {
                return new Legacy.VstPluginCommandLegacyAdapter(pluginCmdStub);
            }

            return new VstPluginCommandAdapter(pluginCmdStub);
        }
    }
}
