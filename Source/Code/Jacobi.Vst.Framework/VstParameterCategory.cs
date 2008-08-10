namespace Jacobi.Vst.Framework
{
    using System;

    /// <summary>
    /// Names a parameter category.
    /// </summary>
    public class VstParameterCategory
    {
        private string _name;
        /// <summary>
        /// Gets or sets the name of the parameter category.
        /// </summary>
        /// <remarks>The Name cannot exceed 23 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxCategoryLabelLength, "Name");

                _name = value;
            }
        }
    }
}
