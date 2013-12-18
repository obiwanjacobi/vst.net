using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Plugin
{
    public class ParameterCollection : KeyedCollection<UInt32, Parameter>
    {
        protected override uint GetKeyForItem(Parameter item)
        {
            if (item == null) return 0;

            return item.Id;
        }
    }
}
