namespace Jacobi.Vst.Samples.MidiNoteSampler
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Plugin;
    using System;

    /// <summary>
    /// Implements the audio processing of the plugin using the <see cref="SampleManager"/>.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessor
    {
        private readonly SampleManager _sampleManager;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(SampleManager sampleManager)
            : base(2, 2, 0, noSoundInStop: true)
        {
            _sampleManager = sampleManager ?? throw new ArgumentNullException(nameof(sampleManager));
        }

        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (_sampleManager.IsPlaying)
            {
                _sampleManager.PlayAudio(outChannels);
            }
            else // audio thru
            {
                base.Process(inChannels, outChannels);
            }

            if (_sampleManager.IsRecording)
            {
                _sampleManager.RecordAudio(inChannels);
            }
        }
    }
}
