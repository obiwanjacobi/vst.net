using Jacobi.Vst3.Common;
using Jacobi.Vst3.Core;
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

            SetValue(ValueInfo.ParameterInfo.DefaultNormalizedValue, null, false);
        }

        public ParameterValueInfo ValueInfo { get; protected set; }

        public uint Id
        {
            get { return ValueInfo.ParameterInfo.ParamId; }
        }

        public bool IsReadOnly { get; set; }

        public bool ResetToDefaultValue()
        {
            return SetValue(ValueInfo.ParameterInfo.DefaultNormalizedValue, null, true);
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
                OnPropertyChanged(nameof(NormalizedValue));
            }

            if (plainChanged && notify)
            {
                OnPropertyChanged(nameof(PlainValue));
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
            value = value.Crop(0.0, 1.0);

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
            value = value.Crop(ValueInfo.MinValue, ValueInfo.MaxValue);

            if (_plainValue != value)
            {
                _plainValue = value;
                return true;
            }

            return false;
        }

        public virtual double ToNormalized(double plainValue)
        {
            plainValue = plainValue.Crop(ValueInfo.MinValue, ValueInfo.MaxValue);

            var scale = ValueInfo.MaxValue - ValueInfo.MinValue;
            var offset = -ValueInfo.MinValue;

            return (plainValue + offset) / scale;
        }

        public virtual double ToPlain(double normValue)
        {
            normValue = normValue.Crop(0.0, 1.0);

            var scale = ValueInfo.MaxValue - ValueInfo.MinValue;
            var offset = -ValueInfo.MinValue;

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

            if (ValueInfo.Precision > 0)
            {
                string format = new string('#', ValueInfo.Precision);

                return value.ToString("0." + format);
            }

            return value.ToString();
        }

        public virtual bool TryParse(string displayValue, out double normValue)
        {
            return Double.TryParse(displayValue, out normValue);
        }
    }
}
