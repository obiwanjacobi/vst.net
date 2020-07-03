using Jacobi.Vst3.Common;
using System;

namespace Jacobi.Vst3.Plugin
{
    public class ParameterCollection : KeyedCollectionWithIndex<UInt32, Parameter>
    {
        protected override uint GetKeyForItem(Parameter item)
        {
            if (item == null) return 0;

            return item.Id;
        }
    }
}
