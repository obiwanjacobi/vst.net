namespace Jacobi.Vst3.Core
{
    public enum VirtualKeyCodes
    {
        KEY_BACK = 1,
        KEY_TAB,
        KEY_CLEAR,
        KEY_RETURN,
        KEY_PAUSE,
        KEY_ESCAPE,
        KEY_SPACE,
        KEY_NEXT,
        KEY_END,
        KEY_HOME,

        KEY_LEFT,
        KEY_UP,
        KEY_RIGHT,
        KEY_DOWN,
        KEY_PAGEUP,
        KEY_PAGEDOWN,

        KEY_SELECT,
        KEY_PRINT,
        KEY_ENTER,
        KEY_SNAPSHOT,
        KEY_INSERT,
        KEY_DELETE,
        KEY_HELP,
        KEY_NUMPAD0,
        KEY_NUMPAD1,
        KEY_NUMPAD2,
        KEY_NUMPAD3,
        KEY_NUMPAD4,
        KEY_NUMPAD5,
        KEY_NUMPAD6,
        KEY_NUMPAD7,
        KEY_NUMPAD8,
        KEY_NUMPAD9,
        KEY_MULTIPLY,
        KEY_ADD,
        KEY_SEPARATOR,
        KEY_SUBTRACT,
        KEY_DECIMAL,
        KEY_DIVIDE,
        KEY_F1,
        KEY_F2,
        KEY_F3,
        KEY_F4,
        KEY_F5,
        KEY_F6,
        KEY_F7,
        KEY_F8,
        KEY_F9,
        KEY_F10,
        KEY_F11,
        KEY_F12,
        KEY_NUMLOCK,
        KEY_SCROLL,

        KEY_SHIFT,
        KEY_CONTROL,
        KEY_ALT,

        KEY_EQUALS,				// only occurs on a Mac
        KEY_CONTEXTMENU,		// Windows only

        // multimedia keys
        KEY_MEDIA_PLAY,
        KEY_MEDIA_STOP,
        KEY_MEDIA_PREV,
        KEY_MEDIA_NEXT,
        KEY_VOLUME_UP,
        KEY_VOLUME_DOWN,

        KEY_F13,
        KEY_F14,
        KEY_F15,
        KEY_F16,
        KEY_F17,
        KEY_F18,
        KEY_F19,

        VKEY_FIRST_CODE = KEY_BACK,
        VKEY_LAST_CODE = KEY_F19,

        VKEY_FIRST_ASCII = 128
        /*
         KEY_0 - KEY_9 are the same as ASCII '0' - '9' (0x30 - 0x39) + FIRST_ASCII
         KEY_A - KEY_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A) + FIRST_ASCII
        */
    }
}
