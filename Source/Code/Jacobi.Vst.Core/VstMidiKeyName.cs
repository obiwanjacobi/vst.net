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

        /// <summary>
        /// Always filled by the plugin with the note name of the 
        /// <see cref="CurrentKeyNumber"/> in the <see cref="CurrentProgramIndex"/>.
        /// </summary>
        public string Name { get; set; }
    }
}
