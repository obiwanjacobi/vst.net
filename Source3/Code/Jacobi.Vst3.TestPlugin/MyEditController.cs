using System;
using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;
using Jacobi.Vst3.Plugin;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Edit Controller")]
    [Guid("D74D670B-28B8-4AB2-9180-D4D12B52F54B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class MyEditController : EditControllerWithUnits
    {
        public MyEditController()
        {
            var gainParam = CreateGainParameter();

            Parameters.Add(gainParam);

            this.RootUnit = new Unit("Root", 1, null, null);
            this.Units.Add(this.RootUnit);
        }

        private GainParameter CreateGainParameter()
        {
            return new GainParameter();
        }
    }

    public class GainParameter : Parameter
    {
        public GainParameter()
        {
            var valueInfo = new ParameterValueInfo();

            valueInfo.ParameterInfo.DefaultNormalizedValue = 0.45;
            valueInfo.ParameterInfo.Flags = ParameterInfo.ParameterFlags.CanAutomate;
            valueInfo.ParameterInfo.ParamId = 1;
            valueInfo.ParameterInfo.ShortTitle = "Gain";
            valueInfo.ParameterInfo.StepCount = 0;
            valueInfo.ParameterInfo.Title = "Gain";
            valueInfo.ParameterInfo.UnitId = 0;
            valueInfo.ParameterInfo.Units = "dB";

            valueInfo.MinValue = 0.0;
            valueInfo.MaxValue = 10.0;
            valueInfo.Precision = 2;

            this.ValueInfo = valueInfo;
        }
    }
}
