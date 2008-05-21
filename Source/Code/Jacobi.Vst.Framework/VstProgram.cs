using System;
namespace Jacobi.Vst.Framework
{
    public class VstProgram : IVstPluginParameters
    {
        public VstProgram(VstParameterCategoryCollection categories)
        {
            Throw.IfArgumentIsNull(categories, "categories");

            _categories = categories;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxProgramNameLength, "Name");

                _name = value;
            }
        }

        #region IVstPluginParameters Members

        private VstParameterCategoryCollection _categories;
        public VstParameterCategoryCollection Categories
        {
            get { return _categories; }
        }

        private VstParameterCollection _paramters;
        public VstParameterCollection Parameters
        {
            get
            {
                if (_paramters == null)
                {
                    _paramters = new VstParameterCollection();
                }

                return _paramters;
            }
        }

        #endregion
    }
}
