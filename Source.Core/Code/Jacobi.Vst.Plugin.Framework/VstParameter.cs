namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Common;
    using System;

    /// <summary>
    /// The VstParameter represents a parameter value for one plugin parameter.
    /// </summary>
    public class VstParameter : ObservableObject, IActivatable, IDisposable
    {
        /// <summary>
        /// Constructs a new instance based on the parameter meta info.
        /// </summary>
        /// <param name="parameterInfo">Must not be null.</param>
        public VstParameter(VstParameterInfo parameterInfo)
        {
            Throw.IfArgumentIsNull(parameterInfo, nameof(parameterInfo));

            Info = parameterInfo;

            if (Info.ParameterManager != null)
            {
                Info.ParameterManager.SubscribeTo(this);
            }

            // set default value
            Value = Info.DefaultValue;
        }

        /// <summary>
        /// Gets the parameter meta info passed in at the constructor.
        /// </summary>
        public VstParameterInfo Info { get; private set; }

        /// <summary>
        /// Gets or set a reference to the (first) collection this parameter is in.
        /// </summary>
        internal protected VstParameterCollection Parent { get; set; }

        /// <summary>
        /// Gets the zero-based index this parameter has in the parameter collection.
        /// </summary>
        public int Index
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.IndexOf(this);
                }

                return -1;
            }
        }

        private float _value;
        /// <summary>
        /// Gets or sets the numberical value of the parameter.
        /// </summary>
        public float Value
        {
            get { return _value; }
            set { SetProperty(value, ref _value, nameof(Value)); }
        }

        /// <summary>
        /// Gets or sets the normalized value of the parameter [0.0, 1.0].
        /// </summary>
        public float NormalizedValue
        {
            get
            {
                if (Info.NormalizationInfo != null)
                {
                    return Info.NormalizationInfo.GetNormalizedValue(Value);
                }

                return Value;
            }
            set
            {
                if (Info.NormalizationInfo != null)
                {
                    Value = Info.NormalizationInfo.GetRawValue(value);
                }
                else
                {
                    Value = value;
                }
            }
        }

        private string _displayValue;
        /// <summary>
        /// Gets the value of the parameter formatted for displaying.
        /// </summary>
        /// <remarks>Derived classes can set this property but the length should not exceed 8 characters. 
        /// By default the <see cref="Value"/> property is returned as string (untill you set a non-null value).</remarks>
        public virtual string DisplayValue
        {
            get
            {
                if (_displayValue == null)
                {
                    return Value.ToString();
                }

                return _displayValue;
            }
            protected set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, nameof(DisplayValue));

                SetProperty(value, ref _displayValue, nameof(DisplayValue));
            }
        }

        /// <summary>
        /// Called when the parameter value is parsed from a string.
        /// </summary>
        /// <param name="value">The string containing the parameter value.</param>
        /// <returns>Returns true when the <paramref name="value"/> was successfully parsed.</returns>
        /// <remarks>The default implementation will try to parse to a <see cref="float"/> value.</remarks>
        public virtual bool ParseValue(string value)
        {
            if (Single.TryParse(value, out float fValue))
            {
                Value = fValue;
                return true;
            }

            return false;
        }

        #region IActivatable Members

        private bool _isActive;
        /// <summary>
        /// Gets the active state for this parameter.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(value, ref _isActive, nameof(IsActive)); }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Override this method in derived classes to cleanup managed and/or
        /// unmanaged resources.
        /// </summary>
        /// <param name="disposing">When true dispose managed and unmanaged resources. 
        /// when false dispose only unmanaged resources.</param>
        /// <remarks>All references are cleared (null), also the callback handlers.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // clear all references
                DisplayValue = null;
                Info = null;
                Parent = null;
            }
        }
        #endregion
    }
}
