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

        private string _name = String.Empty;
        /// <summary>
        /// The name for the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        /// <remarks>The value must not exceed 64 characters.</remarks>
        /// <exception cref="System.ArgumentException">Thrown when the value exceeds 63 characters.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Constants.MaxMidiNameLength, nameof(Name));

                _name = value;
            }
        }

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
#pragma warning disable S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes
    public enum VstMidiProgramNameFlags
#pragma warning restore S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes
    {
        /// <summary>Null value.</summary>
        None = 0,
        /// <summary>Omni mode is on.</summary>
        MidiIsOmni = 1,
    }
}
