namespace Jacobi.Vst.Samples.Delay
{
    using System;
    
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    /// <summary>
    /// This class manages the plugin audio processing.
    /// </summary>
    internal class AudioProcessor : IVstPluginAudioProcessor
    {
        private FxTestPlugin _plugin;
        private Delay _delay;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="plugin">Must not be null.</param>
        public AudioProcessor(FxTestPlugin plugin)
        {
            _plugin = plugin;
            _delay = new Delay();
        }

        /// <summary>
        /// Gets the Delay effect.
        /// </summary>
        public Delay Delay { get { return _delay; } }

        #region IVstPluginAudioProcessor Members

        /// <summary>
        /// Always returns 1.
        /// </summary>
        public int InputCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Always returns 1.
        /// </summary>
        public int OutputCount
        {
            get { return 1; }
        }

        /// <summary>
        /// Always returns 0.
        /// </summary>
        /// <remarks>A correct implementation should return the current delay time.</remarks>
        public int TailSize
        {
            get { return 0; }
        }

        //private double _sampleRate;
        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        /// <remarks>This property is a proxy for the <see cref="Delay.SampleRate"/> property.</remarks>
        public double SampleRate
        {
            get { return (Double)_delay.SampleRate; }
            set { _delay.SampleRate = (Single)value; }
        }

        private int _blockSize;
        /// <summary>
        /// Not used.
        /// </summary>
        public int BlockSize
        {
            get { return _blockSize; }
            set { _blockSize = value; }
        }

        /// <summary>
        /// Perform audio processing on the specified <paramref name="inChannels"/> 
        /// and produce a delay effect on the <paramref name="outChannels"/>.
        /// </summary>
        /// <param name="inChannels">The audio input buffers.</param>
        /// <param name="outChannels">The audio output buffers.</param>
        public void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            VstAudioBuffer audioChannel = outChannels[0];

            for (int n = 0; n < audioChannel.SampleCount; n++)
            {
                audioChannel[n] = Delay.ProcessSample(inChannels[0][n]);
            }
        }

        public bool SetPanLaw(VstPanLaw type, float gain)
        {
            return false;
        }
        #endregion
    }
}
