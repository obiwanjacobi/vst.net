namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Plugin;

    /// <summary>
    /// Implements the audio processing of the plugin using the <see cref="SampleManager"/>.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessorBase
    {
        private readonly Plugin _plugin;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(Plugin plugin)
            : base(2, 2, 0)
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
                VstAudioBuffer input = inChannels[0];
                VstAudioBuffer output = outChannels[0];

                for (int index = 0; index < output.SampleCount; index++)
                {
                    output[index] = input[index];
                }

                input = inChannels[1];
                output = outChannels[1];

                for (int index = 0; index < output.SampleCount; index++)
                {
                    output[index] = input[index];
                }
            }

            if (_plugin.SampleManager.IsRecording)
            {
                _plugin.SampleManager.RecordAudio(inChannels);
            }
        }
    }
}
