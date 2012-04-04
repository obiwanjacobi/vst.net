using Jacobi.Vst.Core;
using Jacobi.Vst.Framework;
using Jacobi.Vst.Framework.Plugin;

namespace VstNetItemTemplates
{
    /// <summary>
    /// This object performs audio processing for your plugin.
    /// </summary>
    internal sealed class AudioProcessor : VstPluginAudioProcessorBase
    {
        /// <summary>
        /// TODO: assign the input count.
        /// </summary>
        private const int AudioInputCount = 2;
        /// <summary>
        /// TODO: assign the output count.
        /// </summary>
        private const int AudioOutputCount = 2;
        /// <summary>
        /// TODO: assign the tail size.
        /// </summary>
        private const int InitialTailSize = 0;

        private Plugin _plugin;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioProcessor(Plugin plugin)
            : base(AudioInputCount, AudioOutputCount, InitialTailSize)
        {
            _plugin = plugin;
        }

        /// <summary>
        /// Called by the host to allow the plugin to process audio samples.
        /// </summary>
        /// <param name="inChannels">Never null.</param>
        /// <param name="outChannels">Never null.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            // calling the base class transfers input samples to the output channels unchanged (bypass).
            base.Process(inChannels, outChannels);
        }
    }
}
