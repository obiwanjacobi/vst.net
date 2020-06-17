using System;

namespace Jacobi.Vst3.Plugin
{
    public class RangeParameter : Parameter
    {
        public RangeParameter(ParameterValueInfo paramValueInfo)
            : base(paramValueInfo)
        { }

        public override double ToNormalized(double plainValue)
        {
            if (ValueInfo.ParameterInfo.StepCount > 1)
            {
                return (plainValue - ValueInfo.MinValue) / ValueInfo.ParameterInfo.StepCount;
            }

            return base.ToNormalized(plainValue);
        }

        public override double ToPlain(double normValue)
        {
            if (ValueInfo.ParameterInfo.StepCount > 1)
            {
                return Math.Min(ValueInfo.ParameterInfo.StepCount, (normValue * (ValueInfo.ParameterInfo.StepCount + 1)) + ValueInfo.MinValue);
            }

            return base.ToPlain(normValue);
        }
    }
}
