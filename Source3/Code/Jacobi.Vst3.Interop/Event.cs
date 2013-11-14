using System;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Explicit, CharSet = Platform.CharacterSet, Pack = Platform.StructurePack)]
    public struct Event
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.I4)]
        public Int32 BusIndex;				///< event bus index

        [FieldOffset(4)]
        [MarshalAs(UnmanagedType.I4)]
        public Int32 SampleOffset;			///< sample frames related to the current block start sample position

        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.R8)]
        public Double PpqPosition;	        ///< position in project

        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.I4)]
        public EventFlags Flags;		    ///< combination of \ref EventFlags

        [FieldOffset(20)]
        [MarshalAs(UnmanagedType.I4)]
        public EventTypes Type;				///< a value from \ref EventTypes

        // union

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public NoteOnEvent NoteOn;                      ///< type == kNoteOnEvent

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public NoteOffEvent NoteOff;							///< type == kNoteOffEvent

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public DataEvent Data;									///< type == kDataEvent

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public PolyPressureEvent PolyPressure;					///< type == kPolyPressureEvent

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public NoteExpressionValueEvent NoteExpressionValue;	///< type == kNoteExpressionValueEvent

        [FieldOffset(24)]
        [MarshalAs(UnmanagedType.Struct)]
        public NoteExpressionTextEvent NoteExpressionText;		///< type == kNoteExpressionTextEvent

        public enum EventFlags
        {
            IsLive = 1 << 0,			///< indicates that the event is played live (directly from keyboard)

            UserReserved1 = 1 << 14,	///< reserved for user (for internal use)
            UserReserved2 = 1 << 15	    ///< reserved for user (for internal use)
        };

        public enum EventTypes
        {
            NoteOnEvent = 0,			///< is \ref NoteOnEvent
            NoteOffEvent,				///< is \ref NoteOffEvent
            DataEvent,					///< is \ref DataEvent
            PolyPressureEvent,			///< is \ref PolyPressureEvent
            NoteExpressionValueEvent,	///< is \ref NoteExpressionValueEvent
            NoteExpressionTextEvent	    ///< is \ref NoteExpressionTextEvent
        };

    }
}
