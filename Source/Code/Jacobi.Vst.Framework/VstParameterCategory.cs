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
                Throw.IfArgumentTooLong(value, Core.Constants.MaxCategoryLabelLength, "Name");

                _name = value;
            }
        }
    }
}
