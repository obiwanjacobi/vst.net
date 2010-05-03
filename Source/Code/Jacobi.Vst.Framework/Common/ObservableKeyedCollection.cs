using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Framework.Common
{
    public abstract class ObservableKeyedCollection<KeyT, ValueT> : KeyedCollection<KeyT, ValueT>, INotifyCollectionChanged
    {
        protected override void ClearItems()
        {
            ArrayList oldItems = new ArrayList(this);

            base.ClearItems();

            OnCollectionChanged(NotifyColletionChangedAction.Reset, null, oldItems);
        }

        protected override void InsertItem(int index, ValueT item)
        {
            base.InsertItem(index, item);

            OnCollectionChanged(NotifyColletionChangedAction.Add, item, null);
        }

        protected override void RemoveItem(int index)
        {
            ValueT oldItem = this[index];

            base.RemoveItem(index);

            OnCollectionChanged(NotifyColletionChangedAction.Remove, null, oldItem);
        }

        protected override void SetItem(int index, ValueT item)
        {
            ValueT oldItem = this[index];

            base.SetItem(index, item);

            OnCollectionChanged(NotifyColletionChangedAction.Replace, item, oldItem);
        }

        #region INotifyCollectionChanged Members

        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionChanged;

        protected void OnCollectionChanged(NotifyColletionChangedAction action, object newItem, object oldItem)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(action, newItem, oldItem));
            }
        }

        protected void OnCollectionChanged(NotifyColletionChangedAction action, IList newItems, IList oldItems)
        {
            var handler = CollectionChanged;

            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(action, newItems, oldItems));
            }
        }

        #endregion
    }
}
