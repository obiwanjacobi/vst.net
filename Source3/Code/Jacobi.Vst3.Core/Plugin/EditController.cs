using Jacobi.Vst3.Core;
using Jacobi.Vst3.Core.Plugin;
using System.Text;

namespace Jacobi.Vst3.Plugin
{
    public abstract class EditController : ConnectionPoint, IEditController, IEditController2
    {
        protected EditController()
        {
            Parameters = new ParameterCollection();
        }

        public ParameterCollection Parameters { get; private set; }

        public IComponentHandler ComponentHandler { get; private set; }

        public IComponentHandler2 ComponentHandler2 { get; private set; }

        public IComponentHandler3 ComponentHandler3 { get; private set; }

        public override int Terminate()
        {
            ComponentHandler = null;
            ComponentHandler2 = null;
            ComponentHandler3 = null;

            return base.Terminate();
        }

        #region IEditController Members

        public virtual int SetComponentState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetComponentState");

            return TResult.E_NotImplemented;
        }

        public virtual int SetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetState");

            var serializer = new ParameterSerializer(state, StreamAccessMode.Read);
            serializer.Load(Parameters);

            return TResult.S_OK;
        }

        public virtual int GetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetState");

            var serializer = new ParameterSerializer(state, StreamAccessMode.Write);
            serializer.Save(Parameters);
            
            return TResult.S_OK;
        }

        public virtual int GetParameterCount()
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParameterCount");

            return Parameters.Count;
        }

        public virtual int GetParameterInfo(int paramIndex, ref ParameterInfo info)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParameterInfo " + paramIndex);

            if (paramIndex >= 0 && paramIndex < Parameters.Count)
            {
                var param = Parameters.GetAt(paramIndex);

                info.DefaultNormalizedValue = param.ValueInfo.ParameterInfo.DefaultNormalizedValue;
                info.Flags = param.ValueInfo.ParameterInfo.Flags;
                info.ParamId = param.ValueInfo.ParameterInfo.ParamId;
                info.ShortTitle = param.ValueInfo.ParameterInfo.ShortTitle;
                info.StepCount = param.ValueInfo.ParameterInfo.StepCount;
                info.Title = param.ValueInfo.ParameterInfo.Title;
                info.UnitId = param.ValueInfo.ParameterInfo.UnitId;
                info.Units = param.ValueInfo.ParameterInfo.Units;

                return TResult.S_OK;
            }

            return TResult.E_InvalidArg;
        }

        public virtual int GetParamStringByValue(uint paramId, double valueNormalized, StringBuilder str)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamStringByValue " + paramId + ", " + valueNormalized);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                str.Append(param.ToString(valueNormalized));
            }

            return TResult.E_InvalidArg;
        }

        public virtual int GetParamValueByString(uint paramId, string str, ref double valueNormalized)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamValueByString " + paramId + ", " + str);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                if (param.TryParse(str, out double val))
                {
                    valueNormalized = val;

                    return TResult.S_OK;
                }

                return TResult.S_False;
            }

            return TResult.E_InvalidArg;
        }

        public virtual double NormalizedParamToPlain(uint paramId, double valueNormalized)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.NormalizedParamToPlain " + paramId + ", " + valueNormalized);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                return param.ToPlain(valueNormalized);
            }

            return 0.0;
        }

        public virtual double PlainParamToNormalized(uint paramId, double plainValue)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.PlainParamToNormalized " + paramId + ", " + plainValue);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                return param.ToNormalized(plainValue);
            }

            return 0.0;
        }

        public virtual double GetParamNormalized(uint paramId)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamNormalized " + paramId);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                return param.NormalizedValue;
            }

            return 0.0;
        }

        public virtual int SetParamNormalized(uint paramId, double value)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetParamNormalized " + paramId + ", " + value);

            if (Parameters.Contains(paramId))
            {
                var param = Parameters[paramId];

                param.NormalizedValue = value;

                return TResult.S_OK;
            }

            return TResult.E_InvalidArg;
        }

        public virtual int SetComponentHandler(IComponentHandler handler)
        {
            if (handler == null)
            {
                System.Diagnostics.Trace.WriteLine("IEditController.SetComponentHandler [null]");
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("IEditController.SetComponentHandler [ptr]");
            }

            ComponentHandler = handler;
            ComponentHandler2 = handler as IComponentHandler2;
            ComponentHandler3 = handler as IComponentHandler3;

            return TResult.S_OK;
        }

        public virtual IPlugView CreateView(string name)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.CreateView " + name);

            return null;
        }

        #endregion

        #region IEditController2 Members

        public virtual int SetKnobMode(KnobModes mode)
        {
            System.Diagnostics.Trace.WriteLine("IEditController2.SetKnobMode " + mode);

            return TResult.E_NotImplemented;
        }

        public virtual int OpenHelp(bool onlyCheck)
        {
            System.Diagnostics.Trace.WriteLine("IEditController2.OpenHelp");

            return TResult.E_NotImplemented;
        }

        public virtual int OpenAboutBox(bool onlyCheck)
        {
            System.Diagnostics.Trace.WriteLine("IEditController2.OpenAboutBox");

            return TResult.E_NotImplemented;
        }

        #endregion
    }
}
