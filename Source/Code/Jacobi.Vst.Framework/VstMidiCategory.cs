namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    /// <summary>
    /// The VstMidiCategory is used by a plugin to define Midi Program Category hierarchies.
    /// </summary>
    public class VstMidiCategory
    {
        private string _name;
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        /// <remarks>The Name cannot exceed 63 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, "Name");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <remarks>When this Property is null, the instance represents a root category.</remarks>
        public VstMidiCategory ParentCategory { get; set; }
    }
}
