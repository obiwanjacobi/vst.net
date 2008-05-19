namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;
    using System;

    public class VstParameter
    {
        public VstParameter(VstParameterInfo parameterInfo)
        {
            if (parameterInfo == null)
            {
                throw new ArgumentNullException("parameterInfo");
            }

            Info = parameterInfo;
        }

        public VstParameterInfo Info
        { get; private set; }

        public VstParameterCategory Category
        { get; set; }
        
        public float NormalizedValue 
        { get; set; }

        private string _displayValue;
        public virtual string DisplayValue 
        {
            get { return _displayValue; }
            protected set
            {
                Throw.IfArgumentTooLong(value, Core.Constants.MaxParameterStringLength, "DisplayValue");

                _displayValue = value;
            }
        }

        public string Key
        { get; set; }

        public virtual bool ParseValue(string value)
        {
            float fValue;
            if (Single.TryParse(value, out fValue))
            {
                NormalizedValue = fValue;
                return true;
            }

            return false;
        }
    }
}
