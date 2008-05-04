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

        public virtual string DisplayValue 
        { get; protected set; }
        
        public string Name
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
