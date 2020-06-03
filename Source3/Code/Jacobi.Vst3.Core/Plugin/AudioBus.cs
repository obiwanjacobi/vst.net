using Jacobi.Vst3.Core;

namespace Jacobi.Vst3.Plugin
{
    public class AudioBus : Bus
    {
        public AudioBus(SpeakerArrangement speakerArr, string name, BusTypes busType)
            : base(name, busType, BusInfo.BusFlags.DefaultActive)
        {
            SpeakerArrangement = speakerArr;
        }

        public AudioBus(SpeakerArrangement speakerArr, string name, BusTypes busType, BusInfo.BusFlags flags)
            : base(name, busType, flags)
        {
            SpeakerArrangement = speakerArr;
        }

        public SpeakerArrangement SpeakerArrangement { get; set; }

        public override bool GetInfo(ref BusInfo info)
        {
            info.MediaType = MediaTypes.Audio;
            // current definition is within 32 bits
            info.ChannelCount = (int)VstExtensions.NumberOfSetBits((uint)SpeakerArrangement);

            return base.GetInfo(ref info);
        }
    }
}
