namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Common;

    /// <summary>
    /// The VstMidiCategory is used by a plugin to define Midi Program Category hierarchies.
    /// </summary>
    public class VstMidiCategory : ObservableObject
    {
        private string _name;
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        /// <remarks>The Name cannot exceed 64 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, nameof(Name));

                SetProperty(value, ref _name, nameof(Name));
            }
        }

        private VstMidiCategory _parentCategory;
        /// <summary>
        /// Gets or sets the parent category.
        /// </summary>
        /// <remarks>When this Property is null, the instance represents a root category.</remarks>
        public VstMidiCategory ParentCategory
        {
            get { return _parentCategory; }
            set
            {
                SetProperty(value, ref _parentCategory, nameof(ParentCategory));
            }
        }
    }
}
