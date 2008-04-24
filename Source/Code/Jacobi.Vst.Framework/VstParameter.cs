namespace Jacobi.Vst.Framework
{
    public class VstParameter
    {
        VstParameterInfo Info { get { return new VstParameterInfo(); } }
        bool CanBeAutomated { get { return false; } }
        string DisplayValue { get { return string.Empty; } }
        string Name { get { return string.Empty; } }
        string Label { get { return string.Empty; } }
    }
}
