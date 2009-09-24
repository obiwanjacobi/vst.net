namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// Forwards the calls of the <see cref="IVstHostDeprecated"/> interface to the Host Command Stub.
    /// </summary>
    internal class VstHostDeprecated : IVstHostDeprecated
    {
        private VstHost _host;

        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostDeprecated(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstHostDeprecated Members

        public bool WantMidi()
        {
            IVstHostCommandsDeprecated deprecatedCmds = _host.HostCommandStub as IVstHostCommandsDeprecated;

            if (deprecatedCmds != null)
            {
                deprecatedCmds.WantMidi();
                return true;
            }

            return false;
        }

        #endregion
    }
}
