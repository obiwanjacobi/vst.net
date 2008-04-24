namespace Jacobi.Vst.Framework
{
    using System;

    public interface IVstHostAutomation
    {
        int AutomationState { get;} // return enum
        IDisposable EditParemeter(VstParameter parameter);
    }
}
