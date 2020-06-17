using Jacobi.Vst3.Common;
using System;

namespace Jacobi.Vst3.Plugin
{
    public class Parameter : ObservableObject
    {
        protected Parameter()
        { }

        public Parameter(ParameterValueInfo paramValueInfo)
        {
            ValueInfo = paramValueInfo;

            SetValue(this.ValueInfo.ParameterInfo.DefaultNormalizedValue, null, false);
        }

        public ParameterValueInfo ValueInfo { get; protected set; }

        public uint Id
        {
            get { return this.ValueInfo.ParameterInfo.ParamId; }
        }

        public bool IsReadOnly { get; set; }

        public bool ResetToDefaultValue()
        {
            return SetValue(this.ValueInfo.ParameterInfo.DefaultNormalizedValue, null, true);
        }

        protected bool SetValue(double? normValue, double? plainValue, bool notify)
        {
            if (IsReadOnly) return false;

            bool normChanged = false;
            bool plainChanged = false;

            if (normValue.HasValue)
            {
                normChanged = SetNormalizedValue(normValue.Value);
                plainChanged = SetPlainValue(ToPlain(normValue.Value));
            }

            if (plainValue.HasValue)
            {
                normChanged = SetNormalizedValue(ToNormalized(plainValue.Value));
                plainChanged = SetPlainValue(plainValue.Value);
            }

            if (normChanged && notify)
            {
                OnPropertyChanged("NormalizedValue");
            }

            if (plainChanged && notify)
            {
                OnPropertyChanged("PlainValue");
            }

            return normChanged || plainChanged;
        }

        private double _normalizedValue;

        public double NormalizedValue
        {
            get { return _normalizedValue; }
            set { SetValue(value, null, true); }
        }

        private bool SetNormalizedValue(double value)
        {
            value = Crop(value, 0.0, 1.0);

            if (_normalizedValue != value)
            {
                _normalizedValue = value;
                return true;
            }

            return false;
        }

        private double _plainValue;

        public double PlainValue
        {
            get { return _plainValue; }
            set { SetValue(null, value, true); }
        }

        private bool SetPlainValue(double value)
        {
            value = Crop(value, this.ValueInfo.MinValue, this.ValueInfo.MaxValue);

            if (_plainValue != value)
            {
                _plainValue = value;
                return true;
            }

            return false;
        }

        public virtual double ToNormalized(double plainValue)
        {
            plainValue = Crop(plainValue, this.ValueInfo.MinValue, this.ValueInfo.MaxValue);

            var scale = this.ValueInfo.MaxValue - this.ValueInfo.MinValue;
            var offset = -this.ValueInfo.MinValue;

            return (plainValue + offset) / scale;
        }

        public virtual double ToPlain(double normValue)
        {
            normValue = Crop(normValue, 0.0, 1.0);

            var scale = this.ValueInfo.MaxValue - this.ValueInfo.MinValue;
            var offset = -this.ValueInfo.MinValue;

            return (normValue * scale) - offset;
        }

        public virtual string ToString(double normValue)
        {
            if (ValueInfo.ParameterInfo.StepCount == 1)
            {
                if (normValue >= 0.5)
                {
                    return "On";
                }
                else
                {
                    return "Off";
                }
            }

            var value = ToPlain(normValue);

            if (this.ValueInfo.Precision > 0)
            {
                string format = new string('#', this.ValueInfo.Precision);

                return value.ToString("0." + format);
            }

            return value.ToString();
        }

        public virtual bool TryParse(string displayValue, out double normValue)
        {
            return Double.TryParse(displayValue, out normValue);
        }

        protected static double Crop(double value, double min, double max)
        {
            if (value > max)
            {
                value = max;
            }

            if (value < min)
            {
                value = min;
            }

            return value;
        }
    }
}
