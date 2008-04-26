namespace Jacobi.Vst.Framework
{
    using System;
    using System.Drawing;
    using Jacobi.Vst.Core;

    public interface IVstPluginEditor : IDisposable
    {
        VstKnobMode KnobMode { get; set; }
        Rectangle Bounds {get;}
        void Open(IntPtr hWnd);
        void ProcessIdle();
        //void Close(); // call Dispose

        void KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
        void KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
    }
}
