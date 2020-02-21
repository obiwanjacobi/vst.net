namespace Jacobi.Vst.Core
{
    /// <summary>
    /// The VstMidiKeyName contains field that are used to 
    /// communicate names of individual notes (keys).
    /// </summary>
    public class VstMidiKeyName
    {
        /// <summary>
        /// When filled by the host indicating the current program, 
        /// otherwise the plugin should fill this with its current program.
        /// </summary>
        public int CurrentProgramIndex { get; set; }

        /// <summary>
        /// Filled by the host with the note number for wich the plugin 
        /// should return the <see cref="Name"/>.
        /// </summary>
        public int CurrentKeyNumber { get; set; }

        private string _name;
        /// <summary>
        /// Always filled by the plugin with the note name of the 
        /// <see cref="CurrentKeyNumber"/> in the <see cref="CurrentProgramIndex"/>.
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
    }
}
