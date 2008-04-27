namespace Jacobi.Vst.Framework
{
    using System;
    using System.Globalization;

    public interface IVstHostShell
    {
        bool UpdateDisplay();
        bool SizeWindow(int width, int height);
        CultureInfo Culture { get; }
        string BaseDirectory { get;}
        IDisposable OpenFileSelector(/*file selector struct*/);
    }
}
