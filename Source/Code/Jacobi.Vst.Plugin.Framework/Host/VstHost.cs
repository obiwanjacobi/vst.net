namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Core.Plugin;
    using System;

    /// <summary>
    /// Implements the proxy to the vst host.
    /// </summary>
    internal sealed class VstHost : IVstHost, IDisposable
    {
        private readonly IVstHostAutomation _automation;
        private readonly IVstHostSequencer _sequencer;
        private readonly IVstHostShell _shell;
        private readonly IVstMidiProcessor _midiProcessor;

        /// <summary>
        /// Constructs a new instance of the host class based on the <paramref name="hostCmdProxy"/> 
        /// (from Interop) and a reference to the current <paramref name="plugin"/>.
        /// </summary>
        /// <param name="hostCmdProxy">Must not be null.</param>
        /// <param name="plugin">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="hostCmdProxy"/> or 
        /// <paramref name="plugin"/> is not set to an instance of an object.</exception>
        public VstHost(IVstHostCommandProxy hostCmdProxy, IVstPlugin plugin)
        {
            Throw.IfArgumentIsNull(hostCmdProxy, nameof(hostCmdProxy));
            Throw.IfArgumentIsNull(plugin, nameof(plugin));

            HostCommandProxy = hostCmdProxy;
            Plugin = plugin;

            _automation = new VstHostAutomation(this);
            _sequencer = new VstHostSequencer(this);
            _shell = new VstHostShell(this);
            _midiProcessor = new VstHostMidiProcessor(this);
        }

        /// <summary>
        /// Gets the Host Command Stub (Interop).
        /// </summary>
        public IVstHostCommandProxy HostCommandProxy { get; private set; }
        /// <summary>
        /// Gets the current Plugin instance.
        /// </summary>
        public IVstPlugin Plugin { get; private set; }

        #region IVstHost Members

        private VstProductInfo? _productInfo;
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
                        HostCommandProxy.Commands.GetProductString(),
                        HostCommandProxy.Commands.GetVendorString(),
                        HostCommandProxy.Commands.GetVendorVersion());
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
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.AcceptIOChanges)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.AcceptIoChanges;
                    // IVstHostOfflineProcessor
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.Offline)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.Offline;
                    // IVstHostShell.OpenFileSelector
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.OpenFileSelector)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.OpenFileSelector;
                    // IVstMidiProcessor implemented on Host
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReceiveVstMidiEvent)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReceiveMidiEvents;
                    // will call IVstPluginConnections
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReportConnectionChanges)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReportConnectionChanges;
                    // will call IVstMidiProcessor implemented on plugin
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstMidiEvent)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendMidiEvents;
                    // Realtime flag set in VstMidiEvent
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstMidiEventFlagIsRealtime)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.RealtimeMidiFlag;
                    // GetTimeInfo works?
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SendVstTimeInfo)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendTimeInfo;
                    // will call IVstPluginHost
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ShellCategory)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.PluginHost;
                    // IVstHostShell.SizeWindow works
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.SizeWindow)) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SizeWindow;
                    // will call IVstPluginProcess
                    if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.StartStopProcess)) == VstCanDoResult.Yes)
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
            get { return HostCommandProxy.Commands.GetProcessLevel(); }
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
            if (HostCommandProxy is T) return true;
            return GetInstance<T>() != null;
        }

        /// <summary>
        /// Retrieves an instance (or null) for the specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of interface or class.</typeparam>
        /// <returns>Returns null when <typeparamref name="T"/> is not supported.</returns>
        public T? GetInstance<T>() where T : class
        {
            if (HostCommandProxy is T refT) return refT;

            var type = typeof(T);

            if (typeof(IVstHostAutomation).Equals(type))
            {
                return (T)_automation;
            }
            if (typeof(IVstHostSequencer).Equals(type))
            {
                return (T)_sequencer;
            }
            if (typeof(IVstHostShell).Equals(type))
            {
                return (T)_shell;
            }
            if (typeof(IVstMidiProcessor).Equals(type))
            {
                CheckMidiSource();

                // does host support MIDI?
                if (HostCommandProxy.Commands.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReceiveVstMidiEvent)) != VstCanDoResult.No)
                {
                    return (T)_midiProcessor;
                }
            }

            return null;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Called to dispose of this host instance.
        /// </summary>
        public void Dispose()
        {
            HostCommandProxy.Dispose();
        }

        #endregion

        private void CheckMidiSource()
        {
            if (!Plugin.Supports<IVstPluginMidiSource>())
            {
                // TODO: Resource?
                throw new InvalidOperationException(
                    "A plugin cannot send events to the host when it does not implement IVstPluginMidiSource.");
            }
        }
    }
}
