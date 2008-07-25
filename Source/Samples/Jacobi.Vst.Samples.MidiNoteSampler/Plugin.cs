namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    internal class Plugin : VstPluginWithInterfaceManagerBase
    {
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

        public SampleManager SampleManager { get; private set; }

        protected override IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            if (instance == null) return new AudioProcessor(this);

            return base.CreateAudioProcessor(instance);
        }

        protected override IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            if (instance == null) return new MidiProcessor(this);

            return base.CreateMidiProcessor(instance);
        }
    }
}
