namespace Jacobi.Vst.Plugin.Framework
{
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Plugin.Framework.Common;
    using System;

    /// <summary>
    /// Names a parameter category.
    /// </summary>
    public class VstParameterCategory : ObservableObject
    {
        private string _name = String.Empty;
        /// <summary>
        /// Gets or sets the name of the parameter category.
        /// </summary>
        /// <remarks>The Name cannot exceed 24 characters.</remarks>
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxCategoryLabelLength, nameof(Name));

                SetProperty(value, ref _name, nameof(Name));
            }
        }
    }
}
