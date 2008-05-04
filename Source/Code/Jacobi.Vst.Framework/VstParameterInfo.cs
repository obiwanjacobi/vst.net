using System;
namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// Contains meta info for a parameter.
    /// </summary>
    public class VstParameterInfo
    {
        public const int MaxParameterLabelLength = 63;
        public const int MaxParameterShortLabelLength = 7;
        

        public bool CanBeAutomated
        { get; set; }

        public bool IsSwitch
        { get; set; }
        public bool CanRamp
        { get; set; }

        private string _label;
        public string Label 
        {
            get { return _label; }
            set
            {
                if (value != null && value.Length > MaxParameterLabelLength)
                {
                    throw new ArgumentException("Label too long.", "Label");
                }

                _label = value;
            }
        }

        private string _shortLabel;
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                if (value != null && value.Length > MaxParameterShortLabelLength)
                {
                    throw new ArgumentException("ShortLabel too long.", "Label");
                }

                _shortLabel = value;
            }
        }

        public bool MinMaxIntegerIsValid
        {
            get { return (MinInteger != 0 || MaxInteger != 0); }
        }
        public int MinInteger
        { get; set; }
        public int MaxInteger
        { get; set; }

        public bool StepIntegerIsValid
        {
            get { return (StepInteger != 0 && LargeStepInteger != 0); }
        }
        public int StepInteger
        { get; set; }
        public int LargeStepInteger
        { get; set; }

        public bool StepFloatIsValid
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
