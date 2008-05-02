namespace Jacobi.Vst.Framework
{
    public class VstProgram : IVstPluginParameters
    {
        public string Name;        

        #region IVstPluginParameters Members

        private VstParameterCollection _paramters;

        public VstParameterCollection Parameters
        {
            get
            {
                if (_paramters == null)
                {
                    _paramters = new VstParameterCollection();
                }

                return _paramters;
            }
        }

        #endregion
    }
}
