using System.Windows.Forms;
using System;

namespace Jacobi.Vst.Core.TestPlugin
{
    class WindowWrapper : IWin32Window
    {
        public WindowWrapper(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }

        #region IWin32Window Members
        private IntPtr _hWnd;
        public IntPtr Handle
        {
            get { return _hWnd; }
        }

        #endregion
    }
}
