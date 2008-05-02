namespace Jacobi.Vst.Framework.TestPlugin
{
    using Jacobi.Vst.Framework.Common;
    using System;

    class FxPluginInterfaceManager : IExtensibleObject, IDisposable
    {
        private FxTestPlugin _plugin;

        public FxPluginInterfaceManager(FxTestPlugin plugin)
        {
            _plugin = plugin;
        }

        private ExtensibleInterfaceRef<IVstPluginAudioProcessor> _audio;
        private ExtensibleInterfaceRef<IVstPluginAudioProcessor> GetAudio<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstPluginAudioProcessor>.IsMatch<T>())
            {
                if (_audio == null)
                {
                    _audio = new ExtensibleInterfaceRef<IVstPluginAudioProcessor>();
                    _audio.Instance = new AudioProcessor(_plugin);
                    _audio.ThreadSafeInstance = _audio.Instance;
                }

                return _audio;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstPluginPrograms> _programs;
        private ExtensibleInterfaceRef<IVstPluginPrograms> GetPrograms<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstPluginPrograms>.IsMatch<T>())
            {
                if (_programs == null)
                {
                    _programs = new ExtensibleInterfaceRef<IVstPluginPrograms>();
                    _programs.Instance = new PluginPrograms(_plugin);
                    _programs.ThreadSafeInstance = _programs.Instance;
                }

                return _programs;
            }

            return null;
        }

        private ExtensibleInterfaceRef<IVstPluginParameters> _paramters;
        private ExtensibleInterfaceRef<IVstPluginParameters> GetParameters<T>() where T : class
        {
            if (ExtensibleInterfaceRef<IVstPluginParameters>.IsMatch<T>())
            {
                if (_paramters == null)
                {
                    ExtensibleInterfaceRef<IVstPluginPrograms> programs = GetPrograms<IVstPluginPrograms>();

                    _paramters = new ExtensibleInterfaceRef<IVstPluginParameters>();
                    _paramters.Instance = programs.Instance.ActiveProgram;
                    _paramters.ThreadSafeInstance = _paramters.Instance;
                }

                return _paramters;
            }

            return null;
        }

        #region IExtensibleObject Members

        public bool Supports<T>(bool threadSafe) where T : class
        {
            ExtensibleInterfaceRef<IVstPluginAudioProcessor> audio = GetAudio<T>();
            if (audio != null)
            {
                return (audio.Instance != null);
            }

            ExtensibleInterfaceRef<IVstPluginPrograms> programs = GetPrograms<T>();
            if (programs != null)
            {
                return (programs.Instance != null);
            }

            ExtensibleInterfaceRef<IVstPluginParameters> parameters = GetParameters<T>();
            if (parameters != null)
            {
                return (parameters.Instance != null);
            }

            return false;
        }

        public T GetInstance<T>(bool threadSafe) where T : class
        {
            ExtensibleInterfaceRef<IVstPluginAudioProcessor> audio = GetAudio<T>();
            if (audio != null)
            {
                return audio.Instance as T;
            }

            ExtensibleInterfaceRef<IVstPluginPrograms> programs = GetPrograms<T>();
            if (programs != null)
            {
                return programs.Instance as T;
            }

            ExtensibleInterfaceRef<IVstPluginParameters> parameters = GetParameters<T>();
            if (parameters != null)
            {
                return parameters.Instance as T;
            }

            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _audio.Dispose();
            _plugin = null;
        }

        #endregion
    }
}
