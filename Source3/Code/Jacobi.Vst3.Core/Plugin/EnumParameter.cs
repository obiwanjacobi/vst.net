using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Plugin
{
    public class EnumParameter : Parameter
    {
        protected EnumParameter()
        { }

        public EnumParameter(Type enumType)
            : base(new EnumParameterValueInfo(enumType))
        { }

        public EnumParameter(EnumParameterValueInfo valueInfo)
            : base(valueInfo)
        { }

        public new EnumParameterValueInfo ValueInfo
        {
            get { return (EnumParameterValueInfo)base.ValueInfo; }
        }

        public override string ToString(double normValue)
        {
            var plainValue = (int)ToPlain(normValue);

            var name = (from kv in ValueInfo.Enumerations
                        where kv.Value == plainValue
                        select kv.Key).FirstOrDefault();

            return name;
        }

        public override bool TryParse(string displayValue, out double normValue)
        {
            var upper = displayValue.ToUpperInvariant();

            var result = from kv in ValueInfo.Enumerations
                         where kv.Key.ToUpperInvariant() == upper
                         select kv.Value;

            if (result.Any())
            {
                normValue = ToNormalized(result.First());
                return true;
            }

            normValue = 0.0;
            return false;
        }
    }

    public class EnumParameterValueInfo : ParameterValueInfo
    {
        protected EnumParameterValueInfo()
        { }

        public EnumParameterValueInfo(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("Specified type is not an enum: " + enumType.FullName, "enumType");

            Enumerations = new Dictionary<string, int>();

            var values = Enum.GetValues(enumType);
            var names = Enum.GetNames(enumType);

            for (int i = 0; i < values.Length; i++)
            {
                Enumerations.Add(names[i], (int)values.GetValue(i));
            }

            MinValue = (from int value in values select value).Min();
            MaxValue = (from int value in values select value).Max();
            ParameterInfo.StepCount = Enumerations.Count;
            Precision = 0;
        }

        public IDictionary<string, int> Enumerations { get; protected set; }
    }
}
