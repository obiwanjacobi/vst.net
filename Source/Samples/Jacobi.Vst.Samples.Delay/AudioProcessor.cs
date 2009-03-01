namespace Jacobi.Vst.Samples.Delay
{
    using System;
    
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;
    using Jacobi.Vst.Framework.Plugin;

    /// <summary>
    /// This class manages the plugin audio processing.
    /// </summary>
    internal class AudioProcessor : VstPluginAudioProcessorBase
    {
        private FxTestPlugin _plugin;
        private Delay _delay;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(FxTestPlugin plugin)
            : base(1, 1, 0)
        {
            _plugin = plugin;
            _delay = new Delay();
        }

        /// <summary>
        /// Gets the Delay effect.
        /// </summary>
        public Delay Delay { get { return _delay; } }

        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        /// <remarks>This property is a proxy for the <see cref="T:Jacobi.Vst.Samples.Delay.Delay.SampleRate"/> property.</remarks>
        public override double SampleRate
        {
            get { return (Double)_delay.SampleRate; }
            set { _delay.SampleRate = (Single)value; }
        }

        /// <summary>
        /// Perform audio processing on the specified <paramref name="inChannels"/> 
        /// and produce a delay effect on the <paramref name="outChannels"/>.
        /// </summary>
        /// <param name="inChannels">The audio input buffers.</param>
        /// <param name="outChannels">The audio output buffers.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            VstAudioBuffer audioChannel = outChannels[0];

            for (int n = 0; n < audioChannel.SampleCount; n++)
            {
                audioChannel[n] = Delay.ProcessSample(inChannels[0][n]);
            }
        }
    }
}
