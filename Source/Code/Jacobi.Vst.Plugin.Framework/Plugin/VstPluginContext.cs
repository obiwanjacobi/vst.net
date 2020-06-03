namespace Jacobi.Vst.Plugin.Framework.Plugin
{
    using Jacobi.Vst.Core.Plugin;
    using Jacobi.Vst.Plugin.Framework.Host;
    using System;

    /// <summary>
    /// Contains all root references in context of a plugin.
    /// </summary>
    internal sealed class VstPluginContext : IDisposable
    {
        /// <summary>Reference to the plugin information.</summary>
        public VstPluginInfo? PluginInfo;
        /// <summary>Reference to the plugin root object.</summary>
        public IVstPlugin? Plugin;
        /// <summary>Reference to Host Proxy.</summary>
        public VstHost? Host;

        #region IDisposable Members

        /// <summary>
        /// Disposes all members, cascades the Dispose call.
        /// </summary>
        public void Dispose()
        {
            PluginInfo = null;

            if (Plugin != null)
            {
                Plugin.Dispose();
                Plugin = null;
            }

            if (Host != null)
            {
                Host.Dispose();
                Host = null;
            }
        }

        #endregion
    }
}
