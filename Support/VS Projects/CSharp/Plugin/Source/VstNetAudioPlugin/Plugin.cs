using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VstNetAudioPlugin
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
        private const string PluginName = "MyPluginName";
        /// <summary>
        /// TODO: assign a product name.
        /// </summary>
        private const string ProductName = "MyProduct";
        /// <summary>
        /// TODO: assign a vendor name.
        /// </summary>
        private const string VendorName = "MyVendor";
        /// <summary>
        /// TODO: assign a plugin version.
        /// </summary>
        private const int PluginVersion = 0000;

        /// <summary>
        /// Initializes the one an only instance of the Plugin root object.
        /// </summary>
        public Plugin()
            : base(PluginName, 
            new VstProductInfo(ProductName, VendorName, PluginVersion),
                // TODO: what type of plugin are your making?
                VstPluginCategory.Effect,
                VstPluginCapabilities.NoSoundInStop | VstPluginCapabilities.ReceiveTimeInfo,
                // initial delay: number of samples your plugin lags behind.
                0, 
                UniquePluginId)
        { }

        /// <summary>
        /// Gets the audio processor object.
        /// </summary>
        public AudioProcessor AudioProcessor
        {
            get { return GetInstance<AudioProcessor>(); }
        }

        /// <summary>
        /// Gets the plugin editor object.
        /// </summary>
        public PluginEditor PluginEditor
        {
            get { return GetInstance<PluginEditor>(); }
        }

        /// <summary>
        /// Gets the plugin programs object.
        /// </summary>
        public PluginPrograms PluginPrograms
        {
            get { return GetInstance<PluginPrograms>(); }
        }

        /// <summary>
        /// Implement this when you do audio processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null)
            {
                return new AudioProcessor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateAudioProcessor(instance);
        }

        /// <summary>
        /// Implement this when you need a custom editor (UI).
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginEditor CreateEditor(IVstPluginEditor instance)
        {
            if (instance == null)
            {
                return new PluginEditor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateEditor(instance);
        }

        /// <summary>
        /// Implement this when you support bypassing your audio processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginBypass CreateBypass(IVstPluginBypass instance)
        {
            // we implement the bypass interface on our audio processor object.
            return (IVstPluginBypass)AudioProcessor;
        }

        /// <summary>
        /// Implement this when you implement plugin programs or presets.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            if (instance == null)
            {
                return new PluginPrograms(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreatePrograms(instance);
        }
    }
}
