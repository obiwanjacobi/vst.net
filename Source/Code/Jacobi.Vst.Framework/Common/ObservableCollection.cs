using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Jacobi.Vst.Framework.Common
{
    public class ObservableCollection<T> : Collection<T>, INotifyCollectionChanged
    {
        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            OnCollectionChanged(NotifyColletionChangedAction.Add, item, null);
        }

        protected override void RemoveItem(int index)
        {
            T oldItem = this[index];

            base.RemoveItem(index);

            OnCollectionChanged(NotifyColletionChangedAction.Remove, null, oldItem);
        }

        protected override void SetItem(int index, T item)
        {
            T oldItem = this[index];

            base.SetItem(index, item);

            OnCollectionChanged(NotifyColletionChangedAction.Replace, item, oldItem);
        }

        protected override void ClearItems()
        {
            ArrayList oldItems = new ArrayList(this);

            base.ClearItems();

            OnCollectionChanged(NotifyColletionChangedAction.Reset, null, oldItems);
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
