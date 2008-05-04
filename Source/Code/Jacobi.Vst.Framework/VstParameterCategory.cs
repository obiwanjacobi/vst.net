using System;
namespace Jacobi.Vst.Framework
{
    public class VstParameterCategory
    {
        public const int MaxParameterCategoryNameLength = 23;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value.Length > MaxParameterCategoryNameLength)
                {
                    throw new ArgumentException("Name too long.", "Name");
                }

                _name = value;
            }
        }
    }
}
