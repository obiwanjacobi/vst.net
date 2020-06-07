using Microsoft.Extensions.Configuration;

namespace Jacobi.Vst.Core.Plugin
{
    /// <summary>
    /// The Plugin Command Stub called by the Interop.
    /// </summary>
    public interface IVstPluginCommandStub
    {
        /// <summary>
        /// Retrieves the plugin info to pass onto the host.
        /// </summary>
        /// <param name="hostCmdProxy">A reference to the host command proxy the plugin can use to call the host. Must not be null.</param>
        /// <returns>Returns an instance with the plugin info filled in. 
        /// If null is returned the plugin load sequence is aborted.</returns>
        VstPluginInfo? GetPluginInfo(IVstHostCommandProxy hostCmdProxy);

        /// <summary>
        /// Gets or sets the custom plugin specific configuration object.
        /// </summary>
        /// <remarks>Can be null if the plugin has not deployed a config file.</remarks>
        public IConfiguration? PluginConfiguration { get; set; }

        /// <summary>
        /// If null, the object is unitialized or disposed.
        /// </summary>
        public IVstPluginCommands24? Commands { get; }
    }
}
