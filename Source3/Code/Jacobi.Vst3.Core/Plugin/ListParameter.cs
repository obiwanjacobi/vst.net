using System;
using System.Collections.Generic;

namespace Jacobi.Vst3.Plugin
{
    public class ListParameter<T> : Parameter
    {
        public ListParameter(ListParameterValueInfo<T> paramValueInfo)
            : base(paramValueInfo)
        { }

        public new ListParameterValueInfo<T> ValueInfo
        {
            get { return (ListParameterValueInfo<T>)base.ValueInfo; }
        }

        public override string ToString(double normValue)
        {
            int index = (int)ToPlain(normValue);

            if (index >= 0 && index < ValueInfo.Values.Count)
            {
                return ConvertToString(ValueInfo.Values[index]);
            }

            return String.Empty;
        }

        protected virtual string ConvertToString(T value)
        {
            return value.ToString();
        }

        public override bool TryParse(string displayValue, out double normValue)
        {
            int index = 0;

            foreach (var val in ValueInfo.Values)
            {
                var str = ConvertToString(val);

                if (displayValue.Equals(str, StringComparison.OrdinalIgnoreCase))
                {
                    normValue = ToNormalized(index);
                    return true;
                }

                index++;
            }

            normValue = 0.0;
            return false;
        }
    }

    public class ListParameterValueInfo<T> : ParameterValueInfo
    {
        protected ListParameterValueInfo()
        {
            Values = new List<T>();
        }

        public ListParameterValueInfo(int precision = 4)
            : base(precision)
        {
            Values = new List<T>();
        }

        public List<T> Values { get; protected set; }
    }
}
