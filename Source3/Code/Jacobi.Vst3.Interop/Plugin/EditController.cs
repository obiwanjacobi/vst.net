using System;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public class EditController : ComponentBase, IEditController, IEditController2
    {
        public IComponentHandler ComponentHandler { get; private set; }

        public IComponentHandler2 ComponentHandler2 { get; private set; }

        public IComponentHandler3 ComponentHandler3 { get; private set; }

        public override int Terminate()
        {
            this.ComponentHandler = null;
            this.ComponentHandler2 = null;
            this.ComponentHandler3 = null;

            return base.Terminate();
        }

        #region IEditController Members


        public virtual int SetComponentState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public virtual int SetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public virtual int GetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public virtual int GetParameterCount()
        {
            throw new NotImplementedException();
        }

        public virtual int GetParameterInfo(int paramIndex, ref ParameterInfo info)
        {
            throw new NotImplementedException();
        }

        public virtual int GetParamStringByValue(uint paramId, double valueNormalized, System.Text.StringBuilder @string)
        {
            throw new NotImplementedException();
        }

        public virtual int GetParamValueByString(uint paramId, string @string, ref double valueNormalized)
        {
            throw new NotImplementedException();
        }

        public virtual double NormalizedParamToPlain(uint paramId, double valueNormalized)
        {
            throw new NotImplementedException();
        }

        public virtual double PlainParamToNormalized(uint paramId, double plainValue)
        {
            throw new NotImplementedException();
        }

        public virtual double GetParamNormalized(uint paramId)
        {
            throw new NotImplementedException();
        }

        public virtual int SetParamNormalized(int paramIndex, double value)
        {
            throw new NotImplementedException();
        }

        public virtual int SetComponentHandler(IComponentHandler handler)
        {
            if (handler == null)
            {
                return TResult.E_InvalidArg;
            }

            this.ComponentHandler = handler;
            this.ComponentHandler2 = handler as IComponentHandler2;
            this.ComponentHandler3 = handler as IComponentHandler3;

            return TResult.S_OK;
        }

        public virtual IPlugView CreateView(string name)
        {
            return null;
        }

        #endregion

        #region IEditController2 Members

        public int SetKnobMode(KnobModes mode)
        {
            return TResult.E_NotImplemented;
        }

        public int OpenHelp(bool onlyCheck)
        {
            return TResult.E_NotImplemented;
        }

        public int OpenAboutBox(bool onlyCheck)
        {
            return TResult.E_NotImplemented;
        }

        #endregion
    }
}
