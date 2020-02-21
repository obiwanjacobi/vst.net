namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Speaker arrangment information.
    /// </summary>
    public class VstSpeakerArrangement
    {
        /// <summary>
        /// e.g. #kSpeakerArr51 for 5.1  @see VstSpeakerArrangementType.
        /// </summary>
        public VstSpeakerArrangementType Type { get; set; }
        
        /// <summary>
        /// Variable sized speaker array.
        /// </summary>
        public VstSpeakerProperties[] Speakers { get; set; }
    }
}
