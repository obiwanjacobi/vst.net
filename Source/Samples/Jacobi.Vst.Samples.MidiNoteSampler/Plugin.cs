namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// The Plugin root class that derives from the framework provided base class that also include the interface manager.
    /// </summary>
    internal class Plugin : VstPluginWithInterfaceManagerBase
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Plugin()
            : base("VST.NET Midi Note Sampler Plugin", 
                new VstProductInfo("VST.NET Code Samples", "Jacobi Software (c) 2008", 1000),
                VstPluginCategory.Synth, 
                VstPluginCapabilities.NoSoundInStop, 
                0, 
                36373435)
        {
            SampleManager = new SampleManager();
        }

        /// <summary>
        /// Gets the sample manager.
        /// </summary>
        public SampleManager SampleManager { get; private set; }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new AudioProcessor(this);

            return base.CreateAudioProcessor(instance);
        }

        /// <summary>
        /// Creates a default instance and reuses that for all threads.
        /// </summary>
        /// <param name="instance">A reference to the default instance or null.</param>
        /// <returns>Returns the default instance.</returns>
        protected override IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            if (instance == null) return new MidiProcessor(this);

            return base.CreateMidiProcessor(instance);
        }
    }
}
