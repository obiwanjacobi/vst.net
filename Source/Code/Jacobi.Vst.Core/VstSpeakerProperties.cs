namespace Jacobi.Vst.Core
{
    public class VstSpeakerProperties
    {
        public float Azimath;
        public float Elevation;
        public float Radius;
        public string Name;
        public VstSpeakerTypes SpeakerType;
    }

    public enum VstSpeakerTypes
    {
        SpeakerUndefined = 0x7fffffff,	// Undefined
        SpeakerM = 0,					// Mono (M)
        SpeakerL,						// Left (L)
        SpeakerR,						// Right (R)
        SpeakerC,						// Center (C)
        SpeakerLfe,					    // Subbass (Lfe)
        SpeakerLs,						// Left Surround (Ls)
        SpeakerRs,						// Right Surround (Rs)
        SpeakerLc,						// Left of Center (Lc)
        SpeakerRc,						// Right of Center (Rc)
        SpeakerS,						// Surround (S)
        SpeakerCs = SpeakerS,			// Center of Surround (Cs) = Surround (S)
        SpeakerSl,						// Side Left (Sl)
        SpeakerSr,						// Side Right (Sr)
        SpeakerTm,						// Top Middle (Tm)
        SpeakerTfl,					    // Top Front Left (Tfl)
        SpeakerTfc,					    // Top Front Center (Tfc)
        SpeakerTfr,					    // Top Front Right (Tfr)
        SpeakerTrl,					    // Top Rear Left (Trl)
        SpeakerTrc,					    // Top Rear Center (Trc)
        SpeakerTrr,					    // Top Rear Right (Trr)
        SpeakerLfe2					    // Subbass 2 (Lfe2)
    }
}
