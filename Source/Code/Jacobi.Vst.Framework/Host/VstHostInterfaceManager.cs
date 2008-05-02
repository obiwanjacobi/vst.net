namespace Jacobi.Vst.Framework.Host
{
    using System;
    using Jacobi.Vst.Framework.Common;

    internal class VstHostInterfaceManager : IExtensibleObject, IDisposable
    {
        private VstHost _host;

        public VstHostInterfaceManager(VstHost host)
        {
            _host = host;
        }

        private ExtensibleInterfaceRef<IVstHostShell> _shell;
        private ExtensibleInterfaceRef<IVstHostShell> GetShell<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstHostShell>.IsMatch<T>())
            {
                if (_shell == null)
                {
                    _shell = new ExtensibleInterfaceRef<IVstHostShell>();
                    _shell.Instance = new VstHostShell(_host);
                    _shell.ThreadSafeInstance = _shell.Instance;
                }

                return _shell;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstHostSequencer> _sequencer;
        private ExtensibleInterfaceRef<IVstHostSequencer> GetSequencer<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstHostSequencer>.IsMatch<T>())
            {
                if (_sequencer == null)
                {
                    _sequencer = new ExtensibleInterfaceRef<IVstHostSequencer>();
                    _sequencer.Instance = new VstHostSequencer(_host);
                    _sequencer.ThreadSafeInstance = _sequencer.Instance;
                }

                return _sequencer;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstHostAutomation> _automation;
        private ExtensibleInterfaceRef<IVstHostAutomation> GetAutomation<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstHostAutomation>.IsMatch<T>())
            {
                if (_automation == null)
                {
                    _automation = new ExtensibleInterfaceRef<IVstHostAutomation>();
                    _automation.Instance = new VstHostAutomation(_host);
                    _automation.ThreadSafeInstance = _automation.Instance;
                }

                return _automation;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstHostIO> _io;
        private ExtensibleInterfaceRef<IVstHostIO> GetIO<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstHostIO>.IsMatch<T>())
            {
                if (_io == null)
                {
                    _io = new ExtensibleInterfaceRef<IVstHostIO>();
                    if ((_host.Capabilities & VstHostCapabilities.AcceptIoChanges) > 0)
                    {
                        _io.Instance = new VstHostIO(_host);
                        _io.ThreadSafeInstance = _io.Instance;
                    }
                }

                return _io;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstHostOfflineProcessor> _offline;
        private ExtensibleInterfaceRef<IVstHostOfflineProcessor> GetOffline<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstHostOfflineProcessor>.IsMatch<T>())
            {
                if (_offline == null)
                {
                    _offline = new ExtensibleInterfaceRef<IVstHostOfflineProcessor>();
                    if ((_host.Capabilities & VstHostCapabilities.Offline) > 0)
                    {
                        _offline.Instance = new VstHostOfflineProcessor(_host);
                        _offline.ThreadSafeInstance = _offline.Instance;
                    }
                }

                return _offline;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstMidiProcessor> _midiProcessor;
        private ExtensibleInterfaceRef<IVstMidiProcessor> GetMidiProcessor<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstMidiProcessor>.IsMatch<T>())
            {
                if (_midiProcessor == null)
                {
                    _midiProcessor = new ExtensibleInterfaceRef<IVstMidiProcessor>();
                    if ((_host.Capabilities & VstHostCapabilities.ReceiveMidiEvents) > 0)
                    {
                        _midiProcessor.Instance = new VstHostMidiProcessor(_host);
                        _midiProcessor.ThreadSafeInstance = _midiProcessor.Instance;
                    }
                }

                return _midiProcessor;
            }

            return null;
        }

        #region IExtensibleObject Members

        public bool Supports<T>(bool threadSafe) where T : class
        {
            ExtensibleInterfaceRef<IVstHostShell> shell = GetShell<T>();
            if (shell != null)
            {
                return (shell.Instance != null);
            }

            ExtensibleInterfaceRef<IVstHostSequencer> sequencer = GetSequencer<T>();
            if (sequencer != null)
            {
                return (sequencer.Instance != null);
            }

            ExtensibleInterfaceRef<IVstHostAutomation> automation = GetAutomation<T>();
            if (automation != null)
            {
                return (automation.Instance != null);
            }

            ExtensibleInterfaceRef<IVstHostIO> io = GetIO<T>();
            if(io != null)
            {
                return (io.Instance != null);
            }

            ExtensibleInterfaceRef<IVstHostOfflineProcessor> offline = GetOffline<T>();
            if (offline != null)
            {
                return (offline.Instance != null);
            }

            ExtensibleInterfaceRef<IVstMidiProcessor> midiProcessor = GetMidiProcessor<T>();
            if (midiProcessor != null)
            {
                return (midiProcessor.Instance != null);
            }

            return false;
        }

        public T GetInstance<T>(bool threadSafe) where T : class
        {
            ExtensibleInterfaceRef<IVstHostShell> shell = GetShell<T>();
            if (shell != null)
            {
                return shell.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostSequencer> sequencer = GetSequencer<T>();
            if (sequencer != null)
            {
                return sequencer.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostAutomation> automation = GetAutomation<T>();
            if (automation != null)
            {
                return automation.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostIO> io = GetIO<T>();
            if (io != null)
            {
                return io.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostOfflineProcessor> offline = GetOffline<T>();
            if (offline != null)
            {
                return offline.Instance as T;
            }

            ExtensibleInterfaceRef<IVstMidiProcessor> midiProcessor = GetMidiProcessor<T>();
            if (midiProcessor != null)
            {
                if (!_host.Plugin.Supports<IVstPluginMidiSource>())
                {
                    throw new InvalidOperationException(
                        "A plugin cannot send events to the host when it does not implement IVstPluginMidiSource.");
                }

                return midiProcessor.Instance as T;
            }

            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_automation != null)
            {
                _automation.Dispose();
                _automation = null;
            }
            
            if (_io != null)
            {
                _io.Dispose();
                _io = null;
            }

            if (_midiProcessor != null)
            {
                _midiProcessor.Dispose();
                _midiProcessor = null;
            }

            if (_offline != null)
            {
                _offline.Dispose();
                _offline = null;
            }

            if (_sequencer != null)
            {
                _sequencer.Dispose();
                _sequencer = null;
            }

            if (_shell != null)
            {
                _shell.Dispose();
                _shell = null;
            }
        }

        #endregion
    }
}
