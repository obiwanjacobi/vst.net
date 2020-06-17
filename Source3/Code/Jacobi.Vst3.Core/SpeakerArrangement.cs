namespace Jacobi.Vst3.Core
{
    public enum SpeakerArrangement : ulong
    {
        ArrEmpty = 0,          // empty arrangement
        ArrMono = Speakers.SpeakerM,  // M
        ArrStereo = Speakers.SpeakerL | Speakers.SpeakerR,    // L R
        ArrStereoSurround = Speakers.SpeakerLs | Speakers.SpeakerRs,	  // Ls Rs
        ArrStereoCenter = Speakers.SpeakerLc | Speakers.SpeakerRc,   // Lc Rc
        ArrStereoSide = Speakers.SpeakerSl | Speakers.SpeakerSr,	  // Sl Sr
        ArrStereoCLfe = Speakers.SpeakerC | Speakers.SpeakerLfe,  // C Lfe
        Arr30Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC,  // L R C
        Arr30Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerS,  // L R S
        Arr31Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe,  // L R C   Lfe
        Arr31Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLfe | Speakers.SpeakerS,    // L R Lfe S
        Arr40Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerS,    // L R C   S (LCRS)
        Arr40Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLs | Speakers.SpeakerRs,   // L R Ls  Rs (Quadro)
        Arr41Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerS,	 // L R C   Lfe S (LCRS+Lfe)
        Arr41Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs,  // L R Lfe Ls Rs (Quadro+Lfe)
        Arr50 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs,	 // L R C   Ls Rs 
        Arr51 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs,  // L R C  Lfe Ls Rs
        Arr60Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs,  // L R C  Ls  Rs Cs
        Arr60Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerSl | Speakers.SpeakerSr,  // L R Ls Rs  Sl Sr 
        Arr61Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs,  // L R C   Lfe Ls Rs Cs
        Arr61Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerSl | Speakers.SpeakerSr,  // L R Lfe Ls  Rs Sl Sr 
        Arr70Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLc | Speakers.SpeakerRc,  // L R C   Ls  Rs Lc Rc 
        Arr70Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerSl | Speakers.SpeakerSr,  // L R C   Ls  Rs Sl Sr
        Arr71Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLc | Speakers.SpeakerRc, 	// L R C Lfe Ls Rs Lc Rc
        Arr71Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerSl | Speakers.SpeakerSr,	// L R C Lfe Ls Rs Sl Sr
        Arr80Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLc | Speakers.SpeakerRc | Speakers.SpeakerCs,  // L R C Ls  Rs Lc Rc Cs
        Arr80Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerSl | Speakers.SpeakerSr,	// L R C Ls  Rs Cs Sl Sr
        Arr80Cube = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTfl | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R Ls Rs Tfl Tfr Trl Trr
        Arr81Cine = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLc | Speakers.SpeakerRc | Speakers.SpeakerCs,	 // L R C Lfe Ls Rs Lc Rc Cs
        Arr81Music = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerSl | Speakers.SpeakerSr,	 // L R C Lfe Ls Rs Cs Sl Sr 
        Arr102 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr | Speakers.SpeakerLfe2,	// L R C Lfe Ls Rs Tfl Tfc Tfr Trl Trr Lfe2
        Arr122 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLc | Speakers.SpeakerRc | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr | Speakers.SpeakerLfe2,	// L R C Lfe Ls Rs Lc Rc Tfl Tfc Tfr Trl Trr Lfe2
        ArrBFormat1stOrder = Speakers.SpeakerW | Speakers.SpeakerX | Speakers.SpeakerY | Speakers.SpeakerZ,  // W X Y Z (First Order)
        ArrBFormat = ArrBFormat1stOrder,

        Arr71CineTopCenter = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerTm, 	// L R C Lfe Ls Rs Cs Tm
        Arr71CineCenterHigh = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerTfc, 	// L R C Lfe Ls Rs Cs Tfc
        Arr71CineFrontHigh = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTfl | Speakers.SpeakerTfr, 	// L R C Lfe Ls Rs Tfl Tfr
        Arr71CineSideHigh = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTsl | Speakers.SpeakerTsr, 	// L R C Lfe Ls Rs Tsl Tsr
        Arr71CineSideFill = Arr61Music,
        Arr71CineFullRear = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerLcs | Speakers.SpeakerRcs, 	// L R C Lfe Ls Rs Lcs Rcs
        Arr71CineFullFront = Arr71Cine,

        Arr90 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTfl | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Ls Rs Tfl Tfr Trl Trr			
        Arr91 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTfl | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Lfe Ls Rs Tfl Tfr Trl Trr			
        Arr100 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Ls Rs Tm Tfl Tfr Trl Trr
        Arr101 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Lfe Ls Rs Tm Tfl Tfr Trl Trr
        Arr110 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Ls Rs Tm Tfl Tfc Tfr Trl Trr
        Arr111 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrr,	// L R C Lfe Ls Rs Tm Tfl Tfc Tfr Trl Trr
        Arr130 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrc | Speakers.SpeakerTrr,	// L R C Ls Rs Cs Tm Tfl Tfc Tfr Trl Trc Trr
        Arr131 = Speakers.SpeakerL | Speakers.SpeakerR | Speakers.SpeakerC | Speakers.SpeakerLfe | Speakers.SpeakerLs | Speakers.SpeakerRs | Speakers.SpeakerCs | Speakers.SpeakerTm | Speakers.SpeakerTfl | Speakers.SpeakerTfc | Speakers.SpeakerTfr | Speakers.SpeakerTrl | Speakers.SpeakerTrc | Speakers.SpeakerTrr,	// L R C Lfe Ls Rs Cs Tm Tfl Tfc Tfr Trl Trc Trr
    }
}
