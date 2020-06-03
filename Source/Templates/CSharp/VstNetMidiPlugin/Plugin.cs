using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using Jacobi.Vst.Plugin.Framework.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// The Plugin root object.
    /// </summary>
    internal sealed class Plugin : VstPluginWithServices
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
        /// TODO: what type of plugin are your making?
        /// </summary>
        private const VstPluginCategory PluginCategory = VstPluginCategory.Effect;
        /// <summary>
        /// TODO: what can your plugin do?
        /// </summary>
        private const VstPluginCapabilities PluginCapabilities = VstPluginCapabilities.NoSoundInStop;

        /// <summary>
        /// Initializes the one an only instance of the Plugin root object.
        /// </summary>
        public Plugin()
            : base(PluginName,
            new VstProductInfo(ProductName, VendorName, PluginVersion),
                PluginCategory,
                PluginCapabilities,
                // initial delay: number of samples your plugin lags behind.
                0,
                UniquePluginId)
        { }

        /// <summary>
        /// Gets the audio processor object.
        /// </summary>
        public DummyAudioProcessor AudioProcessor
        {
            get { return GetInstance<DummyAudioProcessor>(); }
        }

        /// <summary>
        /// Gets the midi processor object.
        /// </summary>
        public MidiProcessor MidiProcessor
        {
            get { return GetInstance<MidiProcessor>(); }
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
        /// Called once to get all the plugin components.
        /// Add components for the IVstXxxx interfaces you want to support.
        /// </summary>
        /// <param name="services">Is never null.</param>
        protected override void RegisterServices(IServiceCollection services)
        {
            services.AddPluginComponent(new DummyAudioProcessor(this));
            services.AddPluginComponent(new MidiProcessor(this));
            services.AddPluginComponent(new PluginEditor(this));
            services.AddPluginComponent(new PluginPrograms(this));
        }
    }
}
