namespace Jacobi.Vst.Framework
{
    using System;
    using System.Globalization;
    using Jacobi.Vst.Core;

    /// <summary>
    /// Provides access to the windowing shell of the host.
    /// </summary>
    public interface IVstHostShell
    {
        /// <summary>
        /// Updates the host display.
        /// </summary>
        /// <returns>Returns true when the host support updating the window, otherwise false is returned.</returns>
        bool UpdateDisplay();
        /// <summary>
        /// Sizes the window to the specified <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="width">New width of the window in pixels.</param>
        /// <param name="height">New height of the window in pixels.</param>
        /// <returns>Returns true when the host support sizing the window, otherwise false is returned.</returns>
        bool SizeWindow(int width, int height);
        /// <summary>
        /// Gets the culture the host is running in.
        /// </summary>
        CultureInfo Culture { get; }
        /// <summary>
        /// Gets the plugin base directory of the host.
        /// </summary>
        string BaseDirectory { get;}
        /// <summary>
        /// Opens the File Selector.
        /// </summary>
        /// <param name="fileSelect">Information on how the file selector should behave and selected paths.</param>
        /// <returns>Returns null if the host does not support the Open File Selector.</returns>
        /// <remarks>Call <see cref="IDisposable.Dispose"/> on the return value to close the File Selector.</remarks>
        IDisposable OpenFileSelector(VstFileSelect fileSelect);
    }
}
