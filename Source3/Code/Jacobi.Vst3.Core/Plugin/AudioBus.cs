using Jacobi.Vst3.Core;

namespace Jacobi.Vst3.Plugin
{
    public class AudioBus : Bus
    {
        public AudioBus(string name, SpeakerArrangement speakerArr)
            : this(name, speakerArr, BusTypes.Main, BusInfo.BusFlags.DefaultActive)
        { }

        public AudioBus(string name, SpeakerArrangement speakerArr, BusTypes busType)
            : this(name, speakerArr, busType, BusInfo.BusFlags.DefaultActive)
        { }

        public AudioBus(string name, SpeakerArrangement speakerArr, BusTypes busType, BusInfo.BusFlags flags)
            : base(name, busType, flags)
        {
            MediaType = MediaTypes.Audio;
            SpeakerArrangement = speakerArr;
        }

        public SpeakerArrangement SpeakerArrangement { get; set; }

        public override bool GetInfo(ref BusInfo info)
        {
            // current definition of SpeakerArrangement is within 32 bits
            info.ChannelCount = (int)VstExtensions.NumberOfSetBits((uint)SpeakerArrangement);
            return base.GetInfo(ref info);
        }
    }
}
