namespace Jacobi.Vst.Framework.Plugin
{
    using Jacobi.Vst.Framework.Common;
    using System;

    /// <summary>
    /// This class manages all plugin interface instances on behalf of a plugin.
    /// </summary>
    /// <remarks>The following interfaces are supported:
    /// <see cref="IVstPluginAudioProcessor"/>, <see cref="IVstPluginAudioPrecisionProcessor"/>,
    /// <see cref="IVstPluginBypass"/>, <see cref="IVstPluginConnections"/>,
    /// <see cref="IVstPluginEditor"/>, <see cref="IVstMidiProcessor"/>, 
    /// <see cref="IVstPluginMidiPrograms"/>, <see cref="IVstPluginMidiSource"/>, 
    /// <see cref="IVstPluginParameters"/>, <see cref="IVstPluginPersistence"/>,
    /// <see cref="IVstPluginProcess"/> and <see cref="IVstPluginPrograms"/>.</remarks>
    public abstract class PluginInterfaceManagerBase : IExtensible, IDisposable
    {
        private readonly InterfaceManager<IVstPluginAudioPrecisionProcessor> _audioprecision;
        private readonly InterfaceManager<IVstPluginAudioProcessor> _audioProcessor;
        private readonly InterfaceManager<IVstPluginBypass> _bypass;
        private readonly InterfaceManager<IVstPluginConnections> _connections;
        private readonly InterfaceManager<IVstPluginEditor> _editor;
        private readonly InterfaceManager<IVstMidiProcessor> _midiProcessor;
        private readonly InterfaceManager<IVstPluginMidiPrograms> _midiPrograms;
        private readonly InterfaceManager<IVstPluginMidiSource> _midiSource;
        private readonly InterfaceManager<IVstPluginParameters> _parameters;
        private readonly InterfaceManager<IVstPluginPersistence> _persistence;
        private readonly InterfaceManager<IVstPluginProcess> _process;
        private readonly InterfaceManager<IVstPluginPrograms> _programs;

