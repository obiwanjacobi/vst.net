namespace Jacobi.Vst.Framework
{
    /// <summary>
    /// The VstMidiChannelInfo contains Midi Program information for a Midi channel.
    /// </summary>
    /// <remarks>Although an instance of the VstMidiChannelInfo class represents Midi Program information
    /// for one Midi channel, it has no member to identify that Midi channel. This is to allow easy reuse
    /// of one VstMidiChannelInfo instance for multiple channels.</remarks>
    public class VstMidiChannelInfo
    {
        private VstMidiCategoryCollection _categories;
        /// <summary>
        /// Gets the collection of <see cref="VstMidiCategory"/>s.
        /// </summary>
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
        /// <summary>
        /// Gets a collection of <see cref="VstMidiProgram"/>s.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the active/current Midi Program for this channel.
        /// </summary>
        public VstMidiProgram ActiveProgram
        { get; set; }
    }
}
