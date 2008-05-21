namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using System;

    public class VstParameter : IActivatable
    {
        public VstParameter(VstParameterInfo parameterInfo)
        {
            Throw.IfArgumentIsNull(parameterInfo, "parameterInfo");

            Info = parameterInfo;

            if (Info.ParameterManager != null)
            {
                Info.ParameterManager.SubscribeTo(this);
            }

            // TODO: set default value (raise a changed event?)
        }

        public VstParameterInfo Info
        { get; private set; }

        public VstParameterCategory Category
        { get; set; }

        private float _normalizedValue;
        public float NormalizedValue
        {
            get { return _normalizedValue; }
            set
            {
                _normalizedValue = value;

                OnNormalizedValueChanged();
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
                NormalizedValue = fValue;
                return true;
            }

            return false;
        }

        public event EventHandler<EventArgs> NormalizedValueChanged;
        public event EventHandler<EventArgs> ActivationChanged;

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

        protected virtual void OnNormalizedValueChanged()
        {
            EventHandler<EventArgs> handler = NormalizedValueChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void OnActivationChanged()
        {
            EventHandler<EventArgs> handler = ActivationChanged;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
