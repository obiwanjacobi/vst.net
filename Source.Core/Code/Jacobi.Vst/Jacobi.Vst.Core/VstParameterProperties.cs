namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Used to communicate properties of a plugin parameter to the host.
    /// </summary>
    public class VstParameterProperties
    {
        /// <summary>
        /// Flags that are approproate for the parameter.
        /// </summary>
        public VstParameterPropertiesFlags Flags { get; set; }

        /// <summary>
        /// The value step for the parameter.
        /// </summary>
        public float StepFloat { get; set; }

        /// <summary>
        /// The small value step for the parameter.
        /// </summary>
        public float SmallStepFloat { get; set; }

        /// <summary>
        /// The large value step for the parameter.
        /// </summary>
        public float LargeStepFloat { get; set; }

        /// <summary>
        /// The minimal value for the parameter.
        /// </summary>
        public int MinInteger { get; set; }

        /// <summary>
        /// The maximal value for the parameter.
        /// </summary>
        public int MaxInteger { get; set; }

        /// <summary>
        /// The value step for the parameter.
        /// </summary>
        public int StepInteger { get; set; }

        /// <summary>
        /// The large value step for the parameter.
        /// </summary>
        public int LargeStepInteger { get; set; }

        private string _label;
        /// <summary>
        /// The label for the parameter.
        /// </summary>
        /// <remarks>The value must not exceed 64 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 63 characters.</exception>
        public string Label
        {
            get { return _label; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxLabelLength, "Label");

                _label = value;
            }
        }

        private string _shortLabel;
        /// <summary>
        /// A short label for the parameter.
        /// </summary>
        /// <remarks>The value must not exceed 8 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 7 characters.</exception>
        public string ShortLabel
        {
            get { return _shortLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxShortLabelLength, "ShortLabel");

                _shortLabel = value;
            }
        }

        /// <summary>
        /// The order in which to display the parameter relative to the other plugin parameters.
        /// </summary>
        /// <remarks>This is used for remote controller display purposes.
        /// Note that the <see cref="VstParameterPropertiesFlags.ParameterSupportsDisplayCategory"/> flag must be set.
        /// The host can scan all parameters, and find out in what order to display them.</remarks>
        public short DisplayIndex { get; set; }

        /// <summary>
        /// An indication of the category the parameter belongs to.
        /// </summary>
        /// <remarks>0: no category, else group index + 1.</remarks>
        public short Category { get; set; }

        /// <summary>
        /// The number of parameters in the category.
        /// </summary>
        public short ParameterCountInCategory { get; set; }

        private string _catLabel;
        /// <summary>
        /// The label for the category.
        /// </summary>
        /// <remarks>The value must not exceed 24 characters.</remarks>
        /// <exception cref="ArgumentException">Thrown when the value exceeds 23 characters.</exception>
        public string CategoryLabel
        {
            get { return _catLabel; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxLabelLength, "CategoryLabel");

                _catLabel = value;
            }
        }
    }

    /// <summary>
    /// Flags for the parameter properties.
    /// </summary>
    [Flags]
    public enum VstParameterPropertiesFlags
    {
        /// <summary>Parameter is a switch (on/off).</summary>
        ParameterIsSwitch = 1 << 0,
        /// <summary>MinInteger, MaxInteger are valid.</summary>
        ParameterUsesIntegerMinMax = 1 << 1,
        /// <summary>StepFloat, SmallStepFloat, LargeStepFloat are valid.</summary>
        ParameterUsesFloatStep = 1 << 2,
        /// <summary>StepInteger, LargeStepInteger are valid.</summary>
        ParameterUsesIntStep = 1 << 3,
        /// <summary>DisplayIndex is valid.</summary>
        ParameterSupportsDisplayIndex = 1 << 4,
        /// <summary>Category, etc. valid.</summary>
        ParameterSupportsDisplayCategory = 1 << 5,
        /// <summary>Set if parameter value can ramp up/down.</summary>
        ParameterCanRamp = 1 << 6
    }
}
