namespace Jacobi.Vst.Plugin.Framework.Host
{
    using Jacobi.Vst.Core;
    using System;
    using System.Globalization;

    /// <summary>
    /// Forwards the <see cref="IVstHostShell"/> methods to the host stub.
    /// </summary>
    internal sealed class VstHostShell : IVstHostShell
    {
        private readonly IVstHostCommands20 _commands;
        private readonly IVstHost _host;
        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="host"/> is not set to an instance of an object.</exception>
        public VstHostShell(VstHost host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _commands = host.HostCommandProxy.Commands;
        }

        #region IVstHostShell Members

        public bool UpdateDisplay()
        {
            return _commands.UpdateDisplay();
        }

        public bool SizeWindow(int width, int height)
        {
            return _commands.SizeWindow(width, height);
        }

        public CultureInfo? _culture;
        /// <summary>
        /// Gets the culture of the host.
        /// </summary>
        /// <remarks>If the host does not implement the call to getLanguage, the current UI culture is returned.</remarks>
        public CultureInfo Culture
        {
            get
            {
                if (_culture == null)
                {
                    var language = _commands.GetLanguage();

                    _culture = language switch
                    {
                        VstHostLanguage.English => CultureInfo.GetCultureInfo("en"),
                        VstHostLanguage.French => CultureInfo.GetCultureInfo("fr"),
                        VstHostLanguage.German => CultureInfo.GetCultureInfo("de"),
                        VstHostLanguage.Italian => CultureInfo.GetCultureInfo("it"),
                        VstHostLanguage.Japanese => CultureInfo.GetCultureInfo("ja"),
                        VstHostLanguage.Spanish => CultureInfo.GetCultureInfo("es"),
                        _ => CultureInfo.CurrentUICulture,
                    };
                }

                return _culture;
            }
        }

        private string _baseDir = String.Empty;
        public string BaseDirectory
        {
            get
            {
                if (_baseDir == null)
                {
                    _baseDir = _commands.GetDirectory();
                }

                return _baseDir;
            }
        }

        /// <summary>
        /// Make the host show an open file dialog - if supported.
        /// </summary>
        /// <returns>Returns null when the host does not support opening a file selector.</returns>
        public IDisposable? OpenFileSelector(VstFileSelect fileSelect)
        {
            // check capability of the host
            if ((_host.Capabilities & VstHostCapabilities.OpenFileSelector) > 0)
            {
                return new FileSelectorScope(_commands, fileSelect);
            }

            return null;
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for the file selector.
        /// </summary>
        private sealed class FileSelectorScope : IDisposable
        {
            private IVstHostCommands20? _commands;
            private VstFileSelect? _fileSelect;

            public FileSelectorScope(IVstHostCommands20 commands, VstFileSelect fileSelect)
            {
                _commands = commands;
                _fileSelect = fileSelect;

                if (!_commands.OpenFileSelector(_fileSelect))
                {
                    throw new InvalidOperationException(
                        Properties.Resources.FileSelectorScope_OpenNotSupported);
                }
            }

            #region IDisposable Members

            /// <summary>
            /// Call by the client when it is done with the file selector.
            /// </summary>
            /// <remarks>We do not check wheter or not the host supports closing the file selector...</remarks>
            public void Dispose()
            {
                if (_commands != null && _fileSelect != null)
                {
                    _commands.CloseFileSelector(_fileSelect);
                    _commands = null;
                    _fileSelect = null;
                }
            }

            #endregion
        }
    }
}
