using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Jacobi.Vst3.Interop.Plugin
{
    public class ClassRegistration
    {
        // internals
        internal string DisplayName { get; set; }
        internal Guid ClassTypeId { get; set; }

        // optional
        public ObjectCreatorCallback CreatorCallback { get; set; }

        /// <summary>
        /// The class must have a <see cref="GuidAttribute"/> and a <see cref="DisplayNameAttribute"/>.
        /// </summary>
        public Type ClassType { get; set; }

        public ObjectClasses ObjectClass { get; set; }

        private SubCategoryCollection _subCategories;

        public SubCategoryCollection SubCategories
        {
            get
            {
                if (_subCategories == null)
                {
                    _subCategories = new SubCategoryCollection();
                }

                return _subCategories;
            }

            protected set
            {
                if (_subCategories != null) 
                    throw new InvalidOperationException("SubCategories is already initialized.");

                _subCategories = value;
            }
        }

        public uint ClassFlags { get; set; }

        public string Vendor { get; set; }

        public Version Version { get; set; }

        public enum ObjectClasses
        {
            Unknown,
            AudioModuleClass,
            ComponentControllerClass
        }
    }
}
