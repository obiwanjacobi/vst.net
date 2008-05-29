namespace Jacobi.Vst.Framework
{
    using System;

    public class VstProgram : IVstPluginParameters, IDisposable
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

        private VstParameterCollection _parameters;
        public VstParameterCollection Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new VstParameterCollection();
                }

                return _parameters;
            }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            _name = null;
            _categories = null;

            if (_parameters != null)
            {
                _parameters.Clear();    // disposes VstParameter instances
                _parameters = null;
            }
        }

        #endregion
    }
}
