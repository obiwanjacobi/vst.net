namespace Jacobi.Vst.Framework
{
    using System;

    public class VstParameterCategory
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > Core.Constants.MaxCategoryLabelLength)
                {
                    throw new ArgumentException("Name too long.", "Name");
                }

                _name = value;
            }
        }
    }
}
