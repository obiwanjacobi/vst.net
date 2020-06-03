using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Core
{
    [StructLayout(LayoutKind.Sequential, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct NoteExpressionTextEvent
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 TypeId;	                	///< see \ref NoteExpressionTypeID (kTextTypeID or kPhoneticTypeID)

        [MarshalAs(UnmanagedType.I4)]
        public Int32 NoteId;						///< associated note identifier to apply the change

        [MarshalAs(UnmanagedType.U4)]
        public UInt32 Size;						///< number of bytes in text (includes null byte)

        [MarshalAs(UnmanagedType.LPWStr)]
        public String Text;    					///< UTF-16, null terminated
    }
}
