namespace Jacobi.Vst.Core
{
    /// <summary>
    /// The speaker arrangement types.
    /// </summary>
    public enum VstSpeakerArrangementType
    {
        /// <summary>User defined.</summary>
        SpeakerArrUserDefined = -2,
        /// <summary>An empty arrangement.</summary>
        SpeakerArrEmpty = -1,

        /// <summary>M</summary>
        SpeakerArrMono = 0,
        /// <summary>L R</summary>
        SpeakerArrStereo,
        /// <summary>Ls Rs</summary>
        SpeakerArrStereoSurround,
        /// <summary>Lc Rc</summary>
        SpeakerArrStereoCenter,
        /// <summary>Sl Sr</summary>
        SpeakerArrStereoSide,
        /// <summary>C Lfe</summary>
        SpeakerArrStereoCLfe,
        /// <summary>L R C</summary>
        SpeakerArr30Cine,
        /// <summary>L R S</summary>
        SpeakerArr30Music,
        /// <summary>L R C Lfe</summary>
        SpeakerArr31Cine,
        /// <summary>L R Lfe S</summary>
        SpeakerArr31Music,
        /// <summary>L R C   S (LCRS)</summary>
        SpeakerArr40Cine,
        /// <summary>L R Ls  Rs (Quadro)</summary>
        SpeakerArr40Music,
        /// <summary>L R C   Lfe S (LCRS+Lfe)</summary>
        SpeakerArr41Cine,
        /// <summary>L R Lfe Ls Rs (Quadro+Lfe)</summary>
        SpeakerArr41Music,
        /// <summary>L R C Ls  Rs</summary>
        SpeakerArr50,
        /// <summary>L R C Lfe Ls Rs</summary>
        SpeakerArr51,
        /// <summary>L R C   Ls  Rs Cs</summary>
        SpeakerArr60Cine,
        /// <summary>L R Ls  Rs  Sl Sr</summary>
        SpeakerArr60Music,
        /// <summary>L R C   Lfe Ls Rs Cs</summary>
        SpeakerArr61Cine,
        /// <summary>L R Lfe Ls  Rs Sl Sr</summary>
        SpeakerArr61Music,
        /// <summary>L R C Ls  Rs Lc Rc</summary>
        SpeakerArr70Cine,
        /// <summary>L R C Ls  Rs Sl Sr</summary>
        SpeakerArr70Music,
        /// <summary>L R C Lfe Ls Rs Lc Rc</summary>
        SpeakerArr71Cine,
        /// <summary>L R C Lfe Ls Rs Sl Sr</summary>
        SpeakerArr71Music,
        /// <summary>L R C Ls  Rs Lc Rc Cs</summary>
        SpeakerArr80Cine,
        /// <summary>L R C Ls  Rs Cs Sl Sr</summary>
        SpeakerArr80Music,
        /// <summary>L R C Lfe Ls Rs Lc Rc Cs</summary>
        SpeakerArr81Cine,
        /// <summary>L R C Lfe Ls Rs Cs Sl Sr</summary>
        SpeakerArr81Music,
        /// <summary>L R C Lfe Ls Rs Tfl Tfc Tfr Trl Trr Lfe2</summary>
        SpeakerArr102,
    }
}
