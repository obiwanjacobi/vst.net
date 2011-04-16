namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// The VstMidiChannelInfo contains Midi Program information for a Midi channel.
    /// </summary>
    /// <remarks>Although an instance of the VstMidiChannelInfo class represents Midi Program information
    /// for one Midi channel, it has no member to identify that Midi channel. This is to allow easy reuse
    /// of one VstMidiChannelInfo instance for multiple channels.</remarks>
    public class VstMidiChannelInfo : ObservableObject
    {
        public const string ActiveProgramPropertyName = "ActiveProgram";
        public const string HaveNamesChangedPropertyName = "HaveNamesChanged";

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
                    _programs.MidiProgramNameChanged += new EventHandler<EventArgs>(VstMidiProgram_NameChanged);
                }

                return _programs;
            }
        }

        private VstMidiProgram _activeProgram;
        /// <summary>
        /// Gets or sets the active/current Midi Program for this channel.
        /// </summary>
        public VstMidiProgram ActiveProgram
        {
            get { return _activeProgram; }
            set
            {
                SetProperty(value, ref _activeProgram, ActiveProgramPropertyName);
            }
        }

        private bool _haveNamesChanged;
        /// <summary>
        /// Indicates if either the Midi Program Names have changed or the Key Names.
        /// </summary>
        /// <remarks>The framework will automatically reset this property (false)
        /// when the host has inquired if the names have changed.</remarks>
        public bool HaveNamesChanged
        {
            get { return _haveNamesChanged; }
            set
            {
                SetProperty(value, ref _haveNamesChanged, HaveNamesChangedPropertyName);
            }
        }

        // event handler that receives VstMidiProgram.NameChanged events
        private void VstMidiProgram_NameChanged(object sender, EventArgs e)
        {
            HaveNamesChanged = true;
        }
    }
}
