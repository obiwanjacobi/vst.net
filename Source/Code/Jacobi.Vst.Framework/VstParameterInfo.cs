using System;
namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Contains meta info for a parameter.
    /// </summary>
    public class VstParameterInfo
    {
        private VstParameterManager _paramMgr;
        public VstParameterManager ParameterManager
        {
            get { return _paramMgr; }
            set
            {
                if (value != null && value.ParameterInfo != this)
                {
                    throw new ArgumentException("Assigned VstParameterManager value is not linked to this VstParamaterInfo instance.", "ParameterManager");
                }

                _paramMgr = value;
            }
        }

        public float DefaultValue
        { get; set; }

        public bool CanBeAutomated
        { get; set; }

        public bool IsSwitch
        { get; set; }
        public bool CanRamp
        { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, "Name");

                _name = value;
            }
        }

        private string _label;
        public string Label 
        {
            get { return _label; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, "Label");

                _label = value;
            }
        }

        private string _shortLabel;
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

        public bool IsMinMaxIntegerValid
        {
            get { return (MinInteger != 0 || MaxInteger != 0); }
        }
        public int MinInteger
        { get; set; }
        public int MaxInteger
        { get; set; }

        public bool IsStepIntegerValid
        {
            get { return (StepInteger != 0 && LargeStepInteger != 0); }
        }
        public int StepInteger
        { get; set; }
        public int LargeStepInteger
        { get; set; }

        public bool IsStepFloatValid
        {
            get { return (StepFloat != 0.0f && SmallStepFloat != 0.0f && LargeStepFloat != 0.0f); }
        }
        public float StepFloat
        { get; set; }
        public float SmallStepFloat
        { get; set; }
        public float LargeStepFloat
        { get; set; }
    }
}
