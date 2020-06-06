using System.Collections.ObjectModel;

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
