namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System;

    /// <summary>
    /// Forwards the <see cref="IVstHostAutomation"/> calls to the host stub.
    /// </summary>
    internal sealed class VstHostAutomation : IVstHostAutomation
    {
        private readonly IVstHostCommands20 _commands;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="commands">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="commands"/> is not set to an instance of an object.</exception>
        public VstHostAutomation(IVstHostCommands20 commands)
        {
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        #region IVstHostAutomation Members

        public VstAutomationStates AutomationState
        {
            get { return _commands.GetAutomationState(); }
        }

        public IDisposable? BeginEditParameter(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, nameof(parameter));

            if (_commands.BeginEdit(parameter.Index))
            {
                return new EditParameterScope(_commands, parameter.Index);
            }

            return null;
        }

        public void NotifyParameterValueChanged(VstParameter parameter)
        {
            Throw.IfArgumentIsNull(parameter, nameof(parameter));

            _commands.SetParameterAutomated(parameter.Index, parameter.Value);
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for <see cref="BeginEditParameter"/>.
        /// </summary>
        private sealed class EditParameterScope : IDisposable
        {
            private IVstHostCommands20? _commands;
            private readonly int _index;

            public EditParameterScope(IVstHostCommands20 commands, int index)
            {
                _commands = commands ?? throw new ArgumentNullException(nameof(commands));
                _index = index;
            }

            #region IDisposable Members

            /// <summary>
            /// Called by the client when done with edit.
            /// </summary>
            public void Dispose()
            {
                if (_commands != null)
                {
                    _commands.EndEdit(_index);
                    _commands = null;
                }
            }

            #endregion
        }
    }
}
