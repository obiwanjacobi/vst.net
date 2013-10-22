using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

namespace Jacobi.Vst3.TestPlugin
{
    public delegate object ObjectCreatorCallback(IServiceProvider svcProvider, Type classType);

    public class ClassRegistration
    {
        // optional
        public ObjectCreatorCallback CreatorCallback { get; set; }

        /// <summary>
        /// The class must have a <see cref="GuidAttribute"/> and a <see cref="DisplayNameAttribute"/>.
        /// </summary>
        public Type ClassType { get; set; }

        public string Category { get; set; }

        private IList<string> _subCategories;

        public IList<string> SubCategories
        {
            get
            {
                if (_subCategories == null)
                {
                    _subCategories = new List<string>();
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
    }
}
