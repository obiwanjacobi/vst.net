namespace Jacobi.Vst.Framework.Plugin
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;
    using Jacobi.Vst.Framework.Host;
    using System;

    internal class VstPluginContext : IDisposable
    {
        public VstPluginInfo PluginInfo;
        public ExtensibleObjectRef<IVstPlugin> Plugin;
        public ExtensibleObjectRef<VstHost> Host;

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
