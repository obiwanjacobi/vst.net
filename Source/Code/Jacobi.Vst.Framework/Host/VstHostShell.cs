namespace Jacobi.Vst.Framework.Host
{
    using System;
    using System.Globalization;
    using Jacobi.Vst.Core;

    internal class VstHostShell : IVstHostShell
    {
        private VstHost _host;

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
