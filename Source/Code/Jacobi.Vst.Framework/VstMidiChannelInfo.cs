namespace Jacobi.Vst.Framework
{
    public class VstMidiChannelInfo
    {
        public VstMidiCategoryCollection Categories { get { return null; } }
        public VstMidiProgramCollection Programs { get { return null; } }
        public VstMidiProgram ActiveProgram { get; set; }
    }
}
