using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VstNetItemTemplates
{
    /// <summary>
    /// The Plugin root object.
    /// </summary>
    internal sealed class Plugin : VstPluginWithInterfaceManagerBase
    {
        /// <summary>
        /// TODO: assign a unique plugin.
        /// </summary>
        private static readonly int UniquePluginId = new FourCharacterCode("1234").ToInt32();
        /// <summary>
        /// TODO: assign a plugin name.
        /// </summary>
        private static readonly string PluginName = "MyPluginName";
        /// <summary>
        /// TODO: assign a product name.
        /// </summary>
        private static readonly string ProductName = "MyProduct";
        /// <summary>
        /// TODO: assign a vendor name.
        /// </summary>
        private static readonly string VendorName = "MyVendor";
        /// <summary>
        /// TODO: assign a plugin version.
        /// </summary>
        private static readonly int PluginVersion = 0000;

        /// <summary>
        /// Initializes the one an only instance of the Plugin root object.
        /// </summary>
        public Plugin()
            : base(PluginName, 
            new VstProductInfo(ProductName, VendorName, PluginVersion),
                // TODO: what type of plugin are your making?
                VstPluginCategory.Effect,
                VstPluginCapabilities.NoSoundInStop,
                // initial delay: number of samples your plugin lags behind.
                0, 
                UniquePluginId)
        { }
    }
}
