namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System;

    /// <summary>
    /// Forwards the <see cref="IVstHostAutomation"/> calls to the host stub.
    /// </summary>
    internal sealed class VstHostAutomation : IVstHostAutomation
    {
        private readonly VstHost _host;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostAutomation(VstHost host)
        {
            Throw.IfArgumentIsNull(host, nameof(host));

            _host = host;
        }

        #region IVstHostAutomation Members

        public VstAutomationStates AutomationState
        {
            get { return _host.HostCommandProxy.Commands.GetAutomationState(); }
        }

        public IDisposable? BeginEditParameter(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, nameof(parameter));

            if (_host.HostCommandProxy.Commands.BeginEdit(parameter.Index))
            {
                return new EditParameterScope(_host, parameter.Index);
            }

            return null;
        }

        public void NotifyParameterValueChanged(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, nameof(parameter));

            _host.HostCommandProxy.Commands.SetParameterAutomated(parameter.Index, parameter.Value);
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for <see cref="BeginEditParameter"/>.
        /// </summary>
        private sealed class EditParameterScope : IDisposable
        {
            private VstHost? _host;
            private readonly int _index;

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
                    _host.HostCommandProxy.Commands.EndEdit(_index);
                    _host = null;
                }
            }

            #endregion
        }
    }
}
