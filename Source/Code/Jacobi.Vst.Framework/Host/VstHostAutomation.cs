namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// Forwards the <see cref="IVstHostAutomation"/> calls to the host stub.
    /// </summary>
    internal class VstHostAutomation : IVstHostAutomation
    {
        private VstHost _host;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
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

        public void SetParameterAutomated(int parameterIndex, float value)
        {
            _host.HostCommandStub.SetParameterAutomated(parameterIndex, value);
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for <see cref="EditParameter"/>.
        /// </summary>
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

            /// <summary>
            /// Called by the client when done with edit.
            /// </summary>
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
