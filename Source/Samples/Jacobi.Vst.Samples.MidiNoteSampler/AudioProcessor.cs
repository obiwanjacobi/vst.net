namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Plugin;

    /// <summary>
    /// Implements the audio processing of the plugin using the <see cref="SampleManager"/>.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessor
    {
        private readonly Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(Plugin plugin)
            : base(2, 2, 0, noSoundInStop: true)
        {
            _plugin = plugin;
        }

        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (_plugin.SampleManager.IsPlaying)
            {
                _plugin.SampleManager.PlayAudio(outChannels);
            }
            else // audio thru
            {
                base.Process(inChannels, outChannels);
            }

            if (_plugin.SampleManager.IsRecording)
            {
                _plugin.SampleManager.RecordAudio(inChannels);
            }
        }
    }
}
