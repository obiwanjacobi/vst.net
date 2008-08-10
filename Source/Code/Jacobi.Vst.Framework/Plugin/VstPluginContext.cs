namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;
    using Jacobi.Vst.Framework.Host;

    /// <summary>
    /// Contains all root references in context of a plugin.
    /// </summary>
    internal class VstPluginContext : IDisposable
    {
        public VstPluginInfo PluginInfo;
        public IVstPlugin Plugin;
        public VstHost Host;

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
