namespace Jacobi.Vst.Framework
{
    using System;
    using System.Drawing;
    using Jacobi.Vst.Core;

    /// <summary>
    /// This interface is implemented when a plugin support a custom editor (UI).
    /// </summary>
    /// <remarks>The editor is a custom GUI that display the plugin's settings.
    /// Note that the life time of the GUI form is NOT linked to the lifetime of 
    /// the instance of the class that implements this interface. <seealso cref="Close"/></remarks>
    public interface IVstPluginEditor
    {
        /// <summary>
        /// Gets or sets the knob mode.
        /// </summary>
        /// <remarks>The host calls into the plugin to set the knob mode, which ends up here.</remarks>
        VstKnobMode KnobMode { get; set; }
        /// <summary>
        /// Gets the dimensions of the UI form.
        /// </summary>
        Rectangle Bounds {get;}
        /// <summary>
        /// Called by the host when the UI should be displayed.
        /// </summary>
        /// <param name="hWnd">This is the handle of the parent window.</param>
        void Open(IntPtr hWnd);
        /// <summary>
        /// Called by the host when some idle time is available.
        /// </summary>
        /// <remarks>Keep your processing short.</remarks>
        void ProcessIdle();
        /// <summary>
        /// Called by the host when the UI form must be closed.
        /// </summary>
        /// <remarks>The instance of the object implementing this interface is not terminated.</remarks>
        void Close();
        /// <summary>
        /// Called by the host when the user presses a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns true if the key is handled.</returns>
        /// <remarks>Typically this method requires no implementation when using WinForms.</remarks>
        bool KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
        /// <summary>
        /// Called by the host when the user releases a key.
        /// </summary>
        /// <param name="ascii">The identification of the key.</param>
        /// <param name="virtualKey">Virtual key information.</param>
        /// <param name="modifers">Additional keys pressed.</param>
        /// <returns>Returns true if the key is handled.</returns>
        /// <remarks>Typically this method requires no implementation when using WinForms.</remarks>
        bool KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers);
    }
}
