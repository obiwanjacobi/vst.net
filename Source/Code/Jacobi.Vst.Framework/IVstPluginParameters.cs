namespace Jacobi.Vst.Framework
{
    public interface IVstPluginParameters
    {
        VstParameterCategoryCollection Categories { get; }
        VstParameterCollection Parameters { get; }
    }
}
