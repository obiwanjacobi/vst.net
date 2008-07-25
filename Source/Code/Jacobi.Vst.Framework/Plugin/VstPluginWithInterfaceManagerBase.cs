namespace Jacobi.Vst.Framework.Plugin
{
    using Jacobi.Vst.Core;

    public abstract class VstPluginWithInterfaceManagerBase : Plugin.PluginInterfaceManagerBase, IVstPlugin
    {
        public VstPluginWithInterfaceManagerBase(string name, VstProductInfo productInfo, 
            VstPluginCategory category, VstPluginCapabilities capabilities, 
            int initialDelay, int pluginID)
        {
            Throw.IfArgumentIsNull(productInfo, "productInfo");
            Throw.IfArgumentIsNull(name, "name");

            ProductInfo = productInfo;
            Name = name;
            Category = category;
            Capabilities = capabilities;
            InitialDelay = initialDelay;
            PluginID = pluginID;
        }

        #region IVstPlugin Members

        public VstProductInfo ProductInfo { get; private set; }

        public string Name { get; private set; }

        public VstPluginCategory Category { get; private set; }

        public VstPluginCapabilities Capabilities { get; private set; }

        public int InitialDelay { get; private set; }

        public int PluginID { get; private set; }

        public virtual void Open(IVstHost host)
        {
            Host = host;
        }

        public virtual void Suspend()
        {
            // no-op
        }

        public virtual void Resume()
        {
            // no-op
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Host = null;
            }

            base.Dispose(disposing);
        }

        protected IVstHost Host { get; private set; }
    }
}
