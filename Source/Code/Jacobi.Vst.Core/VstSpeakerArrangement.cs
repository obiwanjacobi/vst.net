namespace Jacobi.Vst.Core
{
    public class VstSpeakerArrangement
    {
        public VstSpeakerArrangementType Type;		///< e.g. #kSpeakerArr51 for 5.1  @see VstSpeakerArrangementType
	    public VstSpeakerProperties[] Speakers;	    ///< variable sized speaker array
    }
}
