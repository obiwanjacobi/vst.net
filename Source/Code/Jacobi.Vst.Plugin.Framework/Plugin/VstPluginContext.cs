namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    using Jacobi.Vst.Core.Plugin;
    using Jacobi.Vst.Plugin.Framework.Host;
    using System;

    /// <summary>
    /// Contains all root references in context of a plugin.
    /// </summary>
    public sealed class VstPluginContext : IDisposable
    {
        internal VstPluginContext(IVstPlugin plugin, VstHost host, VstPluginInfo info)
        {
            Plugin = plugin ?? throw new ArgumentNullException(nameof(plugin));
            _host = host ?? throw new ArgumentNullException(nameof(host));
            PluginInfo = info ?? throw new ArgumentNullException(nameof(plugin));
        }

        /// <summary>Reference to the plugin information.</summary>
        public VstPluginInfo PluginInfo { get; }
        /// <summary>Reference to the plugin root object.</summary>
        public IVstPlugin Plugin { get; }

        private readonly VstHost _host;
        /// <summary>Reference to Host Proxy.</summary>
        public IVstHost Host { get { return _host; } }
        internal VstHost VstHost { get { return _host; } }

        #region IDisposable Members

        /// <summary>
        /// Disposes all members, cascades the Dispose call.
        /// </summary>
        public void Dispose()
        {
            Plugin.Dispose();
            _host.Dispose();
        }

        #endregion
    }
}
