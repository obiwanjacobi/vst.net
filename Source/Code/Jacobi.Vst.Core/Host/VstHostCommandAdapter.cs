using System;

namespace Jacobi.Vst.Core.Host
{
    /// <summary>
    /// The VstHostCommandAdapter class implements the Plugin <see cref="Jacobi.Vst.Core.Plugin.IVstHostCommandProxy"/>
    /// interface and forwards those calls to a <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation
    /// provided when the adapter class is constructed.
    /// </summary>
    public class VstHostCommandAdapter : Plugin.IVstHostCommandProxy
    {
        private IVstHostCommandStub? _hostCmdStub;

        /// <summary>
        /// Constructs a new instance based on the <paramref name="hostCmdStub"/>
        /// </summary>
        /// <param name="hostCmdStub">Will be used to forward calls to. Must not be null.</param>
        public VstHostCommandAdapter(IVstHostCommandStub hostCmdStub)
        {
            Throw.IfArgumentIsNull(hostCmdStub, nameof(hostCmdStub));

            _hostCmdStub = hostCmdStub;
        }

        #region IVstHostCommandStub Members

        /// <summary>
        /// This call is forwarded to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        /// <param name="pluginInfo">Passed with the forwarded call.</param>
        /// <returns>Returns the value returned from the forwarded call.</returns>
        public bool UpdatePluginInfo(Plugin.VstPluginInfo pluginInfo)
        {
            ThrowIfDisposed();
            _hostCmdStub!.PluginContext.PluginInfo = pluginInfo;

            return true;
        }

        public IVstHostCommands20 Commands
        {
            get { return _hostCmdStub.Commands; }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Clears the reference to the <see cref="Jacobi.Vst.Core.Host.IVstHostCommandStub"/> implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called to dispose this object instance.
        /// </summary>
        /// <param name="disposing">When true also disposes of managed resources. Otherwise only unmanaged resources are disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hostCmdStub = null;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_hostCmdStub == null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
        #endregion
    }
}
