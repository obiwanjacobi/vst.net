namespace Jacobi.Vst.Samples.CorePlugin
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Helper class to easily attach a Windows.Forms UserControl to a Win32 native main window of the host.
    /// </summary>
    /// <typeparam name="T">The type of the managed Control.</typeparam>
    class WinFormsWrapper<T> where T : UserControl, new()
    {
        private T _instance;

        private void EnsureInstance()
        {
            if (_instance == null)
            {
                _instance = new T();
                _instance.CreateControl();
            }
        }

        /// <summary>
        /// Gets and instance of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>Never returns null.</remarks>
        public T Instance
        {
            get
            {
                EnsureInstance();
                return _instance;
            }
        }

        /// <summary>
        /// Opens and attaches the Control to the <paramref name="hWnd"/>.
        /// </summary>
        /// <param name="hWnd">The native win32 handle to the main window of the host.</param>
        public void Open(IntPtr hWnd)
        {
            EnsureInstance();
            NativeMethods.SetParent(_instance.Handle, hWnd);
            _instance.Show();
        }

        /// <summary>
        /// Returns the bounding rectangle of the Control.
        /// </summary>
        /// <param name="rect">Receives the bounding rectangle.</param>
        /// <remarks>The same size as in design-time.</remarks>
        public void GetBounds(out Rectangle rect)
        {
            EnsureInstance();
            rect = _instance.Bounds;
        }

        /// <summary>
        /// Closes and destroys the Control.
        /// </summary>
        public void Close()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }
    }
}
