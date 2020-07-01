using Jacobi.Vst3.Core;
using System;

namespace Jacobi.Vst3.Plugin
{
    public class ByPassParameter : Parameter
    {
        public ByPassParameter(ParameterValueInfo valueInfo)
        {
            ValueInfo = valueInfo;
        }

        public ByPassParameter(int unitId, uint paramId)
        {
            var valueInfo = new ParameterValueInfo();

            valueInfo.ParameterInfo.DefaultNormalizedValue = 0;
            valueInfo.ParameterInfo.Flags =
                ParameterInfo.ParameterFlags.IsBypass | ParameterInfo.ParameterFlags.CanAutomate;
            valueInfo.ParameterInfo.ParamId = paramId;
            valueInfo.ParameterInfo.ShortTitle = "Bypass";
            valueInfo.ParameterInfo.StepCount = 0;
            valueInfo.ParameterInfo.Title = "Bypass";
            valueInfo.ParameterInfo.UnitId = unitId;
            valueInfo.ParameterInfo.Units = String.Empty;

            valueInfo.MinValue = 0.0;
            valueInfo.MaxValue = 1.0;
            valueInfo.Precision = 0;

            ValueInfo = valueInfo;
        }
    }
}
