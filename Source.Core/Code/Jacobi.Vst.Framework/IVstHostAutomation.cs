namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    /// <summary>
    /// Provides access to the autiomation functionality of the host.
    /// </summary>
    public interface IVstHostAutomation
    {
        /// <summary>
        /// Gets the current automation state.
        /// </summary>
        VstAutomationStates AutomationState { get; }

        /// <summary>
        /// Starts an 'edit parameter' scope.
        /// </summary>
        /// <param name="parameter">The parameter to notify the host for. Must not be null.</param>
        /// <returns>Returns a tracker object to mark the end of the edit scope. 
        /// Can return null if the host does not support this.</returns>
        /// <remarks>Call <see cref="IDisposable.Dispose"/> on the return value to end the scope.</remarks>
        IDisposable BeginEditParameter(VstParameter parameter);

        /// <summary>
        /// Notifies the Host that the value of the <paramref name="parameter"/>.
        /// </summary>
        /// <param name="parameter">Must not be null.</param>
        /// <remarks>
        /// The plugin can call this method to allow the parameter value change to be automated by the host.
        /// Assign the new value to the <see cref="VstParameter"/> instance and pass it to this method to notify
        /// the host of the value change.
        /// </remarks>
        void NotifyParameterValueChanged(VstParameter parameter);
    }
}
