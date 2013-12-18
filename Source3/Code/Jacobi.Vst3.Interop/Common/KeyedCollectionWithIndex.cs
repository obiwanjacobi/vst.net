using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Jacobi.Vst3.Common
{
    public abstract class KeyedCollectionWithIndex<KeyT, ItemT> : KeyedCollection<KeyT, ItemT>
    {
        public ItemT GetAt(int index)
        {
            var baseThis = (Collection<ItemT>)this;

            return baseThis[index];
        }
    }
}
