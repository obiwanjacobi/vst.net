namespace Jacobi.Vst.Core
{
    using System;

    /// <summary>
    /// Used to communicate the Midi program name to the host.
    /// </summary>
    public class VstMidiProgramName
    {
        /// <summary>
        /// Filled by the host requesting a midi program.
        /// </summary>
        public int CurrentProgramIndex { get; set; }

        /// <summary>
        /// The flags for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public VstMidiProgramNameFlags Flags { get; set; }

        /// <summary>
        /// The name for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The program change number for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public byte MidiProgram { get; set; }

        /// <summary>
        /// The least significant bank select number for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public byte MidiBankLSB { get; set; }
        
        /// <summary>
        /// The most significant bank select number for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public byte MidiBankMSB { get; set; }

        /// <summary>
        /// The index of the category that is parent to the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public int ParentCategoryIndex { get; set; }
    }

    /// <summary>
    /// Flags for the midi program (name).
    /// </summary>
    [Flags]
    public enum VstMidiProgramNameFlags
    {
        /// <summary>Null value.</summary>
        None = 0,
        /// <summary>Omni mode is on.</summary>
        MidiIsOmni = 1,
    }
}
