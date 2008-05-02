namespace Jacobi.Vst.Framework
{
    using Jacobi.Vst.Core;

    public class VstParameter
    {
        private VstParameterProperties _props;
        public VstParameterProperties Properties
        {
            get
            {
                if (_props == null)
                {
                    _props = new VstParameterProperties();
                }

                return _props;
            }
            set
            {
                _props = value;
            }
        }

        public bool CanBeAutomated { get; set; }
        public float NormalizedValue { get; set; }
        public string DisplayValue { get; private set; }
        public string Name { get; private set; }
        public string Label { get; private set; }

        public virtual bool ParseValue(string str)
        {
            return false;
        }
    }
}
