using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VstNetMidiPlugin
{
    /// <summary>
    /// The Plugin root object.
    /// </summary>
    internal sealed class Plugin : VstPluginWithInterfaceManagerBase
    {
        /// <summary>
        /// TODO: assign a unique plugin.
        /// </summary>
        private const int UniquePluginId = new FourCharacterCode("1234").ToInt32();
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
                VstPluginCapabilities.NoSoundInStop,
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
        /// Implement this when you do audio processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            // Dont expose an AudioProcessor if Midi is output in the MidiProcessor
            if (!MidiProcessor.SyncWithAudioProcessor) return null;

            if (instance == null)
            {
                return new DummyAudioProcessor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateAudioProcessor(instance);
        }

        /// <summary>
        /// Implement this when you do midi processing.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            if (instance == null)
            {
                return new MidiProcessor(this);
            }

            // TODO: implement a thread-safe wrapper.
            return base.CreateMidiProcessor(instance);
        }

        /// <summary>
        /// Implement this when you output midi events to the host.
        /// </summary>
        /// <param name="instance">A previous instance returned by this method. 
        /// When non-null, return a thread-safe version (or wrapper) for the object.</param>
        /// <returns>Returns null when not supported by the plugin.</returns>
        protected override IVstPluginMidiSource CreateMidiSource(IVstPluginMidiSource instance)
        {
            // we implement this interface on out midi processor.
            return (IVstPluginMidiSource)MidiProcessor;
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
