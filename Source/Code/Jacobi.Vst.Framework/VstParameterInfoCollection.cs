namespace Jacobi.Vst.Framework
{
    using System.Collections.ObjectModel;
    using System.Collections.Generic;

    public class VstParameterInfoCollection : Collection<VstParameterInfo>
    {
        public void AddRange(IEnumerable<VstParameterInfo> enumerator)
        {
            foreach (VstParameterInfo paramInfo in enumerator)
            {
                Add(paramInfo);
            }
        }
    }
}
