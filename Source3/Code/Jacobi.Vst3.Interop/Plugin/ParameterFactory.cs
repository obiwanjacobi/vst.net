using Jacobi.Vst3.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Plugin
{
    public class ParameterFactory
    {
        public virtual Parameter Create(ParameterValueInfo paramValueInfo)
        {
            Parameter param = null;

            if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsBypass) > 0)
            {
                param = new ByPassParameter(paramValueInfo);
            }
            else if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsProgramChange) > 0)
            {
                param = new ProgramParameter(paramValueInfo);
            }
            else if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsList) > 0)
            {
                param = new ListParameter<string>((ListParameterValueInfo<string>)paramValueInfo);
            }
            else
            {
                param = new Parameter(paramValueInfo);
            }

            param.IsReadOnly = (paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsReadOnly) > 0;

            return param;
        }
    }
}
