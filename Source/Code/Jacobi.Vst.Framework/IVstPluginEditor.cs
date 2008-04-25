namespace Jacobi.Vst.Framework
{
    using System;
    using System.Drawing;

    public interface IVstPluginEditor : IDisposable
    {
        Rectangle Dimension {get;}
        void Open(IntPtr hWnd);
        void ProcessIdle();
        //void Close(); // call Dispose
    }
}
