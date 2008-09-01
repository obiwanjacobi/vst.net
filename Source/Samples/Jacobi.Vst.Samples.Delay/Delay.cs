namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Framework;

    /// <summary>
    /// This class implements a DPS routine that acts as a delay.
    /// </summary>
    internal class Delay
    {
        private float[] _delayBuffer;
        private int _bufferIndex;
        private int _bufferLength;

        private VstParameterManager _delayTimeMgr;
        private VstParameterManager _feedbackMgr;
        private VstParameterManager _dryLevelMgr;
        private VstParameterManager _wetLevelMgr;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Delay()
        {
            _paramInfos = new VstParameterInfoCollection();

            #region Initialize Parameters

            // delay time parameter
            VstParameterInfo paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "dt";
            paramInfo.Label = "Delay Time";
            paramInfo.ShortLabel = "T-Dly:";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 1000;
            paramInfo.LargeStepFloat = 100.0f;
            paramInfo.SmallStepFloat = 1.0f;
            paramInfo.StepFloat = 10.0f;
            paramInfo.DefaultValue = 200f;
            _delayTimeMgr = paramInfo.ParameterManager = new VstParameterManager(paramInfo);

            _paramInfos.Add(paramInfo);

            // feedback parameter
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "fb";
            paramInfo.Label = "Feedback";
            paramInfo.ShortLabel = "Feedbk:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.2f;
            _feedbackMgr = paramInfo.ParameterManager = new VstParameterManager(paramInfo);

            _paramInfos.Add(paramInfo);

            // dry Level parameter
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "dl";
            paramInfo.Label = "Dry Level";
            paramInfo.ShortLabel = "DryLvl:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.8f;
            _dryLevelMgr = paramInfo.ParameterManager = new VstParameterManager(paramInfo);

            _paramInfos.Add(paramInfo);

            // wet Level parameter
            paramInfo = new VstParameterInfo();
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "wl";
            paramInfo.Label = "Wet Level";
            paramInfo.ShortLabel = "WetLvl:";
            paramInfo.LargeStepFloat = 0.1f;
            paramInfo.SmallStepFloat = 0.01f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 0.4f;
            _wetLevelMgr = paramInfo.ParameterManager = new VstParameterManager(paramInfo);

            _paramInfos.Add(paramInfo);

            #endregion

            _delayTimeMgr.ValueChanged += new System.EventHandler<System.EventArgs>(_delayTimeMgr_ValueChanged);
        }

        private void _delayTimeMgr_ValueChanged(object sender, System.EventArgs e)
        {
            VstParameterManager paramMgr = (VstParameterManager)sender;
            _bufferLength = (int)(paramMgr.CurrentValue * _sampleRate / 1000);
        }

        private VstParameterInfoCollection _paramInfos;
        /// <summary>
        /// Gets the Parameter definitions that originate here.
        /// </summary>
        public VstParameterInfoCollection ParameterInfos
        {
            get { return _paramInfos; }
        }

        private float _sampleRate;
        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        public float SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;

                // allocate buffer for max delay time
                int bufferLength = (int)(_delayTimeMgr.ParameterInfo.MaxInteger * _sampleRate / 1000);
                _delayBuffer = new float[bufferLength];
            }
        }

        /// <summary>
        /// Processes the <paramref name="sample"/> using a delay effect.
        /// </summary>
        /// <param name="sample">A single sample.</param>
        /// <returns>Returns the new value for the sample.</returns>
        public float ProcessSample(float sample)
        {
            if (_delayBuffer == null) return sample;

            // process output
            float output = (_dryLevelMgr.CurrentValue * sample) + (_wetLevelMgr.CurrentValue * _delayBuffer[_bufferIndex]);

            // process delay buffer
            _delayBuffer[_bufferIndex] = sample + (_feedbackMgr.CurrentValue * _delayBuffer[_bufferIndex]);

            _bufferIndex++;

            // manage current buffer position
            if (_bufferIndex >= _bufferLength)
            {
                _bufferIndex = 0;
            }

            return output;
        }

    }
}
