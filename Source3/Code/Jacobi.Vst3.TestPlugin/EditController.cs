using System;
using System.Runtime.InteropServices;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.TestPlugin
{
    [System.ComponentModel.DisplayName("My Edit Controller")]
    [Guid("D74D670B-28B8-4AB2-9180-D4D12B52F54B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class EditController : IEditController
    {
        #region IEditController Members

        public int Initialize(object context)
        {
            return TResult.S_OK;
        }

        public int Terminate()
        {
            return TResult.S_OK;
        }

        public int SetComponentState(IBStream state)
        {
            return TResult.S_OK;
        }

        public int SetState(IBStream state)
        {
            return TResult.S_OK;
        }

        public int GetState(IBStream state)
        {
            return TResult.E_NotImplemented;
        }

        public int GetParameterCount()
        {
            return 0;
        }

        public int GetParameterInfo(int paramIndex, ref ParameterInfo info)
        {
            return TResult.E_NotImplemented;
        }

        public int GetParamStringByValue(uint paramId, double valueNormalized, System.Text.StringBuilder @string)
        {
            return TResult.E_NotImplemented;
        }

        public int GetParamValueByString(uint paramId, string @string, ref double valueNormalized)
        {
            return TResult.E_NotImplemented;
        }

        public double NormalizedParamToPlain(uint paramId, double valueNormalized)
        {
            return TResult.E_NotImplemented;
        }

        public double PlainParamToNormalized(uint paramId, double plainValue)
        {
            return TResult.E_NotImplemented;
        }

        public double GetParamNormalized(uint paramId)
        {
            return TResult.E_NotImplemented;
        }

        public int SetParamNormalized(int paramIndex, double value)
        {
            return TResult.E_NotImplemented;
        }

        private IComponentHandler _handler;

        public int SetComponentHandler(IComponentHandler handler)
        {
            _handler = handler;
            return TResult.S_OK;
        }

        public IPlugView CreateView(string name)
        {
            return null;
        }

        #endregion
    }
}
