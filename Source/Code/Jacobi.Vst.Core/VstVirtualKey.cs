namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Platform-independent definition of Virtual Keys.
    /// </summary>
    public enum VstVirtualKey
    {
        /// <summary>Zero value.</summary>
        None,
        /// <summary></summary>
        Back,
        /// <summary></summary>
        Tab,
        /// <summary></summary>
        Clear,
        /// <summary></summary>
        Return,
        /// <summary></summary>
        Pause,
        /// <summary></summary>
        Escape,
        /// <summary></summary>
        Space,
        /// <summary></summary>
        Next,
        /// <summary></summary>
        End,
        /// <summary></summary>
        Home,
        /// <summary></summary>
        Left,
        /// <summary></summary>
        Up,
        /// <summary></summary>
        Right,
        /// <summary></summary>
        Down,
        /// <summary></summary>
        PageUp,
        /// <summary></summary>
        PageDown,
        /// <summary></summary>
        Select,
        /// <summary></summary>
        Print,
        /// <summary></summary>
        Enter,
        /// <summary></summary>
        Snapshot,
        /// <summary></summary>
        Insert,
        /// <summary></summary>
        Delete,
        /// <summary></summary>
        Help,
        /// <summary></summary>
        NumPad0,
        /// <summary></summary>
        NumPad1,
        /// <summary></summary>
        NumPad2,
        /// <summary></summary>
        NumPad3,
        /// <summary></summary>
        NumPad4,
        /// <summary></summary>
        NumPad5,
        /// <summary></summary>
        NumPad6,
        /// <summary></summary>
        NumPad7,
        /// <summary></summary>
        NumPad8,
        /// <summary></summary>
        NumPad9,
        /// <summary></summary>
        Multiply,
        /// <summary></summary>
        Add,
        /// <summary></summary>
        Separator,
        /// <summary></summary>
        Subtract,
        /// <summary></summary>
        Decimal,
        /// <summary></summary>
        Divide,
        /// <summary></summary>
        F1,
        /// <summary></summary>
        F2,
        /// <summary></summary>
        F3,
        /// <summary></summary>
        F4,
        /// <summary></summary>
        F5,
        /// <summary></summary>
        F6,
        /// <summary></summary>
        F7,
        /// <summary></summary>
        F8,
        /// <summary></summary>
        F9,
        /// <summary></summary>
        F10,
        /// <summary></summary>
        F11,
        /// <summary></summary>
        F12,
        /// <summary></summary>
        NumLock,
        /// <summary></summary>
        Scroll,
        /// <summary></summary>
        Shift,
        /// <summary></summary>
        Control,
        /// <summary></summary>
        Alt,
        /// <summary></summary>
        Equals
    }

    /// <summary>
    /// Platform-independent definition of modifier Keys
    /// </summary>
    [Flags]
    public enum VstModifierKeys
    {
        /// <summary>Shift</summary>
        Shift = 1 << 0,
        /// <summary>Alt</summary>
        Alternate = 1 << 1,
        /// <summary>Control on Mac</summary>
        Command = 1 << 2,
        /// <summary>Ctrl on PC, Apple on Mac</summary>
        Control = 1 << 3
    }
}
