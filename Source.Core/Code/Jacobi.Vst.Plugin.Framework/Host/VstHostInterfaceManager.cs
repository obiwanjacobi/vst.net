namespace Jacobi.Vst.Framework.Host
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;
    using System;

    /// <summary>
    /// This class manages the extensible interfaces for the host.
    /// </summary>
    /// <remarks>The supported host interfaces are:
    /// <see cref="IVstHostAutomation"/>, 
    /// <see cref="IVstHostSequencer"/>, <see cref="IVstHostShell"/> and
    /// <see cref="IVstMidiProcessor"/>.</remarks>
    internal class VstHostInterfaceManager : IExtensible, IDisposable
    {
        private readonly VstHost _host;

        private readonly InterfaceManager<IVstHostAutomation> _automation;
        private readonly InterfaceManager<IVstHostSequencer> _sequencer;
        private readonly InterfaceManager<IVstHostShell> _shell;
        private readonly InterfaceManager<IVstMidiProcessor> _midiProcessor;

        /// <summary>
        /// Constructs all the interface managers.
        /// </summary>
        private VstHostInterfaceManager()
        {
            _automation = new InterfaceManager<IVstHostAutomation>(CreateAutomation);
            _sequencer = new InterfaceManager<IVstHostSequencer>(CreateSequencer);
            _shell = new InterfaceManager<IVstHostShell>(CreateShell);
            _midiProcessor = new InterfaceManager<IVstMidiProcessor>(CreateMidiProcessor);
        }

        /// <summary>
        /// Constructs an instance based on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostInterfaceManager(VstHost host)
            : this()
        {
            Throw.IfArgumentIsNull(host, nameof(host));

            _host = host;
        }

        private IVstHostAutomation CreateAutomation(IVstHostAutomation instance)
        {
            if (instance == null) return new VstHostAutomation(_host);

            return instance;
        }

        private IVstHostSequencer CreateSequencer(IVstHostSequencer instance)
        {
            if (instance == null) return new VstHostSequencer(_host);

            return instance;
        }

        private IVstHostShell CreateShell(IVstHostShell instance)
        {
            if (instance == null) return new VstHostShell(_host);

            return instance;
        }

        private IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            if (instance == null &&
                _host.HostCommandStub.CanDo(VstCanDoHelper.ToString(VstHostCanDo.ReceiveVstMidiEvent)) != VstCanDoResult.No)
            {
                return new VstHostMidiProcessor(_host);
            }

            return instance;
        }

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return (GetInstance<T>() != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="IVstMidiProcessor"/> interface is requested
        /// and the plugin does not implement the <see cref="IVstPluginMidiSource"/> interface.</exception>
        public T GetInstance<T>() where T : class
        {
            ExtensibleInterfaceRef<IVstMidiProcessor> midiProcessor = _midiProcessor.MatchInterface<T>();
            if (midiProcessor != null)
            {
                if (!_host.Plugin.Supports<IVstPluginMidiSource>())
                {
                    throw new InvalidOperationException(
                        "A plugin cannot send events to the host when it does not implement IVstPluginMidiSource.");
                }

                return midiProcessor.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostSequencer> sequencer = _sequencer.MatchInterface<T>();
            if (sequencer != null)
            {
                return sequencer.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostAutomation> automation = _automation.MatchInterface<T>();
            if (automation != null)
            {
                return automation.Instance as T;
            }

            ExtensibleInterfaceRef<IVstHostShell> shell = _shell.MatchInterface<T>();
            if (shell != null)
            {
                return shell.Instance as T;
            }

            return _host.HostCommandStub as T;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _automation.Dispose();
            _midiProcessor.Dispose();
            _sequencer.Dispose();
            _shell.Dispose();
        }

        #endregion
    }
}
