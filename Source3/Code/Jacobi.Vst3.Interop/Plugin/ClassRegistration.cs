using System;
using System.Collections.Generic;
using Jacobi.Vst3.Interop;

namespace Jacobi.Vst3.Plugin
{
    public class ClassRegistration
    {
        // optional
        public ObjectCreatorCallback CreatorCallback { get; set; }

        public Type ClassType { get; set; }

        // GuidAttribute on ClassType
        public Guid ClassTypeId { get; set; }

        // maps to Category
        public ObjectClasses ObjectClass { get; set; }

        public ComponentClassFlags ClassFlags { get; set; }

        // DisplayName on ClassType
        public string DisplayName { get; set; }

        private CategoryCollection _categories;
        // maps to subCategories
        public CategoryCollection Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoryCollection();
                }

                return _categories;
            }

            protected set
            {
                if (_categories != null)
                    throw new InvalidOperationException("Categories is already initialized.");

                _categories = value;
            }
        }

        public string Vendor { get; set; }

        public Version Version { get; set; }

        public enum ObjectClasses
        {
            None,
            AudioModuleClass,
            ComponentControllerClass,
            TestClass   // validator and test host
        }
    }
}
