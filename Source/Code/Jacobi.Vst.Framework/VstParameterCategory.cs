﻿namespace Jacobi.Vst.Framework
{
    using System;
    using Jacobi.Vst.Core;
    using Jacobi.Vst.Framework.Common;

    /// <summary>
    /// Names a parameter category.
    /// </summary>
    public class VstParameterCategory : ObservableObject
    {
        public const string NamePropertyName = "Name";

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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxCategoryLabelLength, NamePropertyName);

                SetProperty(value, ref _name, NamePropertyName);
            }
        }
    }
}
