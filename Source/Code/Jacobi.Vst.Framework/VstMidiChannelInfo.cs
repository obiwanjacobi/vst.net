namespace Jacobi.Vst.Framework
{
    public class VstMidiChannelInfo
    {
        private VstMidiCategoryCollection _categories;
        public VstMidiCategoryCollection Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new VstMidiCategoryCollection();
                }

                return _categories;
            }
        }

        private VstMidiProgramCollection _programs;
        public VstMidiProgramCollection Programs
        {
            get
            {
                if (_programs == null)
                {
                    _programs = new VstMidiProgramCollection();
                }

                return _programs;
            }
        }

        public VstMidiProgram ActiveProgram
        { get; set; }
    }
}
