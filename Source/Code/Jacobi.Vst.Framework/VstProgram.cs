namespace Jacobi.Vst.Framework
{
    public class VstProgram : IVstPluginParameters
    {
        public string Name;        

        #region IVstPluginParameters Members

        public VstParameterCollection Parameters
        { get { return null; } }

        #endregion
    }
}
