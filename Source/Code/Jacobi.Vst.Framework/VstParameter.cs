namespace Jacobi.Vst.Framework
{
    public class VstParameter
    {
        public VstParameterInfo Info { get { return new VstParameterInfo(); } }
        public bool CanBeAutomated { get { return false; } }
        public float NormalizedValue { get { return 0.0f; } set { } }
        public string DisplayValue { get { return string.Empty; } }
        public string Name { get { return string.Empty; } }
        public string Label { get { return string.Empty; } }
    }
}
