namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Host;

    internal class VstPluginContext : IDisposable
    {
        public VstPluginInfo PluginInfo;
        public IVstPlugin Plugin;
        public VstHost Host;

        #region IDisposable Members

        public void Dispose()
        {
            PluginInfo = null;
            
            Plugin.Dispose();
            Plugin = null;
            
            Host.Dispose();
            Host = null;
        }

        #endregion
    }
}
