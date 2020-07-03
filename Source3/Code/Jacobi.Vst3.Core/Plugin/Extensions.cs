using Jacobi.Vst3.Plugin;
using System.Linq;

namespace Jacobi.Vst3.Core.Plugin
{
    public static class Extensions
    {
        //
        // ProgramList
        //

        public static Parameter CreateChangeProgramParameter(this ProgramList programs, int unitId = 0)
        {
            var valueInfo = new ParameterValueInfo(precision: 0);
            valueInfo.ParameterInfo.UnitId = unitId;
            valueInfo.ParameterInfo.Flags =
                ParameterInfo.ParameterFlags.IsProgramChange | ParameterInfo.ParameterFlags.CanAutomate | ParameterInfo.ParameterFlags.IsList;

            var listParam = new ListParameter<string>(valueInfo);
            listParam.Values.AddRange(programs.Select(p => p.Name));
            return listParam;
        }
    }
}
