namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public class VstParameter : IActivatable, IDisposable
    {
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

        public VstParameterInfo Info
        { get; private set; }

        public VstParameterCategory Category
        { get; set; }

        private float _value;
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
        public virtual string DisplayValue 
        {
            get { return _displayValue; }
            protected set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, "DisplayValue");

                _displayValue = value;
            }
        }

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

        public EventHandler<EventArgs> ValueChangedCallback { get; set; }
        public EventHandler<EventArgs> ActivationChangedCallback { get; set; }

        #region IActivatable Members

        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;

            OnActivationChanged();
        }

        public void Deactivate()
        {
            IsActive = false;

            OnActivationChanged();
        }

        #endregion

        protected virtual void OnValueChanged()
        {
            EventHandler<EventArgs> handler = ValueChangedCallback;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnActivationChanged()
        {
            EventHandler<EventArgs> handler = ActivationChangedCallback;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #region IDisposable Members

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
