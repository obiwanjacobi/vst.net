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
            System.Diagnostics.Trace.WriteLine("IEditController.SetComponentState");

            return TResult.E_NotImplemented;
        }

        public virtual int SetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetState");

            return TResult.E_NotImplemented;
        }

        public virtual int GetState(IBStream state)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetState");

            return TResult.E_NotImplemented;
        }

        public virtual int GetParameterCount()
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParameterCount");

            return 0;
        }

        public virtual int GetParameterInfo(int paramIndex, ref ParameterInfo info)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParameterInfo");

            return TResult.E_NotImplemented;
        }

        public virtual int GetParamStringByValue(uint paramId, double valueNormalized, System.Text.StringBuilder @string)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamStringByValue");

            return TResult.E_NotImplemented;
        }

        public virtual int GetParamValueByString(uint paramId, string @string, ref double valueNormalized)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamValueByString");

            return TResult.E_NotImplemented;
        }

        public virtual double NormalizedParamToPlain(uint paramId, double valueNormalized)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.NormalizedParamToPlain");

            return valueNormalized;
        }

        public virtual double PlainParamToNormalized(uint paramId, double plainValue)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.PlainParamToNormalized");

            return plainValue;
        }

        public virtual double GetParamNormalized(uint paramId)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.GetParamNormalized");

            return 0.0;
        }

        public virtual int SetParamNormalized(int paramIndex, double value)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetParamNormalized");

            return TResult.E_NotImplemented;
        }

        public virtual int SetComponentHandler(IComponentHandler handler)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.SetComponentHandler");

            this.ComponentHandler = handler;
            this.ComponentHandler2 = handler as IComponentHandler2;
            this.ComponentHandler3 = handler as IComponentHandler3;

            return TResult.S_OK;
        }

        public virtual IPlugView CreateView(string name)
        {
            System.Diagnostics.Trace.WriteLine("IEditController.CreateView");

            return null;
        }

        #endregion

        #region IEditController2 Members

        public virtual int SetKnobMode(KnobModes mode)
        {
            System.Diagnostics.Trace.WriteLine("IEditController2.SetKnobMode");

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