        /// <summary>
        /// Constructs a new instance and initializes all interfaces.
        /// </summary>
        protected PluginInterfaceManagerBase()
        {
            _audioprecision = new InterfaceManager<IVstPluginAudioPrecisionProcessor>(CreateAudioPrecisionProcessor)
            {
                DisposeParent = this
            };

            _audioProcessor = new InterfaceManager<IVstPluginAudioProcessor>(CreateAudioProcessor)
            {
                DisposeParent = this
            };

            _bypass = new InterfaceManager<IVstPluginBypass>(CreateBypass)
            {
                DisposeParent = this
            };

            _connections = new InterfaceManager<IVstPluginConnections>(CreateConnections)
            {
                DisposeParent = this
            };

            _editor = new InterfaceManager<IVstPluginEditor>(CreateEditor)
            {
                DisposeParent = this
            };

            _midiProcessor = new InterfaceManager<IVstMidiProcessor>(CreateMidiProcessor)
            {
                DisposeParent = this
            };

            _midiPrograms = new InterfaceManager<IVstPluginMidiPrograms>(CreateMidiPrograms)
            {
                DisposeParent = this
            };

            _midiSource = new InterfaceManager<IVstPluginMidiSource>(CreateMidiSource)
            {
                DisposeParent = this
            };

            _parameters = new InterfaceManager<IVstPluginParameters>(CreateParameters)
            {
                DisposeParent = this
            };

            _persistence = new InterfaceManager<IVstPluginPersistence>(CreatePersistence)
            {
                DisposeParent = this
            };

            _process = new InterfaceManager<IVstPluginProcess>(CreateProcess)
            {
                DisposeParent = this
            };

            _programs = new InterfaceManager<IVstPluginPrograms>(CreatePrograms)
            {
                DisposeParent = this
            };
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginAudioProcessor"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginAudioProcessor"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginAudioProcessor CreateAudioProcessor(IVstPluginAudioProcessor instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginAudioPrecisionProcessor"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginAudioPrecisionProcessor"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.
        /// Note that you do not need to override <see cref="CreateAudioProcessor"/> when you implement the precision audio processor interface.
        /// The PluginInterfaceManagerBase will call this method when a request comes in for the <see cref="IVstPluginAudioProcessor"/>
        /// and cast the result when the call to <see cref="CreateAudioProcessor"/> returns null.</remarks>
        protected virtual IVstPluginAudioPrecisionProcessor CreateAudioPrecisionProcessor(IVstPluginAudioPrecisionProcessor instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginBypass"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginBypass"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginBypass CreateBypass(IVstPluginBypass instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginConnections"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginConnections"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginConnections CreateConnections(IVstPluginConnections instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginEditor"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginEditor"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginEditor CreateEditor(IVstPluginEditor instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstMidiProcessor"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstMidiProcessor"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstMidiProcessor CreateMidiProcessor(IVstMidiProcessor instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginMidiPrograms"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginMidiPrograms"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginMidiPrograms CreateMidiPrograms(IVstPluginMidiPrograms instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginMidiSource"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginMidiSource"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginMidiSource CreateMidiSource(IVstPluginMidiSource instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginParameters"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginParameters"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.
        /// When the plugin also supports <see cref="IVstPluginPrograms"/> this method does not need to be overridden.
        /// The PluginInterfaceManagerBase implementation will route the interface request for the <see cref="IVstPluginParameters"/>
        /// to the <see cref="IVstPluginPrograms.ActiveProgram"/> where it is implemented by the <see cref="VstProgram"/>.</remarks>
        protected virtual IVstPluginParameters CreateParameters(IVstPluginParameters instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginPersistence"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginPersistence"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginPersistence CreatePersistence(IVstPluginPersistence instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginProcess"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginProcess"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginProcess CreateProcess(IVstPluginProcess instance)
        {
            return instance;
        }

        /// <summary>
        /// Called when an instance of the <see cref="IVstPluginPrograms"/> interface is requested.
        /// </summary>
        /// <param name="instance">The default instance or null.</param>
        /// <returns>Returns <paramref name="instance"/>.</returns>
        /// <remarks>Override to create an instance of the <see cref="IVstPluginPrograms"/> interface. 
        /// When <paramref name="instance"/> is null, create the default instance. When the <paramref name="instance"/>
        /// is not null, create a Thread Safe instance, possibly wrapping the default <paramref name="instance"/>.</remarks>
        protected virtual IVstPluginPrograms CreatePrograms(IVstPluginPrograms instance)
        {
            return instance;
        }

        #region IExtensibleObject Members

        /// <summary>
        /// Indicates if the interface <typeparamref name="T"/> is supported by the object.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns true if the interface <typeparamref name="T"/> is supported.</returns>
        public bool Supports<T>() where T : class
        {
            return (GetInstance<T>() != null);
        }

        /// <summary>
        /// Retrieves a reference to an implementation of the interface <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The interface type.</typeparam>
        /// <returns>Returns null when the <typeparamref name="T"/> is not supported.</returns>
        public virtual T GetInstance<T>() where T : class
        {
            // interfaces are more or less ordered in priority

            // special case for the AudioProcessor: IVstPluginAudioprecisionProcessor could also provide the IVstPluginAudioProcessor.
            ExtensibleInterfaceRef<IVstPluginAudioPrecisionProcessor> audioprecision = _audioprecision.MatchInterface<T>();
            ExtensibleInterfaceRef<IVstPluginAudioProcessor> audioProcessor = _audioProcessor.MatchInterface<T>();
            if (audioProcessor != null)
            {
                if (audioProcessor.SafeInstance == null && audioprecision != null)
                {
                    return audioprecision.SafeInstance as T;
                }

                return audioProcessor.SafeInstance as T;
            }

            if (audioprecision != null) return audioprecision.SafeInstance as T;

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

            ExtensibleInterfaceRef<IVstPluginPersistence> persistence = _persistence.MatchInterface<T>();
            if (persistence != null) return persistence.SafeInstance as T;

            return null;
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Call this in the tear down sequence of your plugin.
        /// </summary>
        /// <remarks>The tear down sequence starts when <see cref="IDisposable.Dispose"/> is called on the <see cref="IVstPlugin"/> interface.
        /// All interface instances will be disposed and this instance is no longer usable.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Override this method to do additional cleanup and always call the base class.
        /// </summary>
        /// <param name="disposing">When true, release (dispose) both managed and unmanaged resource. When false, only release unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _audioprecision.Dispose();
                _audioProcessor.Dispose();
                _bypass.Dispose();
                _connections.Dispose();
                _editor.Dispose();
                _midiPrograms.Dispose();
                _midiSource.Dispose();
                _parameters.Dispose();
                _persistence.Dispose();
                _process.Dispose();
                _programs.Dispose();
            }
        }

        #endregion
    }
}
