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

        /// <summary>
        /// Filled by the plugin with the name of the <see cref="CurrentCategoryIndex"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The index of the category that is parent of the <see cref="CurrentCategoryIndex"/>.
        /// </summary>
        /// <remarks>Can be null.</remarks>
        public int ParentCategoryIndex;
    }
}
