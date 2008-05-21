namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Core;

    internal class VstHostAutomation : IVstHostAutomation
    {
        private VstHost _host;

        public VstHostAutomation(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstHostAutomation Members

        public VstAutomationStates AutomationState
        {
            get { return _host.HostCommandStub.GetAutomationState(); }
        }

        public IDisposable EditParameter(int parameterIndex)
        {
            if (_host.HostCommandStub.BeginEdit(parameterIndex))
            {
                return new EditParameterScope(_host, parameterIndex);
            }

            return null;
        }

        #endregion

        private class EditParameterScope : IDisposable
        {
            private VstHost _host;
            private int _index;

            public EditParameterScope(VstHost host, int index)
            {
                _host = host;
                _index = index;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (_host != null)
                {
                    _host.HostCommandStub.EndEdit(_index);
                    _host = null;
                }
            }

            #endregion
        }
    }
}
