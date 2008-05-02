namespace Jacobi.Vst.Core
{
    using System;

    // TODO: Add properties and implement auto-flags
    public class VstParameterProperties
    {
        public VstParameterPropertiesFlags Flags;

        public float StepFloat;
        public float SmallStepFloat;
        public float LargeStepFloat;

        public int MinInteger; 
        public int MaxInteger;
        public int StepInteger;
        public int LargeStepInteger;

        public string Label;
        public string ShortLabel;

        public short DisplayIndex;
        
        public short Category;
        public short NumParametersInCategory;
        public string CategoryLabel;
    }

    [Flags]
    public enum VstParameterPropertiesFlags
    {
        ParameterIsSwitch = 1 << 0,	                // parameter is a switch (on/off)
        ParameterUsesIntegerMinMax = 1 << 1,	    // MinInteger, MaxInteger are valid
        ParameterUsesFloatStep = 1 << 2,	        // StepFloat, SmallStepFloat, LargeStepFloat are valid
        ParameterUsesIntStep = 1 << 3,	            // StepInteger, LargeStepInteger are valid
        ParameterSupportsDisplayIndex = 1 << 4,	    // DisplayIndex is valid
        ParameterSupportsDisplayCategory = 1 << 5,	// Category, etc. valid
        ParameterCanRamp = 1 << 6	                // set if parameter value can ramp up/down
    }
}
