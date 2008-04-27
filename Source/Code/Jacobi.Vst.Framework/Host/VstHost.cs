namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Core;

    internal class VstHost : IExtensibleObject, IVstHost, IDisposable
    {
        private VstHostInterfaceManager _intfMgr;

        public VstHost(IVstHostCommandStub hostCmdStub)
        {
            if (hostCmdStub == null)
            {
                throw new ArgumentNullException("hostCmdStub");
            }

            HostCommandStub = hostCmdStub;

            _intfMgr = new VstHostInterfaceManager(this);
        }

        public IVstHostCommandStub HostCommandStub { get; private set; }

        #region IVstHost Members

        private ProductInfo _productInfo;
        public ProductInfo ProductInfo
        {
            get
            {
                if (_productInfo == null)
                {
                    _productInfo = new ProductInfo(
                        HostCommandStub.GetProductString(),
                        HostCommandStub.GetVendorString(),
                        HostCommandStub.GetVendorVersion());
                }

                return _productInfo;
            }
        }

        private VstHostCapabilities _hostCapabilities;
        public VstHostCapabilities Capabilities
        {
            get
            {
                if (_hostCapabilities == VstHostCapabilities.None)
                {
                    // IvstHostIO
                    if (HostCommandStub.CanDo(VstHostCanDo.AcceptIOChanges) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.AcceptIoChanges;
                    // IVstHostOfflineProcessor
                    if (HostCommandStub.CanDo(VstHostCanDo.Offline) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.Offline;
                    // IVstHostShell.OpenFileSelector
                    if (HostCommandStub.CanDo(VstHostCanDo.OpenFileSelector) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.OpenFileSelector;
                    // IVstMidiProcessor implemented on Host
                    if (HostCommandStub.CanDo(VstHostCanDo.ReceiveVstEvents) == VstCanDoResult.Yes &&
                        HostCommandStub.CanDo(VstHostCanDo.ReceiveVstMidiEvent) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReceiveMidiEvents;
                    // will call IVstPluginConnections
                    if (HostCommandStub.CanDo(VstHostCanDo.ReportConnectionChanges) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.ReportConnectionChanges;
                    // will call IVstMidiProcessor implemented on plugin
                    if (HostCommandStub.CanDo(VstHostCanDo.SendVstEvents) == VstCanDoResult.Yes &&
                        HostCommandStub.CanDo(VstHostCanDo.SendVstMidiEvent) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendMidiEvents;
                    // Realtime flag set in VstMidiEvent
                    if (HostCommandStub.CanDo(VstHostCanDo.SendVstMidiEventFlagIsRealtime) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.RealtimeMidiFlag;
                    // GetTimeInfo works?
                    if (HostCommandStub.CanDo(VstHostCanDo.SendVstTimeInfo) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SendTimeInfo;
                    // will call IVstPluginHost
                    if (HostCommandStub.CanDo(VstHostCanDo.ShellCategory) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.PluginHost;
                    // IVstHostShell.SizeWindow works
                    if (HostCommandStub.CanDo(VstHostCanDo.SizeWindow) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.SizeWindow;
                    // will call IVstPluginProcess
                    if (HostCommandStub.CanDo(VstHostCanDo.StartStopProcess) == VstCanDoResult.Yes)
                        _hostCapabilities |= VstHostCapabilities.StartStopProcess;
                }

                return _hostCapabilities;
            }
        }

        #endregion

        #region IExtensibleObject Members

        public bool Supports<T>(bool threadSafe) where T : class
        {
            return _intfMgr.Supports<T>(threadSafe);
        }

        public T GetInstance<T>(bool threadSafe) where T : class
        {
            return _intfMgr.GetInstance<T>(threadSafe);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            HostCommandStub = null;
        }

        #endregion

    }
}
