namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// The VstMidiCategory is used by a plugin to define Midi Program Category hierarchies.
    /// </summary>
    public class VstMidiCategory : ObservableObject
    {
        /// <summary>Name</summary>
        public const string NamePropertyName = "Name";
        /// <summary>ParentCategory</summary>
        public const string ParentCategoryPropertyName = "ParentCategory";

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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxMidiNameLength, NamePropertyName);

                SetProperty(value, ref _name, NamePropertyName);
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
                SetProperty(value, ref _parentCategory, ParentCategoryPropertyName);
            }
        }
    }
}
