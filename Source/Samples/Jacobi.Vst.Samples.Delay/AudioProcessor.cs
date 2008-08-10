namespace Jacobi.Vst.Samples.Delay
{
    using System;
    
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework;

    internal class AudioProcessor : IVstPluginAudioProcessor
    {
        private FxTestPlugin _plugin;
        private Delay _delay;

        public AudioProcessor(FxTestPlugin plugin)
        {
            _plugin = plugin;
            _delay = new Delay();
        }

        public Delay Delay
        { get { return _delay; } }

        #region IVstPluginAudioProcessor Members

        public int InputCount
        {
            get { return 1; }
        }

        public int OutputCount
        {
            get { return 1; }
        }

        public int TailSize
        {
            get { return 0; }
        }

        //private double _sampleRate;
        public double SampleRate
        {
            get { return (Double)_delay.SampleRate; }
            set { _delay.SampleRate = (Single)value; }
        }

        private int _blockSize;
        public int BlockSize
        {
            get { return _blockSize; }
            set { _blockSize = value; }
        }

        public void Process(VstAudioBuffer[] inChannels, VstAudioBuffer[] outChannels)
        {
            VstAudioBuffer audioChannel = outChannels[0];

            for (int n = 0; n < audioChannel.SampleCount; n++)
            {
                audioChannel[n] = Delay.ProcessSample(inChannels[0][n]);
            }
        }

        #endregion
    }
}
