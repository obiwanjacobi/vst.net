using Jacobi.Vst3.Core;

namespace Jacobi.Vst3.Plugin
{
    public class ParameterFactory
    {
        public virtual Parameter Create(ParameterValueInfo paramValueInfo)
        {
            Parameter param;

            if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsBypass) > 0)
            {
                param = new ByPassParameter(paramValueInfo);
            }
            else if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsProgramChange) > 0)
            {
                param = new ProgramChangeParameter(paramValueInfo);
            }
            else if ((paramValueInfo.ParameterInfo.Flags & ParameterInfo.ParameterFlags.IsList) > 0)
            {
                param = new ListParameter<string>(paramValueInfo);
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
