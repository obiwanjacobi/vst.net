using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using VstNetAudioPlugin.Dsp;

namespace VstNetAudioPlugin
{
    /// <summary>
    /// This object performs audio processing for your plugin.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessorBase, IVstPluginBypass
    {
        /// <summary>
        /// TODO: assign the input count.
        /// </summary>
        private static readonly int AudioInputCount = 2;
        /// <summary>
        /// TODO: assign the output count.
        /// </summary>
        private static readonly int AudioOutputCount = 2;
        /// <summary>
        /// TODO: assign the tail size.
        /// </summary>
        private static readonly int InitialTailSize = 0;

        private Delay _delay;
        private Plugin _plugin;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioProcessor(Plugin plugin)
            : base(AudioInputCount, AudioOutputCount, InitialTailSize)
        {
            _plugin = plugin;
            _delay = new Delay(plugin);
        }

        /// <summary>
        /// Override the default implementation to pass it through to the delay.
        /// </summary>
        public override float SampleRate
        {
            get { return _delay.SampleRate; }
            set { _delay.SampleRate = value; }
        }

        /// <summary>
        /// Called by the host to allow the plugin to process audio samples.
        /// </summary>
        /// <param name="inChannels">Never null.</param>
        /// <param name="outChannels">Never null.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (!Bypass)
            {
                // TODO: Implement your audio (effect) processing here.

                int outCount = outChannels.Length;

                for (int n = 0; n < outCount; n++)
                {
                    for (int i = 0; i < inChannels.Length && n < outCount; i++, n++)
                    {
                        Process(inChannels[i], outChannels[n]);
                    }
                }
            }
            else
            {
                // calling the base class transfers input samples to the output channels unchanged (bypass).
                base.Process(inChannels, outChannels);
            }
        }

        // process a single audio channel
        private void Process(VstAudioBuffer input, VstAudioBuffer output)
        {
            for (int i = 0; i < input.SampleCount; i++)
            {
                output[i] = _delay.ProcessSample(input[i]);
            }
        }

        #region IVstPluginBypass Members

        public bool Bypass { get; set; }

        #endregion
    }
}
