﻿using System;
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
            this.RootUnit = new Unit(1, "Root", null, null);
            this.Units.Add(this.RootUnit);

            var gainParam = new GainParameter(this.RootUnit.Info.Id);

            Parameters.Add(gainParam);
        }
    }

    public class GainParameter : Parameter
    {
        public GainParameter(int unitId)
        {
            var valueInfo = new ParameterValueInfo();

            valueInfo.ParameterInfo.DefaultNormalizedValue = 0.45;
            valueInfo.ParameterInfo.Flags = ParameterInfo.ParameterFlags.CanAutomate;
            valueInfo.ParameterInfo.ParamId = 1;
            valueInfo.ParameterInfo.ShortTitle = "Gain";
            valueInfo.ParameterInfo.StepCount = 0;
            valueInfo.ParameterInfo.Title = "Gain";
            valueInfo.ParameterInfo.UnitId = unitId;
            valueInfo.ParameterInfo.Units = "dB";

            valueInfo.MinValue = 0.0;
            valueInfo.MaxValue = 10.0;
            valueInfo.Precision = 2;

            this.ValueInfo = valueInfo;
        }
    }
}
