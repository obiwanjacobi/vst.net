﻿namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// The VstParameterInfo contains the meta information for a parameter.
    /// </summary>
    public class VstParameterInfo : ObservableObject
    {
        public const string NormalizationInfoPropertyName = "NormalizationInfo";
        public const string ParameterManagerPropertyName = "ParameterManager";
        public const string CategoryPropertyName = "Category";
        public const string DefaultValuePropertyName = "DefaultValue";
        public const string NullValuePropertyName = "NullValue";
        public const string CanBeAutomatedPropertyName = "CanBeAutomated";
        public const string IsSwitchPropertyName = "IsSwitch";
        public const string CanRampPropertyName = "CanRamp";
        public const string NamePropertyName = "Name";
        public const string LabelPropertyName = "Label";
        public const string ShortLabelPropertyName = "ShortLabel";
        public const string MinIntegerPropertyName = "MinInteger";
        public const string MaxIntegerPropertyName = "MaxInteger";
        public const string StepIntegerPropertyName = "StepInteger";
        public const string LargeStepIntegerPropertyName = "LargeStepInteger";
        public const string StepFloatPropertyName = "StepFloat";
        public const string SmallStepFloatPropertyName = "SmallStepFloat";
        public const string LargeStepFloatPropertyName = "LargeStepFloat";

        /// <summary>
        /// Constructs a new instance and sets the <see cref="MaxInteger"/> property to 1.
        /// </summary>
        public VstParameterInfo()
        {
            // set default for [0,1] value range.
            MaxInteger = 1;
        }

        private VstParameterNormalizationInfo _normalizationInfo;
        /// <summary>
        /// When set to an instance of an object it contains 
        /// the normalization factors for this parameter.
        /// </summary>
        public VstParameterNormalizationInfo NormalizationInfo
        {
            get { return _normalizationInfo; }
            set
            {
                SetProperty(value, ref _normalizationInfo, NormalizationInfoPropertyName);
            }
        }

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
                    throw new ArgumentException(Properties.Resources.VstParameterInfo_ParameterManagerNotLinked, ParameterManagerPropertyName);
                }

                SetProperty(value, ref _paramMgr, ParameterManagerPropertyName);
            }
        }

        private VstParameterCategory _category;
        /// <summary>
        /// Gets or sets the parameter category.
        /// </summary>
        public VstParameterCategory Category
        {
            get { return _category; }
            set
            {
                SetProperty(value, ref _category, CategoryPropertyName);
            }
        }

        private float _defaultValue;
        /// <summary>
        /// Gets or sets the default value the <see cref="VstParameter"/> is initialized with.
        /// </summary>
        public float DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                SetProperty(value, ref _defaultValue, DefaultValuePropertyName);
            }
        }

        private float _nullValue;
        /// <summary>
        /// Gets or sets the null value used when the <see cref="VstParameterManager"/> switches active parameters.
        /// </summary>
        public float NullValue
        {
            get { return _nullValue; }
            set
            {
                SetProperty(value, ref _nullValue, NullValuePropertyName);
            }
        }

        private bool _canBeAutomated;
        /// <summary>
        /// Gets or sets an indication wheter the parameter can be automated by the host (true).
        /// </summary>
        public bool CanBeAutomated
        {
            get { return _canBeAutomated; }
            set
            {
                SetProperty(value, ref _canBeAutomated, CanBeAutomatedPropertyName);
            }
        }

        private bool _isSwitch;
        /// <summary>
        /// Gets or sets an indication wheter the parameter is a switch (true).
        /// </summary>
        public bool IsSwitch
        {
            get { return _isSwitch; }
            set
            {
                SetProperty(value, ref _isSwitch, IsSwitchPropertyName);
            }
        }

        private bool _canRamp;
        /// <summary>
        /// Gets or sets an indication wheter the parameter can ramp (true).
        /// </summary>
        public bool CanRamp
        {
            get { return _canRamp; }
            set
            {
                SetProperty(value, ref _canRamp, CanRampPropertyName);
            }
        }

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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, NamePropertyName);

                SetProperty(value, ref _name, NamePropertyName);
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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxLabelLength, LabelPropertyName);

                SetProperty(value, ref _label, LabelPropertyName);
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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxShortLabelLength, ShortLabelPropertyName);

                SetProperty(value, ref _shortLabel, ShortLabelPropertyName);
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

        private int _minInteger;
        /// <summary>
        /// Gets or sets the minimal value of the parameter.
        /// </summary>
        public int MinInteger
        {
            get { return _minInteger; }
            set
            {
                SetProperty(value, ref _minInteger, MinIntegerPropertyName);
            }
        }

        private int _maxInteger;
        /// <summary>
        /// Gets or sets the maximal value of the parameter.
        /// </summary>
        public int MaxInteger
        {
            get { return _maxInteger; }
            set
            {
                SetProperty(value, ref _maxInteger, MaxIntegerPropertyName);
            }
        }

        /// <summary>
        /// Gets an indication whether the <see cref="StepInteger"/> and 
        /// <see cref="LargeStepInteger"/> properties are filled.
        /// </summary>
        public bool IsStepIntegerValid
        {
            get { return (StepInteger != 0 && LargeStepInteger != 0); }
        }

        private int _stepInteger;
        /// <summary>
        /// Gets or sets the steps the parameter value will take.
        /// </summary>
        public int StepInteger
        {
            get { return _stepInteger; }
            set
            {
                SetProperty(value, ref _stepInteger, StepIntegerPropertyName);
            }
        }

        private int _largeStepInteger;
        /// <summary>
        /// Gets or sets the large steps the parameter value will take.
        /// </summary>
        public int LargeStepInteger
        {
            get { return _largeStepInteger; }
            set
            {
                SetProperty(value, ref _largeStepInteger, LargeStepIntegerPropertyName);
            }
        }

        /// <summary>
        /// Gets an indication whether the <see cref="StepFloat"/>, <see cref="SmallStepFloat"/> 
        /// and <see cref="LargeStepFloat"/> properties are set.
        /// </summary>
        public bool IsStepFloatValid
        {
            get { return (StepFloat != 0.0f && SmallStepFloat != 0.0f && LargeStepFloat != 0.0f); }
        }

        private float _stepFloat;
        /// <summary>
        /// Gets or sets the steps the parameter value will take.
        /// </summary>
        public float StepFloat
        {
            get { return _stepFloat; }
            set
            {
                SetProperty(value, ref _stepFloat, StepFloatPropertyName);
            }
        }

        private float _smallStepFloat;
        /// <summary>
        /// Gets or sets the small steps the parameter value will take.
        /// </summary>
        public float SmallStepFloat
        {
            get { return _smallStepFloat; }
            set
            {
                SetProperty(value, ref _smallStepFloat, SmallStepFloatPropertyName);
            }
        }

        private float _largeStepFloat;
        /// <summary>
        /// Gets or sets the large steps the parameter value will take.
        /// </summary>
        public float LargeStepFloat
        {
            get { return _largeStepFloat; }
            set
            {
                SetProperty(value, ref _largeStepFloat, LargeStepFloatPropertyName);
            }
        }
    }
}
