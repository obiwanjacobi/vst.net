using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct KeyswitchInfo
    {
        [MarshalAs(UnmanagedType.U4)]
        public KeyswitchTypeIDs TypeId;					///< see KeyswitchTypeID

        [MarshalAs(UnmanagedType.LPWStr)]
        public String Title;						///< name of key switch (e.g. "Accentuation")

        [MarshalAs(UnmanagedType.LPWStr)]
        public String ShortTitle;					///< short title (e.g. "Acc")

        [MarshalAs(UnmanagedType.I4)]
        public Int32 KeyswitchMin;						///< associated main key switch min (value between [0, 127])

        [MarshalAs(UnmanagedType.I4)]
        public Int32 KeyswitchMax;						///< associated main key switch max (value between [0, 127])

        [MarshalAs(UnmanagedType.I4)]
        public Int32 KeyRemapped;						/** optional remapped key switch (default -1), the Plug-in could provide one remapped 
											key for a key switch (allowing better location on the keyboard of the key switches) */

        [MarshalAs(UnmanagedType.I4)]
        public Int32 UnitId;							///< id of unit this key switch belongs to (see \ref vst3UnitsIntro), -1 means no unit used.

        [MarshalAs(UnmanagedType.I4)]
        public KeyswitchFlags Flags;							///< not yet used (set to 0)

        public enum KeyswitchTypeIDs
        {
            NoteOnKeyswitchTypeID = 0,				///< press before noteOn is played
            OnTheFlyKeyswitchTypeID,				///< press while noteOn is played
            OnReleaseKeyswitchTypeID,				///< press before entering release
            KeyRangeTypeID							///< key should be maintained pressed for playing
        }

        public enum KeyswitchFlags
        {
            None = 0
        }
    }
}
