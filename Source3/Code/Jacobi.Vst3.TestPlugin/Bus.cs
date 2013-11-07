using System;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.TestPlugin
{
    public abstract class Bus
    {
        protected Bus(string name, BusTypes busType, BusInfo.BusFlags flags)
        {
            Name = name;
            BusType = busType;
            Flags = flags;

            IsActive = (flags & BusInfo.BusFlags.DefaultActive) != 0;
        }

        public MediaTypes MediaType { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public BusTypes BusType { get; set; }

        public BusInfo.BusFlags Flags { get; set; }

        public virtual bool GetInfo(ref BusInfo info)
        {
            info.BusType = BusType;
            info.Flags = Flags;
            info.Name = Name;

            return true;
        }
    }
}
