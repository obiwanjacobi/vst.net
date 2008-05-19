namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using Jacobi.Vst.Framework.Common;

    public abstract class PluginInterfaceManagerBase : IExtensibleObject, IDisposable
    {
        private InterfaceManager<IVstPluginAudioPrecissionProcessor> _audioPrecission;
        private InterfaceManager<IVstPluginAudioProcessor> _audioProcessor;
        private InterfaceManager<IVstPluginBypass> _bypass;
        private InterfaceManager<IVstPluginConnections> _connections;
        private InterfaceManager<IVstPluginEditor> _editor;
        private InterfaceManager<IVstPluginHost> _host;
        private InterfaceManager<IVstPluginMidiPrograms> _midiPrograms;
        private InterfaceManager<IVstPluginMidiSource> _midiSource;
        private InterfaceManager<IVstPluginOfflineProcessor> _offlineProcessor;
        private InterfaceManager<IVstPluginParameters> _parameters;
        private InterfaceManager<IVstPluginPersistence> _persistence;
        private InterfaceManager<IVstPluginProcess> _process;
        private InterfaceManager<IVstPluginPrograms> _programs;

        public PluginInterfaceManagerBase()
        {
            _audioPrecission = new InterfaceManager<IVstPluginAudioPrecissionProcessor>(CreateAudioPrecissionProcessor);
            _audioProcessor = new InterfaceManager<IVstPluginAudioProcessor>(CreateAudioProcessor);
            _bypass = new InterfaceManager<IVstPluginBypass>(CreateBypass);
            _connections = new InterfaceManager<IVstPluginConnections>(CreateConnections);
            _editor = new InterfaceManager<IVstPluginEditor>(CreateEditor);
            _host = new InterfaceManager<IVstPluginHost>(CreateHost);
            _midiPrograms = new InterfaceManager<IVstPluginMidiPrograms>(CreateMidiPrograms);
            _midiSource = new InterfaceManager<IVstPluginMidiSource>(CreateMidiSource);
            _offlineProcessor = new InterfaceManager<IVstPluginOfflineProcessor>(CreateOfflineProcessor);
            _parameters = new InterfaceManager<IVstPluginParameters>(CreateParameters);
            _persistence = new InterfaceManager<IVstPluginPersistence>(CreatePersistence);
            _process = new InterfaceManager<IVstPluginProcess>(CreateProcess);
            _programs = new InterfaceManager<IVstPluginPrograms>(CreatePrograms);
        }

        protected virtual IVstPluginAudioProcessor CreateAudioProcessor(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginAudioPrecissionProcessor CreateAudioPrecissionProcessor(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginBypass CreateBypass(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginConnections CreateConnections(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginEditor CreateEditor(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginHost CreateHost(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginMidiPrograms CreateMidiPrograms(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginMidiSource CreateMidiSource(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginOfflineProcessor CreateOfflineProcessor(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginParameters CreateParameters(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginPersistence CreatePersistence(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginProcess CreateProcess(bool threadSafe)
        {
            return null;
        }

        protected virtual IVstPluginPrograms CreatePrograms(bool threadSafe)
        {
            return null;
        }

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return (GetInstance<T>() != null);
        }

        public T GetInstance<T>() where T : class
        {
            // interfaces are more or less ordered in priority

            ExtensibleInterfaceRef<IVstPluginAudioPrecissionProcessor> audioPrecission = _audioPrecission.MatchInterface<T>();
            if (audioPrecission != null) return audioPrecission.Instance as T;

            ExtensibleInterfaceRef<IVstPluginAudioProcessor> audioProcessor  = _audioProcessor.MatchInterface<T>();
            if (audioProcessor != null) return audioProcessor.Instance as T;

            ExtensibleInterfaceRef<IVstPluginPrograms> programs = _programs.MatchInterface<T>();
            if (programs != null) return programs.Instance as T;

            ExtensibleInterfaceRef<IVstPluginParameters> parameters = _parameters.MatchInterface<T>();
            if (parameters != null)
            {
                // special case for parameters
                if (parameters.Instance == null)
                {
                    programs = _programs.GetInterface();

                    // return the parameters of the active program (if there is one)
                    if (programs.Instance != null && programs.Instance.ActiveProgram != null)
                    {
                        return programs.Instance.ActiveProgram as T;
                    }
                }

                return parameters.Instance as T;
            }

            ExtensibleInterfaceRef<IVstPluginMidiPrograms> midiPrograms = _midiPrograms.MatchInterface<T>();
            if (midiPrograms != null) return midiPrograms.Instance as T;

            ExtensibleInterfaceRef<IVstPluginMidiSource> midiSource = _midiSource.MatchInterface<T>();
            if (midiSource != null) return midiSource.Instance as T;

            ExtensibleInterfaceRef<IVstPluginProcess> process = _process.MatchInterface<T>();
            if (process != null) return process.Instance as T;

            ExtensibleInterfaceRef<IVstPluginBypass> bypass = _bypass.MatchInterface<T>();
            if (bypass != null) return bypass.Instance as T;

            ExtensibleInterfaceRef<IVstPluginConnections> connections = _connections.MatchInterface<T>();
            if (connections != null) return connections.Instance as T;

            ExtensibleInterfaceRef<IVstPluginEditor> editor = _editor.MatchInterface<T>();
            if (editor != null) return editor.Instance as T;

            ExtensibleInterfaceRef<IVstPluginHost> host = _host.MatchInterface<T>();
            if (host != null) return host.Instance as T;

            ExtensibleInterfaceRef<IVstPluginOfflineProcessor> offlineProcessor = _offlineProcessor.MatchInterface<T>();
            if (offlineProcessor != null) return offlineProcessor.Instance as T;

            ExtensibleInterfaceRef<IVstPluginPersistence> persistence = _persistence.MatchInterface<T>();
            if (persistence != null) return persistence.Instance as T;

            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _audioPrecission.Dispose();
            _audioProcessor.Dispose();
            _bypass.Dispose();
            _connections.Dispose();
            _editor.Dispose();
            _host.Dispose();
            _midiPrograms.Dispose();
            _midiSource.Dispose();
            _offlineProcessor.Dispose();
            _parameters.Dispose();
            _persistence.Dispose();
            _process.Dispose();
            _programs.Dispose();
        }

        #endregion

        //---------------------------------------------------------------------

        private class InterfaceManager<T> : IDisposable
            where T : class
        {
            public delegate T CreateInterface(bool threadSafe);

            private ExtensibleInterfaceRef<T> _interfaceRef;
            private CreateInterface _createCallback;

            public InterfaceManager(CreateInterface createCallback)
            {
                _createCallback = createCallback;
            }

            public ExtensibleInterfaceRef<T> MatchInterface<T_Intf>() where T_Intf : class
            {
                if (ExtensibleInterfaceRef<T>.IsMatch<T_Intf>())
                {
                    return GetInterface();
                }

                return null;
            }

            public ExtensibleInterfaceRef<T> GetInterface()
            {
                if (_interfaceRef == null)
                {
                    _interfaceRef = new ExtensibleInterfaceRef<T>();

                    _interfaceRef.Instance = _createCallback(false);
                }
                else if (!_interfaceRef.IsConstructionThread && _interfaceRef.ThreadSafeInstance == null)
                {
                    _interfaceRef.ThreadSafeInstance = _createCallback(true);

                    // use normal instance when no special thread safe instance was created.
                    if (_interfaceRef.ThreadSafeInstance == null)
                    {
                        _interfaceRef.ThreadSafeInstance = _interfaceRef.Instance;
                    }
                }

                return _interfaceRef;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (_interfaceRef != null)
                {
                    _interfaceRef.Dispose();
                    _interfaceRef = null;
                }

                _createCallback = null;
            }

            #endregion
        }
    }
}
