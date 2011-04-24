namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;

    /// <summary>
    /// Implements the proxy to the vst host.
    /// </summary>
    internal class VstHost : IExtensible, IVstHost, IDisposable
    {
        /// <summary>Interface manager for the host.</summary>
        private VstHostInterfaceManager _intfMgr;

        /// <summary>
        /// Constructs a new instance of the host class based on the <paramref name="hostCmdStub"/> 
        /// (from Interop) and a reference to the current <paramref name="plugin"/>.
        /// </summary>
        /// <param name="hostCmdStub">Must not be null.</param>
        /// <param name="plugin">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="hostCmdStub"/> or 
        /// <paramref name="plugin"/> is not set to an instance of an object.</exception>
        public VstHost(IVstHostCommandStub hostCmdStub, IVstPlugin plugin)
        {
            Throw.IfArgumentIsNull(hostCmdStub, "hostCmdStub");
            Throw.IfArgumentIsNull(plugin, "plugin");

            HostCommandStub = hostCmdStub;
            Plugin = plugin;

            _intfMgr = new VstHostInterfaceManager(this);
        }

        /// <summary>
        /// Gets the Host Command Stub (Interop).
        /// </summary>
        public IVstHostCommandStub HostCommandStub { get; private set; }
        /// <summary>
        /// Gets the current Plugin instance.
        /// </summary>
        public IVstPlugin Plugin { get; private set; }

        #region IVstHost Members

        private VstProductInfo _productInfo;
        /// <summary>
        /// Gets the product information of the vst host.
        /// </summary>
        /// <remarks>
        /// Implemented lazy with caching. First-time call will fire 3 callbacks to the host.
        /// </remarks>
        public VstProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new VstProductInfo(
                        HostCommandStub.GetProductString(),
                        HostCommandStub.GetVendorString(),
                        HostCommandStub.GetVendorVersion());
                }

                return _productInfo;
            }
        }

        private VstHostCapabilities _hostCapabilities;
        /// <summary>
        /// Gets the vst host capabilities.
        /// </summary>
        /// <remarks>
        /// Implemented lazy with caching. Fires multiple CanDo requests at the host.
        /// </remarks>
        public VstHostCapabilities Capabilities
        {
            get
            {
                if (_hostCapabilities == VstHostCapabilities.None)
                {
                    // IVstHostSequencer.UpdatePluginIO works
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.AcceptIOChanges)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.AcceptIoChanges;
                    // IVstHostOfflineProcessor
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.Offline)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.Offline;
                    // IVstHostShell.OpenFileSelector
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.OpenFileSelector)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.OpenFileSelector;
                    // IVstMidiProcessor implemented on Host
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReceiveVstMidiEvent)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReceiveMidiEvents;
                    // will call IVstPluginConnections
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReportConnectionChanges)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReportConnectionChanges;
                    // will call IVstMidiProcessor implemented on plugin
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstMidiEvent)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendMidiEvents;
                    // Realtime flag set in VstMidiEvent
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstMidiEventFlagIsRealtime)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.RealtimeMidiFlag;
                    // GetTimeInfo works?
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstTimeInfo)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendTimeInfo;
                    // will call IVstPluginHost
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ShellCategory)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.PluginHost;
                    // IVstHostShell.SizeWindow works
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SizeWindow)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SizeWindow;
                    // will call IVstPluginProcess
                    if (HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.StartStopProcess)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.StartStopProcess;
                }

                return _hostCapabilities;
            }
        }

        /// <summary>
        /// Gets the vst host thread that is currently executing the plugin code (calling this property).
        /// </summary>
        public VstProcessLevels ProcessLevel
        {
            get { return HostCommandStub.GetProcessLevel(); }
        }

        #endregion

        #region IExtensibleObject Members

        /// <summary>
        /// Indicates wheather a interface (or class) is supported by the host.
        /// </summary>
        /// <typeparam name="T">The type of interface or class.</typeparam>
        /// <returns>Returns true when the type <typeparamref name="T"/> is supported.</returns>
        public bool Supports<T>() where T : class
        {
            if (typeof(IVstHostCommandStub).IsAssignableFrom(typeof(T))) 
                return ((HostCommandStub as T) != null);

            return _intfMgr.Supports<T>();
        }

        /// <summary>
        /// Retrieves an instance (or null) for the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of interface or class.</typeparam>
        /// <returns>Returns null when <typeparamref name="T"/> is not supported.</returns>
        public T GetInstance<T>() where T : class
        {
            if (typeof(IVstHostCommandStub).IsAssignableFrom(typeof(T))) 
                return HostCommandStub as T;

            return _intfMgr.GetInstance<T>();
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Called to dispose of this host instance.
        /// </summary>
        public void Dispose()
        {
            if (_intfMgr != null)
            {
                _intfMgr.Dispose();
                _intfMgr = null;
            }

            if (HostCommandStub != null)
            {
                HostCommandStub.Dispose();
                HostCommandStub = null;
            }

            Plugin = null;
        }

        #endregion

    }
}
