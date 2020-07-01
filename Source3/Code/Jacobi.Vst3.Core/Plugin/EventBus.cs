using Jacobi.Vst3.Core;

namespace Jacobi.Vst3.Plugin
{
    public class EventBus : Bus
    {
        public EventBus(string name, int channelCount)
            : this(name, channelCount, BusTypes.Main, BusInfo.BusFlags.DefaultActive)
        { }

        public EventBus(string name, int channelCount, BusTypes busType)
            : this(name, channelCount, busType, BusInfo.BusFlags.DefaultActive)
        { }

        public EventBus(string name, int channelCount, BusTypes busType, BusInfo.BusFlags flags)
            : base(name, busType, flags)
        {
            MediaType = MediaTypes.Event;
            ChannelCount = channelCount;
        }

        public int ChannelCount { get; private set; }

        public override bool GetInfo(ref BusInfo info)
        {
            info.ChannelCount = ChannelCount;
            return base.GetInfo(ref info);
        }
    }
}
