namespace Jacobi.Vst.Core
{
    using System;

    public class VstPinProperties
    {
        public VstPinPropertiesFlags Flags;

        public string Label;
        public string ShortLabel;

        public VstSpeakerArrangementType ArrangementType;
    }

    [Flags]
    public enum VstPinPropertiesFlags
    {
        PinIsActive = 1 << 0,		// pin is active, ignored by Host
        PinIsStereo = 1 << 1,		// pin is first of a stereo pair
        PinUseSpeaker = 1 << 2		// VstPinProperties::arrangementType is valid and can be used to get the wanted arrangement
    };
}
