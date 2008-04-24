namespace Jacobi.Vst.Core
{
    public enum VstSpeakerArrangementType
    {
        SpeakerArrUserDefined = -2, // user defined
        SpeakerArrEmpty = -1,		// empty arrangement
        
        SpeakerArrMono = 0,		    // M
        SpeakerArrStereo,			// L R
        SpeakerArrStereoSurround,	// Ls Rs
        SpeakerArrStereoCenter,	    // Lc Rc
        SpeakerArrStereoSide,		// Sl Sr
        SpeakerArrStereoCLfe,		// C Lfe
        SpeakerArr30Cine,			// L R C
        SpeakerArr30Music,			// L R S
        SpeakerArr31Cine,			// L R C Lfe
        SpeakerArr31Music,			// L R Lfe S
        SpeakerArr40Cine,			// L R C   S (LCRS)
        SpeakerArr40Music,			// L R Ls  Rs (Quadro)
        SpeakerArr41Cine,			// L R C   Lfe S (LCRS+Lfe)
        SpeakerArr41Music,			// L R Lfe Ls Rs (Quadro+Lfe)
        SpeakerArr50,				// L R C Ls  Rs 
        SpeakerArr51,				// L R C Lfe Ls Rs
        SpeakerArr60Cine,			// L R C   Ls  Rs Cs
        SpeakerArr60Music,			// L R Ls  Rs  Sl Sr 
        SpeakerArr61Cine,			// L R C   Lfe Ls Rs Cs
        SpeakerArr61Music,			// L R Lfe Ls  Rs Sl Sr 
        SpeakerArr70Cine,			// L R C Ls  Rs Lc Rc 
        SpeakerArr70Music,			// L R C Ls  Rs Sl Sr
        SpeakerArr71Cine,			// L R C Lfe Ls Rs Lc Rc
        SpeakerArr71Music,			// L R C Lfe Ls Rs Sl Sr
        SpeakerArr80Cine,			// L R C Ls  Rs Lc Rc Cs
        SpeakerArr80Music,			// L R C Ls  Rs Cs Sl Sr
        SpeakerArr81Cine,			// L R C Lfe Ls Rs Lc Rc Cs
        SpeakerArr81Music,			// L R C Lfe Ls Rs Cs Sl Sr 
        SpeakerArr102,				// L R C Lfe Ls Rs Tfl Tfc Tfr Trl Trr Lfe2
    }
}
