using Jacobi.Vst.Plugin.Framework;
using System;
using System.ComponentModel;

namespace VstNetAudioPlugin.Dsp
{
    /// <summary>
    /// This is an example of a Digital Sound Processing component you could have in your plugin.
    /// </summary>
    internal sealed class Delay
    {
        private const string ParameterCategoryName = "Delay";

        private float[] _delayBuffer;
        private int _bufferIndex;
        private int _bufferLength;

        private readonly Plugin _plugin;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Delay(Plugin plugin)
        {
            _plugin = plugin;

            InitializeParameters();

            // when the delay time parameter value changes, we like to know about it.
            DelayTimeMgr.PropertyChanged += new PropertyChangedEventHandler(DelayTimeMgr_PropertyChanged);

            _plugin.Opened += new EventHandler(Plugin_Opened);
        }

        private void Plugin_Opened(object sender, System.EventArgs e)
        {
            // A reference to the host is only available after 
            // the plugin has been loaded and opened by the host.
            var hostAutomation = _plugin.Host.GetInstance<IVstHostAutomation>();

            DelayTimeMgr.HostAutomation = hostAutomation;
            FeedbackMgr.HostAutomation = hostAutomation;
            DryLevelMgr.HostAutomation = hostAutomation;
            WetLevelMgr.HostAutomation = hostAutomation;

            _plugin.Opened -= new EventHandler(Plugin_Opened);
        }

        internal VstParameterManager DelayTimeMgr { get; private set; }
        internal VstParameterManager FeedbackMgr { get; private set; }
        internal VstParameterManager DryLevelMgr { get; private set; }
        internal VstParameterManager WetLevelMgr { get; private set; }

        // This method initializes the plugin parameters this Dsp component owns.
        private void InitializeParameters()
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = _plugin.PluginPrograms.ParameterInfos;

            // retrieve the category for all delay parameters.
            VstParameterCategory paramCategory =
                _plugin.PluginPrograms.GetParameterCategory(ParameterCategoryName);

            // delay time parameter
            VstParameterInfo paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Time",
                Label = "MilSecs",
                ShortLabel = "ms",
                MinInteger = 0,
                MaxInteger = 1000,
                LargeStepFloat = 100.0f,
                SmallStepFloat = 1.0f,
                StepFloat = 10.0f,
                DefaultValue = 200f
            };
            DelayTimeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            // feedback parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Feedbck",
                Label = "Factor",
                ShortLabel = "*",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.2f
            };
            FeedbackMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            // dry Level parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Dry Lvl",
                Label = "Decibel",
                ShortLabel = "Db",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.8f
            };
            DryLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            // wet Level parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = true,
                Name = "Wet Lvl",
                Label = "Decibel",
                ShortLabel = "Db",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.4f
            };
            WetLevelMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);
        }

        private void DelayTimeMgr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentValue")
            {
                VstParameterManager paramMgr = (VstParameterManager)sender;
                _bufferLength = (int)(paramMgr.CurrentValue * _sampleRate / 1000);
            }
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
                int bufferLength = (int)(DelayTimeMgr.ParameterInfo.MaxInteger * _sampleRate / 1000);
                _delayBuffer = new float[bufferLength];

                _bufferLength = (int)(DelayTimeMgr.CurrentValue * _sampleRate / 1000);
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
            float output = (DryLevelMgr.CurrentValue * sample) + (WetLevelMgr.CurrentValue * _delayBuffer[_bufferIndex]);

            // process delay buffer
            _delayBuffer[_bufferIndex] = sample + (FeedbackMgr.CurrentValue * _delayBuffer[_bufferIndex]);

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
