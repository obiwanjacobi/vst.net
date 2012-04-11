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
        private IVstHostSequencer _sequencer;
        private VstTimeInfoFlags _defaultTimeInfoFlags;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AudioProcessor(Plugin plugin)
            : base(AudioInputCount, AudioOutputCount, InitialTailSize)
        {
            _plugin = plugin;

            // TODO: We use one delay object to process two audio channels.
            // Typically you would use dedicated DSP objects for each channel.
            Delay = new Delay(plugin);

            // TODO: change this to your specific needs.
            _defaultTimeInfoFlags = VstTimeInfoFlags.ClockValid;
        }

        internal Delay Delay { get; private set; }

        /// <summary>
        /// Override the default implementation to pass it through to the delay.
        /// </summary>
        public override float SampleRate
        {
            get { return Delay.SampleRate; }
            set { Delay.SampleRate = value; }
        }

        private VstTimeInfo _timeInfo;
        /// <summary>
        /// Gets the current time info.
        /// </summary>
        /// <remarks>The Time Info is refreshed with each call to Process.</remarks>
        protected VstTimeInfo TimeInfo
        {
            get
            {
                if (_sequencer == null && _plugin.Host != null)
                {
                    _sequencer = _plugin.Host.GetInstance<IVstHostSequencer>();
                }

                if (_timeInfo == null && _sequencer != null)
                {
                    _timeInfo = _sequencer.GetTime(_defaultTimeInfoFlags);
                }

                return _timeInfo;
            }
        }

        /// <summary>
        /// Called by the host to allow the plugin to process audio samples.
        /// </summary>
        /// <param name="inChannels">Never null.</param>
        /// <param name="outChannels">Never null.</param>
        public override void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            // by resetting the time info each cycle, accessing the TimeInfo property will fetch new info.
            _timeInfo = null;

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
                output[i] = Delay.ProcessSample(input[i]);
            }
        }

        #region IVstPluginBypass Members

        public bool Bypass { get; set; }

        #endregion
    }
}
