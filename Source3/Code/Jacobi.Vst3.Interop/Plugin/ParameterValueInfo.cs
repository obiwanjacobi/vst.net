using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Interop.Plugin
{
    public class ParameterValueInfo
    {
        protected ParameterValueInfo()
        {
        }

        public ParameterValueInfo(int precision = 4)
        {
            MinValue = 0.0;
            MaxValue = 1.0;
            Precision = precision;
        }

        public ParameterInfo ParameterInfo;

        public double MinValue { get; protected set; }

        public double MaxValue { get; protected set; }

        public int Precision { get; protected set; }
    }
}
