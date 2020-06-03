namespace Jacobi.Vst.Samples.Delay
{
    using Jacobi.Vst.Plugin.Framework;
    using System.ComponentModel;

    /// <summary>
    /// This class implements a DPS routine that acts as a delay.
    /// </summary>
    internal sealed class Delay
    {
        private float[] _delayBuffer;
        private int _bufferIndex;
        private int _bufferLength;

        private readonly VstParameterManager _delayTimeMgr;
        private readonly VstParameterManager _feedbackMgr;
        private readonly VstParameterManager _dryLevelMgr;
        private readonly VstParameterManager _wetLevelMgr;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Delay()
        {
            _paramInfos = new VstParameterInfoCollection();

            #region Initialize Parameters

            // delay time parameter
            VstParameterInfo paramInfo = new VstParameterInfo
            {
                CanBeAutomated = true,
                Name = "dt",
                Label = "Delay Time",
                ShortLabel = "T-Dly:",
                MinInteger = 0,
                MaxInteger = 1000,
                LargeStepFloat = 100.0f,
                SmallStepFloat = 1.0f,
                StepFloat = 10.0f,
                DefaultValue = 200f
            };
            _delayTimeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // feedback parameter
            paramInfo = new VstParameterInfo
            {
                CanBeAutomated = true,
                Name = "fb",
                Label = "Feedback",
                ShortLabel = "Feedbk:",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.2f
            };
            _feedbackMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // dry Level parameter
            paramInfo = new VstParameterInfo
            {
                CanBeAutomated = true,
                Name = "dl",
                Label = "Dry Level",
                ShortLabel = "DryLvl:",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.8f
            };
            _dryLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            // wet Level parameter
            paramInfo = new VstParameterInfo
            {
                CanBeAutomated = true,
                Name = "wl",
                Label = "Wet Level",
                ShortLabel = "WetLvl:",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.4f
            };
            _wetLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            _paramInfos.Add(paramInfo);

            #endregion

            _delayTimeMgr.PropertyChanged += new PropertyChangedEventHandler(_delayTimeMgr_PropertyChanged);
        }

        private void _delayTimeMgr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentValue")
            {
                VstParameterManager paramMgr = (VstParameterManager)sender;
                _bufferLength = (int)(paramMgr.CurrentValue * _sampleRate / 1000);
            }
        }

        private readonly VstParameterInfoCollection _paramInfos;
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

                _bufferLength = (int)(_delayTimeMgr.CurrentValue * _sampleRate / 1000);
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
