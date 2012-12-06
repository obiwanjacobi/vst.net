namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Used to communicate speaker properties to the host.
    /// </summary>
    /// <remarks>The origin for azimuth is right (as by math conventions dealing with radians).
	/// The elevation origin is also right, visualizing a rotation of a circle across the
	/// -pi/pi axis of the horizontal circle. Thus, an elevation of -pi/2 corresponds
	/// to bottom, and a speaker standing on the left, and 'beaming' upwards would have
	/// an azimuth of -pi, and an elevation of pi/2.
	/// For user interface representation, grads are more likely to be used, and the
    /// origins will obviously 'shift' accordingly.</remarks>
    public class VstSpeakerProperties
    {
        /// <summary>
        /// unit: rad, range: -PI...PI, exception: 10.f for LFE channel.
        /// </summary>
        public float Azimath { get; set; }

        /// <summary>
        /// unit: rad, range: -PI/2...PI/2, exception: 10.f for LFE channel.
        /// </summary>
        public float Elevation { get; set; }

        /// <summary>
        /// unit: meter, exception: 0.f for LFE channel.
        /// </summary>
        public float Radius { get; set; }

        private string _name;
        /// <summary>
        /// for new setups, new names should be given (L/R/C... won't do).
        /// </summary>
        /// <remarks>The value must not exceed 64 characters.</remarks>
        /// <exception cref="System.ArgumentException">Thrown when the value exceeds 63 characters.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxMidiNameLength, "Name");

                _name = value;
            }
        }

        /// <summary>
        /// The speaker type.
        /// </summary>
        public VstSpeakerTypes SpeakerType { get; set; }
    }

    /// <summary>
    /// Single speaker types.
    /// </summary>
    public enum VstSpeakerTypes
    {
        /// <summary>Undefined</summary>
        SpeakerUndefined = 0x7fffffff,
        /// <summary>Mono (M)</summary>
        SpeakerM = 0,
        /// <summary>Left (L)</summary>
        SpeakerL,
        /// <summary>Right (R)</summary>
        SpeakerR,
        /// <summary>Center (C)</summary>
        SpeakerC,
        /// <summary>Subbass (Lfe)</summary>
        SpeakerLfe,
        /// <summary>Left Surround (Ls)</summary>
        SpeakerLs,
        /// <summary>Right Surround (Rs)</summary>
        SpeakerRs,
        /// <summary>Left of Center (Lc)</summary>
        SpeakerLc,
        /// <summary>Right of Center (Rc)</summary>
        SpeakerRc,
        /// <summary>Surround (S)</summary>
        SpeakerS,
        /// <summary>Center of Surround (Cs) = Surround (S)</summary>
        SpeakerCs = SpeakerS,
        /// <summary>Side Left (Sl)</summary>
        SpeakerSl,
        /// <summary>Side Right (Sr)</summary>
        SpeakerSr,
        /// <summary>Top Middle (Tm)</summary>
        SpeakerTm,
        /// <summary>Top Front Left (Tfl)</summary>
        SpeakerTfl,
        /// <summary>Top Front Center (Tfc)</summary>
        SpeakerTfc,
        /// <summary>Top Front Right (Tfr)</summary>
        SpeakerTfr,
        /// <summary>Top Rear Left (Trl)</summary>
        SpeakerTrl,
        /// <summary>Top Rear Center (Trc)</summary>
        SpeakerTrc,
        /// <summary>Top Rear Right (Trr)</summary>
        SpeakerTrr,
        /// <summary>Subbass 2 (Lfe2)</summary>
        SpeakerLfe2
    }
}
