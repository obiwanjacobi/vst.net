namespace Jacobi.Vst.Framework.Host
{
    using System;
    using System.Globalization;
    using Jacobi.Vst.Core;

    /// <summary>
    /// Forwards the <see cref="IVstHostShell"/> methods to the host stub.
    /// </summary>
    internal class VstHostShell : IVstHostShell
    {
        private VstHost _host;
        /// <summary>
        /// Constructs an instance on the host proxy.
        /// </summary>
        /// <param name="host">Must not be null.</param>
        public VstHostShell(VstHost host)
        {
            Throw.IfArgumentIsNull(host, "host");

            _host = host;
        }

        #region IVstHostShell Members

        public bool UpdateDisplay()
        {
            return _host.HostCommandStub.UpdateDisplay();
        }

        public bool SizeWindow(int width, int height)
        {
            return _host.HostCommandStub.SizeWindow(width, height);
        }

        public CultureInfo _culture;
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
                    VstHostLanguage language = _host.HostCommandStub.GetLanguage();

                    switch (language)
                    {
                        case VstHostLanguage.English:
                            _culture = new CultureInfo("en");
                            break;
                        case VstHostLanguage.French:
                            _culture = new CultureInfo("fr");
                            break;
                        case VstHostLanguage.German:
                            _culture = new CultureInfo("de");
                            break;
                        case VstHostLanguage.Italian:
                            _culture = new CultureInfo("it");
                            break;
                        case VstHostLanguage.Japanese:
                            _culture = new CultureInfo("ja");
                            break;
                        case VstHostLanguage.Spanish:
                            _culture = new CultureInfo("es");
                            break;
                        default:
                            _culture = CultureInfo.CurrentUICulture;
                            break;
                    }
                }

                return _culture;
            }
        }

        private string _baseDir;
        public string BaseDirectory
        {
            get
            {
                if (_baseDir == null)
                {
                    _baseDir = _host.HostCommandStub.GetDirectory();
                }

                return _baseDir;
            }
        }

        /// <summary>
        /// Under construction!
        /// </summary>
        /// <returns>Returns null when the host does not support opening a file selector.</returns>
        public IDisposable OpenFileSelector()
        {
            // check capability of the host
            if ((_host.Capabilities & VstHostCapabilities.OpenFileSelector) > 0)
            {
                return new FileSelectorScope(_host);
            }

            return null;
        }

        #endregion

        //---------------------------------------------------------------------

        /// <summary>
        /// Implements the scope for the file selector.
        /// </summary>
        private class FileSelectorScope : IDisposable
        {
            private VstHost _host;

            public FileSelectorScope(VstHost host)
            {
                _host = host;

                if (_host.HostCommandStub.OpenFileSelector() == false)
                {
                    throw new InvalidOperationException("Host does not implement OpenFileSelector.");
                }
            }

            #region IDisposable Members
            /// <summary>
            /// Call by the client when it is done with the file selector.
            /// </summary>
            /// <remarks>We do not check wheter or not the host supports closing the file selector...</remarks>
            public void Dispose()
            {
                if (_host != null)
                {
                    _host.HostCommandStub.CloseFileSelector();
                    _host = null;
                }
            }

            #endregion
        }
    }
}
