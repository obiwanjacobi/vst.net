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

        public int CountParametersIn(VstParameterCategory category)
        {
            int count = 0;

            foreach (VstParameter param in this.Items)
            {
                if (param.Category == category)
                {
                    count++;
                }
            }

            return count;
        }

        protected override string GetKeyForItem(VstParameter item)
        {
            return item.Name;
        }
    }
}
