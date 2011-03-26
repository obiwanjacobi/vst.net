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

        public IDisposable BeginEditParameter(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, "parameter");

            if (_host.HostCommandStub.BeginEdit(parameter.Index))
            {
                return new EditParameterScope(_host, parameter.Index);
            }

            return null;
        }

        public void NotifyParameterValueChange(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, "parameter");

            _host.HostCommandStub.SetParameterAutomated(parameter.Index, parameter.Value);
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for <see cref="BeginEditParameter"/>.
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
