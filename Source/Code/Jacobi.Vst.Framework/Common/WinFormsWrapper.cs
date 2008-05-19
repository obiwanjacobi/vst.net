namespace Jacobi.Vst.Framework.Common
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class WinFormsWrapper<T> : IDisposable 
        where T : UserControl, new()
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

        public T Instance
        {
            get
            {
                EnsureInstance();

                return _instance;
            }
        }

        public void Open(IntPtr hWnd)
        {
            EnsureInstance();
            
            NativeMethods.SetParent(_instance.Handle, hWnd);

            _instance.Show();
        }

        public Rectangle Bounds
        {
            get
            {
                EnsureInstance();
                
                return _instance.Bounds;
            }
        }

        public void Close()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
