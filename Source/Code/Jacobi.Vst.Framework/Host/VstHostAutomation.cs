namespace Jacobi.Vst.Framework.Host
{
    using System;

    internal class VstHostAutomation : IVstHostAutomation
    {
        private VstHost _host;

        public VstHostAutomation(VstHost host)
        {
            _host = host;
        }

        #region IVstHostAutomation Members

        public int AutomationState
        {
            get { throw new System.NotImplementedException(); }
        }

        public IDisposable EditParemeter(VstParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
