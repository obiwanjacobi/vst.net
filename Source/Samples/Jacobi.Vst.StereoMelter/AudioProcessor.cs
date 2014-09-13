using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;
using Jacobi.Vst.StereoMelter.Dsp;

namespace Jacobi.Vst.StereoMelter
{
    /// <summary>
    /// This object performs audio processing for your plugin.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessorBase, IVstPluginBypass
    {
        /// <summary>Stereo input.</summary>
        private const int AudioInputCount = 2;
        /// <summary>Stereo output.</summary>
        private const int AudioOutputCount = 2;
        /// <summary>No tail.</summary>
        private const int InitialTailSize = 0;

        private Plugin _plugin;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioProcessor(Plugin plugin)
            : base(AudioInputCount, AudioOutputCount, InitialTailSize)
        {
            _plugin = plugin;

            Melter = new Melter(plugin);
        }

        internal Melter Melter { get; private set; }

        /// <summary>
        /// Called by the host to allow the plugin to process audio samples.
        /// </summary>
        /// <param name="inChannels">Never null.</param>
        /// <param name="outChannels">Never null.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            if (!Bypass)
            {
                var inputLeft = inChannels[0];
                var inputRight = inChannels[1];
                var outputLeft = outChannels[0];
                var outputRight = outChannels[1];

                for (int i = 0; i < inputLeft.SampleCount; i++)
                {
                    float left;
                    float right;

                    // process left and right stereo sample at the same time.
                    Melter.Process(inputLeft[i], inputRight[i], out left, out right);

                    outputLeft[i] = left;
                    outputRight[i] = right;
                }
            }
            else
            {
                // calling the base class transfers input samples to the output channels unchanged (bypass).
                base.Process(inChannels, outChannels);
            }
        }

        #region IVstPluginBypass Members

        public bool Bypass { get; set; }

        #endregion
    }
}
