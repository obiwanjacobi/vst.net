namespace Jacobi.Vst.Framework.Common
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// This wrapper class makes it easy to use a WinForms (User) Control as an Editor UI.
    /// </summary>
    /// <typeparam name="T">The type of WinForms (User) Control.</typeparam>
    public class WinFormsControlWrapper<T> : IDisposable 
        where T : Control, new()
    {
        private void EnsureInstance()
        {
            if (_instance == null)
            {
                _instance = new T();
                _instance.CreateControl();
            }
        }

        private T _instance;
        /// <summary>
        /// Gets the instance of <typeparamref name="T"/>. Can be null.
        /// </summary>
        public T Instance { get { return _instance; } }

        /// <summary>
        /// Gets the instance of <typeparamref name="T"/>. Never null.
        /// </summary>
        public T SafeInstance
        {
            get
            {
                EnsureInstance();

                return _instance;
            }
        }

        /// <summary>
        /// Shows the User Control <typeparamref name="T"/>.
        /// </summary>
        /// <param name="hWnd">The handle of the parent window.</param>
        /// <remarks>The instance of <typeparamref name="T"/> is attached as child window to the parent <paramref name="hWnd"/>.</remarks>
        public void Open(IntPtr hWnd)
        {
            EnsureInstance();
            
            NativeMethods.SetParent(_instance.Handle, hWnd);

            _instance.Show();
        }

        /// <summary>
        /// Gets the dimensions of the (User) Control.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                EnsureInstance();
                
                return _instance.Bounds;
            }
        }

        /// <summary>
        /// Closes and Disposes the (User) Control.
        /// </summary>
        public void Close()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Calls <see cref="Close"/>.
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
