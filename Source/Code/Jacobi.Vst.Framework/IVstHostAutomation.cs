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
        /// <param name="parameterIndex">The index of the parameter to notify the host for.</param>
        /// <returns>Returns a tracker object to mark the end of the edit scope.</returns>
        /// <remarks>Call <see cref="IDisposable.Dispose"/> on the return value to end the scope.</remarks>
        IDisposable EditParameter(int parameterIndex);

        /// <summary>
        /// Notifies the Host that the value of the parameter at <paramref name="parameterIndex"/> has a new <paramref name="value"/>.
        /// </summary>
        /// <param name="parameterIndex">Must be greater/equal to zero and smaller than the parameter count.</param>
        /// <param name="value">The new value assigned to the parameter.</param>
        /// <remarks>The plugin can call this method to allow the parameter value change to be automated by the host.</remarks>
        void SetParameterAutomated(int parameterIndex, float value);
    }
}
