namespace Jacobi.Vst.Core.TestPlugin
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

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

        public void GetBounds(out Rectangle rect)
        {
            EnsureInstance();
            rect = _instance.Bounds;
        }

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
