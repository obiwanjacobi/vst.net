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

        public VstParameterCollection FindParametersIn(VstParameterCategory category)
        {
            VstParameterCollection results = new VstParameterCollection();

            foreach (VstParameter param in this.Items)
            {
                if (param.Category == category)
                {
                    results.Add(param);
                }
            }

            return results;
        }

        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Key;
        }
    }
}
