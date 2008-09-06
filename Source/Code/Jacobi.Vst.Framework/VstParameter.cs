namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstParameter represents a parameter value for one plugin parameter.
    /// </summary>
    public class VstParameter : IActivatable, IDisposable
    {
        /// <summary>
        /// Constructs a new instance based on the parameter meta info.
        /// </summary>
        /// <param name="parameterInfo">Must not be null.</param>
        public VstParameter(VstParameterInfo parameterInfo)
        {
            Throw.IfArgumentIsNull(parameterInfo, "parameterInfo");

            Info = parameterInfo;

            if (Info.ParameterManager != null)
            {
                Info.ParameterManager.SubscribeTo(this);
            }

            // set default value (raise a changed event?)
            Value = Info.DefaultValue;
        }

        /// <summary>
        /// Gets the parameter meta info passed in at the constructor.
        /// </summary>
        public VstParameterInfo Info { get; private set; }

        private float _value;
        /// <summary>
        /// Gets or sets the numberical value of the parameter.
        /// </summary>
        public float Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;

                    OnValueChanged();
                }
            }
        }

        private string _displayValue;
        /// <summary>
        /// Gets the value of the parameter formatted for displaying.
        /// </summary>
        /// <remarks>Derived classes can set this property but the length should not exceed 7 characters.</remarks>
        public virtual string DisplayValue 
        {
            get { return _displayValue; }
            protected set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, "DisplayValue");

                _displayValue = value;
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
            float fValue;
            if (Single.TryParse(value, out fValue))
            {
                Value = fValue;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets or sets the callback delegate that is called when the parameter value changes.
        /// </summary>
        public EventHandler<EventArgs> ValueChangedCallback { get; set; }

        /// <summary>
        /// Gets or sets the callback delegate that is called when the parameter activation changes.
        /// </summary>
        public EventHandler<EventArgs> ActivationChangedCallback { get; set; }

        #region IActivatable Members

        /// <summary>
        /// Gets the active state for this parameter.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Activates the parameter instance.
        /// </summary>
        public void Activate()
        {
            IsActive = true;

            OnActivationChanged();
        }

        /// <summary>
        /// deactivates the parameter instance.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;

            OnActivationChanged();
        }

        #endregion

        /// <summary>
        /// Called to raise the ValueChanged event.
        /// </summary>
        protected virtual void OnValueChanged()
        {
            EventHandler<EventArgs> handler = ValueChangedCallback;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called to raise the ActivationChanged event.
        /// </summary>
        protected virtual void OnActivationChanged()
        {
            EventHandler<EventArgs> handler = ActivationChangedCallback;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <remarks>All references are cleared (null).</remarks>
        public virtual void Dispose()
        {
            // clear all references
            DisplayValue = null;
            Info = null;
            ValueChangedCallback = null;
            ActivationChangedCallback = null;
        }

        #endregion
    }
}
