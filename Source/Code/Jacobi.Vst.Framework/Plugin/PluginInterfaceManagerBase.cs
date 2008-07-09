namespace Jacobi.Vst.Framework.Plugin
{
    using System;
    using Jacobi.Vst.Framework.Common;

    public abstract class PluginInterfaceManagerBase : IExtensible, IDisposable
    {
        private InterfaceManager<IVstPluginAudioPrecissionProcessor> _audioPrecission;
        private InterfaceManager<IVstPluginAudioProcessor> _audioProcessor;
        private InterfaceManager<IVstPluginBypass> _bypass;
        private InterfaceManager<IVstPluginConnections> _connections;
        private InterfaceManager<IVstPluginEditor> _editor;
        private InterfaceManager<IVstPluginHost> _host;
        private InterfaceManager<IVstMidiProcessor> _midiProcessor;
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
            _audioPrecission.DisposeParent = this;
            
            _audioProcessor = new InterfaceManager<IVstPluginAudioProcessor>(CreateAudioProcessor);
            _audioProcessor.DisposeParent = this;
            
            _bypass = new InterfaceManager<IVstPluginBypass>(CreateBypass);
            _bypass.DisposeParent = this;
            
            _connections = new InterfaceManager<IVstPluginConnections>(CreateConnections);
            _connections.DisposeParent = this;
            
            _editor = new InterfaceManager<IVstPluginEditor>(CreateEditor);
            _editor.DisposeParent = this;
            
            _host = new InterfaceManager<IVstPluginHost>(CreateHost);
            _host.DisposeParent = this;
            
            _midiProcessor = new InterfaceManager<IVstMidiProcessor>(CreateMidiProcessor);
            _midiProcessor.DisposeParent = this;
            
            _midiPrograms = new InterfaceManager<IVstPluginMidiPrograms>(CreateMidiPrograms);
            _midiPrograms.DisposeParent = this;
            
            _midiSource = new InterfaceManager<IVstPluginMidiSource>(CreateMidiSource);
            _midiSource.DisposeParent = this;
            
            _offlineProcessor = new InterfaceManager<IVstPluginOfflineProcessor>(CreateOfflineProcessor);
            _offlineProcessor.DisposeParent = this;
            
            _parameters = new InterfaceManager<IVstPluginParameters>(CreateParameters);
            _parameters.DisposeParent = this;
            
            _persistence = new InterfaceManager<IVstPluginPersistence>(CreatePersistence);
            _persistence.DisposeParent = this;
            
            _process = new InterfaceManager<IVstPluginProcess>(CreateProcess);
            _process.DisposeParent = this;
            
            _programs = new InterfaceManager<IVstPluginPrograms>(CreatePrograms);
            _programs.DisposeParent = this;
        }

        protected virtual IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            return instance;
        }

        protected virtual IVstPluginAudioPrecissionProcessor CreateAudioPrecissionProcessor(IVstPluginAudioPrecissionProcessor instance)
        {
            return instance;
        }

        protected virtual IVstPluginBypass CreateBypass(IVstPluginBypass instance)
        {
            return instance;
        }

        protected virtual IVstPluginConnections CreateConnections(IVstPluginConnections instance)
        {
            return instance;
        }

        protected virtual IVstPluginEditor CreateEditor(IVstPluginEditor instance)
        {
            return instance;
        }

        protected virtual IVstPluginHost CreateHost(IVstPluginHost instance)
        {
            return instance;
        }

        protected virtual IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            return instance;
        }

        protected virtual IVstPluginMidiPrograms CreateMidiPrograms(IVstPluginMidiPrograms instance)
        {
            return instance;
        }

        protected virtual IVstPluginMidiSource CreateMidiSource(IVstPluginMidiSource instance)
        {
            return instance;
        }

        protected virtual IVstPluginOfflineProcessor CreateOfflineProcessor(IVstPluginOfflineProcessor instance)
        {
            return instance;
        }

        protected virtual IVstPluginParameters CreateParameters(IVstPluginParameters instance)
        {
            return instance;
        }

        protected virtual IVstPluginPersistence CreatePersistence(IVstPluginPersistence instance)
        {
            return instance;
        }

        protected virtual IVstPluginProcess CreateProcess(IVstPluginProcess instance)
        {
            return instance;
        }

        protected virtual IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            return instance;
        }

        #region IExtensibleObject Members

        public bool Supports<T>() where T : class
        {
            return (GetInstance<T>() != null);
        }

        public virtual T GetInstance<T>() where T : class
        {
            // interfaces are more or less ordered in priority

            // special case for the AudioProcessor: IVstPluginAudioPrecissionProcessor could also provide the IVstPluginAudioProcessor.
            ExtensibleInterfaceRef<IVstPluginAudioPrecissionProcessor> audioPrecission = _audioPrecission.MatchInterface<T>();
            ExtensibleInterfaceRef<IVstPluginAudioProcessor> audioProcessor = _audioProcessor.MatchInterface<T>();
            if (audioProcessor != null)
            {
                if (audioProcessor.SafeInstance == null && audioPrecission != null)
                {
                    return audioPrecission.SafeInstance as T;
                }

                return audioProcessor.SafeInstance as T;
            }

            if (audioPrecission != null) return audioPrecission.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginPrograms> programs = _programs.MatchInterface<T>();
            if (programs != null) return programs.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginParameters> parameters = _parameters.MatchInterface<T>();
            if (parameters != null)
            {
                // special case for parameters
                if (parameters.SafeInstance == null)
                {
                    programs = _programs.GetInterface();

                    // return the parameters of the active program (if there is one)
                    if (programs.SafeInstance != null && programs.SafeInstance.ActiveProgram != null)
                    {
                        return programs.SafeInstance.ActiveProgram as T;
                    }
                }

                return parameters.SafeInstance as T;
            }

            ExtensibleInterfaceRef<IVstMidiProcessor> midiProcessor = _midiProcessor.MatchInterface<T>();
            if (midiProcessor != null) return midiProcessor.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginMidiPrograms> midiPrograms = _midiPrograms.MatchInterface<T>();
            if (midiPrograms != null) return midiPrograms.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginMidiSource> midiSource = _midiSource.MatchInterface<T>();
            if (midiSource != null) return midiSource.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginProcess> process = _process.MatchInterface<T>();
            if (process != null) return process.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginBypass> bypass = _bypass.MatchInterface<T>();
            if (bypass != null) return bypass.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginConnections> connections = _connections.MatchInterface<T>();
            if (connections != null) return connections.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginEditor> editor = _editor.MatchInterface<T>();
            if (editor != null) return editor.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginHost> host = _host.MatchInterface<T>();
            if (host != null) return host.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginOfflineProcessor> offlineProcessor = _offlineProcessor.MatchInterface<T>();
            if (offlineProcessor != null) return offlineProcessor.SafeInstance as T;

            ExtensibleInterfaceRef<IVstPluginPersistence> persistence = _persistence.MatchInterface<T>();
            if (persistence != null) return persistence.SafeInstance as T;

            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
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
        }

        #endregion
    }
}
