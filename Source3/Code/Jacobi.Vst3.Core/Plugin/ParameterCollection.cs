using System;
using System.Collections.ObjectModel;

namespace Jacobi.Vst3.Plugin
{
    public class ParameterCollection : KeyedCollection<UInt32, Parameter>
    {
        protected override uint GetKeyForItem(Parameter item)
        {
            if (item == null) return 0;

            return item.Id;
        }

        public Parameter GetAt(int index)
        {
            return Items[index];
        }
    }
}
