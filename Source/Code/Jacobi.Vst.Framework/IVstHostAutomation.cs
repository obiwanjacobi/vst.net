namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;

    public interface IVstHostAutomation
    {
        VstAutomationStates AutomationState { get; }
        IDisposable EditParameter(int parameterIndex);
    }
}
