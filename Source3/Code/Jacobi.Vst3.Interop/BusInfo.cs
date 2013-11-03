using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Jacobi.Vst3.Interop
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = Platform.StructurePack)]
    public struct BusInfo
    {
        [MarshalAs(UnmanagedType.I4)]
        public MediaTypes MediaType;		///< Media type - has to be a value of \ref MediaTypes

        [MarshalAs(UnmanagedType.I4)]
        public BusDirections Direction;		///< input or output \ref BusDirections

        [MarshalAs(UnmanagedType.I4)]
        public Int32 ChannelCount;			///< number of channels (if used then need to be recheck after \ref IAudioProcessor::setBusArrangements is called)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeBusName)]
        public string Name;				///< name of the bus

        [MarshalAs(UnmanagedType.I4)]
        public BusTypes BusType;			///< main or aux - has to be a value of \ref BusTypes

        [MarshalAs(UnmanagedType.I4)]
        public BusFlags Flags;				///< flags - a combination of \ref BusFlags
        
        public enum BusFlags
        {
            None = 0,
            DefaultActive = 1 << 0	///< bus active per default
        };		
    }
}
