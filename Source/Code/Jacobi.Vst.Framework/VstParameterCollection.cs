namespace Jacobi.Vst.Framework
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class VstParameterCollection : KeyedCollection<string, VstParameter>
    {
        public void AddRange(IEnumerable<VstParameter> parameters)
        {
            foreach (VstParameter param in parameters)
            {
                Add(param);
            }
        }

        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Name;
        }
    }
}
