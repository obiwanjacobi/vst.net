namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstParameterInfo contains the meta information for a parameter.
    /// </summary>
    public class VstParameterInfo
    {
        private VstParameterManager _paramMgr;
        /// <summary>
        /// Gets or sets the <see cref="VstParameterManager"/> for this parameter type.
        /// </summary>
        /// <remarks>The <see cref="VstParameterManager.ParameterInfo"/> property must be assigned to <b>this</b> instance, 
        /// otherwise an <see cref="ArgumentException"/> is thrown.</remarks>
        /// <exception cref="ArgumentException">Thrown when the <see cref="VstParameterManager"/> instance that is set, 
        /// does not managed <b>this</b> parameter.</exception>
        public VstParameterManager ParameterManager
        {
            get { return _paramMgr; }
            set
            {
                if (value != null && value.ParameterInfo != this)
                {
                    throw new ArgumentException(Properties.Resources.VstParameterInfo_ParameterManagerNotLinked, "ParameterManager");
                }

                _paramMgr = value;
            }
        }

        /// <summary>
        /// Gets or sets the parameter category.
        /// </summary>
        public VstParameterCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the default value the <see cref="VstParameter"/> is initialized with.
        /// </summary>
        public float DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets an indication wheter the parameter can be automated by the host (true).
        /// </summary>
        public bool CanBeAutomated { get; set; }

        /// <summary>
        /// Gets or sets an indication wheter the parameter is a switch (true).
        /// </summary>
        public bool IsSwitch { get; set; }

        /// <summary>
        /// Gets or sets an indication wheter the parameter can ramp (true).
        /// </summary>
        public bool CanRamp { get; set; }

        private string _name;
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <remarks>The Name cannot exceed 7 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 7 characters.</exception>
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
        /// <summary>
        /// Gets or sets the label of the parameter.
        /// </summary>
        /// <remarks>The Label cannot exceed 63 characters</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 63 characters.</exception>
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
        /// <summary>
        /// Gets or sets the short label of the parameter.
        /// </summary>
        /// <remarks>The ShortLabel cannot exceed 7 characters</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 7 characters.</exception>
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

        /// <summary>
        /// Gets an indication whether the <see cref="MinInteger"/> and 
        /// <see cref="MaxInteger"/> properties are filled.
        /// </summary>
        public bool IsMinMaxIntegerValid
        {
            get { return (MinInteger != 0 || MaxInteger != 0); }
        }

        /// <summary>
        /// Gets or sets the minimal value of the parameter.
        /// </summary>
        public int MinInteger { get; set; }

        /// <summary>
        /// Gets or sets the maximal value of the parameter.
        /// </summary>
        public int MaxInteger { get; set; }

        /// <summary>
        /// Gets an indication whether the <see cref="StepInteger"/> and 
        /// <see cref="LargeStepInteger"/> properties are filled.
        /// </summary>
        public bool IsStepIntegerValid
        {
            get { return (StepInteger != 0 && LargeStepInteger != 0); }
        }
        
        /// <summary>
        /// Gets or sets the steps the parameter value will take.
        /// </summary>
        public int StepInteger { get; set; }

        /// <summary>
        /// Gets or sets the large steps the parameter value will take.
        /// </summary>
        public int LargeStepInteger { get; set; }

        /// <summary>
        /// Gets an indication whether the <see cref="StepFloat"/>, <see cref="SmallStepFloat"/> 
        /// and <see cref="LargeStepFloat"/> properties are set.
        /// </summary>
        public bool IsStepFloatValid
        {
            get { return (StepFloat != 0.0f && SmallStepFloat != 0.0f && LargeStepFloat != 0.0f); }
        }

        /// <summary>
        /// Gets or sets the steps the parameter value will take.
        /// </summary>
        public float StepFloat { get; set; }

        /// <summary>
        /// Gets or sets the small steps the parameter value will take.
        /// </summary>
        public float SmallStepFloat { get; set; }

        /// <summary>
        /// Gets or sets the large steps the parameter value will take.
        /// </summary>
        public float LargeStepFloat { get; set; }
    }
}
