using System;
namespace Jacobi.Vst.Framework
{
    public class VstProgram : IVstPluginParameters
    {
        public VstProgram(VstParameterCategoryCollection categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }

            _categories = categories;
        }

        public string Name
        { get; set; }

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
