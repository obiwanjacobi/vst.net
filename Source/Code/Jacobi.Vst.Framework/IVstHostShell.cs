namespace Jacobi.Vst.Framework
{
    using System;
    using System.Globalization;

    public interface IVstHostShell
    {
        void UpdateDisplay();
        bool SizeWindow(int width, int height);
        CultureInfo Culture { get; } //getLanguage
        string BaseDirectory { get;}
        IDisposable OpenFileSelector(/*file selector struct*/);
        IDisposable EditParameter(VstParameter parameter);
    }
}
