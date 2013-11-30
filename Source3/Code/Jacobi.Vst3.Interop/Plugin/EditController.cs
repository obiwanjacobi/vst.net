using System;
using Jacobi.Vst3.Interop;
using Jacobi.Vst3.Common;

namespace Jacobi.Vst3.Plugin
{
    public class EditController : ComponentBase, IEditController, IEditController2
    {
        private ComRef<IComponentHandler> _componentHandler;
        public IComponentHandler ComponentHandler
        {
            get { return ComRef<IComponentHandler>.GetInstance(_componentHandler); }
        }

        private ComRef<IComponentHandler2> _componentHandler2;
        public IComponentHandler2 ComponentHandler2
        {
            get { return ComRef<IComponentHandler2>.GetInstance(_componentHandler2); }
        }

        private ComRef<IComponentHandler3> _componentHandler3;
        public IComponentHandler3 ComponentHandler3
        {
            get { return ComRef<IComponentHandler3>.GetInstance(_componentHandler3); }
        }

        public override int Terminate()
        {
            ComRef<IComponentHandler>.Dispose(ref this._componentHandler);
            ComRef<IComponentHandler2>.Dispose(ref this._componentHandler2);
            ComRef<IComponentHandler3>.Dispose(ref this._componentHandler3);

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

            this._componentHandler = ComRef<IComponentHandler>.Create(handler);
            this._componentHandler2 = ComRef<IComponentHandler2>.Create(handler as IComponentHandler2);
            this._componentHandler3 = ComRef<IComponentHandler3>.Create(handler as IComponentHandler3);

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
