namespace Jacobi.Vst.Framework.TestPlugin
{
    class AudioProcessor : IVstPluginAudioProcessor
    {
        private FxTestPlugin _plugin;

        public AudioProcessor(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

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

        private double _sampleRate;
        public double SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }

        private int _blockSize;
        public int BlockSize
        {
            get { return _blockSize; }
            set { _blockSize = value; }
        }

        public void Process(VstAudioChannel[] inChannels, VstAudioChannel[] outChannels)
        {
            // audio pass thru
            foreach (VstAudioChannel audioChannel in outChannels)
            {
                for (int n = 0; n < audioChannel.SampleCount; n++)
                {
                    audioChannel[n] = inChannels[0][n];
                }
            }
        }

        #endregion
    }
}
