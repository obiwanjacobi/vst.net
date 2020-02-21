namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Platform-independent definition of Virtual Keys.
    /// </summary>
    public enum VstVirtualKey
    {
        /// <summary>Null value.</summary>
        VKEY_NONE,
        /// <summary></summary>
        VKEY_BACK,
        /// <summary></summary>
        VKEY_TAB,
        /// <summary></summary>
        VKEY_CLEAR,
        /// <summary></summary>
        VKEY_RETURN,
        /// <summary></summary>
        VKEY_PAUSE,
        /// <summary></summary>
        VKEY_ESCAPE,
        /// <summary></summary>
        VKEY_SPACE,
        /// <summary></summary>
        VKEY_NEXT,
        /// <summary></summary>
        VKEY_END,
        /// <summary></summary>
        VKEY_HOME,
        /// <summary></summary>
        VKEY_LEFT,
        /// <summary></summary>
        VKEY_UP,
        /// <summary></summary>
        VKEY_RIGHT,
        /// <summary></summary>
        VKEY_DOWN,
        /// <summary></summary>
        VKEY_PAGEUP,
        /// <summary></summary>
        VKEY_PAGEDOWN,
        /// <summary></summary>
        VKEY_SELECT,
        /// <summary></summary>
        VKEY_PRINT,
        /// <summary></summary>
        VKEY_ENTER,
        /// <summary></summary>
        VKEY_SNAPSHOT,
        /// <summary></summary>
        VKEY_INSERT,
        /// <summary></summary>
        VKEY_DELETE,
        /// <summary></summary>
        VKEY_HELP,
        /// <summary></summary>
        VKEY_NUMPAD0,
        /// <summary></summary>
        VKEY_NUMPAD1,
        /// <summary></summary>
        VKEY_NUMPAD2,
        /// <summary></summary>
        VKEY_NUMPAD3,
        /// <summary></summary>
        VKEY_NUMPAD4,
        /// <summary></summary>
        VKEY_NUMPAD5,
        /// <summary></summary>
        VKEY_NUMPAD6,
        /// <summary></summary>
        VKEY_NUMPAD7,
        /// <summary></summary>
        VKEY_NUMPAD8,
        /// <summary></summary>
        VKEY_NUMPAD9,
        /// <summary></summary>
        VKEY_MULTIPLY,
        /// <summary></summary>
        VKEY_ADD,
        /// <summary></summary>
        VKEY_SEPARATOR,
        /// <summary></summary>
        VKEY_SUBTRACT,
        /// <summary></summary>
        VKEY_DECIMAL,
        /// <summary></summary>
        VKEY_DIVIDE,
        /// <summary></summary>
        VKEY_F1,
        /// <summary></summary>
        VKEY_F2,
        /// <summary></summary>
        VKEY_F3,
        /// <summary></summary>
        VKEY_F4,
        /// <summary></summary>
        VKEY_F5,
        /// <summary></summary>
        VKEY_F6,
        /// <summary></summary>
        VKEY_F7,
        /// <summary></summary>
        VKEY_F8,
        /// <summary></summary>
        VKEY_F9,
        /// <summary></summary>
        VKEY_F10,
        /// <summary></summary>
        VKEY_F11,
        /// <summary></summary>
        VKEY_F12,
        /// <summary></summary>
        VKEY_NUMLOCK,
        /// <summary></summary>
        VKEY_SCROLL,
        /// <summary></summary>
        VKEY_SHIFT,
        /// <summary></summary>
        VKEY_CONTROL,
        /// <summary></summary>
        VKEY_ALT,
        /// <summary></summary>
        VKEY_EQUALS
    }

    /// <summary>
    /// Platform-independent definition of modifier Keys
    /// </summary>
    [Flags]
    public enum VstModifierKeys
    {
        /// <summary>Shift</summary>
        MODIFIER_SHIFT = 1 << 0,
        /// <summary>Alt</summary>
        MODIFIER_ALTERNATE = 1 << 1,
        /// <summary>Control on Mac</summary>
        MODIFIER_COMMAND = 1 << 2,
        /// <summary>Ctrl on PC, Apple on Mac</summary>
        MODIFIER_CONTROL = 1 << 3
    }
}
