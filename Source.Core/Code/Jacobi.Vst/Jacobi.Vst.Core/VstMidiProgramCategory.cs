namespace Jacobi.Vst.Core
{
    /// <summary>
    /// Used to communicate the Midi category name back to the host.
    /// </summary>
    public class VstMidiProgramCategory
    {
        /// <summary>
        /// Filled by the host to request the category <see cref="Name"/>.
        /// </summary>
        public int CurrentCategoryIndex { get; set; }

        private string _name;
        /// <summary>
        /// Filled by the plugin with the name of the <see cref="CurrentCategoryIndex"/>.
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

        /// <summary>
        /// The index of the category that is parent of the <see cref="CurrentCategoryIndex"/>.
        /// </summary>
        /// <remarks>Can be null.</remarks>
        public int ParentCategoryIndex;
    }
}
