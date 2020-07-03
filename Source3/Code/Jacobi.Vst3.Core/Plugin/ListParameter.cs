using System;
using System.Collections.Generic;

namespace Jacobi.Vst3.Plugin
{
    public class ListParameter<T> : Parameter
    {
        public ListParameter(ParameterValueInfo paramValueInfo)
            : base(paramValueInfo)
        {
            if ((paramValueInfo.ParameterInfo.Flags & Core.ParameterInfo.ParameterFlags.IsList) == 0)
            {
                throw new ArgumentException("The specified ParameterInfo has no IsList flag.", nameof(paramValueInfo));
            }
            Values = new List<T>();
        }

        public List<T> Values { get; protected set; }

        public override string ToString(double normValue)
        {
            int index = (int)ToPlain(normValue);

            if (index >= 0 && index < Values.Count)
            {
                return ConvertToString(Values[index]);
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

            foreach (var val in Values)
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
}
