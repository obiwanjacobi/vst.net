using Jacobi.Vst.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jacobi.Vst.StereoMelter.Dsp
{
    internal class Melter
    {
        private const string ParameterCategoryName = "Effect";

        private Plugin _plugin;

        public Melter(Plugin plugin)
        {
            _plugin = plugin;

            InitializeParameters();

            _plugin.Opened += new EventHandler(Plugin_Opened);
        }

        private void Plugin_Opened(object sender, System.EventArgs e)
        {
            var hostAutomation = _plugin.Host.GetInstance<IVstHostAutomation>();

            InputTapMgr.HostAutomation = hostAutomation;
            LeftVolumeMgr.HostAutomation = hostAutomation;
            RightVolumeMgr.HostAutomation = hostAutomation;
            InverterMgr.HostAutomation = hostAutomation;
            OutputBalanceMgr.HostAutomation = hostAutomation;

            _plugin.Opened -= new EventHandler(Plugin_Opened);
        }

        internal VstParameterManager InputTapMgr { get; private set; }
        internal VstParameterManager LeftVolumeMgr { get; private set; }
        internal VstParameterManager RightVolumeMgr { get; private set; }
        internal VstParameterManager InverterMgr { get; private set; }
        internal VstParameterManager OutputBalanceMgr { get; private set; }

        private void InitializeParameters()
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = _plugin.PluginPrograms.ParameterInfos;

            // retrieve the category for all melter parameters.
            VstParameterCategory paramCategory =
                _plugin.PluginPrograms.GetParameterCategory(ParameterCategoryName);

            var paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "Tap";
            paramInfo.Label = "Tap";
            paramInfo.ShortLabel = "|";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 2;
            paramInfo.LargeStepFloat = 1.0f;
            paramInfo.SmallStepFloat = 1.0f;
            paramInfo.StepFloat = 1.0f;
            paramInfo.CanRamp = true;
            paramInfo.DefaultValue = 0f;
            InputTapMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "LeftGain";
            paramInfo.Label = "LftGn";
            paramInfo.ShortLabel = "%";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 1;
            paramInfo.LargeStepFloat = 0.10f;
            paramInfo.SmallStepFloat = 0.03f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 1.0f;
            LeftVolumeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "RghtGain";
            paramInfo.Label = "RghtGn";
            paramInfo.ShortLabel = "%";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 1;
            paramInfo.LargeStepFloat = 0.10f;
            paramInfo.SmallStepFloat = 0.03f;
            paramInfo.StepFloat = 0.05f;
            paramInfo.DefaultValue = 1.0f;
            RightVolumeMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "Invert";
            paramInfo.Label = "Invert";
            paramInfo.ShortLabel = "!";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 1;
            paramInfo.LargeStepFloat = 1f;
            paramInfo.SmallStepFloat = 1f;
            paramInfo.StepFloat = 1f;
            paramInfo.IsSwitch = true;
            paramInfo.DefaultValue = 0f;
            this.InverterMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);

            paramInfo = new VstParameterInfo();
            paramInfo.Category = paramCategory;
            paramInfo.CanBeAutomated = true;
            paramInfo.Name = "Balance";
            paramInfo.Label = "Balance";
            paramInfo.ShortLabel = "Bal";
            paramInfo.MinInteger = 0;
            paramInfo.MaxInteger = 240;
            paramInfo.LargeStepFloat = 10f;
            paramInfo.SmallStepFloat = 3f;
            paramInfo.StepFloat = 5f;
            paramInfo.DefaultValue = 120f;
            this.OutputBalanceMgr = new VstParameterManager(paramInfo);
            VstParameterNormalizationInfo.AttachTo(paramInfo);

            parameterInfos.Add(paramInfo);
        }

        public void Process(float left, float right, out float outLeft, out float outRight)
        {
            var inputTap = this.InputTapMgr.CurrentValue;
            var leftVolume = this.LeftVolumeMgr.CurrentValue;
            var rightVolume = this.RightVolumeMgr.CurrentValue;
            var inverter = this.InverterMgr.CurrentValue;
            var balance = this.OutputBalanceMgr.CurrentValue;

            outLeft = left * leftVolume;
            outRight = right * rightVolume;


        }
    }
}
