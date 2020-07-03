using Jacobi.Vst3.Core;
using Jacobi.Vst3.Plugin;
using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Edit Controller")]
    [Guid("D74D670B-28B8-4AB2-9180-D4D12B52F54B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MyEditController : EditControllerWithUnits
    {
        public MyEditController()
        {
            RootUnit = new RootUnit("Root", null);
            Units.Add(RootUnit);

            Parameters.Add(new ByPassParameter(RootUnit.Info.Id, 1));
            Parameters.Add(new GainParameter(RootUnit.Info.Id, 2));
        }
    }

    public class GainParameter : Parameter
    {
        public GainParameter(int unitId, uint paramId)
        {
            var valueInfo = new ParameterValueInfo();

            valueInfo.ParameterInfo.DefaultNormalizedValue = 0.45;
            valueInfo.ParameterInfo.Flags = ParameterInfo.ParameterFlags.CanAutomate;
            valueInfo.ParameterInfo.ParamId = paramId;
            valueInfo.ParameterInfo.ShortTitle = "Gain";
            valueInfo.ParameterInfo.StepCount = 0;
            valueInfo.ParameterInfo.Title = "Gain";
            valueInfo.ParameterInfo.UnitId = unitId;
            valueInfo.ParameterInfo.Units = "dB";

            valueInfo.MinValue = 0.0;
            valueInfo.MaxValue = 10.0;
            valueInfo.Precision = 2;

            ValueInfo = valueInfo;
        }
    }
}
